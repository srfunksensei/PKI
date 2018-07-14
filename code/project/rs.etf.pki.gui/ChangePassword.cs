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
    public partial class ChangePassword : Form
    {
        // for determing which application to open
        private string userType;

        // 
        private bool changed = false;

        // parent control
        // can only be login form
        private Control parent;

        public ChangePassword(Control parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void clearFormComponents()
        {
            usernameTextBox.Clear();
            oldPassTextBox.Clear();
            newPassTextBox.Clear();
            newPassAgainTextBox.Clear();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            if (changed && (!oldPassTextBox.Text.Equals(string.Empty) ||
                !newPassTextBox.Text.Equals(string.Empty) ||
                !newPassAgainTextBox.Text.Equals(string.Empty)))
            {
                SqlConnection connection = null;

                try
                {
                    // for connection string
                    project.Properties.Settings s = new project.Properties.Settings();

                    // create new connection
                    connection = new System.Data.SqlClient.SqlConnection();

                    // set connection string
                    connection.ConnectionString = s.TESTConnectionString;

                    // open connection
                    connection.Open();

                    string sqlCommand = "UPDATE All_Users SET pass = @usrPass WHERE username = @usrName";

                    // create command
                    SqlCommand sql = new SqlCommand(sqlCommand, connection);
                    sql.Parameters.Add(new SqlParameter("@usrPass", newPassAgainTextBox.Text));
                    sql.Parameters.Add(new SqlParameter("@usrName", usernameTextBox.Text));

                    // and execute it
                    sql.ExecuteNonQuery();

                    if (parent is BaseApplication)
                    {
                        // if parent is type of BaseApp then
                        // clear componetns
                        clearFormComponents();

                        // refresh user info
                        ((BaseApplication)parent).fillUserInfo();

                        // hide this
                        this.Hide();
                    }
                    else if (parent is LoginForm)
                    {
                        // else if parent is type of LoginForm

                        // set isLogin on true
                        sqlCommand = "UPDATE All_Users SET isLogin = 1 WHERE username = @usrName";

                        // create command
                        sql = new SqlCommand(sqlCommand, connection);
                        sql.Parameters.Add(new SqlParameter("@usrName", usernameTextBox.Text));

                        // and execute it
                        sql.ExecuteNonQuery();

                        // open application depending on
                        // user type
                        if ("administrator".Equals(userType.ToLower()))
                        {
                            ((LoginForm)parent).Application = new AdminApplication(usernameTextBox.Text);
                        }
                        else if ("laboratory personnel".Equals(userType.ToLower()))
                        {
                            ((LoginForm)parent).Application = new LabApplication(usernameTextBox.Text);
                        }
                        else if ("teacher".Equals(userType.ToLower()))
                        {
                            ((LoginForm)parent).Application = new TeacherApplication(usernameTextBox.Text);
                        }

                        ((LoginForm)parent).Application.setParent(((LoginForm)parent));
                        ((LoginForm)parent).Application.Show();

                        // and dispose this
                        this.Dispose();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    // close connection
                    if (connection != null) connection.Close();

                    changed = false;

                    clearFormComponents();
                }
            }
            else
            {
                MessageBox.Show("You haven't inputed anything", "Warning");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            clearFormComponents();
            oldPassTextBox.Focus();
            this.Hide();

            changed = false;
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            changePassErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            changePassErrorProvider.SetError(c, msgError);
        }

        private void oldPassTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!oldPassTextBox.Text.Equals(string.Empty))
            {
                // for connection string
                project.Properties.Settings s = new project.Properties.Settings();

                SqlConnection connection = null;
                SqlDataReader rdr = null;

                try
                {
                    // create new connection
                    connection = new System.Data.SqlClient.SqlConnection();

                    // set connection string
                    connection.ConnectionString = s.TESTConnectionString;

                    // open connection
                    connection.Open();

                    // create command
                    string sqlCommand = "SELECT pass FROM All_Users WHERE username = @usrName";

                    SqlCommand sql = new SqlCommand(sqlCommand, connection);
                    sql.Parameters.Add(new SqlParameter("@usrName", usernameTextBox.Text));

                    // and execute it
                    rdr = sql.ExecuteReader();


                    if (rdr.Read())
                    {
                        if (!((string)rdr["pass"]).Equals(oldPassTextBox.Text))
                        {
                            // report error
                            validationSetError(e, oldPassTextBox, "Old password is not the same like this you inputed!");
                            oldPassTextBox.SelectAll();
                        }
                        else
                        {
                            clearErrorProvider();
                        }
                    }

                }
                catch (Exception)
                {
                }
                finally
                {
                    // close reader
                    if (rdr != null) rdr.Close();

                    // close connection
                    if (connection != null) connection.Close();
                }
            }
            else
            {
                clearErrorProvider();
            }

            changed = true;
        }

        private void newPassAgainTextBox_Validating(object sender, CancelEventArgs e)
        {
            string newPass = newPassTextBox.Text,
                newPassAgain = newPassAgainTextBox.Text;

            if (!newPassAgain.Equals(newPass))
            {
                validationSetError(e, newPassAgainTextBox, "New passwords are not the same!");
                newPassAgainTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        // if application is open 
        // then fill only username, because userType is known
        public void fillFormComponents(string username)
        {
            usernameTextBox.Text = username;
        }

        //if application is not open then 
        // we must know witch application to open 
        // depending on user type
        public void fillFormComponents(string userType, string username, bool cancelButtonShow)
        {
            usernameTextBox.Text = username;
            this.userType = userType;
            cancelButton.Enabled = cancelButtonShow;
        }

        
    }
}
