using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PKI_project.rs.etf.pki.logic;

namespace PKI_project
{
    public partial class BaseApplication : Form
    {
        //parent
        private LoginForm parent;

        // to know how to fill user info
        private string username;

        private AboutBox about;
        protected BaseReserve reserve, changeReservation;
        private ChangePassword changePass;
        protected ChangeUserInfo changeUserInfo;

        // list of classroom numbers, users
        protected string[] classroomNums = { },
            allUsers = { };

        // for validating
        private bool classroomNumFreeEntered,
            classroomNumOccupiedEndtered,
            classroomNumMyReservationEntered;

        // connection
        private SqlConnection connection = null;
        protected SqlDataReader rdr = null;

        // for synchronization
        protected static object lockClassrooms = new object(),
            lockUsers = new object(),
            lockDB = new object();

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string[] ClassroomNums
        {
            get { return classroomNums; }
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public bool ClassroomNumMyReservationEntered
        {
            get { return classroomNumMyReservationEntered; }
            set { classroomNumMyReservationEntered = value; }
        }

        public bool ClassroomNumOccupiedEndtered
        {
            get { return classroomNumOccupiedEndtered; }
            set { classroomNumOccupiedEndtered = value; }
        }

        public bool ClassroomNumFreeEntered
        {
            get { return classroomNumFreeEntered; }
            set { classroomNumFreeEntered = value; }
        }

        public BaseApplication()
        {
            InitializeComponent();

            // open connection
            openConnection();

            // fill user info
            //fillUserInfo();

            // get all classrooms in list
            getClassrooms();

            // get all users in list
            getAllUsers();

            // initialize component that are not
            // have been initialized
            initializeWindowFormComponents();
        }

        public BaseApplication(string username)
        {
            // set user name
            Username = username;

            InitializeComponent();

            // open connection
            openConnection();

            // fill user info
            fillUserInfo();

            // get all classrooms in list
            getClassrooms();

            // get all users in list
            getAllUsers();

            // initialize component that are not
            // have been initialized
            initializeWindowFormComponents();
        }

        // initialize Window Form components for application
        private void initializeWindowFormComponents()
        {
            about = null;
            reserve = null;
            changePass = null;
            changeUserInfo = null;
            changeReservation = null;

            ClassroomNumMyReservationEntered = ClassroomNumOccupiedEndtered =
                ClassroomNumFreeEntered = false;

            tabSelect();

            clearAllComponentsSearchFreeTab();
            clearAllComponentsSearchOccupiedTab();
            clearAllComponentsSearchMyReservationTab();


            searchTypeTabControl.DrawItem += new DrawItemEventHandler(searchTypeTabControl_DrawItem);
            reservationsTabControl.DrawItem += new DrawItemEventHandler(reservationsTabControl_DrawItem);
        }

        // make new connection and open it
        private void openConnection()
        {
            if (connection == null)
            {
                // for connection string
                project.Properties.Settings s = new project.Properties.Settings();

                try
                {
                    // create new connection
                    connection = new System.Data.SqlClient.SqlConnection();

                    // set connection string
                    connection.ConnectionString = s.TESTConnectionString;

                    // open connection
                    connection.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Connection to the database failed!", "Warning");
                }
            }
        }

        // close connection
        private void closeConnection()
        {
            // close reader
            if (rdr != null) rdr.Close();

            // close connection
            if (connection != null) connection.Close();
        }

        // get all classrooms
        protected void getClassrooms()
        {
            lock (lockClassrooms)
            {
                try
                {
                    string sqlCommandText = "SELECT number, isVisible FROM All_Classrooms";

                    SqlCommand sql = new SqlCommand(sqlCommandText, connection);

                    // and execute it
                    rdr = sql.ExecuteReader();

                    List<string> list = new List<string>();

                    while (rdr.Read())
                    {
                        // fill classrooms if they are visible
                        if ((bool)rdr["isVisible"]) list.Add((string)rdr["number"]);
                    }

                    classroomNums = null;
                    classroomNums = list.ToArray();
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

        // get all users in system
        protected void getAllUsers()
        {
            lock (lockUsers)
            {
                try
                {
                    string sqlCommandText = "SELECT username FROM All_Users";

                    SqlCommand sql = new SqlCommand(sqlCommandText, connection);

                    // and execute it
                    rdr = sql.ExecuteReader();

                    List<string> list = new List<string>();

                    while (rdr.Read())
                    {
                        // fill classrooms
                        list.Add((string)rdr["username"]);
                    }

                    allUsers = list.ToArray();

                    // add to text boxes for auto completition
                    setAutoCompleteAndAutoCompleteMode(userNameTextBox, allUsers);
                    
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

        public void fillUserInfo()
        {
            try
            {
                // sql query
                string sqlCommandText = "SELECT * FROM All_Users WHERE username = @usrName";

                // make sql command
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);
                sql.Parameters.Add(new SqlParameter("@usrName", Username));

                // and execute it
                rdr = sql.ExecuteReader();

                if (rdr.Read())
                {
                    typeComboBox.Text = (string)rdr["userType"];
                    userNameTextBox.Text = (string)rdr["username"];
                    passTextBox.Text = (!(rdr["pass"] is DBNull) && (!((string)rdr["pass"]).Equals("NULL"))) ? ((string)rdr["pass"]) : string.Empty;
                    nameTextBox.Text = (!(rdr["name"] is DBNull) && (!((string)rdr["name"]).Equals("NULL"))) ? ((string)rdr["name"]) : string.Empty;
                    surnameTextBox.Text = (!(rdr["surname"] is DBNull) && (!((string)rdr["surname"]).Equals("NULL"))) ? ((string)rdr["surname"]) : string.Empty;
                    titleTextBox.Text = (!(rdr["title"] is DBNull) && (!((string)rdr["title"]).Equals("NULL"))) ? ((string)rdr["title"]) : string.Empty;
                    officeTextBox.Text = (!(rdr["office"] is DBNull) && (!((string)rdr["office"]).Equals("NULL"))) ? ((string)rdr["office"]) : string.Empty;
                    telephoneTextBox.Text = (!(rdr["phone"] is DBNull) && (!((string)rdr["phone"]).Equals("NULL"))) ? ((string)rdr["phone"]) : string.Empty;
                    mailTextBox.Text = (!(rdr["mail"] is DBNull) && (!((string)rdr["mail"]).Equals("NULL"))) ? ((string)rdr["mail"]) : string.Empty;
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

        // get all free terms and fill it to the table
        private void fillReservationsForSpecificDateHasRowsOneClassroom(SqlDataReader rdr, DataGridView dataGrid, int startTime, int endTime)
        {
            // if there is some reservation for that day, 
            // we must see if there is some free term that 
            // corresponds to demanded search

            // list of all reservation start and end times
            List<int> reservationListTimes = new List<int>();

            // list of all free terms
            List<string> free = new List<string>();

            string[] rowParams = null, workingHoursString = null;
            string ev_date = string.Empty;

            bool firstTime = true;

            while (rdr.Read())
            {
                if (firstTime)
                {
                    List<string> tmpList = new List<string>();
                    tmpList.Add((string)rdr["number"]);
                    tmpList.Add(rdr["blackboard"].ToString());
                    tmpList.Add(rdr["computers"].ToString());
                    tmpList.Add(rdr["projector"].ToString());
                    tmpList.Add(rdr["laboratory"].ToString());
                    tmpList.Add(rdr["capacity"].ToString());

                    ev_date = (string)rdr["ev_date"];

                    workingHoursString = ((string)rdr["workingHours"]).Split('-');

                    rowParams = tmpList.ToArray();

                    firstTime = false;
                }

                string[] temp = ((string)rdr["ev_time"]).Split('-');
                reservationListTimes.Add(int.Parse(temp[0]));
                reservationListTimes.Add(int.Parse(temp[1]));
            }

            int[] workingHours = { int.Parse(workingHoursString[0]), int.Parse(workingHoursString[1]) };


            reservationListTimes.Sort(); // sort all reservations

            for (int i = 0; i < reservationListTimes.Count; i += 2)
            {
                // if we want some specific period of time 
                // we must filter only times that are in that range
                if (startTime != -1 && reservationListTimes[i] <= startTime) continue;
                if (endTime != -1 && reservationListTimes[i] >= endTime) continue;

                // clear
                free.Clear();

                free.AddRange(rowParams);

                if (i == 0)
                {
                    if (workingHours[0] != reservationListTimes[i])
                    {
                        free.Add(workingHours[0].ToString() + "-" + reservationListTimes[0].ToString());
                    }
                    else if (workingHours[0] == reservationListTimes[i])
                    {
                        continue;
                    }
                    else if (reservationListTimes.Count > 2 && reservationListTimes[i + 1] == reservationListTimes[i + 2])
                    {
                        continue;
                    }
                    else if (reservationListTimes.Count > 2)
                    {
                        free.Add(reservationListTimes[i + 1].ToString() + "-" + reservationListTimes[i + 2].ToString());
                    }
                }
                else if (reservationListTimes[i - 1] == reservationListTimes[i])
                {
                    continue;
                }
                else
                {
                    free.Add(reservationListTimes[i - 1].ToString() + "-" + reservationListTimes[i].ToString());
                }

                free.Add(ev_date);

                // add new row
                dataGrid.Rows.Add(free.ToArray());
            }

            if ((reservationListTimes.Last() != workingHours[1] && endTime == -1) ||
                (reservationListTimes.Last() < endTime && endTime != -1))
            {
                free.Clear();
                free.AddRange(rowParams);
                free.Add(reservationListTimes.Last().ToString() + "-" + (endTime == -1 ? workingHours[1].ToString() : endTime.ToString()));
                free.Add(ev_date);

                // add new row
                dataGrid.Rows.Add(free.ToArray());
            }

            rdr.Close();
        }

        private void fillReservationsForSpecificDateNoRows(SqlDataReader rdr, DataGridView dataGrid, string sqlCommandText, 
            int startTime, int endTime, string ev_date)
        {
            if (!rdr.IsClosed) rdr.Close();

            SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

            rdr = sql.ExecuteReader();

            List<string> tmpList = new List<string>();

            while (rdr.Read())
            {
                string[] workingHoursStirng = ((string)rdr["workingHours"]).Split('-');
                int[] workingHours = { int.Parse(workingHoursStirng[0]), int.Parse(workingHoursStirng[1]) };

                if (startTime != -1 && workingHours[1] < startTime) continue;
                if (endTime != -1 && workingHours[0] > endTime) continue;

                tmpList.Add((string)rdr["number"]);
                tmpList.Add(rdr["blackboard"].ToString());
                tmpList.Add(rdr["computers"].ToString());
                tmpList.Add(rdr["projector"].ToString());
                tmpList.Add(rdr["laboratory"].ToString());
                tmpList.Add(rdr["capacity"].ToString());
                tmpList.Add(startTime == -1 ? (string)rdr["workingHours"] : startTime.ToString() + "-" + endTime.ToString());
                tmpList.Add(ev_date);

                dataGrid.Rows.Add(tmpList.ToArray());

                tmpList.Clear();
            }

            rdr.Close();
        }

        // sets auto complete mode
        // and fill collection for completition 
        protected void setAutoCompleteAndAutoCompleteMode(TextBox text, string[] autoCompleteParams)
        {
            text.AutoCompleteCustomSource.Clear();

            text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            text.AutoCompleteCustomSource.AddRange(autoCompleteParams);
            text.Refresh();
        }

        private void searchTypeTabControl_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = searchTypeTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = searchTypeTabControl.GetTabRect(e.Index);

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

        private void reservationsTabControl_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = reservationsTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = reservationsTabControl.GetTabRect(e.Index);

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

        // selects the Search tab in tab control
        private void tabSelect()
        {
            baseTabControl.SelectedIndex = 1;
        }

        // set parent
        public void setParent(LoginForm parent)
        {
            this.parent = parent;
        }

        // refresh all classroom number text boxes
        public virtual void setNewClassroomsForAutocomplete()
        {
            this.getClassrooms();

            this.setAutoCompleteAndAutoCompleteMode(classNumFreeTextBox, ClassroomNums);
            this.setAutoCompleteAndAutoCompleteMode(classroomOccupiedTextBox, ClassroomNums);
            this.setAutoCompleteAndAutoCompleteMode(classroomPickTextBox, ClassroomNums);
            this.setAutoCompleteAndAutoCompleteMode(classroomNumMyReservationsTextBox, ClassroomNums);
            
        }

        // refresh all classroom number text boxes
        public virtual void setNewUsersForAutoComplete()
        {
            this.getAllUsers();

            this.setAutoCompleteAndAutoCompleteMode(userOccupiedTextBox, allUsers);
        }

        // shows calendar of reservation for specific classroom and month of year
        private void showDataGridResults(int month, string year, string classroomNum, DataGridView dataGrid)
        {
            string date, monthDate = string.Empty;

            int numDays = 0, numDaysTemp = 0;

            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: { numDays = numDaysTemp = 31; break; }
                case 2: { numDays = numDaysTemp = 28; break; }
                case 4:
                case 6:
                case 9:
                case 11: { numDays = numDaysTemp = 30; break; }
            }

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: monthDate = "0" + month.ToString(); break;
                case 10:
                case 11:
                case 12:
                    monthDate = month.ToString(); break;
            }

            date = numDaysTemp.ToString() + "." + monthDate + "." + year;

            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            dataGrid.Refresh();

            try
            {
                List<string> allRes = new List<string>();

                string sqlCommandText =
                    "SELECT res.course, res.ev_time, res.ev_date, cr.workingHours, cr.comment , cr.notice" +
                    " FROM All_Reservations res, All_Classrooms cr" +
                    " WHERE cr.isVisible = 1 AND cr.number = @number AND res.classroomNumberID = cr.classroomID" +
                    " AND res.ev_date = @specificDate";

                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

                // get all reservation for specific month
                while (numDaysTemp > 0)
                {
                    sql.Parameters.Add(new SqlParameter("@number", classroomNum));
                    sql.Parameters.Add(new SqlParameter("@specificDate", date));

                    rdr = sql.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            allRes.Add((string)rdr["course"]);
                            allRes.Add((string)rdr["ev_time"]);
                            allRes.Add((string)rdr["ev_date"]);
                            allRes.Add((string)rdr["workingHours"]);
                            allRes.Add((string)rdr["comment"]);
                            allRes.Add((string)rdr["notice"]);
                        }
                    }

                    sql.Parameters.Clear();

                    numDaysTemp--;
                    date = (numDaysTemp < 10 ? "0" + numDaysTemp.ToString() : numDaysTemp.ToString()) + "." + monthDate + "." + year;

                    rdr.Close();
                }

                if (allRes.Count > 0)
                {
                    bool commentOK = false;
                    int startWorkingHour, endWorkingHour;

                    for (int i = 0; i < allRes.Count; i += 6)
                    {
                        if (!commentOK)
                        {
                            commentsTextBox.Text = allRes[i + 4];
                            noticeTextBox.Text = allRes[i + 5];

                            string[] workingHours = allRes[i + 3].Split('-');
                            startWorkingHour = int.Parse(workingHours[0]);
                            endWorkingHour = int.Parse(workingHours[1]);

                            List<string> emptyList = new List<string>();

                            for (int j = startWorkingHour; j < endWorkingHour; j++)
                            {
                                dataGrid.Columns.Add("column" + j, j + "-" + (j + 1));
                                emptyList.Add(string.Empty);
                            }

                            for (int j = 0; j < numDays; j++)
                            {
                                dataGrid.Rows.Add(emptyList.ToArray());
                            }

                            commentOK = true;
                        }

                        // row which has to be updated
                        string[] splitDate = allRes[i + 2].Split('.');
                        int row = int.Parse(splitDate[0]) - 1;


                        for (int j = 0; j < dataGrid.Columns.Count; j++)
                        {
                            string[] columnHeader = dataGrid.Columns[j].HeaderText.Split('-'),
                                resTime = allRes[i + 1].Split('-');
                            int numColumnsForUpdate = int.Parse(resTime[1]) - int.Parse(resTime[0]);

                            if (columnHeader[0].Equals(resTime[0]))
                            {
                                // update cells
                                while (numColumnsForUpdate > 0)
                                {
                                    dataGrid.Rows[row].Cells[j++].Value = allRes[i];
                                    numColumnsForUpdate--;
                                }

                                break;
                            }

                        }
                    }
                }
                else
                {
                    if (!rdr.IsClosed) rdr.Close();

                    sqlCommandText =
                        "SELECT workingHours, comment , notice" +
                        " FROM All_Classrooms" +
                        " WHERE isVisible = 1 AND number = @number";

                    sql.CommandText = sqlCommandText;
                    sql.Parameters.Clear();
                    sql.Parameters.Add(new SqlParameter("@number", classroomNum));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        // set comment and notice if any
                        commentsTextBox.Text = (string)rdr["comment"];
                        noticeTextBox.Text = (string)rdr["notice"];

                        // get working hours
                        string[] workingHours = ((string)rdr["workingHours"]).Split('-');
                        int startWorkingHour = int.Parse(workingHours[0]),
                            endWorkingHour = int.Parse(workingHours[1]);

                        List<string> emptyList = new List<string>();

                        for (int i = startWorkingHour; i < endWorkingHour; i++)
                        {
                            dataGrid.Columns.Add("column" + i, i + "-" + (i + 1));
                            emptyList.Add(string.Empty);
                        }

                        for (int j = 0; j < numDays; j++)
                        {
                            dataGrid.Rows.Add(emptyList.ToArray());
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

                dataGrid.Refresh();
            }
        }

        private int calculateDayDifference(string startDate, string endDate)
        {
            string[] startDateSplit = startDate.Split('.'),
                endDateSplit = endDate.Split('.');

            int startDay = int.Parse(startDateSplit[0]), endDay = int.Parse(endDateSplit[0]),
                startMonth = int.Parse(startDateSplit[1]), endMonth = int.Parse(endDateSplit[1]),
                startYear = int.Parse(startDateSplit[2]), endYear = int.Parse(endDateSplit[2]);

            DateTime startDateTime = new DateTime(startYear, startMonth, startDay),
               endDateTime = new DateTime(endYear, endMonth, endDay);

            TimeSpan diff = endDateTime - startDateTime;

            return diff.Days + 1;
        }

        private DateTime makeStartDate(string startDate)
        {
            string[] startDateSplit = startDate.Split('.');

            int startDay = int.Parse(startDateSplit[0]),
                startMonth = int.Parse(startDateSplit[1]),
                startYear = int.Parse(startDateSplit[2]);

            return new DateTime(startYear, startMonth, startDay);
        }

        //
        // ITEM CLICK METHODS
        //
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (about == null || about.IsDisposed)
            {
                about = new AboutBox();
            }

            about.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            baseTabControl.SelectedTab = searchTabPage;
        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            baseTabControl.SelectedTab = uInfoTabPage;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            parent.Dispose();
        }

        protected void reserveButton_Click(object sender, EventArgs e)
        {
            if (reserve == null || reserve.IsDisposed)
            {
                reserve = new BaseReserve(this);
            }

            // fill child form
            string[] times = searchResultsFreeDataGridView.SelectedRows[0].Cells[6].Value.ToString().Split('-');

            //username, start and end time       
            reserve.fillFormComponents(!typeComboBox.Text.ToLower().Equals("teacher"), userNameTextBox.Text, times[0], times[1], ref lockDB, 0);
           
            // set for autocompletion
            reserve.setUserCollectionForAutoComplete(allUsers);

            // make new reservation
            reserve.Reservation = new Reservation(userNameTextBox.Text, searchResultsFreeDataGridView.SelectedRows[0].Cells[0].Value.ToString(),
                int.Parse(times[0]), int.Parse(times[1]), searchResultsFreeDataGridView.SelectedRows[0].Cells[7].Value.ToString());

            reserve.Show();
        }

        protected void reserveAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in searchResultsFreeDataGridView.Rows)
            {
                if (reserve == null || reserve.IsDisposed)
                {
                    reserve = new BaseReserve(this);
                }

                reserve.setRowsForProcessing(searchResultsFreeDataGridView.Rows, userNameTextBox.Text, !typeComboBox.Text.ToLower().Equals("teacher"));

                // fill child form
                string[] times = searchResultsFreeDataGridView.Rows[0].Cells[6].Value.ToString().Split('-');

                //username, start and end time       
                reserve.fillFormComponents(!typeComboBox.Text.ToLower().Equals("teacher"), userNameTextBox.Text, times[0], times[1], ref lockDB, 1);

                // set for autocompletion
                reserve.setUserCollectionForAutoComplete(allUsers);

                // make new reservation
                reserve.Reservation = new Reservation(userNameTextBox.Text, searchResultsFreeDataGridView.Rows[0].Cells[0].Value.ToString(),
                    int.Parse(times[0]), int.Parse(times[1]), searchResultsFreeDataGridView.Rows[0].Cells[7].Value.ToString());

                reserve.Show();
            }
        }

        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            if (changePass == null || changePass.IsDisposed)
            {
                changePass = new ChangePassword(this);
            }

            // fill info
            changePass.fillFormComponents(typeComboBox.Text, Username, true);
            changePass.Show();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            if (changeUserInfo == null || changeUserInfo.IsDisposed)
            {
                changeUserInfo = new ChangeUserInfo(this);
            }

            // fill child form
            changeUserInfo.fillAllComponents(typeComboBox.Text, userNameTextBox.Text, passTextBox.Text, nameTextBox.Text,
                surnameTextBox.Text, titleTextBox.Text, officeTextBox.Text, telephoneTextBox.Text, mailTextBox.Text);
            changeUserInfo.Show();
        }

        private void changeMyReservationsButton_Click(object sender, EventArgs e)
        {
            if (changeReservation == null || changeReservation.IsDisposed)
            {
                changeReservation = new ChangeReservation(this);
            }

            // fill child form
            string[] times = searchResultsMyReservationsDataGridView.SelectedRows[0].Cells[6].Value.ToString().Split('-');

            //username, start and end time       
            changeReservation.fillFormComponents(!typeComboBox.Text.ToLower().Equals("teacher"), userNameTextBox.Text, times[0], times[1], ref lockDB, 2);

            // set for autocompletion
            changeReservation.setUserCollectionForAutoComplete(allUsers);

            // make new reservation
            changeReservation.Reservation = new Reservation(userNameTextBox.Text, searchResultsMyReservationsDataGridView.SelectedRows[0].Cells[0].Value.ToString(),
                int.Parse(times[0]), int.Parse(times[1]), searchResultsMyReservationsDataGridView.SelectedRows[0].Cells[7].Value.ToString());

            changeReservation.Show();
        }

        protected void unreserveOccupiedButton_Click(object sender, EventArgs e)
        {
            deleteOneReservation(searchResultsOccupiedDataGridView);
        }

        protected void unreserveAllOccupiedButton_Click(object sender, EventArgs e)
        {
            deleteAllReservations(searchResultsOccupiedDataGridView);
        }

        private void unreserveMyReservationsButton_Click(object sender, EventArgs e)
        {
            deleteOneReservation(searchResultsMyReservationsDataGridView);
        }

        private void unreserveAllMyReservationsButton_Click(object sender, EventArgs e)
        {
            deleteAllReservations(searchResultsMyReservationsDataGridView);
        }

        private void reservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            baseTabControl.SelectedTab = reservationsTabPage;
        }

        private void searchFreeButton_Click(object sender, EventArgs e)
        {
            string classroomNumber, specificDate, startDate, endDate;
            bool blackboard, computers, projector, laboratory;
            int classroomCapacity, startTime, endTime;

            getSearchFreeTermsComponentsContent(out classroomNumber, out blackboard, out computers, out projector, out laboratory,
                out classroomCapacity, out startTime, out endTime, out specificDate, out startDate, out endDate);

            try
            {
                // sql query
                string sqlCommandText =
                    "SELECT DISTINCT cr.number, cr.blackboard, cr.computers, cr.projector, cr.laboratory, cr.capacity, res.ev_time, res.ev_date, cr.workingHours" +
                    " FROM All_Classrooms cr, All_Reservations res" +
                    " WHERE cr.classroomID = res.classroomNumberID AND cr.isVisible = 1";

                SqlCommand sql = new SqlCommand();

                if (!classroomNumber.Equals(string.Empty))
                {
                    sqlCommandText += " AND cr.number = @classroomNumber";
                    sql.Parameters.Add(new SqlParameter("@classroomNumber", classroomNumber));

                    ClassroomNumFreeEntered = true;
                }
                if (blackboard)
                {
                    sqlCommandText += " AND cr.blackboard = @blackboard";
                    sql.Parameters.Add(new SqlParameter("@blackboard", blackboard));
                }
                if (computers)
                {
                    sqlCommandText += " AND cr.computers = @computers";
                    sql.Parameters.Add(new SqlParameter("@computers", computers));
                }
                if (projector)
                {
                    sqlCommandText += " AND cr.projector = @projector";
                    sql.Parameters.Add(new SqlParameter("@projector", projector));
                }
                if (laboratory)
                {
                    sqlCommandText += " AND cr.laboratory = @laboratory";
                    sql.Parameters.Add(new SqlParameter("@laboratory", laboratory));
                }
                if (classroomCapacity != -1)
                {
                    sqlCommandText += (ClassroomNumFreeEntered ? " AND cr.capacity = @classroomCapacity" : " AND cr.capacity >= @classroomCapacity");
                    sql.Parameters.Add(new SqlParameter("@classroomCapacity", classroomCapacity));
                }
                if (startDate.Equals(endDate))
                {
                    sqlCommandText += " AND res.ev_date = @specificDate";
                    sql.Parameters.Add(new SqlParameter("@specificDate", specificDate));
                }

                searchResultsFreeDataGridView.Rows.Clear();
                searchResultsFreeDataGridView.Refresh();

                sql.Connection = Connection;

                if (specificDateFreeTimePicker.Checked)
                {
                    if (ClassroomNumFreeEntered)
                    {
                        // if classroom num entered

                        // free terms for specific date
                        sql.CommandText = sqlCommandText;

                        // execute command
                        rdr = sql.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            // if there is some row

                            // if user enterd specific classroom for search
                            fillReservationsForSpecificDateHasRowsOneClassroom(rdr, searchResultsFreeDataGridView, startTime, endTime);
                        }
                        else
                        {
                            sqlCommandText = makeSqlCommandText(ClassroomNumFreeEntered, classroomNumber, blackboard, computers, projector, laboratory, classroomCapacity);
                            fillReservationsForSpecificDateNoRows(rdr, searchResultsFreeDataGridView, sqlCommandText, startTime, endTime, specificDate);
                        }
                    }
                    else
                    {
                        // if classroom is not inputed

                        // if he wants to see for all classrooms
                        for (int i = 0; i < ClassroomNums.Length; i++)
                        {
                            string tmpSqlCommandText = sqlCommandText + " AND cr.number = @classroomNumber";
                            sql.Parameters.Add(new SqlParameter("@classroomNumber", ClassroomNums[i]));
                            
                            sql.CommandText = tmpSqlCommandText;

                            // execute command
                            rdr = sql.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                // if there is some row

                                // if user enterd specific classroom for search
                                fillReservationsForSpecificDateHasRowsOneClassroom(rdr, searchResultsFreeDataGridView, startTime, endTime);
                            }
                            else
                            {
                                tmpSqlCommandText = makeSqlCommandText(true, ClassroomNums[i], blackboard, computers, projector, laboratory, classroomCapacity);
                                fillReservationsForSpecificDateNoRows(rdr, searchResultsFreeDataGridView, tmpSqlCommandText, startTime, endTime, specificDate);
                            }

                            // remove number for next classroom
                            sql.Parameters.RemoveAt("@classroomNumber");
                        }
                    }
                    
                }
                else
                {
                    // free terms for specific time period

                    // differece in days between end date and start date
                    int dayDiff = calculateDayDifference(startDate, endDate);
                    
                    // start date
                    DateTime specificDateTime = makeStartDate(startDate);

                    // append specific date
                    sqlCommandText += " AND res.ev_date = @ev_date";

                    while (dayDiff > 6)
                    {
                        // make new date
                        string ev_date =
                            (specificDateTime.Day < 10 ? "0" + specificDateTime.Day.ToString() : specificDateTime.Day.ToString()) + "." +
                            (specificDateTime.Month < 10 ? "0" + specificDateTime.Month.ToString() : specificDateTime.Month.ToString()) + "." +
                            (specificDateTime.Year.ToString());

                        // add parameter
                        sql.Parameters.Add(new SqlParameter("@ev_date", ev_date));


                        if (ClassroomNumFreeEntered)
                        {
                            // if classroom num entered

                            // free terms for specific date
                            sql.CommandText = sqlCommandText;

                            // execute command
                            rdr = sql.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                // if there is some row

                                // if user enterd specific classroom for search
                                fillReservationsForSpecificDateHasRowsOneClassroom(rdr, searchResultsFreeDataGridView, startTime, endTime);
                            }
                            else
                            {
                                string tmpSqlCommandText = makeSqlCommandText(ClassroomNumFreeEntered, classroomNumber, blackboard, computers, projector, laboratory, classroomCapacity);
                                fillReservationsForSpecificDateNoRows(rdr, searchResultsFreeDataGridView, tmpSqlCommandText, startTime, endTime, ev_date);
                            }
                        }
                        else
                        {
                            // if classroom is not inputed

                            // if he wants to see for all classrooms
                            for (int i = 0; i < ClassroomNums.Length; i++)
                            {
                                string tmpSqlCommandText = sqlCommandText + " AND cr.number = @classroomNumber";
                                sql.Parameters.Add(new SqlParameter("@classroomNumber", ClassroomNums[i]));

                                sql.CommandText = tmpSqlCommandText;

                                // execute command
                                rdr = sql.ExecuteReader();

                                if (rdr.HasRows)
                                {
                                    // if there is some row

                                    // if user enterd specific classroom for search
                                    fillReservationsForSpecificDateHasRowsOneClassroom(rdr, searchResultsFreeDataGridView, startTime, endTime);
                                }
                                else
                                {
                                    tmpSqlCommandText = makeSqlCommandText(true, ClassroomNums[i], blackboard, computers, projector, laboratory, classroomCapacity);
                                    fillReservationsForSpecificDateNoRows(rdr, searchResultsFreeDataGridView, tmpSqlCommandText, startTime, endTime, ev_date);
                                }

                                // remove number for next classroom
                                sql.Parameters.RemoveAt("@classroomNumber");
                            }
                        }

                        dayDiff -= 7;
                        specificDateTime = specificDateTime.AddDays(7);

                        sql.Parameters.RemoveAt("@ev_date");
                    }
                }

                // show results
                searchResultsFreeDataGridView.Refresh();

            }
            catch (Exception)
            {
            }
            finally
            {
                if (!rdr.IsClosed) rdr.Close();

                clearAllComponentsSearchFreeTab();
            }
        }

