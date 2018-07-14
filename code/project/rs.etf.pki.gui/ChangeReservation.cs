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
    public partial class ChangeReservation : PKI_project.BaseReserve
    {
        public ChangeReservation(BaseApplication parent)
            : base(parent)
        {
            InitializeComponent();
        }

        private void reserveButton_Click(object sender, EventArgs e)
        {
            if (Reservation != null && !forTextBox.Text.Equals(string.Empty) &&
                    !coureseTextBox.Text.Equals(string.Empty) && !startTimeReserveTextBox.Text.Equals(string.Empty) &&
                    !endTimeReserveTextBox.Text.Equals(string.Empty))
            {
                lock (lockDB)
                {
                    string sqlCommandText =
                        "UPDATE All_Reservations SET" +
                        " course = @course, ev_time = @ev_time, ev_date = @ev_date," +
                        " classroomNumberID = (SELECT classroomID FROM  All_Classrooms where number = @number)," +
                        " userID = (select userID from All_Users where username = @username)" +
                        " WHERE ev_time = @old_time AND ev_date = @old_date";

                    SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                    sql.Parameters.Add(new SqlParameter("@course", coureseTextBox.Text));
                    sql.Parameters.Add(new SqlParameter("@ev_time", startTimeReserveTextBox.Text + "-" + endTimeReserveTextBox.Text));
                    sql.Parameters.Add(new SqlParameter("@ev_date", specificDateChangeDateTimePicker.Text));
                    sql.Parameters.Add(new SqlParameter("@number", Reservation.ClassroomNum));
                    sql.Parameters.Add(new SqlParameter("@username", forTextBox.Text));
                    sql.Parameters.Add(new SqlParameter("@old_time", Reservation.StartTime + "-" + Reservation.EndTime));
                    sql.Parameters.Add(new SqlParameter("@old_date", Reservation.Date));

                    sql.ExecuteNonQuery();
                }

                this.clearForm();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You haven't entered all parameters", "Warning");
            }
        }
    }
}

