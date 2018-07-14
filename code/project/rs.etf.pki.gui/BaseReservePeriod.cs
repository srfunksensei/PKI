using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PKI_project.rs.etf.pki.logic;
using System.Data.SqlClient;

namespace PKI_project
{
    public partial class BaseReserve : Form
    {
        private string[] allUsers;

        protected object lockDB;

        private DataGridViewRowCollection allRows;
        // position of row which is processed
        private int pos;

        // type 0 - reserve only one term
        //      1 - reserve multiple
        //      2 - change reservation
        private int type = 0;

        // parent
        protected BaseApplication parent;

        // new reservation
        private Reservation reservation = null;

        internal Reservation Reservation
        {
            get { return reservation; }
            set { reservation = value; }
        }

        public BaseReserve()
        {
            InitializeComponent();
        }

        public BaseReserve(BaseApplication parent)
        {
            InitializeComponent();

            this.parent = parent;
        }

        public void setUserCollectionForAutoComplete(string[] allUsers)
        {
            forTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            forTextBox.AutoCompleteCustomSource.AddRange(allUsers);

            this.allUsers = allUsers;
        }

        // enable - if teacher, they can't reserve for someone else
        public void fillFormComponents(bool enable, string username, string startTime, string endTime, ref object lockDB, int type)
        {
            forTextBox.Enabled = enable;
            forTextBox.Text = username;

            startTimeReserveTextBox.Text = startTime;
            endTimeReserveTextBox.Text = endTime;

            this.lockDB = lockDB;
            this.type = type;
        }

        public void setRowsForProcessing(DataGridViewRowCollection allRows, string username, bool enable)
        {
            this.allRows = allRows;

            forTextBox.Enabled = enable;
            forTextBox.Text = username;

            pos = 0;
        }

        protected void clearForm()
        {
            this.forTextBox.Clear();
            this.coureseTextBox.Clear();
            this.startTimeReserveTextBox.Clear();
            this.endTimeReserveTextBox.Clear();
        }

