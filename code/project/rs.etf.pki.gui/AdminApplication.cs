using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Threading;

namespace PKI_project
{
    public partial class AdminApplication : PKI_project.BaseApplication
    {
        private BaseAddForm addClassroomsToPersonnel, addPersonnelToClassrooms;
        private ChangeClassroomInfo changeClassroomInfo;
        private CreateUserForm createUser;

        // menu items for Edit menu
        private ToolStripMenuItem manageUsersToolStripMenuItem, manageClassroomsToolStripMenuItem;

        private bool classroomNumSearchEntered;
                
        private string[] allNonVisibleClassrooms = null,// list of all non-visible classrooms
            allLaboratoryPersonnel = null;              // list of all laboratory personnel


        //The delegate must have the same signature as the method
        public delegate string[] AddPersonnelToClassroomCaller();

        public BaseAddForm AddPersonnelToClassrooms
        {
            get { return addPersonnelToClassrooms; }
            set { addPersonnelToClassrooms = value; }
        }

        public BaseAddForm AddClassroomsToPersonnel
        {
            get { return addClassroomsToPersonnel; }
            set { addClassroomsToPersonnel = value; }
        }

        public bool ClassroomNumSearchEntered
        {
            get { return classroomNumSearchEntered; }
            set { classroomNumSearchEntered = value; }
        }

        public AdminApplication(string username)
            : base(username)
        {
            InitializeComponent();

            initializeWindowsFormsComponent();
        }

        private void initializeWindowsFormsComponent()
        {
            getLaboratoryPersonnel();

            // 
            addClassroomsToPersonnel = null;
            addPersonnelToClassrooms = null;
            changeClassroomInfo = null;
            createUser = null;

            // 
            // manageUsersToolStripMenuItem
            //
            manageUsersToolStripMenuItem = new ToolStripMenuItem();
            manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            manageUsersToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            manageUsersToolStripMenuItem.Text = "Manage Users";
            manageUsersToolStripMenuItem.Click += new System.EventHandler(this.manageUsersToolStripMenuItem_Click);

            //
            // manageClassroomsToolStripMenuItem
            //
            manageClassroomsToolStripMenuItem = new ToolStripMenuItem();
            manageClassroomsToolStripMenuItem.Name = "manageClassroomsToolStripMenuItem";
            manageClassroomsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            manageClassroomsToolStripMenuItem.Text = "Manage Classrooms";
            manageClassroomsToolStripMenuItem.Click += new System.EventHandler(this.manageClassroomsToolStripMenuItem_Click);

            this.addMenuItemsToMenu(editToolStripMenuItem, manageUsersToolStripMenuItem, manageClassroomsToolStripMenuItem);

            //
            // add handlers for tabs
            //
            manageUsersTabControl.DrawItem += new DrawItemEventHandler(manageUsersTabControl_DrawItem);
            manageClassroomsTabControl.DrawItem += new DrawItemEventHandler(manageClassroomsTabControl_DrawItem);

            //
            // set auto complete for components
            //
            setAutoCompleteAndAutoCompleteMode(laboratoryPersonnelSearchClassroomTextBox, allLaboratoryPersonnel);
            setAutoCompleteAndAutoCompleteMode(searchUserNameTextBox, allUsers);
            setAutoCompleteAndAutoCompleteMode(searchClassroomNumberTextBox, classroomNums);

            this.setNewClassroomsForAutocomplete();
            this.setNewUsersForAutoComplete();
        }

        private void manageUsersTabControl_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = manageUsersTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = manageUsersTabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void manageClassroomsTabControl_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = manageClassroomsTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = manageClassroomsTabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void changeInfo()
        {
            if (changeUserInfo == null || changeUserInfo.IsDisposed)
            {
                changeUserInfo = new ChangeUserInfo(this);
            }

            changeUserInfo.clearAllComponents();
            changeUserInfo.Show();
        }

        private void addRemovePersonel()
        {
            if (addPersonnelToClassrooms == null || addPersonnelToClassrooms.IsDisposed)
            {
                addPersonnelToClassrooms = new AddPersonnel();
            }

            addPersonnelToClassrooms.Show();
        }

        // 
        // get all content from Create New User Tab
        // 
        private void getCreateNewUserComponentsContent(
            out string userType,    // user type
            out string userName,    // username
            out string pass,        // password
            out string name,        // user real name
            out string surname,     // user surname
            out string title,       // user title
            out string office,      // user office
            out string phone,       // user phone
            out string mail)        // user mail
        {
            userType = typeCreateUserComboBox.Text;
            userName = usernameCreateUserTextBox.Text;
            pass = passCreateUserTextBox.Text;
            name = nameCreateUserTextBox.Text;
            surname = surnameCreateUserTextBox.Text;
            title = titleCreateUserTextBox.Text;
            office = officeCreateUserTextBox.Text;
            phone = phoneCreateUserTextBox.Text;
            mail = mailCreateUserTextBox.Text;
        }

        // 
        // get all content from Create New Classroom Tab
        // 
        private void getCreateNewClassroomComponentsContent(
            out string num,             // classroom number
            out bool blackboard,        // has blackboard
            out bool computers,         // has computers
            out bool projector,         // has projector
            out bool laboratory,        // can do laboratory excersises
            out int cap,                // classroom capacity
            out string workingHours,    // classroom working hours
            out string comment,         // some comment
            out string notice)          // notice
        {
            num = createCNumTextBox.Text;
            blackboard = createBlackboardCheckBox.Checked;
            computers = createComputersCheckBox.Checked;
            projector = createProjectorCheckBox.Checked;
            laboratory = createLaboratoryCheckBox.Checked;
            cap = int.Parse(createCapacityTextBox.Text);
            workingHours = createStartWorkingTextBox.Text + "-" + createEndWorkingTextBox.Text;
            comment = createCommentTextBox.Text;
            notice = createNoticeTextBox.Text;
        }

