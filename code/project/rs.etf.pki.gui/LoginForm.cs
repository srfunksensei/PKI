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
    public partial class LoginForm : Form
    {
        // for registering new users
        private RegisterForm register;

        // application which will be opened 
        // if login is succesfull
        private BaseApplication application;

        public BaseApplication Application
        {
            get { return application; }
            set { application = value; }
        }

        public LoginForm()
        {
            InitializeComponent();

            register = null;
            application = null;
        }

        private void clearFormComponents()
        {
            userNameTextBox.Clear();
            passTextBox.Clear();
        }

        public void setFocusOnUsername()
        {
            userNameTextBox.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (register == null)
            {
                register = new RegisterForm();
            }

            register.Show();

            clearFormComponents();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginOnSystem();            
            this.Hide();
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            loginErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            loginErrorProvider.SetError(c, msgError);
        }

        private void userNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (userNameTextBox.Text.Equals(string.Empty))
            {
                validationSetError(e, userNameTextBox, "Username can not be left blank!");
                userNameTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void loginOnSystem()
        {
            // for connection string
            project.Properties.Settings s = new project.Properties.Settings();
            

            string userName = userNameTextBox.Text,
                pass = passTextBox.Text,
                sqlCommand = "SELECT * FROM All_Users " +
                    "WHERE username = @user_id AND pass = @pass_id";

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
                SqlCommand sql = new SqlCommand(sqlCommand, connection);
                sql.Parameters.Add(new SqlParameter("@user_id", userNameTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@pass_id", passTextBox.Text));

                // and execute it
                rdr = sql.ExecuteReader();

                if (rdr.Read())
                {
                    if ((bool)rdr["isLogin"] == false)
                    {
                        // if user loged in for the first time
                        // show ChangePassForm

                        ChangePassword changePass = new ChangePassword(this);
                        changePass.fillFormComponents((string)rdr["userType"], (string)rdr["username"], false);
                        changePass.Show();
                    }
                    else
                    {
                        // else depending on userType open form

                        if ("administrator".Equals(((string)rdr["userType"]).ToLower()))
                        {
                            application = new AdminApplication((string)rdr["username"]);
                        }
                        else if ("laboratory personnel".Equals(((string)rdr["userType"]).ToLower()))
                        {
                            application = new LabApplication((string)rdr["username"]);
                        }
                        else if ("teacher".Equals(((string)rdr["userType"]).ToLower()))
                        {
                            application = new TeacherApplication((string)rdr["username"]);
                        }

                        application.setParent(this);
                        application.Show();
                    }
                } else
                {
                    clearFormComponents();

                    MessageBox.Show("There is no user with that username and password!", "Warning");

                    setFocusOnUsername();
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
    }
}