        private bool checkReservation(List<int> reservationTimes)
        {
            if (reservationTimes != null
                &&
                reservationTimes.Contains(int.Parse(startTimeReserveTextBox.Text))
                &&
                reservationTimes.Contains(int.Parse(endTimeReserveTextBox.Text)))
            {
                int pos = reservationTimes.IndexOf(int.Parse(startTimeReserveTextBox.Text));
                if (pos % 2 == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            clearForm();
            this.Hide();
        }

        private void reserveButton_Click(object sender, EventArgs e)
        {
            if (type == 0)
            {
                if (reservation != null && !forTextBox.Text.Equals(string.Empty) &&
                    !coureseTextBox.Text.Equals(string.Empty) && !startTimeReserveTextBox.Text.Equals(string.Empty) &&
                    !endTimeReserveTextBox.Text.Equals(string.Empty))
                {

                    lock (lockDB)
                    {
                        string sqlCommandText = "SELECT * FROM All_Reservations WHERE" + 
                            " (classroomNumberID = (SELECT classroomID FROM All_Classrooms WHERE number = @number)) AND ev_date = @ev_date";
                        SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                        sql.Parameters.Add(new SqlParameter("@number", reservation.ClassroomNum));
                        sql.Parameters.Add(new SqlParameter("@ev_date", reservation.Date));

                        SqlDataReader rdr = null;

                        List<int> reservationTimes = new List<int>();

                        try
                        {
                            rdr = sql.ExecuteReader();

                            while (rdr.Read())
                            {
                                string[] times = ((string)rdr["ev_time"]).Split('-');
                                reservationTimes.Add(int.Parse(times[0]));
                                reservationTimes.Add(int.Parse(times[1]));
                            }
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            rdr.Close();
                        }

                        bool ok = checkReservation(reservationTimes);

                        if (ok)
                        {
                            // if there is no reservation for this time

                            sqlCommandText =
                                "INSERT All_Reservations(course, ev_time, ev_date, classroomNumberID, userID ) VALUES(" +
                                    "@course, @ev_time, @ev_date , (SELECT classroomID FROM  All_Classrooms where number = @number)," +
                                        "(select userID from All_Users where username = @username))";
                            
                            sql.CommandText = sqlCommandText;
                            sql.Parameters.Add(new SqlParameter("@course", coureseTextBox.Text));
                            sql.Parameters.Add(new SqlParameter("@ev_time", startTimeReserveTextBox.Text + "-" + endTimeReserveTextBox.Text));
                            sql.Parameters.Add(new SqlParameter("@username", forTextBox.Text));

                            sql.ExecuteNonQuery();

                        }
                        else
                        {
                            // if there is reservation for this time
                            MessageBox.Show("Warning", "This term is already occupied!");
                        }
                    }
                    
                    this.clearForm();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You haven't entered all parameters", "Warning");
                }
            }
            else if (type == 1)
            {
                string[] times = allRows[pos].Cells[6].Value.ToString().Split('-');

                lock (lockDB)
                {
                    string sqlCommandText =
                                    "INSERT All_Reservations(course, ev_time, ev_date, classroomNumberID, userID ) VALUES(" +
                                        "@course, @ev_time, @ev_date , (SELECT classroomID FROM  All_Classrooms where number = @number)," +
                                            "(select userID from All_Users where username = @username))";

                    SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                    sql.Parameters.Add(new SqlParameter("@number", allRows[pos].Cells[0].Value.ToString()));
                    sql.Parameters.Add(new SqlParameter("@ev_date", allRows[pos].Cells[7].Value.ToString()));
                    sql.Parameters.Add(new SqlParameter("@course", coureseTextBox.Text));
                    sql.Parameters.Add(new SqlParameter("@ev_time", int.Parse(times[0]) + "-" + int.Parse(times[1])));
                    sql.Parameters.Add(new SqlParameter("@username", forTextBox.Text));

                    sql.ExecuteNonQuery();
                }

                string username = forTextBox.Text;
                bool enable = forTextBox.Enabled;

                this.Hide();

                if(++pos < allRows.Count)
                {
                    times = allRows[pos].Cells[6].Value.ToString().Split('-');

                    startTimeReserveTextBox.Text = times[0];
                    endTimeReserveTextBox.Text = times[1];
                    coureseTextBox.Clear();

                    this.Show();
                }
            }
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        protected void clearErrorProvider()
        {
            reservationErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            reservationErrorProvider.SetError(c, msgError);
        }

        private void forTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (forTextBox.Enabled)
            {
                string username = forTextBox.Text;
                bool ok = false;

                for (int i = 0; i < allUsers.Length; i++)
                {
                    if (username.Equals(allUsers[i]))
                    {
                        ok = true;
                        break;
                    }
                }

                if (!ok)
                {
                    validationSetError(e, forTextBox, "Username you inputed is not in the system!");
                }
                else
                {
                    clearErrorProvider();
                }
            }
        }

        private void coureseTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (coureseTextBox.Text.Trim().Equals(string.Empty))
            {
                reservationErrorProvider.SetError(coureseTextBox, "Course must be inputed!");
                e.Cancel = true;
            }
            else
            {
                reservationErrorProvider.Clear();
            }
        }

        private void startTimeReserveTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error

            try
            {
                int start = int.Parse(startTimeReserveTextBox.Text),
                    end = int.Parse(endTimeReserveTextBox.Text);

                if (start > 24 || start < 0)
                {
                    // if time is not in the range 00-24
                    validationSetError(e, endTimeReserveTextBox, "Working hours are in range 00-24");
                    startTimeReserveTextBox.SelectAll();
                }
                else if (start >= end)
                {
                    // if start time is greater than end time

                    validationSetError(e, endTimeReserveTextBox, "Start time is greater than end time");
                    startTimeReserveTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                validationSetError(e, endTimeReserveTextBox, "Time must be a number");
                startTimeReserveTextBox.SelectAll();
            }
        }

        private void endTimeReserveTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error

            try
            {
                int start = int.Parse(startTimeReserveTextBox.Text),
                    end = int.Parse(endTimeReserveTextBox.Text);

                if (end > 24 || end < 0)
                {
                    // if time is not in the range 00-24
                    validationSetError(e, endTimeReserveTextBox, "Working hours are in range 00-24");
                    endTimeReserveTextBox.SelectAll();
                }
                else if (start >= end)
                {
                    // if start time is greater than end time

                    validationSetError(e, endTimeReserveTextBox, "End time is less than start time");
                    endTimeReserveTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                validationSetError(e, endTimeReserveTextBox, "Time must be a number");
                endTimeReserveTextBox.SelectAll();
            }
        }
    }
}