        private void blockUser(string username, DataGridView dataGridView)
        {
            string sqlCommandText =
                "SELECT isBlocked FROM All_Users WHERE username = @username";

            // create command
            SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
            sql.Parameters.Add(new SqlParameter("@username", username));

            rdr = sql.ExecuteReader();

            if (rdr.Read())
            {
                if ((bool)rdr["isBlocked"])
                {
                    // unblock user
                    sqlCommandText =
                        "UPDATE All_Users SET isBlocked = 0 WHERE username = @username";

                    // change color to red to those useres that are blocked
                    dataGridView.Rows[dataGridView.SelectedRows[0].Index].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // block user
                    sqlCommandText =
                        "UPDATE All_Users SET isBlocked = 1 WHERE username = @username";

                    // change color to red to those useres that are blocked
                    dataGridView.Rows[dataGridView.SelectedRows[0].Index].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            rdr.Close();

            // create command
            sql = new SqlCommand(sqlCommandText, Connection);
            sql.Parameters.Add(new SqlParameter("@username", username));

            // and execute it
            sql.ExecuteNonQuery();
        }

        private void deleteUserFromAllUsers(string username, DataGridView dataGridView)
        {
            // delete from classrooms laboratory personnel
            // if is laboratory worker
            deleteLaboratoryPersonnelFromClassroomPersonnel(username);

            string sqlCommandText =
                "DELETE FROM All_Users WHERE username = @username";

            // create command
            SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
            sql.Parameters.Add(new SqlParameter("@username", username));

            // and execute it
            sql.ExecuteNonQuery();

            // delete from table
            dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
            dataGridView.Refresh();

            this.setNewUsersForAutoComplete();
        }

        private void deleteLaboratoryPersonnelFromClassroomPersonnel(string username){
            try
            {
                string sqlCommandTex = "SELECT userType FROM All_Users WHERE username = @username";
                SqlCommand sql = new SqlCommand(sqlCommandTex, Connection);
                sql.Parameters.Add(new SqlParameter("@username", username));

                rdr = sql.ExecuteReader();

                if (rdr.Read() && ((string)rdr["userType"]).ToLower().Equals("laboratory personnel"))
                {
                    rdr.Close();

                    // make command
                    sqlCommandTex = "SELECT * FROM All_LabPersonnel WHERE" +
                        " labPersonnel_1 = @labPersonnel OR labPersonnel_2 = @labPersonnel OR " +
                        " labPersonnel_3 = @labPersonnel OR labPersonnel_4 = @labPersonnel OR labPersonnel_5 = @labPersonnel";

                    sql = new SqlCommand(sqlCommandTex, Connection);
                    sql.Parameters.Add(new SqlParameter("@labPersonnel", username));

                    // and execute it
                    rdr = sql.ExecuteReader();

                    // get all occurences of labpersonnel with given username
                    List<int> labIDs = new List<int>();
                    List<string> labPersonnels = new List<string>();

                    while (rdr.Read())
                    {
                        // get all personnel for update
                        labIDs.Add((int)rdr["labPersonnelID"]);

                        if (!(rdr["labPersonnel_1"] is DBNull) &&
                            ((string)rdr["labPersonnel_1"]).Equals(username))
                        {
                            labPersonnels.Add("labPersonnel_1");
                        }
                        else if (!(rdr["labPersonnel_2"] is DBNull) &&
                            ((string)rdr["labPersonnel_2"]).Equals(username))
                        {
                            labPersonnels.Add("labPersonnel_2");
                        }
                        else if (!(rdr["labPersonnel_3"] is DBNull) &&
                            ((string)rdr["labPersonnel_3"]).Equals(username))
                        {
                            labPersonnels.Add("labPersonnel_3");
                        }
                        else if (!(rdr["labPersonnel_4"] is DBNull) &&
                            ((string)rdr["labPersonnel_4"]).Equals(username))
                        {
                            labPersonnels.Add("labPersonnel_4");
                        }
                        else
                        {
                            labPersonnels.Add("labPersonnel_5");
                        }

                    }

                    rdr.Close();

                    // set labpersonnel whos to be deleted to NULL
                    string firstPartOfCommand = "UPDATE All_LabPersonnel SET ";

                    for (int i = 0; i < labIDs.Count; i++)
                    {
                        sqlCommandTex = firstPartOfCommand + labPersonnels[i] + " = NULL WHERE labPersonnelID = " + labIDs[i];
                        sql = new SqlCommand(sqlCommandTex, Connection);
                        sql.ExecuteNonQuery();
                    }

                    // if there is some classroom that has been
                    // left without any labpersonnel than move it to non visible
                    for (int i = 0; i < labIDs.Count; i++)
                    {
                        //rdr.Close();

                        sqlCommandTex = "SELECT labPersonnelID " +
                                " FROM All_LabPersonnel " +
                                " WHERE labPersonnelID = " + labIDs[i].ToString() + " AND " +
                                " (labPersonnel_1 IS NULL OR labPersonnel_1 = 'NULL') AND " +
                                " (labPersonnel_2 IS NULL OR labPersonnel_2 = 'NULL') AND " +
                                " (labPersonnel_3 IS NULL OR labPersonnel_3 = 'NULL') AND " +
                                " (labPersonnel_4 IS NULL OR labPersonnel_4 = 'NULL') AND " +
                                " (labPersonnel_5 IS NULL OR labPersonnel_5 = 'NULL')";

                        sql = new SqlCommand(sqlCommandTex, Connection);

                        rdr = sql.ExecuteReader();

                        if (rdr.Read())
                        {
                            // must close reader
                            rdr.Close();

                            sqlCommandTex = "UPDATE All_Classrooms SET isVisible = 0 WHERE personelID = " + labIDs[i].ToString();
                            sql = new SqlCommand(sqlCommandTex, Connection);
                            sql.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!rdr.IsClosed) rdr.Close();

                this.setNewClassroomsForAutocomplete();
            }
        }

        private void changeUserInfoUpdate(string username)
        {
            string sqlCommandText =
                "SELECT username, pass, userType, name, surname, title, office, phone, mail FROM All_Users" +
                " WHERE username = @username";

            // create command
            SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
            sql.Parameters.Add(new SqlParameter("@username", username));

            // and execute it
            rdr = sql.ExecuteReader();

            if (rdr.Read())
            {
                changeUserInfo.fillAllComponents(
                    (string)rdr["userType"], (string)rdr["username"], (string)rdr["pass"],
                    ( rdr["name"] is DBNull ? string.Empty : (string)rdr["name"]),
                    (rdr["surname"] is DBNull ? string.Empty : (string)rdr["surname"]),
                    (rdr["title"] is DBNull ? string.Empty : (string)rdr["title"]),
                    (rdr["office"] is DBNull ? string.Empty : (string)rdr["office"]),
                    (rdr["phone"] is DBNull ? string.Empty : (string)rdr["phone"]),
                    ( rdr["mail"]is DBNull ? string.Empty : (string)rdr["mail"]));
            }

            rdr.Close();
        }

        // refresh New User Tab
        public void refreshCreateNewUserTab()
        {
            // number of new users for registration
            int num = 0;

            try
            {
                registrationNewUserDataGridView.Rows.Clear();

                string sqlCommandText = "SELECT name, surname, title, office, phone, mail, userType FROM New_Users";

                // create command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

                // execute command
                rdr = sql.ExecuteReader();

                while (rdr.Read())
                {
                    string[] rowParams = { 
                    (rdr["name"] is DBNull ? string.Empty : (string)rdr["name"]),
                    (rdr["surname"] is DBNull ? string.Empty : (string)rdr["surname"]),
                    (rdr["title"] is DBNull ? string.Empty : (string)rdr["title"]),
                    (rdr["office"] is DBNull ? string.Empty : (string)rdr["office"]),
                    (rdr["phone"] is DBNull ? string.Empty : (string)rdr["phone"]),
                    (rdr["mail"] is DBNull ? string.Empty : (string)rdr["mail"]),
                    (rdr["userType"] is DBNull ? string.Empty : (string)rdr["userType"])
                };

                    // add new row
                    registrationNewUserDataGridView.Rows.Add(rowParams);

                    num++;
                }
            }
            catch (Exception)
            {
            }
            finally
            {

                // show results
                registrationNewUserDataGridView.Refresh();

                // change text
                newUsersTabPage.Text = "New users(" + num + ")";

                rdr.Close();
            }
        }

        // refresh all users tab page
        public void refreshAllUsersTabPage()
        {
            try
            {
                allUsersDataGridView.Rows.Clear();

                string sqlCommandText = "SELECT username, name, surname, phone, mail, userType, isBlocked FROM All_Users";

                // create command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

                // execute command
                rdr = sql.ExecuteReader();

                while (rdr.Read())
                {
                    string[] rowParams = { 
                    (rdr["username"] is DBNull ? string.Empty : (string)rdr["username"]),
                    (rdr["name"] is DBNull ? string.Empty : (string)rdr["name"]),
                    (rdr["surname"] is DBNull ? string.Empty : (string)rdr["surname"]),
                    (rdr["phone"] is DBNull ? string.Empty : (string)rdr["phone"]),
                    (rdr["mail"] is DBNull ? string.Empty : (string)rdr["mail"]),
                    (rdr["userType"] is DBNull ? string.Empty : (string)rdr["userType"])
                };

                    // add new row
                    allUsersDataGridView.Rows.Add(rowParams);

                    if (!(bool)rdr["isBlocked"])
                    {
                        // change color to red to those useres that are blocked
                        allUsersDataGridView.Rows[allUsersDataGridView.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        // change color to red to those useres that are blocked
                        allUsersDataGridView.Rows[allUsersDataGridView.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }

                // show results
                allUsersDataGridView.Refresh();
            }
            catch (Exception)
            {
            }
            finally
            {
                if(!rdr.IsClosed) rdr.Close();
            }
        }

        // reset all text in components
        private void resetAllComponentsCreateUserTab()
        {
            typeCreateUserComboBox.Text = "choose type...";
            usernameCreateUserTextBox.Clear();
            passCreateUserTextBox.Clear();
            nameCreateUserTextBox.Clear();
            surnameCreateUserTextBox.Clear();
            titleCreateUserTextBox.Clear();
            officeCreateUserTextBox.Clear();
            mailCreateUserTextBox.Clear();

            typeCreateUserComboBox.Focus();
        }

        // reset all text in components
        private void resetAllComponentSearchUserTab()
        {
            searchTypeComboBox.Text = "choose type...";
            searchUserNameTextBox.Clear();
        }

        private void clearAllComponentsSearchUsersTabPage()
        {
            resetAllComponentSearchUserTab();

            usersDataGridView.Rows.Clear();
        }

        // reset all text in components
        private void resetAllComponentCreateUserTab()
        {
            typeCreateUserComboBox.Text = "choose type...";
            usernameCreateUserTextBox.Clear();
            passCreateUserTextBox.Clear();
            nameCreateUserTextBox.Clear();
            surnameCreateUserTextBox.Clear();
            titleCreateUserTextBox.Clear();
            officeCreateUserTextBox.Clear();
            phoneCreateUserTextBox.Clear();
            mailCreateUserTextBox.Clear();
        }

        // reset all text in components
        private void resetAllComponentSearchClassroomTab()
        {
            searchClassroomNumberTextBox.Clear();
            blackboardSearchClassroomCheckBox.Checked = false;
            computersSearchClassroomCheckBox.Checked = false;
            projectorSearchClassroomCheckBox.Checked = false;
            laboratorySearchClassroomCheckBox.Checked = false;
            capacitySearchClassroomTextBox.Clear();
            laboratoryPersonnelSearchClassroomTextBox.Clear();

            ClassroomNumSearchEntered = false;
        }

        // cleare components
        private void clearAllComponentsCreateClassroomTab()
        {
            createCNumTextBox.Clear();
            createBlackboardCheckBox.Checked = false;
            createComputersCheckBox.Checked = false;
            createProjectorCheckBox.Checked = false;
            createLaboratoryCheckBox.Checked = false;
            createStartWorkingTextBox.Clear();
            createEndWorkingTextBox.Clear();
            createCapacityTextBox.Clear();
            createCommentTextBox.Clear();
            createNoticeTextBox.Clear();
        }

        // clear all components on Search Classroom Tab Page
        private void clearAllComponentSearchClassroomTab()
        {
            searchClassroomNumberTextBox.Text = string.Empty;
            blackboardSearchClassroomCheckBox.Checked = false;
            computersSearchClassroomCheckBox.Checked = false;
            projectorSearchClassroomCheckBox.Checked = false;
            laboratorySearchClassroomCheckBox.Checked = false;
            capacitySearchClassroomTextBox.Text = string.Empty;
            laboratoryPersonnelSearchClassroomTextBox.Text = string.Empty;
        }

        // refresh classroom num textBoxes
        public override void setNewClassroomsForAutocomplete()
        {
            base.setNewClassroomsForAutocomplete();

            setAutoCompleteAndAutoCompleteMode(searchClassroomNumberTextBox, ClassroomNums);
        }

        // refresh all users textBoxes
        public override void setNewUsersForAutoComplete()
        {
            base.setNewUsersForAutoComplete();
            
            setAutoCompleteAndAutoCompleteMode(searchUserNameTextBox, allUsers);

            this.getLaboratoryPersonnel();
            setAutoCompleteAndAutoCompleteMode(laboratoryPersonnelSearchClassroomTextBox, allLaboratoryPersonnel);
        }

        // get all non-visible classrooms
        private void getNonVisibleClassrooms()
        {
            lock (lockClassrooms)
            {
                try
                {
                    string sqlCommandText = "SELECT number, isVisible FROM All_Classrooms";

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

                    // and execute it
                    rdr = sql.ExecuteReader();

                    List<string> list = new List<string>();

                    while (rdr.Read())
                    {
                        // fill classrooms if they are visible
                        if (!(bool)rdr["isVisible"]) list.Add((string)rdr["number"]);
                    }

                    allNonVisibleClassrooms = null;
                    allNonVisibleClassrooms = list.ToArray();
                }
                catch (Exception)
                {

                }
                finally
                {
                    rdr.Close();
                }

            }
        }

        // get all laboratory personnel
        public void getLaboratoryPersonnel()
        {
            lock (lockUsers)
            {
                try
                {
                    string sqlCommandText = "SELECT username FROM All_Users WHERE userType = @labPersonnel";

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@labPersonnel", "Laboratory personnel"));

                    // and execute it
                    rdr = sql.ExecuteReader();

                    List<string> list = new List<string>();

                    while (rdr.Read())
                    {
                        // fill classrooms if they are visible
                        list.Add((string)rdr["username"]);
                    }

                    allLaboratoryPersonnel = null;
                    allLaboratoryPersonnel = list.ToArray();
                }
                catch (Exception)
                {
                }
                finally
                {
                    rdr.Close();
                }
            }
        }

        private void fillVisibleClassrooms()
        {
            this.getClassrooms();

            allVisibleClassroomsListBox.Items.AddRange(ClassroomNums);
        }

        private void fillNonVisibleClassrooms()
        {
            this.getNonVisibleClassrooms();

            allNonVisibleClassroomsListBox.Items.AddRange(allNonVisibleClassrooms);
        }

        public void addToVisibleClassroomFinish(string selectedClassroom)
        {
            string[] selectedPersonnel = addPersonnelToClassrooms.getSelected();

            if (selectedPersonnel.Length > 0)
            {
                try
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number";

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        int number = (int)rdr["personelID"];

                        // must close data reader
                        rdr.Close();

                        makeSQLQueryForUpdateLaboratotyPersonnelAndExecuteIt(selectedPersonnel, sqlCommandText, sql, number, true);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (!rdr.IsClosed) rdr.Close();

                    addPersonnelToClassrooms.clearSelected();

                    if (!allVisibleClassroomsListBox.Items.Contains(selectedClassroom))
                    {
                        allVisibleClassroomsListBox.Items.Add(selectedClassroom);
                        allNonVisibleClassroomsListBox.Items.Remove(selectedClassroom);
                    }

                    this.setNewClassroomsForAutocomplete();
                }
            }
        }

        public void updatePersonnelForClassrooms(string username)
        {
            string[] selectedClassrooms = addClassroomsToPersonnel.getSelected();

            if (selectedClassrooms.Length > 0)
            {
                try
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number";
                    string[] personnel = {"labPersonnel_1", "labPersonnel_2", "labPersonnel_3", "labPersonnel_4", "labPersonnel_5"};

                    for (int i = 0; i < selectedClassrooms.Length; i++)
                    {
                        SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                        sql.Parameters.Add(new SqlParameter("@number", selectedClassrooms[i]));

                        rdr = sql.ExecuteReader();

                        if (rdr.Read())
                        {
                            int id = (int)rdr["personelID"];

                            // must close data reader
                            rdr.Close();

                            for(int j = 0; j< personnel.Length; j++){
                                string tempCommand = "SELECT " + personnel[j] + " FROM All_LabPersonnel WHERE labPersonnelID = @labPersonnelID";
                                sql = new SqlCommand(tempCommand, Connection);
                                sql.Parameters.Add(new SqlParameter("@labPersonnelID", id));

                                rdr = sql.ExecuteReader();

                                if (rdr.Read() &&
                                    (rdr[personnel[j]] is DBNull || 
                                    (!(rdr[personnel[j]] is DBNull) && ((string)rdr[personnel[j]]).Equals("NULL"))))
                                {
                                    rdr.Close();

                                    tempCommand = "UPDATE All_LabPersonnel SET " + personnel[j] + " = @username WHERE labPersonnelID = @labPersonnelID";
                                    sql = new SqlCommand(tempCommand, Connection);
                                    sql.Parameters.Add(new SqlParameter("@username", username));
                                    sql.Parameters.Add(new SqlParameter("@labPersonnelID", id));

                                    sql.ExecuteNonQuery();

                                    break;
                                }

                                rdr.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (!rdr.IsClosed) rdr.Close();

                    addClassroomsToPersonnel.clearSelected();
                }
            }
        }

        public void personnelChanged(string selectedClassroom)
        {
            string[] selectedPersonnel = addPersonnelToClassrooms.getSelected();

            if (selectedPersonnel.Length > 0)
            {
                try
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number";

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        int number = (int)rdr["personelID"];

                        // must close data reader
                        rdr.Close();

                        // set all classroom personnel
                        string[] personnel = { "labPersonnel_1", "labPersonnel_2", "labPersonnel_3", "labPersonnel_4", "labPersonnel_5" };

                        sql.Parameters.Clear();

                        sqlCommandText = "UPDATE All_LabPersonnel SET ";

                        for (int i = 0; i < personnel.Length; i++)
                        {
                            sqlCommandText +=
                                (i != 0) ? ("," + personnel[i] + " = @" + personnel[i]) : (personnel[i] + " = @" + personnel[i]);
                            sql.Parameters.Add(new SqlParameter(("@" + personnel[i]), (i < selectedPersonnel.Length ? selectedPersonnel[i] : "NULL")));
                        }

                        sqlCommandText += " WHERE labPersonnelID = @id";
                        sql.Parameters.Add(new SqlParameter("@id", number));
                        sql.CommandText = sqlCommandText;

                        sql.ExecuteNonQuery();

                        // TODO: if classroom becomes without personnel (if time)
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (!rdr.IsClosed) rdr.Close();

                    addPersonnelToClassrooms.clearSelected();
                }
            }
        }

        // parameters:
        // selected personnel - if we need to do update to visible classrooms
        // sqlCommand text - ref to command strig that will be used
        // sql - sql command
        // number - number of personnelID
        // clear - if we are updating from visible to non visible
        private void makeSQLQueryForUpdateLaboratotyPersonnelAndExecuteIt(string[] selectedPersonnel, string sqlCommandText, SqlCommand sql, int number, bool isVisible)
        {
            // set all classroom personnel
            string[] personnel = { "labPersonnel_1", "labPersonnel_2", "labPersonnel_3", "labPersonnel_4", "labPersonnel_5" };

            sql.Parameters.Clear();

            sqlCommandText = "UPDATE All_LabPersonnel SET ";

            int length = isVisible ? selectedPersonnel.Length : personnel.Length;
            for (int i = 0; i < length; i++)
            {
                sqlCommandText +=
                    (i != 0) ? ("," + personnel[i] + " = @" + personnel[i]) : (personnel[i] + " = @" + personnel[i]);
                sql.Parameters.Add(new SqlParameter(("@" + personnel[i]), (isVisible ? selectedPersonnel[i] : "NULL")));
            }

            sqlCommandText += " WHERE labPersonnelID = @id";
            sql.Parameters.Add(new SqlParameter("@id", number));
            sql.CommandText = sqlCommandText;

            sql.ExecuteNonQuery();

            // update classroom to visible
            sqlCommandText = "UPDATE All_Classrooms SET isVisible = @isVisible WHERE personelID = @personelID";
            sql.Parameters.Clear();
            sql.CommandText = sqlCommandText;
            sql.Parameters.Add(new SqlParameter("@isVisible", isVisible));
            sql.Parameters.Add(new SqlParameter("@personelID", number));

            sql.ExecuteNonQuery();
            
        }

        public void deleteRowFromRegistrationDataGridView()
        {
            registrationNewUserDataGridView.Focus();
            registrationNewUserDataGridView.Rows.Remove(registrationNewUserDataGridView.CurrentRow);
            registrationNewUserDataGridView.Refresh();

            string num = newUsersTabPage.Text.Substring(newUsersTabPage.Text.IndexOf('(') + 1, newUsersTabPage.Text.IndexOf(')') - newUsersTabPage.Text.IndexOf('(') - 1);
            newUsersTabPage.Text = "New users(" + (int.Parse(num.ToString()) - 1) + ")";
        }

        public void deleteRowFromSearchClassroomDataGridView()
        {
            searchClassroomDataGridView.Focus();
            searchClassroomDataGridView.Rows.Remove(searchClassroomDataGridView.CurrentRow);
            searchClassroomDataGridView.Refresh();
        }

        // delete all reservations
        // in the future
        private void deleteFutureReservations(string selectedClassroom, SqlCommand sql)
        {
            string sqlCommandText = "DELETE FROM All_Reservations WHERE " +
                "classroomNumberID = (SELECT classroomID FROM All_Classrooms WHERE number = @number) AND ev_date >= @ev_date";
            sql.CommandText = sqlCommandText;
            sql.Parameters.Clear();
            sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));
            sql.Parameters.Add(new SqlParameter("@ev_date", specificDateOccupiedDateTimePicker.Text));

            sql.ExecuteNonQuery();
        }

        private void deleteFutureReservations(string selectedClassroom, SqlCommand sql, string milan)
        {
            string sqlCommandText = "SELECT ev_date FROM All_Reservations WHERE " +
                 "classroomNumberID = (SELECT classroomID FROM All_Classrooms WHERE number = @number)";

            sql.CommandText = sqlCommandText;
            sql.Parameters.Clear();
            sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

            SqlDataReader rdr = sql.ExecuteReader();

            List<string> dates = new List<string>();

            while (rdr.Read())
            {
                string[] dateString = ((string)rdr["ev_date"]).Split('.'),
                        specificDateString = specificDateOccupiedDateTimePicker.Text.Split('.');

                DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]), int.Parse(dateString[0])),
                    specificDateTime = new DateTime(int.Parse(specificDateString[2]), int.Parse(specificDateString[1]), int.Parse(specificDateString[0]));

                if (date1.CompareTo(specificDateTime) > -1)
                {
                    dates.Add((string)rdr["ev_date"]);
                }
            }

            rdr.Close();

            if (dates.Count > 0)
            {
                sqlCommandText = "DELETE FROM All_Reservations WHERE " +
                    "classroomNumberID = (SELECT classroomID FROM All_Classrooms WHERE number = @number) AND ev_date = @ev_date";
                sql.CommandText = sqlCommandText;

                for (int i = 0; i < dates.Count; i++)
                {
                    sql.Parameters.Add(new SqlParameter("@ev_date", dates[i]));
                    sql.ExecuteNonQuery();

                    sql.Parameters.RemoveAt("@ev_date");
                }
            }
        }

        //
        // CLICK METHODS
        //

        private void changeInfoButton_Click(object sender, EventArgs e)
        {
            changeInfo();

            string username = usersDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            changeUserInfoUpdate(username);

        }

        private void createAddClassroomButton_Click(object sender, EventArgs e)
        {
            if (addClassroomsToPersonnel == null || addClassroomsToPersonnel.IsDisposed)
            {
                addClassroomsToPersonnel = new AddClassrooms();
            }

            addClassroomsToPersonnel.Show();

            string username = usernameCreateUserTextBox.Text;
            
            // fill all classrooms
            AddClassroomsToPersonnel.setAllItemsinAllListBox(ClassroomNums);
            // set caller 
            AddClassroomsToPersonnel.setCaller(this);
            // set Cancel button enabled to false and set username
            AddClassroomsToPersonnel.setCancelButtonEnabled(true, username, -1);
            
        }

        private void changeInfoAllUsersButton_Click(object sender, EventArgs e)
        {
            changeInfo();

            Int32 selectedRowCount = allUsersDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected) - 1;
            string username = allUsersDataGridView.SelectedRows[selectedRowCount].Cells[0].Value.ToString();

            changeUserInfoUpdate(username);
        }

        private void createAddLaboratoryPersonnelbutton_Click(object sender, EventArgs e)
        {
            string selectedClassroom = createCNumTextBox.Text;

            // show dialog
            addRemovePersonel();
            // fill all personnel
            AddPersonnelToClassrooms.setAllItemsinAllListBox(allLaboratoryPersonnel);
            // set caller 
            AddPersonnelToClassrooms.setCaller(this);
            // set Cancel button enabled to false and set selected classroom
            AddPersonnelToClassrooms.setCancelButtonEnabled(true, selectedClassroom, 1);
        }

        private void addRemoveClassroomPersonnelButton_Click(object sender, EventArgs e)
        {
            string selectedClassroom = searchClassroomDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            string[] selectedLaboratoryPersonnel = {};

            try
            {
                string sqlCommandText =
                        "SELECT labPersonnel_1, labPersonnel_2, labPersonnel_3, labPersonnel_4, labPersonnel_5 " +
                        "FROM All_LabPersonnel labs, All_Classrooms crooms " +
                        "WHERE crooms.isVisible = 1 AND crooms.number = @number AND crooms.personelID = labs.labPersonnelID";

                // make sql command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                // add parameters
                sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                // and execute it
                rdr = sql.ExecuteReader();

                if (rdr.Read())
                {
                    // add all laboratory presonnel
                    List<string> temp = new List<string>();
                    if (!(rdr["labPersonnel_1"] is DBNull) && !(((string)rdr["labPersonnel_1"]).Equals("NULL"))) temp.Add((string)rdr["labPersonnel_1"]);
                    if (!(rdr["labPersonnel_2"] is DBNull) && !(((string)rdr["labPersonnel_2"]).Equals("NULL")))  temp.Add((string)rdr["labPersonnel_2"]);
                    if (!(rdr["labPersonnel_3"] is DBNull) && !(((string)rdr["labPersonnel_3"]).Equals("NULL")))  temp.Add((string)rdr["labPersonnel_3"]);
                    if (!(rdr["labPersonnel_4"] is DBNull) && !(((string)rdr["labPersonnel_4"]).Equals("NULL")))  temp.Add((string)rdr["labPersonnel_4"]);
                    if (!(rdr["labPersonnel_5"] is DBNull) && !(((string)rdr["labPersonnel_5"]).Equals("NULL"))) temp.Add((string)rdr["labPersonnel_5"]);

                    selectedLaboratoryPersonnel = temp.ToArray();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                // close reader
                rdr.Close();

                // show dialog
                addRemovePersonel();
                // fill all personnel
                AddPersonnelToClassrooms.setAllItemsinAllListBox(allLaboratoryPersonnel);
                // fill all selected personnel
                addPersonnelToClassrooms.setAllItemsinSelectedListBox(selectedLaboratoryPersonnel);
                // set caller 
                addPersonnelToClassrooms.setCaller(this);
                // set Cancel button enabled to false and set selected classroom
                addPersonnelToClassrooms.setCancelButtonEnabled(true, selectedClassroom, 2);
            }
        }

        private void changeInfoClassroomButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = searchClassroomDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                if (changeClassroomInfo == null)
                {
                    changeClassroomInfo = new ChangeClassroomInfo(this);
                }

                try
                {
                    string sqlCommandText = "SELECT * FROM All_Classrooms WHERE number = @number";
                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedRow[0].Cells[0].Value.ToString()));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        changeClassroomInfo.enableAndShowAllClassroomParameters(
                            (string)rdr["number"], (bool)rdr["blackboard"], (bool)rdr["computers"], (bool)rdr["projector"],
                            (bool)rdr["laboratory"], rdr["capacity"].ToString(), (string)rdr["workingHours"], (string)rdr["comment"],
                            (string)rdr["notice"]);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    rdr.Close();
                }
            }
        }

        private void registrationCreateUserButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = registrationNewUserDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                if (createUser == null)
                {
                    createUser = new CreateUserForm(this);
                }
                
                createUser.fillForm(selectedRow[0].Cells[0].Value.ToString(),
                    selectedRow[0].Cells[1].Value.ToString(),
                    selectedRow[0].Cells[2].Value.ToString(),
                    selectedRow[0].Cells[3].Value.ToString(),
                    selectedRow[0].Cells[4].Value.ToString(),
                    selectedRow[0].Cells[5].Value.ToString(),
                    selectedRow[0].Cells[6].Value.ToString());
                createUser.Show();
            }
        }

        private void manageClassroomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.baseTabControl.SelectedTab = manageClassrommsTabPage;
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.baseTabControl.SelectedTab = manageUsersTabPage;
        }

        private void registrationDeleteUserButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = registrationNewUserDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                string sqlCommandText = "DELETE FROM New_Users WHERE mail = @mail AND userType = @usrType";

                // create command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                sql.Parameters.Add(new SqlParameter("@mail", selectedRow[0].Cells[5].Value.ToString()));
                sql.Parameters.Add(new SqlParameter("@usrType", selectedRow[0].Cells[6].Value.ToString()));

                lock (lockDB)
                {
                    // and execute it
                    sql.ExecuteNonQuery();
                }

                deleteRowFromRegistrationDataGridView();
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!typeCreateUserComboBox.Text.ToLower().Equals("choose type...") &&
                !usernameCreateUserTextBox.Text.Equals(string.Empty))
            {

                string userType, username, pass, name, surname, title, office, phone, mail;
                getCreateNewUserComponentsContent(out userType, out username, out pass, out name, out surname, out title, out office,
                    out phone, out mail);

                string sqlCommandText =
                    "INSERT INTO All_Users(username, pass, userType, name, surname, title, office, phone, mail, isLogin, isBlocked)" +
                    " VALUES (@username, @pass, @userType, @name, @surname, @title, @office, @phone, @mail, 0, 0)";

                // create command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                sql.Parameters.Add(new SqlParameter("@userType", userType));
                sql.Parameters.Add(new SqlParameter("@username", username));
                sql.Parameters.Add(new SqlParameter("@pass", pass));
                sql.Parameters.Add(new SqlParameter("@name", name));
                sql.Parameters.Add(new SqlParameter("@surname", surname));
                sql.Parameters.Add(new SqlParameter("@title", title));
                sql.Parameters.Add(new SqlParameter("@office", office));
                sql.Parameters.Add(new SqlParameter("@phone", phone));
                sql.Parameters.Add(new SqlParameter("@mail", mail));

                // and execute it
                sql.ExecuteNonQuery();

                // if we created new laboratory personnel and added new classrooms for which he will
                // be responsible than update info in table
                if (addClassroomsToPersonnel != null &&
                    userType.ToLower().Equals("laboratory personnel") && 
                    addClassroomsToPersonnel.getSelected().Length > 0)
                {
                    this.updatePersonnelForClassrooms(username);
                }

                if (userType.ToLower().Equals("laboratory personnel")) this.getLaboratoryPersonnel();

                this.setNewUsersForAutoComplete();
                this.refreshAllUsersTabPage();

                // clear all components
                resetAllComponentsCreateUserTab();
            }
            else
            {
                MessageBox.Show("You haven't entered user type and username!", "Warning");
            }
        }

        private void searchUsersButton_Click(object sender, EventArgs e)
        {
            string userType = searchTypeComboBox.Text.Equals("choose type...") ? string.Empty : searchTypeComboBox.Text,
                username = searchUserNameTextBox.Text;

            if (!userType.Equals(string.Empty) || !username.Equals(string.Empty))
            {
                try
                {
                    string sqlCommandText =
                        "SELECT username, name, surname, phone, mail, userType, isBlocked FROM All_Users" +
                        " WHERE ";

                    if (!userType.Equals(string.Empty) && !username.Equals(string.Empty)) sqlCommandText += "userType = @userType AND username = @username";
                    else if (!userType.Equals(string.Empty)) sqlCommandText += "userType = @userType";
                    else sqlCommandText += "username = @username";


                    // create command
                    SqlCommand  sql = new SqlCommand(sqlCommandText, Connection);
                    if (!userType.Equals(string.Empty)) sql.Parameters.Add(new SqlParameter("@userType", userType));
                    if (!username.Equals(string.Empty)) sql.Parameters.Add(new SqlParameter("@username", username));

                    // execute command
                    rdr = sql.ExecuteReader();

                    usersDataGridView.Rows.Clear();
                    usersDataGridView.Refresh();

                    while (rdr.Read())
                    {
                        string[] rowParams = { 
                        (rdr["username"] is DBNull ? string.Empty : (string)rdr["username"]),
                        (rdr["name"] is DBNull ? string.Empty : (string)rdr["name"]),
                        (rdr["surname"] is DBNull ? string.Empty : (string)rdr["surname"]),
                        (rdr["phone"] is DBNull ? string.Empty : (string)rdr["phone"]),
                        (rdr["mail"] is DBNull ? string.Empty : (string)rdr["mail"]),
                        (rdr["userType"] is DBNull ? string.Empty : (string)rdr["userType"])
                    };

                        // add new row
                        usersDataGridView.Rows.Add(rowParams);

                        if ((bool)rdr["isBlocked"])
                        {
                            // change color to red to those useres that are blocked
                            usersDataGridView.Rows[usersDataGridView.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    // show results
                    usersDataGridView.Refresh();

                    resetAllComponentSearchUserTab();

                    rdr.Close();
                }
            }
        }

        private void blockButton_Click(object sender, EventArgs e)
        {
            string username = usersDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            blockUser(username, usersDataGridView);

            refreshAllUsersTabPage();
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            string username = usersDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            deleteUserFromAllUsers(username, usersDataGridView);
            this.setNewUsersForAutoComplete();

            refreshAllUsersTabPage();
        }

        private void blockAllUsersButton_Click(object sender, EventArgs e)
        {
            string username = allUsersDataGridView.SelectedRows[0].Cells[0].Value.ToString();

            blockUser(username, allUsersDataGridView);
        }

        private void deleteAllUsersButton_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = allUsersDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected) - 1;
            string username = allUsersDataGridView.SelectedRows[selectedRowCount].Cells[0].Value.ToString();

            deleteUserFromAllUsers(username, allUsersDataGridView);
        }

        private void createClassroomButton_Click(object sender, EventArgs e)
        {
            if (!createCNumTextBox.Text.Equals(string.Empty) &&
                (createBlackboardCheckBox.Checked || createComputersCheckBox.Checked || 
                createProjectorCheckBox.Checked || createLaboratoryCheckBox.Checked) &&
                !createCapacityTextBox.Text.Equals(string.Empty) &&
                !createStartWorkingTextBox.Text.Equals(string.Empty) &&
                !createEndWorkingTextBox.Text.Equals(string.Empty))
            {
                string number, comment, notice, workingHours;
                bool blackboard, computers, projector, laboratory;
                int cap;

                // first insert laboratory personel if any
                string sqlCommandText;
                SqlCommand sql = null;

                lock (lockDB)
                {
                    bool isVisible = false;
                    
                    // if we created new laboratory personnel and added new classrooms for which he will
                    // be responsible than update info in table
                    if (addPersonnelToClassrooms != null && 
                        addPersonnelToClassrooms.getSelected().Length > 0)
                    {
                        string[] labsWhoAreResponsibleForClassroom = addPersonnelToClassrooms.getSelected();
                        string[] labPersonnel = { "labPersonnel_1", "labPersonnel_2", "labPersonnel_3", "labPersonnel_4", "labPersonnel_5" };


                        // insert command
                        sqlCommandText = "INSERT INTO All_LabPersonnel( ";

                        for (int i = 0; i < labsWhoAreResponsibleForClassroom.Length; i++)
                        {
                            sqlCommandText += labPersonnel[i];
                            sqlCommandText += (i != labsWhoAreResponsibleForClassroom.Length - 1 ? "," : ") VALUES ( ");
                        }

                        sql = new SqlCommand();
                        sql.Connection = Connection;

                        for (int i = 0; i < labsWhoAreResponsibleForClassroom.Length; i++)
                        {
                            sqlCommandText += ("@" + labPersonnel[i]);
                            sqlCommandText += (i != labsWhoAreResponsibleForClassroom.Length - 1 ? "," : ")");

                            sql.Parameters.Add(new SqlParameter("@" + labPersonnel[i], labsWhoAreResponsibleForClassroom[i]));
                        }

                        sql.CommandText = sqlCommandText;

                        isVisible = true;
                    }
                    else
                    {
                        sqlCommandText = "INSERT INTO All_LabPersonnel(labPersonnel_1) VALUES (NULL)";
                        sql = new SqlCommand(sqlCommandText, Connection);
                    }
                    
                    // execute command
                    sql.ExecuteNonQuery();

                    // clear selected personnel from form
                    if(addClassroomsToPersonnel != null) addClassroomsToPersonnel.clearSelected();

                    getCreateNewClassroomComponentsContent(out number, out blackboard, out computers, out projector, out laboratory,
                         out cap, out workingHours, out comment, out notice);

                    sqlCommandText =
                        "INSERT INTO All_Classrooms(number, blackboard, computers, projector, laboratory, capacity, workingHours, comment, notice, isVisible, personelID)" +
                        " VALUES( @number, @blackboard, @computers, @projector, @laboratory, @capacity, @workingHours, @comment, @notice, @isVisible, (SELECT MAX(labPersonnelID) FROM All_LabPersonnel) )";

                    // create command
                    sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", number));
                    sql.Parameters.Add(new SqlParameter("@blackboard", blackboard));
                    sql.Parameters.Add(new SqlParameter("@computers", computers));
                    sql.Parameters.Add(new SqlParameter("@projector", projector));
                    sql.Parameters.Add(new SqlParameter("@laboratory", laboratory));
                    sql.Parameters.Add(new SqlParameter("@capacity", cap));
                    sql.Parameters.Add(new SqlParameter("@workingHours", workingHours));
                    sql.Parameters.Add(new SqlParameter("@comment", comment));
                    sql.Parameters.Add(new SqlParameter("@notice", notice));
                    sql.Parameters.Add(new SqlParameter("@isVisible", isVisible));
                    

                    // and execute it
                    sql.ExecuteNonQuery();
                }

                clearAllComponentsCreateClassroomTab();

                if(addPersonnelToClassrooms != null) addPersonnelToClassrooms.clearSelected();

                this.setNewClassroomsForAutocomplete();
            }
            else
            {
                MessageBox.Show("This filds are requested to be entered:\n classroom number,\n type of classroom (select at least one)," +
                    "\n classroom capacity and \n classroom working hours (start and end)!", "Warning");
            }
        }

        private void searchClassroomButton_Click(object sender, EventArgs e)
        {
            string classroomNum = searchClassroomNumberTextBox.Text,
                laboratoryPersonnelUsername = laboratoryPersonnelSearchClassroomTextBox.Text;
            int classroomCapacity = !capacitySearchClassroomTextBox.Text.Equals(string.Empty) ? int.Parse(capacitySearchClassroomTextBox.Text) : -1;
            bool blackboard = blackboardSearchClassroomCheckBox.Checked,
                computers = computersSearchClassroomCheckBox.Checked,
                projector = projectorSearchClassroomCheckBox.Checked,
                laboratory = laboratorySearchClassroomCheckBox.Checked;

            if (!classroomNum.Equals(string.Empty) ||
                !laboratoryPersonnelUsername.Equals(string.Empty) ||
                blackboard || computers || projector || laboratory ||
                classroomCapacity != -1)
            {
                try
                {
                    string sqlCommandText =
                                "SELECT number, blackboard, computers, projector, laboratory, capacity, workingHours, labPersonnel_1, labPersonnel_2, labPersonnel_3, labPersonnel_4, labPersonnel_5" +
                                " FROM All_Classrooms crooms, All_LabPersonnel labs" +
                                " WHERE crooms.isVisible = 1 AND crooms.personelID = labs.labPersonnelID ";

                    // create command
                    SqlCommand sql = new SqlCommand();

                    if (!classroomNum.Equals(string.Empty))
                    {
                        sqlCommandText += " AND crooms.number = @number";
                        sql.Parameters.Add(new SqlParameter("@number", classroomNum));
                    }
                    if (blackboard)
                    {
                        sqlCommandText += " AND crooms.blackboard = @blackboard";
                        sql.Parameters.Add(new SqlParameter("@blackboard", blackboard));
                    }
                    if (computers)
                    {
                        sqlCommandText += " AND crooms.computers = @computers";
                        sql.Parameters.Add(new SqlParameter("@computers", computers));
                    }
                    if (projector)
                    {
                        sqlCommandText += " AND crooms.projector = @projector";
                        sql.Parameters.Add(new SqlParameter("@projector", projector));
                    }
                    if (laboratory)
                    {
                        sqlCommandText += " AND crooms.laboratory = @laboratory";
                        sql.Parameters.Add(new SqlParameter("@laboratory", laboratory));
                    }
                    if (classroomCapacity != -1)
                    {
                        sqlCommandText += (ClassroomNumSearchEntered ? " AND crooms.capacity = @capacity" : " AND crooms.capacity >= @capacity");
                        sql.Parameters.Add(new SqlParameter("@capacity", classroomCapacity));
                    }
                    if (!laboratoryPersonnelUsername.Equals(string.Empty))
                    {
                        sqlCommandText +=
                            " AND (labs.labPersonnel_1 = @laboratoryPersonnelUsername " +
                            "OR labs.labPersonnel_2 = @laboratoryPersonnelUsername " +
                            "OR labs.labPersonnel_3 = @laboratoryPersonnelUsername " +
                            "OR labs.labPersonnel_4 = @laboratoryPersonnelUsername " +
                            "OR labs.labPersonnel_5 = @laboratoryPersonnelUsername)";
                        sql.Parameters.Add(new SqlParameter("@laboratoryPersonnelUsername", laboratoryPersonnelUsername));
                    }

                    sql.CommandText = sqlCommandText;
                    sql.Connection = Connection;

                    // execute command
                    rdr = sql.ExecuteReader();

                    searchClassroomDataGridView.Rows.Clear();
                    searchClassroomDataGridView.Refresh();

                    while (rdr.Read())
                    {
                        string personnel = string.Empty;
                        personnel += ((rdr["labPersonnel_1"] is DBNull) || ((string)rdr["labPersonnel_1"]).Equals("NULL") ? string.Empty : (string)rdr["labPersonnel_1"] + " ");
                        personnel += ((rdr["labPersonnel_2"] is DBNull) || ((string)rdr["labPersonnel_2"]).Equals("NULL") ? string.Empty : (string)rdr["labPersonnel_2"] + " ");
                        personnel += ((rdr["labPersonnel_3"] is DBNull) || ((string)rdr["labPersonnel_3"]).Equals("NULL") ? string.Empty : (string)rdr["labPersonnel_3"] + " ");
                        personnel += ((rdr["labPersonnel_4"] is DBNull) || ((string)rdr["labPersonnel_4"]).Equals("NULL") ? string.Empty : (string)rdr["labPersonnel_4"] + " ");
                        personnel += ((rdr["labPersonnel_5"] is DBNull) || ((string)rdr["labPersonnel_5"]).Equals("NULL") ? string.Empty : (string)rdr["labPersonnel_5"]);

                        string[] rowParams = { 
                        (rdr["number"] is DBNull ? string.Empty : (string)rdr["number"]),
                        (rdr["blackboard"] is DBNull ? "false" : rdr["blackboard"].ToString()),
                        (rdr["computers"] is DBNull ? "false" : rdr["computers"].ToString()),
                        (rdr["projector"] is DBNull ? "false" : rdr["projector"].ToString()),
                        (rdr["laboratory"] is DBNull ? "false" : rdr["laboratory"].ToString()),
                        (rdr["capacity"] is DBNull ? string.Empty : rdr["capacity"].ToString()),
                        (rdr["workingHours"] is DBNull ? string.Empty : (string)rdr["workingHours"]),
                        personnel
                    };

                        // add new row
                        searchClassroomDataGridView.Rows.Add(rowParams);
                    }

                    // show results
                    searchClassroomDataGridView.Refresh();

                    resetAllComponentSearchClassroomTab();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}", ex);
                }
                finally
                {
                    rdr.Close();
                }
            }
        }

        private void deleteClassroomButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = searchClassroomDataGridView.SelectedRows;
            string selectedClassroom = selectedRow[0].Cells[0].Value.ToString();

            if (selectedRow != null)
            {
                try
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number";
                    string[] personnel = { };

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        int number = (int)rdr["personelID"];

                        // must close data reader
                        rdr.Close();

                        makeSQLQueryForUpdateLaboratotyPersonnelAndExecuteIt(personnel, sqlCommandText, sql, number, false);
                    }

                    // delete reservation from future
                    deleteFutureReservations(selectedClassroom, sql);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (!rdr.IsClosed) rdr.Close();

                    allVisibleClassroomsListBox.Items.Remove(selectedClassroom);
                    allNonVisibleClassroomsListBox.Items.Add(selectedClassroom);

                    deleteRowFromSearchClassroomDataGridView();
                    this.setNewClassroomsForAutocomplete();
                }
            }

        }

        private void addNonVisibleClassroomButton_Click(object sender, EventArgs e)
        {
            if (allNonVisibleClassroomsListBox.SelectedIndex != -1)
            {
                string selectedClassroom = (string)allNonVisibleClassroomsListBox.SelectedItem;

                // show dialog
                addRemovePersonel();
                // fill all personnel
                AddPersonnelToClassrooms.setAllItemsinAllListBox(allLaboratoryPersonnel);
                // set caller 
                addPersonnelToClassrooms.setCaller(this);
                // set Cancel button enabled to false and set selected classroom
                addPersonnelToClassrooms.setCancelButtonEnabled(false, selectedClassroom, 0);
            }
        }

        private void removeVisibleClassroomButton_Click(object sender, EventArgs e)
        {
            if (allVisibleClassroomsListBox.SelectedIndex != -1)
            {
                string selectedClassroom = (string)allVisibleClassroomsListBox.SelectedItem;

                try
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number";
                    string[] personnel = {};

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        int number = (int)rdr["personelID"];

                        // must close data reader
                        rdr.Close();

                        makeSQLQueryForUpdateLaboratotyPersonnelAndExecuteIt(personnel, sqlCommandText, sql, number, false);
                    }

                    // delete reservation from future
                    deleteFutureReservations(selectedClassroom, sql);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (!rdr.IsClosed) rdr.Close();

                    allVisibleClassroomsListBox.Items.Remove(selectedClassroom);
                    allNonVisibleClassroomsListBox.Items.Add(selectedClassroom);

                    this.setNewClassroomsForAutocomplete();
                }
            }
        }

        private void addAllNonVisibleClassroomButton_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<string>();

            for (int i = 0; i < allNonVisibleClassroomsListBox.Items.Count; i++)
            {
                temp.Add(allNonVisibleClassroomsListBox.Items[i].ToString());
            }
                
            // show dialog
            addRemovePersonel();
            // fill all personnel
            AddPersonnelToClassrooms.setAllItemsinAllListBox(allLaboratoryPersonnel);
            // set caller 
            addPersonnelToClassrooms.setCaller(this);
            // set Cancel button enabled to false and set selected classroom
            addPersonnelToClassrooms.setCancelButtonEnabled(false, temp.ToArray());

            allNonVisibleClassroomsListBox.Items.RemoveAt(0);
                
        }

        private void removeAllVisibleClassroomButton_Click(object sender, EventArgs e)
        {
            try
            {
                while( allVisibleClassroomsListBox.Items.Count > 0)
                {
                    string sqlCommandText = "SELECT personelID FROM All_Classrooms WHERE number = @number", 
                        selectedClassroom = allVisibleClassroomsListBox.Items[0].ToString();
                    string[] personnel = { };

                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedClassroom));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        int number = (int)rdr["personelID"];

                        // must close data reader
                        rdr.Close();

                        makeSQLQueryForUpdateLaboratotyPersonnelAndExecuteIt(personnel, sqlCommandText, sql, number, false);
                    }

                    // delete reservation from future
                    deleteFutureReservations(selectedClassroom, sql);

                    allVisibleClassroomsListBox.Items.Remove(selectedClassroom);
                    allNonVisibleClassroomsListBox.Items.Add(selectedClassroom);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!rdr.IsClosed) rdr.Close();

                this.setNewClassroomsForAutocomplete();
            }
        }

        //
        // VALIDATING METHODS
        //

        // return if validation isn't succesfull
        private static bool validateUserType(string userType)
        {
            return !(userType.Length != 0 &&
                            !userType.Contains("choose type...") &&
                            !(userType.ToLower().Equals("administrator") ||
                            userType.ToLower().Equals("laboratory personnel") ||
                            userType.ToLower().Equals("teacher")));
        }

        // return if validation isn't succesfull
        private static bool validateName(String name)
        {
            return !(name.Contains("0") || name.Contains("1") || name.Contains("2") || name.Contains("3") ||
                            name.Contains("4") || name.Contains("5") || name.Contains("6") || name.Contains("7") ||
                            name.Contains("8") || name.Contains("9"));
        }

        // return if validation isn't succesfull
        private static bool validateSurname(String surname)
        {
            return !(surname.Contains("0") || surname.Contains("1") || surname.Contains("2") || surname.Contains("3") ||
                            surname.Contains("4") || surname.Contains("5") || surname.Contains("6") || surname.Contains("7") ||
                            surname.Contains("8") || surname.Contains("9"));
        }

        private void typeCreateUserComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (!validateUserType(typeCreateUserComboBox.Text))
            {
                validationSetError(e, typeCreateUserComboBox, "User type you have enterd is not in the list!");
                typeCreateUserComboBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void usernameCreateUserTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (allUsers != null)
            {
                bool ok = true;
                foreach (string usrName in allUsers)
                {
                    if (usrName.Equals(usernameCreateUserTextBox.Text))
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok)
                {
                    // report error
                    //if there is user with that specific username

                    validationSetError(e, usernameCreateUserTextBox, "User you enterd is already existing in system!");
                    usernameCreateUserTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
        }

        private void nameCreateUserTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            if (!validateName(nameCreateUserTextBox.Text))
            {
                // if name contains number/s
                validationSetError(e, nameCreateUserTextBox, "Name must NOT contain digits!");
                nameCreateUserTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void surnameCreateUserTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            if (!validateSurname(surnameCreateUserTextBox.Text))
            {
                // if name contains number/s
                validationSetError(e, surnameCreateUserTextBox, "Surname must NOT contain digits!");
                surnameCreateUserTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void phoneCreateUserTextBox_Validating(object sender, CancelEventArgs e)
        {
            // regular expression
            string[] patterns = { @"^\d+$", @"^\d{3}-\d{4}$", @"^\d{4}-\d{3}$", @"^\d{3}-\d{2}-{2}$" };
            string phone = phoneCreateUserTextBox.Text;
            bool ok = false;

            foreach (string pattern in patterns)
            {
                if (Regex.IsMatch(phone, pattern))
                {
                    // if phone matches one pattern from pattern list
                    // of regular expression than break
                    ok = true;
                    break;
                }
            }

            if (phone.Length != 0 && !ok)
            {
                validationSetError(e, phoneCreateUserTextBox, "Telephone doesn't match regular expression");
                phoneCreateUserTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void mailCreateUserTextBox_Validating(object sender, CancelEventArgs e)
        {
            // regular expression for email address
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                        + @"[a-zA-Z]{2,}))$";

            string mail = mailCreateUserTextBox.Text;

            if (mail.Length != 0 && !Regex.IsMatch(mail, patternStrict))
            {
                validationSetError(e, mailCreateUserTextBox, "Not recognizable e-mail address!");
                mailCreateUserTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void searchTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (!searchTypeComboBox.Text.Contains("choose type...") &&
                !validateUserType(searchTypeComboBox.Text))
            {
                validationSetError(e, searchTypeComboBox, "User type you have enterd is not in the list!");
                searchTypeComboBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void searchUserNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (searchUserNameTextBox.Modified && !searchUserNameTextBox.Text.Equals(string.Empty))
            {
                validateUserName(e, searchUserNameTextBox);
            }
        }

        private void createCNumTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (ClassroomNums != null)
            {
                bool ok = true;
                foreach (string classNum in ClassroomNums)
                {
                    if (classNum.Equals(createCNumTextBox.Text))
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok || allNonVisibleClassroomsListBox.Items.Contains(createCNumTextBox.Text))
                {
                    // report error
                    //if there is no classroom with that number

                    validationSetError(e, createCNumTextBox, "This classroom is already existing in the system!");
                    createCNumTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            } 
            else if (allNonVisibleClassroomsListBox.Items.Contains(createCNumTextBox.Text))
            {
                // report error
                //if there is no classroom with that number

                validationSetError(e, createCNumTextBox, "This classroom is already existing in the system!");
                createCNumTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void createCapacityTextBox_Validating(object sender, CancelEventArgs e)
        {
            // last two parameters are irrelevant because we are creating
            // new classroom, not searching
            validateClassroomCapacity(e, createCapacityTextBox, false, "");
        }

        private void createStartWorkingTextBox_Validating(object sender, CancelEventArgs e)
        {
            // last two parameters are irrelevant because we are creating
            // new classroom, not searching
            validateStartTime(e, createStartWorkingTextBox, createEndWorkingTextBox, false, "");
        }

        private void createEndWorkingTextBox_Validating(object sender, CancelEventArgs e)
        {
            // last two parameters are irrelevant because we are creating
            // new classroom, not searching
            validateEndTime(e, createStartWorkingTextBox, createEndWorkingTextBox, false, "");
        }

        private void searchClassroomNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomNumber(e, searchClassroomNumberTextBox);
            ClassroomNumSearchEntered = true;
        }

        private void capacitysearchClassroomTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomCapacity(e, capacitySearchClassroomTextBox, ClassroomNumSearchEntered, searchClassroomNumberTextBox.Text);
        }

        private void laboratoryPersonnelsearchClassroomTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (allLaboratoryPersonnel != null && !laboratoryPersonnelSearchClassroomTextBox.Text.Equals(string.Empty))
            {
                bool ok = false;
                foreach (string usrName in allLaboratoryPersonnel)
                {
                    if (usrName.Equals(laboratoryPersonnelSearchClassroomTextBox.Text))
                    {
                        ok = true;
                        break;
                    }
                }

                if (!ok)
                {
                    // report error
                    //if there is no classroom with that number

                    validationSetError(e, laboratoryPersonnelSearchClassroomTextBox, "You can search only by laboratory personnel");
                    laboratoryPersonnelSearchClassroomTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
        }

        private void blackboardSearchClassroomCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, blackboardSearchClassroomCheckBox, 0, ClassroomNumSearchEntered, searchClassroomNumberTextBox.Text);
        }

        private void computerssearchClassroomCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, computersSearchClassroomCheckBox, 1, ClassroomNumSearchEntered, searchClassroomNumberTextBox.Text);
        }

        private void projectorsearchClassroomCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, projectorSearchClassroomCheckBox, 2, ClassroomNumSearchEntered, searchClassroomNumberTextBox.Text);
        }

        private void laboratorysearchClassroomCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, laboratorySearchClassroomCheckBox, 3, ClassroomNumSearchEntered, searchClassroomNumberTextBox.Text);
        }

        private void typeCreateUserComboBox_TextChanged(object sender, EventArgs e)
        {
            string userType = typeCreateUserComboBox.Text;

            if (userType.ToLower().Equals("laboratory personnel"))
            {
                createAddClassroomButton.Enabled = true;
            }
            else
            {
                createAddClassroomButton.Enabled = false;
            }
        }

        private void registrationNewUserDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = registrationNewUserDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                registrationCreateUserButton.Enabled = true;
                registrationDeleteUserButton.Enabled = true;
            }
            else
            {
                registrationCreateUserButton.Enabled = false;
                registrationDeleteUserButton.Enabled = false;
            }
        }

        private void usersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = usersDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                changeInfoButton.Enabled =
                blockButton.Enabled =
                deleteUserButton.Enabled = true;
            }
            else
            {
                changeInfoButton.Enabled =
                blockButton.Enabled =
                deleteUserButton.Enabled = false;
            }
        }

        private void allUsersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = allUsersDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                changeInfoAllUsersButton.Enabled =
                blockAllUsersButton.Enabled =
                deleteAllUsersButton.Enabled = true;
            }
            else
            {
                changeInfoAllUsersButton.Enabled =
                blockAllUsersButton.Enabled =
                deleteAllUsersButton.Enabled = false;
            }
        }

        private void searchTypeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                searchTypeComboBox.Text = "choose type...";
                searchTypeComboBox.Focus();
            }
        }

        private void typeCreateUserComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                typeCreateUserComboBox.Text = "choose type...";
                typeCreateUserComboBox.Focus();
            }
        }

        private void baseTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baseTabControl.SelectedIndex == 3)
            {
                refreshCreateNewUserTab();

                refreshAllUsersTabPage();
            } 
            if(baseTabControl.SelectedIndex != 3)
            {
                resetAllComponentCreateUserTab();

                clearAllComponentsSearchUsersTabPage();
            }
            if (baseTabControl.SelectedIndex == 4)
            {
                allVisibleClassroomsListBox.Items.Clear();
                allNonVisibleClassroomsListBox.Items.Clear();
                fillVisibleClassrooms();
                fillNonVisibleClassrooms();
            }
            if (baseTabControl.SelectedIndex != 4)
            {
                clearAllComponentsCreateClassroomTab();
                clearAllComponentSearchClassroomTab();
            }

            searchClassroomDataGridView.Rows.Clear();
        }

        private void manageUsersTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshCreateNewUserTab();
            refreshAllUsersTabPage();

            if (manageUsersTabControl.SelectedIndex != 1)
            {
                resetAllComponentCreateUserTab();
            }
            if (manageUsersTabControl.SelectedIndex != 2)
            {
                clearAllComponentsSearchUsersTabPage();
            }
        }

        private void manageClassroomsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manageClassroomsTabControl.SelectedIndex != 2)
            {
                allVisibleClassroomsListBox.Items.Clear();
                allNonVisibleClassroomsListBox.Items.Clear();
                fillVisibleClassrooms();
                fillNonVisibleClassrooms();
            }
            if (manageClassroomsTabControl.SelectedIndex != 1)
            {
                searchClassroomDataGridView.Rows.Clear();

                clearAllComponentSearchClassroomTab();
            }
            if (manageClassroomsTabControl.SelectedIndex != 0)
            {
                clearAllComponentsCreateClassroomTab();
            }
        }

        private void searchClassroomDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = searchClassroomDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                changeInfoClassroomButton.Enabled = 
                addRemoveClassroomPersonnelButton.Enabled = 
                deleteClassroomButton.Enabled = true;
            }
            else
            {
                changeInfoClassroomButton.Enabled =
                addRemoveClassroomPersonnelButton.Enabled =
                deleteClassroomButton.Enabled = false;
            }
        }

        private void laboratoryPersonnelSearchClassroomTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                laboratoryPersonnelSearchClassroomTextBox.Clear();
                clearErrorProvider();
            }
        }
    }
}
