using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PKI_project
{
    public partial class ChangeClassroomInfo : Form
    {
        private BaseApplication parent;

        // if classroom number changed
        private bool isTextChanged = false;

        // old classroom number
        // if value has changed we must know which
        // row in table to update
        private string oldClassroomNum;

        public ChangeClassroomInfo(BaseApplication parent)
        {
            this.parent = parent;

            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void enableAndShowAllClassroomParameters(string number, bool blackboard, bool computers, bool projector,
	            bool laboratory, string capacity, string workingHours, string comment, string notice)
        {
            this.changeCNumTextBox.Enabled = true;
            this.changeBlackboardCheckBox.Enabled = true;
            this.changeComputersCheckBox.Enabled = true;
            this.changeProjectorCheckBox.Enabled = true;
            this.changeLaboratoryCheckBox.Enabled = true;
            this.changeCapacityTextBox.Enabled = true;
            this.changeStartWorkingTextBox.Enabled = true;
            this.changeEndWorkingTextBox.Enabled = true;
            this.changeCommentTextBox.Enabled = true;
            this.changeNoticeTextBox.Enabled = true;

            string[] time = workingHours.Split('-');

            this.changeCNumTextBox.Text = number;
            this.changeBlackboardCheckBox.Checked = blackboard;
            this.changeComputersCheckBox.Checked = computers;
            this.changeProjectorCheckBox.Checked = projector;
            this.changeLaboratoryCheckBox.Checked = laboratory;
            this.changeCapacityTextBox.Text = capacity;
            this.changeStartWorkingTextBox.Text = time[0];
            this.changeEndWorkingTextBox.Text = time[1];
            this.changeCommentTextBox.Text = comment;
            this.changeNoticeTextBox.Text = notice;

            oldClassroomNum = number;

            this.Show();
        }

        public void enableAndShowOnlyComment(string number, bool blackboard, bool computers, bool projector,
                bool laboratory, string capacity, string workingHours, string comment, string notice)
        {
            this.changeCNumTextBox.Enabled = false;
            this.changeBlackboardCheckBox.Enabled = false;
            this.changeComputersCheckBox.Enabled = false;
            this.changeProjectorCheckBox.Enabled = false;
            this.changeLaboratoryCheckBox.Enabled = false;
            this.changeCapacityTextBox.Enabled = false;
            this.changeStartWorkingTextBox.Enabled = false;
            this.changeEndWorkingTextBox.Enabled = false;
            this.changeCommentTextBox.Enabled = true;
            this.changeNoticeTextBox.Enabled = false;

            string[] time = workingHours.Split('-');

            this.changeCNumTextBox.Text = number;
            this.changeBlackboardCheckBox.Checked = blackboard;
            this.changeComputersCheckBox.Checked = computers;
            this.changeProjectorCheckBox.Checked = projector;
            this.changeLaboratoryCheckBox.Checked = laboratory;
            this.changeCapacityTextBox.Text = capacity;
            this.changeStartWorkingTextBox.Text = time[0];
            this.changeEndWorkingTextBox.Text = time[1];
            this.changeCommentTextBox.Text = comment;
            this.changeNoticeTextBox.Text = notice;

            oldClassroomNum = number;

            this.Show();
        }

        public void enableAndShowOnlyNotice(string number, bool blackboard, bool computers, bool projector,
                bool laboratory, string capacity, string workingHours, string comment, string notice)
        {
            this.changeCNumTextBox.Enabled = false;
            this.changeBlackboardCheckBox.Enabled = false;
            this.changeComputersCheckBox.Enabled = false;
            this.changeProjectorCheckBox.Enabled = false;
            this.changeLaboratoryCheckBox.Enabled = false;
            this.changeCapacityTextBox.Enabled = false;
            this.changeStartWorkingTextBox.Enabled = false;
            this.changeEndWorkingTextBox.Enabled = false;
            this.changeCommentTextBox.Enabled = false;
            this.changeNoticeTextBox.Enabled = true;

            string[] time = workingHours.Split('-');

            this.changeCNumTextBox.Text = number;
            this.changeBlackboardCheckBox.Checked = blackboard;
            this.changeComputersCheckBox.Checked = computers;
            this.changeProjectorCheckBox.Checked = projector;
            this.changeLaboratoryCheckBox.Checked = laboratory;
            this.changeCapacityTextBox.Text = capacity;
            this.changeStartWorkingTextBox.Text = time[0];
            this.changeEndWorkingTextBox.Text = time[1];
            this.changeCommentTextBox.Text = comment;
            this.changeNoticeTextBox.Text = notice;

            oldClassroomNum = number;

            this.Show();
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            changeClassroomInforErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            changeClassroomInforErrorProvider.SetError(c, msgError);
        }

        private void changeCNumTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (isTextChanged)
            {
                // report error
                String num = changeCNumTextBox.Text; // classroom number can be 25a, 12c, 1, 02...

                SqlDataReader rdr = null;

                try
                {
                    // command
                    string sqlCommandText = "SELECT isVisible FROM All_Classrooms WHERE number = @number";

                    // make sql command
                    SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                    sql.Parameters.Add(new SqlParameter("@number", num));

                    rdr = sql.ExecuteReader();

                    if (rdr.Read())
                    {
                        // if there is no classroom with that number

                        validationSetError(e, changeCNumTextBox, "That classroom is already existanting");
                        changeCNumTextBox.SelectAll();
                    }
                    else
                    {
                        clearErrorProvider();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    rdr.Close();
                }

                isTextChanged = false;
            }
        }

        private void changeCapacityTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            try
            {
                // if capacity is 0 or lower than null 

                int cap = int.Parse(changeCapacityTextBox.Text);

                if (cap <= 0)
                {
                    validationSetError(e, changeCapacityTextBox, "Capacity must be greater than 0");
                    changeCapacityTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                // or non digit is presed

                validationSetError(e, changeCapacityTextBox, "Capacity must be a number!");
                changeCapacityTextBox.SelectAll();
            }
        }

        private void changeStartWorkingTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error

            try
            {
                int start = int.Parse(changeStartWorkingTextBox.Text),
                    end = int.Parse(changeEndWorkingTextBox.Text);

                if (start > 24 || start < 0)
                {
                    // if time is not in the range 00-24
                    validationSetError(e, changeEndWorkingTextBox, "Working hours are in range 00-24");
                    changeStartWorkingTextBox.SelectAll();
                }
                else if (start >= end)
                {
                    // if start time is greater than end time

                    validationSetError(e, changeEndWorkingTextBox, "Start time is greater than end time");
                    changeStartWorkingTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                validationSetError(e, changeEndWorkingTextBox, "Time must be a number");
                changeStartWorkingTextBox.SelectAll();
            }
        }

        private void changeEndWorkingTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            try
            {
                int start = int.Parse(changeStartWorkingTextBox.Text),
                    end = int.Parse(changeEndWorkingTextBox.Text);

                if (end > 24 || end < 0)
                {
                    // if time is not in the range 00-24
                    validationSetError(e, changeEndWorkingTextBox, "Working hours are in range 00-24");
                    changeEndWorkingTextBox.SelectAll();
                }
                else if (start >= end)
                {
                    // if start time is greater than end time

                    validationSetError(e, changeEndWorkingTextBox, "End time is less than start time");
                    changeEndWorkingTextBox.SelectAll();
                }
                else
                {
                    clearErrorProvider();
                }
            }
            catch (Exception)
            {
                validationSetError(e, changeEndWorkingTextBox, "Time must be a number");
                changeEndWorkingTextBox.SelectAll();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string sqlCommandText = "SELECT workingHours FROM All_Classrooms WHERE number = @oldNumber";

            SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
            sql.Parameters.Add(new SqlParameter("@oldNumber", oldClassroomNum));

            SqlDataReader rdr = sql.ExecuteReader();

            try
            {
                if (rdr.Read())
                {
                    string[] workingHoursString = ((string)rdr["workingHours"]).Split('-');

                    if (int.Parse(changeStartWorkingTextBox.Text) > int.Parse(workingHoursString[0]) ||
                        int.Parse(changeEndWorkingTextBox.Text) < int.Parse(workingHoursString[1]))
                    {
                        // working hours changed
                        rdr.Close();

                        sqlCommandText = "SELECT * FROM All_Reservations WHERE" +
                            " classroomNumberID = (SELECT classroomID FROM All_Classrooms WHERE number = @oldNumber)";

                        sql.CommandText = sqlCommandText;

                        rdr = sql.ExecuteReader();

                        List<int> resIDsForDelete = new List<int>(),
                            resIDsForUpdateStart = new List<int>(),
                            resIDsForUpdateEnd = new List<int>();
                        List<string> resTimeUpdateStart = new List<string>(),
                            resTimeUpdateEnd = new List<string>();

                        while (rdr.Read())
                        {
                            string[] dateString = ((string)rdr["ev_date"]).Split('.');

                            DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]), int.Parse(dateString[0])),
                                nowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            if (date1.CompareTo(nowDateTime) > -1)
                            {
                                // if date of reservation is grater than today
                                // delete or change ev time
                                string[] res_time = ((string)rdr["ev_time"]).Split('-');

                                if (int.Parse(changeStartWorkingTextBox.Text) >= int.Parse(res_time[1]) ||
                                    int.Parse(changeEndWorkingTextBox.Text) <= int.Parse(res_time[0]))
                                {
                                    // if reservation time is out of classroom working time
                                    resIDsForDelete.Add((int)rdr["reservationID"]);
                                }
                                else if (int.Parse(changeStartWorkingTextBox.Text) > int.Parse(res_time[0]))
                                {
                                    // if reservation start time is less than new start working hour
                                    resIDsForUpdateStart.Add((int)rdr["reservationID"]);
                                    resTimeUpdateStart.Add((string)rdr["ev_time"]);
                                } 
                                else if (int.Parse(changeEndWorkingTextBox.Text) < int.Parse(res_time[1]))
                                {
                                    // if reservation start time is grater than new end working hour
                                    resIDsForUpdateEnd.Add((int)rdr["reservationID"]);
                                    resTimeUpdateEnd.Add((string)rdr["ev_time"]);
                                }
                            }
                        }

                        rdr.Close();

                        if (resIDsForDelete.Count > 0)
                        {
                            sqlCommandText = "DELETE FROM All_Reservations WHERE reservationID = @reservationID";
                            sql.CommandText = sqlCommandText;
                            sql.Parameters.Clear();

                            for (int i = 0; i < resIDsForDelete.Count; i++)
                            {
                                sql.Parameters.Add(new SqlParameter("@reservationID", resIDsForDelete[i]));
                                sql.ExecuteNonQuery();

                                sql.Parameters.Clear();
                            }
                        }

                        if (resIDsForUpdateEnd.Count > 0)
                        {
                            sqlCommandText = "UPDATE All_Reservations SET ev_time = @ev_time WHERE reservationID = @reservationID";
                            sql.CommandText = sqlCommandText;
                            sql.Parameters.Clear();

                            for (int i = 0; i < resIDsForUpdateEnd.Count; i++)
                            {
                                string[] time = resTimeUpdateEnd[i].Split('-');

                                sql.Parameters.Add(new SqlParameter("@ev_time", time[0] + '-' + changeEndWorkingTextBox.Text));
                                sql.Parameters.Add(new SqlParameter("@reservationID", resIDsForUpdateEnd[i]));
                                sql.ExecuteNonQuery();

                                sql.Parameters.Clear();
                            }
                        }

                        if (resIDsForUpdateStart.Count > 0)
                        {
                            sqlCommandText = "UPDATE All_Reservations SET ev_time = @ev_time WHERE reservationID = @reservationID";
                            sql.CommandText = sqlCommandText;
                            sql.Parameters.Clear();

                            for (int i = 0; i < resIDsForUpdateStart.Count; i++)
                            {
                                string[] time = resTimeUpdateStart[i].Split('-');

                                sql.Parameters.Add(new SqlParameter("@ev_time", changeStartWorkingTextBox.Text + '-' + time[1]));
                                sql.Parameters.Add(new SqlParameter("@reservationID", resIDsForUpdateStart[i]));
                                sql.ExecuteNonQuery();

                                sql.Parameters.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if(!rdr.IsClosed) rdr.Close();
            }

            // command
            sqlCommandText = "UPDATE All_Classrooms SET number = @number, blackboard = @blackboard, computers = @computers, " +
                "projector = @projector, laboratory = @laboratory, capacity = @capacity, workingHours = @workingHours, comment = @comment, notice = @notice" + 
                " WHERE number = @oldNumber";

            // make sql command
            sql.CommandText = sqlCommandText;
            sql.Parameters.Clear();
            sql.Parameters.Add(new SqlParameter("@number", changeCNumTextBox.Text));
            sql.Parameters.Add(new SqlParameter("@blackboard", changeBlackboardCheckBox.Checked));
            sql.Parameters.Add(new SqlParameter("@computers", changeComputersCheckBox.Checked));
            sql.Parameters.Add(new SqlParameter("@projector", changeProjectorCheckBox.Checked));
            sql.Parameters.Add(new SqlParameter("@laboratory", changeLaboratoryCheckBox.Checked));
            sql.Parameters.Add(new SqlParameter("@capacity", int.Parse(changeCapacityTextBox.Text)));
            sql.Parameters.Add(new SqlParameter("@workingHours", changeStartWorkingTextBox.Text + "-" + changeEndWorkingTextBox.Text));
            sql.Parameters.Add(new SqlParameter("@comment", changeCommentTextBox.Text));
            sql.Parameters.Add(new SqlParameter("@notice", changeNoticeTextBox.Text));
            sql.Parameters.Add(new SqlParameter("@oldNumber", oldClassroomNum));

            // execute command
            sql.ExecuteNonQuery();

            parent.setNewClassroomsForAutocomplete();
            this.Hide();
        }

        

        private void changeCNumTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) isTextChanged = false;
            if (e.KeyCode != Keys.Tab && isTextChanged == false ) isTextChanged = true;
        }
    }
}
