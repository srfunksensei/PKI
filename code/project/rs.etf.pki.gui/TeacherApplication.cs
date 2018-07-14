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
    public partial class TeacherApplication : PKI_project.BaseApplication
    {
        public TeacherApplication(string username) : base(username)
        {
            InitializeComponent();

            this.setNewClassroomsForAutocomplete();
            this.setNewUsersForAutoComplete();

            this.unreserveOccupiedButton.Click -= new System.EventHandler(this.unreserveOccupiedButton_Click);
            this.unreserveAllOccupiedButton.Click -= new System.EventHandler(this.unreserveAllOccupiedButton_Click);
        }

        
        // teacher can delete only reservation which he had made
        // see if this is his reservation
        private bool canDeleteReservation(DataGridViewRow dataGridViewRow)
        {
            bool ok = false;

            try
            {
                string sqlCommandTex =
                            "SELECT * FROM All_Reservations" +
                            " WHERE ev_time = @ev_time AND ev_date = @ev_date AND" +
                            " userID = (SELECT userID FROM All_Users WHERE username = @username)";

                SqlCommand sql = new SqlCommand(sqlCommandTex, Connection);
                sql.Parameters.Add(new SqlParameter("@ev_time", dataGridViewRow.Cells[6].Value.ToString()));
                sql.Parameters.Add(new SqlParameter("@ev_date", dataGridViewRow.Cells[7].Value.ToString()));
                sql.Parameters.Add(new SqlParameter("@username", Username));

                rdr = sql.ExecuteReader();

                if (!rdr.Read())
                {
                    ok = false;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                rdr.Close();
            }
            
            return ok;
        }

        private void unreserveOccupiedButton_Click_1(object sender, EventArgs e)
        {
            if (canDeleteReservation(searchResultsOccupiedDataGridView.SelectedRows[0]))
            {
                unreserveOccupiedButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("You can unreserve only reservation you have made", "Error");
            }
        }

        private void unreserveAllOccupiedButton_Click_1(object sender, EventArgs e)
        {
            bool ok = true;

            for (int i = 0; i < searchResultsOccupiedDataGridView.Rows.Count; i++)
            {
                if (!canDeleteReservation(searchResultsOccupiedDataGridView.Rows[i]))
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
                MessageBox.Show("You can unreserve only reservation you have made", "Error");
            }
        }

       
    }
}