        private void searchOccupiedButton_Click(object sender, EventArgs e)
        {
            string user, classroomNumber, specificDate, startDate, endDate;
            bool blackboard, computers, projector, laboratory;
            int classroomCapacity, startTime, endTime;

            getSearchOccupiedTermsComponentsContent(out user, out classroomNumber, out blackboard, out computers, out projector,
                out laboratory, out classroomCapacity, out startTime, out endTime, out specificDate, out startDate, out endDate);

            string time = (startTime != -1 ? startTime.ToString() : string.Empty) + "-" +
                (endTime != -1 ? endTime.ToString() : string.Empty);

            try
            {
                // sql query
                string sqlCommandText =
                    "SELECT DISTINCT cr.number, cr.blackboard, cr.computers, cr.projector, cr.laboratory, cr.capacity, res.ev_time, res.ev_date, res.course" +
                    " FROM All_Classrooms cr, All_Reservations res, All_Users" +
                    " WHERE cr.classroomID = res.classroomNumberID AND cr.isVisible = 1";

                SqlCommand sql = new SqlCommand();

                if (!classroomNumber.Equals(string.Empty))
                {
                    sqlCommandText += " AND cr.number = @classroomNumber";
                    sql.Parameters.Add(new SqlParameter("@classroomNumber", classroomNumber));
                }
                if (blackboard)
                {
                    sqlCommandText += " AND cr.blackboard = @blackboard";
                    sql.Parameters.Add(new SqlParameter("@blackboard", blackboard));
                }
                if (computers)
                {
                    sqlCommandText += " AND cr.computers = @computers";
                    sql.Parameters.Add(new SqlParameter("@computers", computers));
                }
                if (projector)
                {
                    sqlCommandText += " AND cr.projector = @projector";
                    sql.Parameters.Add(new SqlParameter("@projector", projector));
                }
                if (laboratory)
                {
                    sqlCommandText += " AND cr.laboratory = @laboratory";
                    sql.Parameters.Add(new SqlParameter("@laboratory", laboratory));
                }
                if(classroomCapacity != -1){
                    sqlCommandText += (classroomNumOccupiedEndtered ? " AND cr.capacity = @classroomCapacity" : " AND cr.capacity >= @classroomCapacity");
                    sql.Parameters.Add(new SqlParameter("@classroomCapacity", classroomCapacity));
                }
                if (startTime != -1)
                {
                    // TODO: if time
                    sqlCommandText += " AND res.ev_time = @time";
                    sql.Parameters.Add(new SqlParameter("@time", time));
                }
                if (startDate.Equals(endDate))
                {
                    sqlCommandText += " AND res.ev_date = @specificDate";
                    sql.Parameters.Add(new SqlParameter("@specificDate", specificDate));
                }
                
                if (!user.Equals(string.Empty))
                {
                    sqlCommandText += " AND All_Users.userID = res.userID AND All_Users.username = @username";
                    sql.Parameters.Add(new SqlParameter("@username", user));
                }

                sql.Connection = Connection;
                sql.CommandText = sqlCommandText;
                

                // execute command
                rdr = sql.ExecuteReader();

                
                searchResultsOccupiedDataGridView.Rows.Clear();
                searchResultsOccupiedDataGridView.Refresh();

                while (rdr.Read())
                {
                    string[] rowParams = { 
                        (string)rdr["number"], rdr["blackboard"].ToString(), rdr["computers"].ToString(),
                        rdr["projector"].ToString(), rdr["laboratory"].ToString(), rdr["capacity"].ToString(),
                        (string)rdr["ev_time"], (string)rdr["ev_date"], (string)rdr["course"]
                    };

                    if (startDate.Equals(endDate))
                    {
                        searchResultsOccupiedDataGridView.Rows.Add(rowParams);
                    }
                    else
                    {
                        string[] dateString = ((string)rdr["ev_date"]).Split('.'),
                            startDateString = startDate.Split('.'),
                            endDateString = endDate.Split('.');

                        DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]), int.Parse(dateString[0])),
                            startDateTime = new DateTime(int.Parse(startDateString[2]), int.Parse(startDateString[1]), int.Parse(startDateString[0])),
                            endDateTime = new DateTime(int.Parse(endDateString[2]), int.Parse(endDateString[1]), int.Parse(endDateString[0]));

                        if (date1.CompareTo(startDateTime) > -1 && date1.CompareTo(endDateTime) < 1)
                        {
                            // add new row
                            searchResultsOccupiedDataGridView.Rows.Add(rowParams);
                        }
                    }
                }

                // show results
                searchResultsOccupiedDataGridView.Refresh();
                
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!rdr.IsClosed) rdr.Close();

                clearAllComponentsSearchOccupiedTab();
            }
        }

        private void searchMyReservationsButton_Click(object sender, EventArgs e)
        {
            string usrname = userNameTextBox.Text, classroomNumber, specificDate, startDate, endDate;
            bool blackboard, computers, projector, laboratory;
            int classroomCapacity, startTime, endTime;

            getSearchMyReservationTermsComponentsContent(out classroomNumber, out blackboard, out computers, out projector,
                out laboratory, out classroomCapacity, out startTime, out endTime, out specificDate, out startDate, out endDate);

            string time = (startTime != -1 ? startTime.ToString() : string.Empty) + "-" +
                (endTime != -1 ? endTime.ToString() : string.Empty);

            try
            {
                SqlCommand sql = new SqlCommand();

                // sql query
                string sqlCommandText =
                    "SELECT DISTINCT cr.number, cr.blackboard, cr.computers, cr.projector, cr.laboratory, cr.capacity, res.ev_time, res.ev_date, res.course" +
                    " FROM All_Classrooms cr, All_Reservations res, All_Users" +
                    " WHERE All_Users.userID = res.userID AND All_Users.username = @username AND cr.isVisible = 1 AND cr.classroomID = res.classroomNumberID";

                sql.Parameters.Add(new SqlParameter("@username", usrname));

                if (!classroomNumber.Equals(string.Empty))
                {
                    sqlCommandText += " AND cr.number = @classroomNumber";
                    sql.Parameters.Add(new SqlParameter("@classroomNumber", classroomNumber));
                }
                if (blackboard)
                {
                    sqlCommandText += " AND cr.blackboard = @blackboard";
                    sql.Parameters.Add(new SqlParameter("@blackboard", blackboard));
                }
                if (computers)
                {
                    sqlCommandText += " AND cr.computers = @computers";
                    sql.Parameters.Add(new SqlParameter("@computers", computers));
                }
                if (projector)
                {
                    sqlCommandText += " AND cr.projector = @projector";
                    sql.Parameters.Add(new SqlParameter("@projector", projector));
                }
                if (laboratory)
                {
                    sqlCommandText += " AND cr.laboratory = @laboratory";
                    sql.Parameters.Add(new SqlParameter("@laboratory", laboratory));
                }
                if (classroomCapacity != -1)
                {
                    sqlCommandText += (classroomNumOccupiedEndtered ? " AND cr.capacity = @classroomCapacity" : " AND cr.capacity >= @classroomCapacity");
                    sql.Parameters.Add(new SqlParameter("@classroomCapacity", classroomCapacity));
                }
                if (startTime != -1)
                {
                    sqlCommandText += " AND res.ev_time = @time";
                    sql.Parameters.Add(new SqlParameter("@time", time));
                }
                if (startDate.Equals(endDate))
                {
                    sqlCommandText += " AND res.ev_date = @specificDate";
                    sql.Parameters.Add(new SqlParameter("@specificDate", specificDate));
                }
                else
                {
                    sqlCommandText += " AND res.ev_date >= @startDate AND res.ev_date <= @endDate";
                    sql.Parameters.Add(new SqlParameter("@startDate", startDate));
                    sql.Parameters.Add(new SqlParameter("@endDate", endDate));
                }

                sql.Connection = Connection;
                sql.CommandText = sqlCommandText;


                // execute command
                rdr = sql.ExecuteReader();


                searchResultsMyReservationsDataGridView.Rows.Clear();
                searchResultsMyReservationsDataGridView.Refresh();

                while (rdr.Read())
                {
                    string[] rowParams = { 
                        (string)rdr["number"], rdr["blackboard"].ToString(), rdr["computers"].ToString(),
                        rdr["projector"].ToString(), rdr["laboratory"].ToString(), rdr["capacity"].ToString(),
                        (string)rdr["ev_time"], (string)rdr["ev_date"], (string)rdr["course"]
                    };

                    if (startDate.Equals(endDate))
                    {
                        searchResultsMyReservationsDataGridView.Rows.Add(rowParams);
                    }
                    else
                    {
                        string[] dateString = ((string)rdr["ev_date"]).Split('.'),
                            startDateString = startDate.Split('.'),
                            endDateString = endDate.Split('.');

                        DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]), int.Parse(dateString[0])),
                            startDateTime = new DateTime(int.Parse(startDateString[2]), int.Parse(startDateString[1]), int.Parse(startDateString[0])),
                            endDateTime = new DateTime(int.Parse(endDateString[2]), int.Parse(endDateString[1]), int.Parse(endDateString[0]));

                        if (date1.CompareTo(startDateTime) > -1 && date1.CompareTo(endDateTime) < 1)
                        {
                            // add new row
                            searchResultsMyReservationsDataGridView.Rows.Add(rowParams);
                        }
                    }
                }

                // show results
                searchResultsMyReservationsDataGridView.Refresh();

            }
            catch (Exception)
            {
            }
            finally
            {
                rdr.Close();

                clearAllComponentsSearchMyReservationTab();
            }
        }

        private void helpApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider.HelpNamespace);
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        protected void clearErrorProvider()
        {
            errorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            errorProvider.SetError(c, msgError);
        }

        // validate start and end date
        protected void validateDates(CancelEventArgs e, Control c, string[] startDate, string[] endDate)
        {
            int startDay = int.Parse(startDate[0]),
                    startMonth = int.Parse(startDate[1]),
                    startYear = int.Parse(startDate[2]);

            int endDay = int.Parse(endDate[0]),
                endMonth = int.Parse(endDate[1]),
                endYear = int.Parse(endDate[2]);

            if ((startYear > endYear) ||
                (endYear == startYear && endMonth < startMonth) ||
                (endYear == startYear && endMonth == startMonth && endDay < startDay))
            {
                validationSetError(e, c, "Start date for search is greater than end date");
                startDateFreeTimePicker.Select();
            }
            else
            {
                clearErrorProvider();
            }
        }

        // validate classroom number
        protected void validateClassroomNumber(CancelEventArgs e, TextBox textBox)
        {
            if (!textBox.Text.Equals(string.Empty) && classroomNums != null)
            {
                bool ok = false;
                foreach (string classNum in classroomNums)
                {
                    if (classNum.Equals(textBox.Text))
                    {
                        ok = true;
                        break;
                    }
                }

                if (!ok)
                {
                    // report error
                    //if there is no classroom with that number

                    validationSetError(e, textBox, "This classroom is non-existant");
                    textBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            else
            {
                clearErrorProvider();
            }
        }

        // validate capacity
        protected void validateClassroomCapacity(CancelEventArgs e, TextBox capTextBox, bool classNumEntered, string classNum)
        {
            // report error

            if (!capTextBox.Text.Equals(string.Empty))
            {
                // if capacity is inputed
                try
                {
                    // if capacity is 0 or lower than null 

                    int cap = int.Parse(capTextBox.Text);

                    if (cap <= 0)
                    {
                        validationSetError(e, capTextBox, "Capacity must be greater than 0");
                        capTextBox.SelectAll();
                    }
                    else if (classNumEntered)
                    {
                        // if classroom is selected and capacity is 
                        // not equal to that classroom capacity

                        // sql query
                        string sqlCommandText = "SELECT capacity FROM All_Classrooms WHERE number = @num";

                        // make sql command
                        SqlCommand sql = new SqlCommand(sqlCommandText, connection);
                        sql.Parameters.Add(new SqlParameter("@num", classNum));

                        // and execute it
                        rdr = sql.ExecuteReader();

                        if (rdr.Read() && cap != (int)rdr["capacity"])
                        {
                            validationSetError(e, capTextBox, "Capacity of that classroom is not the size you entered");
                            capTextBox.SelectAll();
                        }
                        else
                        {
                            clearErrorProvider();
                        }
                    }
                    else
                    {
                        clearErrorProvider();
                    }
                }
                catch (Exception)
                {
                    // or non digit is presed

                    validationSetError(e, capTextBox, "Capacity must be a number!");
                    capTextBox.SelectAll();
                }
                finally
                {
                    rdr.Close();
                }
            }
            else
            {
                clearErrorProvider();
            }
        }

        // validate start time
        protected void validateStartTime(CancelEventArgs e, TextBox startTimeTextBox, TextBox endTimeTextBox, bool classNumEntered, string classNum)
        {
            if (!startTimeTextBox.Text.Equals(string.Empty))
            {
                // report error
                try
                {
                    int start = int.Parse(startTimeTextBox.Text),
                        end = !endTimeTextBox.Text.Equals(string.Empty) ? int.Parse(endTimeTextBox.Text) : 24;

                    if (start > 24 || start < 0)
                    {
                        // if time is not in the range 00-24
                        validationSetError(e, endTimeTextBox, "Working hours are in range 00-24");
                        startTimeTextBox.SelectAll();
                    }
                    else if (classNumEntered)
                    {
                        // if working hours of classroom doesn't match
                        // sql query
                        string sqlCommandText = "SELECT workingHours FROM All_Classrooms WHERE number = @num";

                        // make sql command
                        SqlCommand sql = new SqlCommand(sqlCommandText, connection);
                        sql.Parameters.Add(new SqlParameter("@num", classNum));

                        // and execute it
                        rdr = sql.ExecuteReader();

                        if (rdr.Read())
                        {
                            string[] time = ((string)rdr["workingHours"]).Split('-');

                            if (start < int.Parse(time[0]))
                            {
                                validationSetError(e, endTimeTextBox, "Start time of classroom is greater than that you entered");
                                startTimeTextBox.SelectAll();
                            }
                            else if (start > int.Parse(time[1]))
                            {
                                validationSetError(e, endTimeTextBox, "End time of classroom is less than that you entered");
                                startTimeTextBox.SelectAll();
                            }
                            else if (start >= end)
                            {
                                validationSetError(e, endTimeTextBox, "Start time is greater than end time");
                                startTimeTextBox.SelectAll();
                            }
                            else 
                            {
                                clearErrorProvider();
                            }
                        }

                    }
                    else if (start >= end)
                    {
                        // if start time is greater than end time
                        validationSetError(e, endTimeTextBox, "Start time is greater than end time");
                        startTimeTextBox.SelectAll();
                    }
                    else
                    {
                        clearErrorProvider();
                    }
                }
                catch (Exception)
                {
                    validationSetError(e, endTimeTextBox, "Time must be a number");
                    startTimeTextBox.SelectAll();
                }
                finally
                {
                    rdr.Close();
                }
            }
        }

        // validate end time
        protected void validateEndTime(CancelEventArgs e, TextBox startTimeTextBox, TextBox endTimeTextBox, bool classNumEntered, string classNum)
        {
            if (!endTimeTextBox.Text.Equals(string.Empty))
            {
                // report error
                try
                {
                    int start = !startTimeTextBox.Text.Equals(string.Empty) ? int.Parse(startTimeTextBox.Text) : 0,
                        end = int.Parse(endTimeTextBox.Text);

                    if (end > 24 || end < 0)
                    {
                        // if time is not in the range 00-24
                        validationSetError(e, endTimeTextBox, "Working hours are in range 00-24");
                        endTimeTextBox.SelectAll();
                    }
                    else if (classNumEntered)
                    {
                        // if working hours of classroom doesn't match
                        // sql query
                        string sqlCommandText = "SELECT workingHours FROM All_Classrooms WHERE number = @num";

                        // make sql command
                        SqlCommand sql = new SqlCommand(sqlCommandText, connection);
                        sql.Parameters.Add(new SqlParameter("@num", classNum));

                        // and execute it
                        rdr = sql.ExecuteReader();

                        if (rdr.Read())
                        {
                            string[] time = ((string)rdr["workingHours"]).Split('-');

                            if (end > int.Parse(time[1]))
                            {
                                validationSetError(e, endTimeTextBox, "End time of classroom is less than that you entered");
                                endTimeTextBox.SelectAll();
                            } else if(end < int.Parse(time[0])){
                                validationSetError(e, endTimeTextBox, "Start time of classroom is greater than that you entered");
                                endTimeTextBox.SelectAll();
                            } else if(start >= end){
                                // if start time is greater than end time
                                validationSetError(e, endTimeTextBox, "End time is less than start time");
                                endTimeTextBox.SelectAll();
                            }
                            else
                            {
                                clearErrorProvider();
                            }
                        }
                    }
                    else if (start >= end)
                    {
                        // if start time is greater than end time
                        validationSetError(e, endTimeTextBox, "End time is less than start time");
                        endTimeTextBox.SelectAll();
                    }
                    else
                    {
                        clearErrorProvider();
                    }
                }
                catch (Exception)
                {
                    validationSetError(e, endTimeTextBox, "Time must be a number");
                    endTimeTextBox.SelectAll();
                }
                finally
                {
                    rdr.Close();
                }
            }
        }

        // validate user names
        protected void validateUserName(CancelEventArgs e, TextBox textBox)
        {
            if (allUsers != null)
            {
                bool ok = false;
                foreach (string usrName in allUsers)
                {
                    if (usrName.Equals(textBox.Text))
                    {
                        ok = true;
                        break;
                    }
                }

                if (!ok)
                {
                    // report error
                    //if there is no classroom with that number

                    validationSetError(e, textBox, "User you enterd is non-existant");
                    textBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
        }

        // validates check box
        // if classroom is selected, the see if that classroom
        // is made for that kind of work
        // kind 0 - blackboard, 1 - computers, 2 -projector, 3 - laboratory
        protected void validateCheckBox(CancelEventArgs e, CheckBox checkBox, int kind, bool classNumEntered, string classNum)
        {
            // report error
            string[] classType = { "blackboard", "computers", "projector", "laboratory" };

            if (checkBox.Checked && classNumEntered)
            {
                // if classroom is selected and classroom is not
                // made for that type of teaching
                try
                {
                    // sql query
                    string sqlCommandText = "SELECT " + classType[kind] + " FROM All_Classrooms WHERE number = @num";

                    // make sql command
                    SqlCommand sql = new SqlCommand(sqlCommandText, connection);
                    if (classNumEntered) sql.Parameters.Add(new SqlParameter("@num", classNum));

                    // and execute it
                    rdr = sql.ExecuteReader();

                    if (rdr.Read() && checkBox.Checked != (bool)rdr[classType[kind]])
                    {
                        validationSetError(e, checkBox, "Classroom is not made for that type of teaching!");
                    }
                    else
                    {
                        clearErrorProvider();
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
            else
            {
                clearErrorProvider();
            }
        }

        private void classNumFreeTextBox_Validating(object sender, CancelEventArgs e)
        {
            // classroom number can be 25a, 12c, 1, 02...
            validateClassroomNumber(e, classNumFreeTextBox);
            setClassroomNumFreeEntered(classNumFreeTextBox);
        }

        private void capacityFreeTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateClassroomCapacity(e, capacityFreeTextBox, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void startTimeFreeTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateStartTime(e, startTimeFreeTextBox, endTimeFreeTextBox, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void endTimeFreeTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateEndTime(e, startTimeFreeTextBox, endTimeFreeTextBox, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void startDateFreeTimePicker_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            if (endDateFreeTimePicker.Checked)
            {
                string[] startDate = startDateFreeTimePicker.Text.Split('.'),
                    endDate = endDateFreeTimePicker.Text.Split('.');

                validateDates(e, endDateFreeTimePicker, startDate, endDate);
            }
        }

        private void endDateFreeTimePicker_Validating(object sender, CancelEventArgs e)
        {
            string[] startDate = startDateFreeTimePicker.Text.Split('.'),
                endDate = endDateFreeTimePicker.Text.Split('.');

            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateDates(e, endDateFreeTimePicker, startDate, endDate);
        }

        private void userOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateUserName(e, userNameTextBox);
        }

        private void classroomOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomNumber(e, classroomOccupiedTextBox);
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
        }

        private void capcaityOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateClassroomCapacity(e, capacityOccupiedTextBox, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void startTimeOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateStartTime(e, startTimeOccupiedTextBox, endTimeOccupiedTextBox, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void endTimeOccupiedTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateEndTime(e, startTimeOccupiedTextBox, endTimeOccupiedTextBox, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void startDateOccupiedDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (endDateOccupiedDateTimePicker.Checked)
            {
                string[] startDate = startDateOccupiedDateTimePicker.Text.Split('.'),
                    endDate = endDateOccupiedDateTimePicker.Text.Split('.');

                validateDates(e, endDateOccupiedDateTimePicker, startDate, endDate);
            }
        }

        private void endDateOccupiedDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            string[] startDate = startDateOccupiedDateTimePicker.Text.Split('.'),
                endDate = endDateOccupiedDateTimePicker.Text.Split('.');

            validateDates(e, endDateOccupiedDateTimePicker, startDate, endDate);
        }

        private void classroomPickTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomNumber(e, classroomPickTextBox);
        }

        private void yearTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            try
            {
                // if year is less than 1980 

                int year = int.Parse(yearTextBox.Text);

                if (year <= 1980)
                {
                    validationSetError(e, yearTextBox, "Year must be greater than 1980");
                    yearTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                // or non digit is presed

                validationSetError(e, yearTextBox, "Year must be a number!");
                yearTextBox.SelectAll();
            }
        }

        private void classroomNumMyReservationsTextBox_Validating(object sender, CancelEventArgs e)
        {
            validateClassroomNumber(e, classroomNumMyReservationsTextBox);
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
        }

        private void capacityMyReservationsTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateClassroomCapacity(e, capacityMyReservationsTextBox, ClassroomNumMyReservationEntered,
                classroomNumMyReservationsTextBox.Text);
        }

        private void startTimeMyReservationsTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateStartTime(e, startTimeMyReservationsTextBox, endTimeMyReservationsTextBox, ClassroomNumMyReservationEntered,
                classroomNumMyReservationsTextBox.Text);
        }

        private void endTimeMyReservationsTextBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateEndTime(e, startTimeMyReservationsTextBox, endTimeMyReservationsTextBox, ClassroomNumMyReservationEntered,
                classroomNumMyReservationsTextBox.Text);
        }

        private void startDateMyReservationsDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (endDateMyReservationsDateTimePicker.Checked)
            {
                string[] startDate = startDateMyReservationsDateTimePicker.Text.Split('.'),
                    endDate = endDateMyReservationsDateTimePicker.Text.Split('.');

                validateDates(e, endDateMyReservationsDateTimePicker, startDate, endDate);
            }
        }

        private void endDateMyReservationsDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            string[] startDate = startDateMyReservationsDateTimePicker.Text.Split('.'),
                endDate = endDateMyReservationsDateTimePicker.Text.Split('.');

            validateDates(e, endDateMyReservationsDateTimePicker, startDate, endDate);
        }

        private void blackboardFreeCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateCheckBox(e, blackboardFreeCheckBox, 0, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void computersFreeCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateCheckBox(e, computersFreeCheckBox, 1, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void projectorFreeCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateCheckBox(e, projectorFreeCheckBox, 2, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void laboratoryFreeCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
            validateCheckBox(e, laboratoryFreeCheckBox, 3, ClassroomNumFreeEntered, classNumFreeTextBox.Text);
        }

        private void blackboardOccupiedCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateCheckBox(e, blackboardOccupiedCheckBox, 0, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void computersOccupiedCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateCheckBox(e, computersOccupiedCheckBox, 1, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void projectorOccupiedCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateCheckBox(e, projectorOccupiedCheckBox, 2, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void laboratoryOccupiedCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumOccupiedEndtered(classroomOccupiedTextBox);
            validateCheckBox(e, laboratoryOccupiedCheckBox, 3, ClassroomNumOccupiedEndtered, classroomOccupiedTextBox.Text);
        }

        private void blackboardMyReservationsCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateCheckBox(e, blackboardMyReservationsCheckBox, 0, ClassroomNumMyReservationEntered, classroomNumMyReservationsTextBox.Text);
        }

        private void computersMyReservationsCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateCheckBox(e, computersMyReservationsCheckBox, 1, ClassroomNumMyReservationEntered, classroomNumMyReservationsTextBox.Text);
        }

        private void projectorMyReservationsCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateCheckBox(e, projectorMyReservationsCheckBox, 2, ClassroomNumMyReservationEntered, classroomNumMyReservationsTextBox.Text);
        }

        private void laboratoryMyReservationsCheckBox_Validating(object sender, CancelEventArgs e)
        {
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            setClassroomNumMyReservationEntered(classroomNumMyReservationsTextBox);
            validateCheckBox(e, laboratoryMyReservationsCheckBox, 3, ClassroomNumMyReservationEntered, classroomNumMyReservationsTextBox.Text);
        }

        private void BaseApplication_Load(object sender, EventArgs e)
        {
        }

        //
        // SERVICE METHODS
        //

        //
        // add menu items to specific menu, or menu item
        protected void addMenuItemsToMenu(ToolStripMenuItem menuItem, params ToolStripItem[] menuStripItems)
        {
            for (long i = 0; i < menuStripItems.Length; i++)
                menuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    menuStripItems[i]
                });
        }

        // 
        // get all content from Search Free Terms Tab
        // 
        protected void getSearchFreeTermsComponentsContent(
            out string classroomNumber, //classroom nuber
            out bool blackboard,        // has a blackboard
            out bool computers,     // has computers
            out bool projector,     // has projector
            out bool laboratory,    // can do laboratory excersise
            out int cap,            // classroom capacity
            out int startTime,      // specific start time
            out int endTime,        // specific end time
            out string specificDate,// specific date
            out string startDate,   // specific start date
            out string endDate)     // specific end date
        {
            classroomNumber = classNumFreeTextBox.Text;

            blackboard = blackboardFreeCheckBox.Checked;
            computers = computersFreeCheckBox.Checked;
            projector = projectorFreeCheckBox.Checked;
            laboratory = laboratoryFreeCheckBox.Checked;

            cap = capacityFreeTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(capacityFreeTextBox.Text);
            startTime = startTimeFreeTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(startTimeFreeTextBox.Text);
            endTime = endTimeFreeTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(endTimeFreeTextBox.Text);

            if (specificDateFreeTimePicker.Checked)
            {
                specificDate = specificDateFreeTimePicker.Text;
            }
            else
            {
                specificDate = string.Empty;
            }

            if (startDateFreeTimePicker.Checked || endDateFreeTimePicker.Checked)
            {
                startDate = startDateFreeTimePicker.Text;
                endDate = endDateFreeTimePicker.Text;
            }
            else
            {
                startDate = endDate = string.Empty;
            }

            setClassroomNumFreeEntered(classNumFreeTextBox);
        }

        // 
        // get all content from Search Occupied Terms Tab
        // 
        protected void getSearchOccupiedTermsComponentsContent(
            out string user,            // username
            out string classroomNumber, //classroom nuber
            out bool blackboard,        // has a blackboard
            out bool computers,     // has computers
            out bool projector,     // has projector
            out bool laboratory,    // can do laboratory excersise
            out int cap,            // classroom capacity
            out int startTime,      // specific start time
            out int endTime,        // specific end time
            out string specificDate,// specific date
            out string startDate,   // specific start date
            out string endDate)     // specific end date
        {
            user = userOccupiedTextBox.Text;
            classroomNumber = classroomOccupiedTextBox.Text;

            blackboard = blackboardOccupiedCheckBox.Checked;
            computers = computersOccupiedCheckBox.Checked;
            projector = projectorOccupiedCheckBox.Checked;
            laboratory = laboratoryOccupiedCheckBox.Checked;

            cap = capacityOccupiedTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(capacityOccupiedTextBox.Text);
            startTime = startTimeOccupiedTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(startTimeOccupiedTextBox.Text);
            endTime = endTimeOccupiedTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(endTimeOccupiedTextBox.Text);

            if (specificDateOccupiedDateTimePicker.Checked)
            {
                specificDate = specificDateOccupiedDateTimePicker.Text;
            }
            else
            {
                specificDate = string.Empty;
            }

            if (startDateOccupiedDateTimePicker.Checked || endDateOccupiedDateTimePicker.Checked)
            {
                startDate = startDateOccupiedDateTimePicker.Text;
                endDate = endDateOccupiedDateTimePicker.Text;
            }
            else
            {
                startDate = endDate = string.Empty;
            }
        }

        // 
        // get all content from My Reservations Tab
        // 
        protected void getSearchMyReservationTermsComponentsContent(
            out string classroomNumber, //classroom nuber
            out bool blackboard,        // has a blackboard
            out bool computers,     // has computers
            out bool projector,     // has projector
            out bool laboratory,    // can do laboratory excersise
            out int cap,            // classroom capacity
            out int startTime,      // specific start time
            out int endTime,        // specific end time
            out string specificDate,// specific date
            out string startDate,   // specific start date
            out string endDate)     // specific end date
        {
            classroomNumber = classroomNumMyReservationsTextBox.Text;

            blackboard = blackboardMyReservationsCheckBox.Checked;
            computers = computersMyReservationsCheckBox.Checked;
            projector = projectorMyReservationsCheckBox.Checked;
            laboratory = laboratoryMyReservationsCheckBox.Checked;

            cap = capacityMyReservationsTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(capacityMyReservationsTextBox.Text);
            startTime = startTimeMyReservationsTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(startTimeMyReservationsTextBox.Text);
            endTime = endTimeMyReservationsTextBox.Text.Equals(string.Empty) ? -1 : int.Parse(endTimeMyReservationsTextBox.Text);

            if (specificDateMyReservationsDateTimePicker.Checked)
            {
                specificDate = specificDateMyReservationsDateTimePicker.Text;
            }
            else
            {
                specificDate = string.Empty;
            }

            if (startDateMyReservationsDateTimePicker.Checked || endDateMyReservationsDateTimePicker.Checked)
            {
                
                startDate = startDateMyReservationsDateTimePicker.Text;
                endDate = endDateMyReservationsDateTimePicker.Text;
            }
            else
            {
                startDate = endDate = string.Empty;
            }
        }

        //
        // clear all components on Search Free Tab
        //
        protected void clearAllComponentsSearchFreeTab()
        {
            classNumFreeTextBox.Text =
                capacityFreeTextBox.Text =
                startTimeFreeTextBox.Text =
                endTimeFreeTextBox.Text = string.Empty;
            blackboardFreeCheckBox.Checked =
                computersFreeCheckBox.Checked =
                projectorFreeCheckBox.Checked =
                laboratoryFreeCheckBox.Checked = false;

            specificDateFreeTimePicker.ResetText();
            startDateFreeTimePicker.ResetText();
            endDateFreeTimePicker.ResetText();

            ClassroomNumFreeEntered = false;

            specificDateFreeTimePicker.Checked = true;
            startDateFreeTimePicker.Checked = endDateFreeTimePicker.Checked = false;
        }

        //
        // clear all components on Search Occupied Tab
        //
        protected void clearAllComponentsSearchOccupiedTab()
        {
            classroomOccupiedTextBox.Text =
                capacityOccupiedTextBox.Text =
                startTimeOccupiedTextBox.Text =
                endTimeOccupiedTextBox.Text = 
                userOccupiedTextBox.Text = string.Empty;
            blackboardOccupiedCheckBox.Checked =
                computersOccupiedCheckBox.Checked =
                projectorOccupiedCheckBox.Checked =
                laboratoryOccupiedCheckBox.Checked = false;

            specificDateOccupiedDateTimePicker.ResetText();
            startDateOccupiedDateTimePicker.ResetText();
            endDateOccupiedDateTimePicker.ResetText();

            ClassroomNumOccupiedEndtered = false;

            specificDateOccupiedDateTimePicker.Checked = true;
            startDateOccupiedDateTimePicker.Checked = endDateOccupiedDateTimePicker.Checked = false;
        }

        //
        // clear all components on Search My Reservation Tab
        //
        protected void clearAllComponentsSearchMyReservationTab()
        {
            classroomNumMyReservationsTextBox.Text =
                capacityMyReservationsTextBox.Text =
                startTimeMyReservationsTextBox.Text =
                endTimeMyReservationsTextBox.Text = string.Empty;
            blackboardMyReservationsCheckBox.Checked =
                computersMyReservationsCheckBox.Checked =
                projectorMyReservationsCheckBox.Checked =
                laboratoryMyReservationsCheckBox.Checked = false;

            specificDateMyReservationsDateTimePicker.ResetText();
            startDateMyReservationsDateTimePicker.ResetText();
            endDateMyReservationsDateTimePicker.ResetText();

            ClassroomNumMyReservationEntered = false;

            specificDateMyReservationsDateTimePicker.Checked = true;
            startDateMyReservationsDateTimePicker.Checked = endDateMyReservationsDateTimePicker.Checked = false;
        }


        protected void setClassroomNumFreeEntered(TextBox textBox)
        {
            if (!textBox.Text.Equals(string.Empty))
            {
                ClassroomNumFreeEntered = true;
            }
            else
            {
                ClassroomNumFreeEntered = false;
            }
        }

        protected void setClassroomNumOccupiedEndtered(TextBox textBox)
        {
            if (!textBox.Text.Equals(string.Empty))
            {
                ClassroomNumOccupiedEndtered = true;
            }
            else
            {
                ClassroomNumOccupiedEndtered = false;
            }
        }

        protected void setClassroomNumMyReservationEntered(TextBox textBox)
        {
            if (!textBox.Text.Equals(string.Empty))
            {
                ClassroomNumMyReservationEntered = true;
            }
            else
            {
                ClassroomNumMyReservationEntered = false;
            }
        }

        private void deleteOneReservation(DataGridView dataGrid)
        {
            // selected ROW!
            // only one can be selected
            DataGridViewSelectedRowCollection selectedRow = dataGrid.SelectedRows;

            if (selectedRow != null)
            {
                string sqlCommandText = "DELETE FROM All_Reservations WHERE ev_time = @ev_time AND ev_date = @ev_date";
                SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

                sql.Parameters.Add(new SqlParameter("@ev_time", selectedRow[0].Cells[6].Value.ToString()));
                sql.Parameters.Add(new SqlParameter("@ev_date", selectedRow[0].Cells[7].Value.ToString()));

                sql.ExecuteNonQuery();

                dataGrid.Rows.Remove(selectedRow[0]);

                dataGrid.Refresh();
            }
        }

        private void deleteAllReservations(DataGridView dataGrid)
        {
            int numRows = dataGrid.Rows.Count;

            string sqlCommandText = "DELETE FROM All_Reservations WHERE ev_time = @ev_time AND ev_date = @ev_date";
            SqlCommand sql = new SqlCommand(sqlCommandText, Connection);

            // delete from db
            for (int i = 0; i < numRows; i++)
            {
                sql.Parameters.Add(new SqlParameter("@ev_time", dataGrid.Rows[i].Cells[6].Value.ToString()));
                sql.Parameters.Add(new SqlParameter("@ev_date", dataGrid.Rows[i].Cells[7].Value.ToString()));

                sql.ExecuteNonQuery();

                sql.Parameters.Clear();
            }

            // delete from table
            while (numRows > 0)
            {
                dataGrid.Rows.RemoveAt(0);
                numRows--;
            }

            dataGrid.Refresh();
        }

        private string makeSqlCommandText(bool ClassroomNumFreeEntered, string classroomNumber, bool blackboard, bool computers, bool projector, bool laboratory, int classroomCapacity)
        {
            string sqlCommandText =
                    "SELECT number, blackboard, computers, projector, laboratory, capacity, workingHours" +
                    " FROM All_Classrooms WHERE isVisible = 1";


            if (ClassroomNumFreeEntered)
            {
                sqlCommandText += " AND number = " + classroomNumber;
            }
            if (blackboard == true)
            {
                sqlCommandText += " AND blackboard = 1";
            }
            if (computers == true)
            {
                sqlCommandText += " AND computers = 1";
            }
            if (projector == true)
            {
                sqlCommandText += " AND projector = 1";
            }
            if (laboratory == true)
            {
                sqlCommandText += " AND laboratory = 1";
            }
            if (classroomCapacity != -1)
            {
                sqlCommandText += (ClassroomNumFreeEntered ?
                    " AND capacity = " + classroomCapacity.ToString() :
                    " AND capacity >= " + classroomCapacity.ToString());
            }

            return sqlCommandText;
        }

        // 
        // VALUE CHANGED METHODS
        //
        private void specificDateMyReservationsDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            specificDateMyReservationsDateTimePicker.Checked = true;
        }

        private void startDateMyReservationsDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            startDateMyReservationsDateTimePicker.Checked = true;
            specificDateMyReservationsDateTimePicker.Checked = false;
        }

        private void endDateMyReservationsDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateMyReservationsDateTimePicker.Checked = true;
            specificDateMyReservationsDateTimePicker.Checked = false;
        }

        private void specificDateOccupiedDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            specificDateOccupiedDateTimePicker.Checked = true;
        }

        private void startDateOccupiedDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            startDateOccupiedDateTimePicker.Checked = true;
            specificDateOccupiedDateTimePicker.Checked = false;
        }

        private void endDateOccupiedDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateOccupiedDateTimePicker.Checked = true;
            specificDateOccupiedDateTimePicker.Checked = false;
        }

        private void specificDateFreeTimePicker_ValueChanged(object sender, EventArgs e)
        {
            specificDateFreeTimePicker.Checked = true;
        }

        private void startDateFreeTimePicker_ValueChanged(object sender, EventArgs e)
        {
            startDateFreeTimePicker.Checked = true;
            specificDateFreeTimePicker.Checked = false;
        }

        private void endDateFreeTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateFreeTimePicker.Checked = true;
            specificDateFreeTimePicker.Checked = false;
        }

        private void classNumFreeTextBox_TextChanged(object sender, EventArgs e)
        {
            setClassroomNumFreeEntered(classNumFreeTextBox);
        }

        private void searchResultsOccupiedDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = searchResultsOccupiedDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                unreserveAllOccupiedButton.Enabled =
                unreserveOccupiedButton.Enabled = true;
            }
            else
            {
                unreserveAllOccupiedButton.Enabled =
                unreserveOccupiedButton.Enabled = false;
            }
        }

        private void searchResultsFreeDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = searchResultsFreeDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                reserveAllButton.Enabled =
                reserveButton.Enabled = true;
            }
            else
            {
                reserveAllButton.Enabled =
                reserveButton.Enabled = false;
            }
        }

        private void searchResultsMyReservationsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount = searchResultsMyReservationsDataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                unreserveMyReservationsButton.Enabled =
                unreserveAllMyReservationsButton.Enabled = 
                changeMyReservationsButton.Enabled = true;
            }
            else
            {
                unreserveMyReservationsButton.Enabled =
                unreserveAllMyReservationsButton.Enabled =
                changeMyReservationsButton.Enabled = false;
            }
        }

        private void baseTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (baseTabControl.SelectedIndex != 1)
            {
                searchResultsOccupiedDataGridView.Rows.Clear();
                clearAllComponentsSearchOccupiedTab();

                searchResultsFreeDataGridView.Rows.Clear();
                clearAllComponentsSearchFreeTab();
            }
            if (baseTabControl.SelectedIndex != 2)
            {
                searchResultsMyReservationsDataGridView.Rows.Clear();
                clearAllComponentsSearchMyReservationTab();
            }
        }

        private void searchTypeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchTypeTabControl.SelectedIndex != 0)
            {
                searchResultsOccupiedDataGridView.Rows.Clear();
                clearAllComponentsSearchOccupiedTab();
            }
            else
            {
                searchResultsFreeDataGridView.Rows.Clear();
                clearAllComponentsSearchFreeTab();
            }
        }

        private void reservationsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reservationsTabControl.SelectedIndex != 1)
            {
                searchResultsMyReservationsDataGridView.Rows.Clear();
                clearAllComponentsSearchMyReservationTab();
            }
        }

        //
        // LINK LABEL CLICK METHODS
        //

        private void januaryLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(1, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void februaryLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(2, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void marchLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(3, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void aprilLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(4, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void mayLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(5, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void juneLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(6, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void julyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(7, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void augustLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(8, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void septemberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(9, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void octoberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(10, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void novemberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(11, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void decemberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!yearTextBox.Text.Equals(string.Empty) && !classroomPickTextBox.Text.Equals(string.Empty))
            {
                showDataGridResults(12, yearTextBox.Text, classroomPickTextBox.Text, monthlyCalendarDataGridView);
            }
            else
            {
                MessageBox.Show("You haven't entered clasroom number or year!", "Warning");
            }
        }

        private void logOffLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
            parent.Show();
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            parent.Show();
        }

        
    }
}