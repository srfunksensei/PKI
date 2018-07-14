using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PKI_project
{
    public partial class LabApplication : PKI_project.BaseApplication
    {

        ChangeClassroomInfo changeClassroomInfo;

        // menu items for Edit menu
        ToolStripMenuItem manageClassroomsToolStripMenuItem;

        bool classroomNumSearchClassroomEntered;

        public bool ClassroomNumSearchClassroomEntered
        {
            get { return classroomNumSearchClassroomEntered; }
            set { classroomNumSearchClassroomEntered = value; }
        }

        public LabApplication(string username)
            : base(username)
        {
            InitializeComponent();

            initializeWindowsFormsComponent();
        }

        private void initializeWindowsFormsComponent()
        {
            changeClassroomInfo = null;

            manageClassroomsToolStripMenuItem = new ToolStripMenuItem();
            manageClassroomsToolStripMenuItem.Name = "manageClassroomsToolStripMenuItem";
            manageClassroomsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            manageClassroomsToolStripMenuItem.Text = "Manage Classrooms";
            manageClassroomsToolStripMenuItem.Click += new System.EventHandler(this.manageClassroomToolStripMenuItem_Click);

            this.addMenuItemsToMenu(editToolStripMenuItem, manageClassroomsToolStripMenuItem);

            this.setAutoCompleteAndAutoCompleteMode(classroomNumberTextBox, this.classroomNums);

            this.reserveButton.Click -= new System.EventHandler(this.reserveButton_Click);
            this.reserveAllButton.Click -= new System.EventHandler(this.reserveAllButton_Click);
            this.unreserveOccupiedButton.Click -= new System.EventHandler(this.unreserveOccupiedButton_Click);
            this.unreserveAllOccupiedButton.Click -= new System.EventHandler(this.unreserveAllOccupiedButton_Click);

            this.setNewClassroomsForAutocomplete();
            this.setNewUsersForAutoComplete();
        }

        private void changeInfoClassroomButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = searchClassroomDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                isCreatedChangeClassroomInfo();

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

        private bool isCreatedChangeClassroomInfo()
        {
            if (changeClassroomInfo == null || changeClassroomInfo.IsDisposed)
            {
                changeClassroomInfo = new ChangeClassroomInfo(this);
                return false;
            }

            return true;
        }

        private void addRemoveCommentlButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = searchClassroomDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                isCreatedChangeClassroomInfo();

                try
                {
                    string sqlCommandText = "SELECT * FROM All_Classrooms WHERE number = @number";
                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedRow[0].Cells[0].Value.ToString()));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        changeClassroomInfo.enableAndShowOnlyComment(
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

        private void addRemoveNoticeButton_Click(object sender, EventArgs e)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = searchClassroomDataGridView.SelectedRows;

            if (selectedRow != null)
            {
                isCreatedChangeClassroomInfo();

                try
                {
                    string sqlCommandText = "SELECT * FROM All_Classrooms WHERE number = @number";
                    SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                    sql.Parameters.Add(new SqlParameter("@number", selectedRow[0].Cells[0].Value.ToString()));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        changeClassroomInfo.enableAndShowOnlyNotice(
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

        private void manageClassroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.baseTabControl.SelectedTab = manageClassroomTabPage;
        }

        private void clearAllComponponentsClassroomSearch()
        {
            classroomNumberTextBox.Clear();
            capacityTextBox.Clear();
            blackboardCheckBox.Checked =
            computersCheckBox.Checked =
            projectorCheckBox.Checked =
            laboratoryCheckBox.Checked = false;
        }

        // is some laboratory personnel responsible
        // for selected classroom
        private bool isResponsibleForClassroom(string classroomNum)
        {
            // get all classroom numbers
            List<string> classroomNumsForWhichLaboratoryPersonnelIsResponsible = new List<string>();

            try
            {
                string sqlCommandTex =
                    "SELECT DISTINCT croom.number FROM All_Classrooms croom, All_LabPersonnel labs" +
                    " WHERE croom.isVisible = 1 AND croom.personelID = labs.labPersonnelID AND " +
                    " (labs.labPersonnel_1 = @username OR labs.labPersonnel_2 = @username OR " +
                    " labs.labPersonnel_3 = @username OR labs.labPersonnel_4 = @username OR " +
                    " labs.labPersonnel_5 = @username)";
                SqlCommand sql = new SqlCommand(sqlCommandTex, Connection);
                sql.Parameters.Add(new SqlParameter("@username", Username));

                rdr = sql.ExecuteReader();

                while (rdr.Read())
                {
                    classroomNumsForWhichLaboratoryPersonnelIsResponsible.Add((string)rdr["number"]);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!rdr.IsClosed) rdr.Close();
            }

            if (classroomNumsForWhichLaboratoryPersonnelIsResponsible.Count > 0)
            {
                return classroomNumsForWhichLaboratoryPersonnelIsResponsible.Contains(classroomNum);
            }
            else
            {
                return false;
            }
        }

        //
        // VALIDATING METHODS
        //

        private void classroomNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomNumber(e, classroomNumberTextBox);
            ClassroomNumSearchClassroomEntered = true;
        }

        private void capacityTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomCapacity(e, capacityTextBox, ClassroomNumSearchClassroomEntered, classroomNumberTextBox.Text);
        }

        private void blackboardCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, blackboardCheckBox, 0, ClassroomNumSearchClassroomEntered, classroomNumberTextBox.Text);
        }

        private void computersCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, computersCheckBox, 1, ClassroomNumSearchClassroomEntered, classroomNumberTextBox.Text);
        }

        private void projectorCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, projectorCheckBox, 2, ClassroomNumSearchClassroomEntered, classroomNumberTextBox.Text);
        }

        private void laboratoryCheckBox_Validating(object sender, CancelEventArgs e)
        {
            validateCheckBox(e, laboratoryCheckBox, 3, ClassroomNumSearchClassroomEntered, classroomNumberTextBox.Text);
        }

        private void searchClassroomButton_Click(object sender, EventArgs e)
        {
            string classroomNum = classroomNumberTextBox.Text;
            int classroomCapacity = !capacityTextBox.Text.Equals(string.Empty) ? int.Parse(capacityTextBox.Text) : -1;
            bool blackboard = blackboardCheckBox.Checked,
                computers = computersCheckBox.Checked,
                projector = projectorCheckBox.Checked,
                laboratory = laboratoryCheckBox.Checked;

            if (!classroomNum.Equals(string.Empty) ||
                blackboard || computers || projector || laboratory ||
                classroomCapacity != -1)
            {
                try
                {
                    string sqlCommandText =
                                "SELECT number, blackboard, computers, projector, laboratory, capacity, workingHours, labPersonnel_1, labPersonnel_2, labPersonnel_3, labPersonnel_4, labPersonnel_5" +
                                " FROM All_Classrooms crooms, All_LabPersonnel labs" +
                                " WHERE crooms.isVisible = 1 AND crooms.personelID = labs.labPersonnelID";

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
                        sqlCommandText += (ClassroomNumSearchClassroomEntered ? " AND crooms.capacity = @capacity" : " AND crooms.capacity >= @capacity");
                        sql.Parameters.Add(new SqlParameter("@capacity", classroomCapacity));
                    }

                    sqlCommandText +=
                        " AND (labs.labPersonnel_1 = @laboratoryPersonnelUsername " +
                        "OR labs.labPersonnel_2 = @laboratoryPersonnelUsername " +
                        "OR labs.labPersonnel_3 = @laboratoryPersonnelUsername " +
                        "OR labs.labPersonnel_4 = @laboratoryPersonnelUsername " +
                        "OR labs.labPersonnel_5 = @laboratoryPersonnelUsername)";
                    sql.Parameters.Add(new SqlParameter("@laboratoryPersonnelUsername", Username));


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

                    // clear tab
                    clearAllComponponentsClassroomSearch();
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

        private void searchClassroomDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = searchClassroomDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                changeInfoClassroomButton.Enabled =
                addRemoveCommentlButton.Enabled =
                addRemoveNoticeButton.Enabled = true;
            }
            else
            {
                changeInfoClassroomButton.Enabled =
                addRemoveCommentlButton.Enabled =
                addRemoveNoticeButton.Enabled = false;
            }
        }

        private void baseTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baseTabControl.SelectedIndex != 3)
            {
                clearAllComponponentsClassroomSearch();
            }
        }

        private void reserveButton_Click1(object sender, EventArgs e)
        {
            if (isResponsibleForClassroom(searchResultsFreeDataGridView.SelectedRows[0].Cells[0].Value.ToString()))
            {
                reserveButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("You can reserve only for classrooms\nfor which you are responsible", "Error");
            }
        }

        private void reserveAllButton_Click1(object sender, EventArgs e)
        {
            bool ok = true;

            for (int i = 0; i < searchResultsFreeDataGridView.Rows.Count; i++)
            {
                if (!isResponsibleForClassroom(searchResultsFreeDataGridView.Rows[i].Cells[0].Value.ToString()))
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                reserveButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("You can reserve only for classrooms\nfor which you are responsible", "Error");
            }
        }

        private void unreserveOccupiedButton_Click1(object sender, EventArgs e)
        {
            if (isResponsibleForClassroom(searchResultsOccupiedDataGridView.SelectedRows[0].Cells[0].Value.ToString()))
            {
                unreserveOccupiedButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("You can unreserve only for classrooms\nfor which you are responsible", "Error");
            }
        }

        private void unreserveAllOccupiedButton_Click1(object sender, EventArgs e)
        {
            bool ok = true;

            for (int i = 0; i < searchResultsOccupiedDataGridView.Rows.Count; i++)
            {
                if (!isResponsibleForClassroom(searchResultsOccupiedDataGridView.Rows[i].Cells[0].Value.ToString()))
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                unreserveAllOccupiedButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("You can unreserve only for classrooms\nfor which you are responsible", "Error");
            }
        }
    }
}
