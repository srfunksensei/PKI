using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace PKI_project
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            registerErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            registerErrorProvider.SetError(c, msgError);
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            String name = nameTextBox.Text;

            if (name.Contains("0") || name.Contains("1") || name.Contains("2") || name.Contains("3") ||
                name.Contains("4") || name.Contains("5") || name.Contains("6") || name.Contains("7") ||
                name.Contains("8") || name.Contains("9"))
            {
                // if name contains number/s
                validationSetError(e, nameTextBox, "Name must NOT contain digits!");
                nameTextBox.SelectAll();
            } else {
                clearErrorProvider();
            }
        }

        private void surnameTextBox_Validating(object sender, CancelEventArgs e)
        {
            // report error
            String surname = surnameTextBox.Text;

            if (surname.Contains("0") || surname.Contains("1") || surname.Contains("2") || surname.Contains("3") ||
                surname.Contains("4") || surname.Contains("5") || surname.Contains("6") || surname.Contains("7") ||
                surname.Contains("8") || surname.Contains("9"))
            {
                // if name contains number/s
                validationSetError(e, surnameTextBox, "Surname must NOT contain digits!");
                surnameTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void mailTextBox_Validating(object sender, CancelEventArgs e)
        {

            if (mailTextBox.Text.Length != 0 && !validateEmailAddress())
            {
                validationSetError(e, mailTextBox, "Not recognizable e-mail address! Enter valid e-mail address. Exaple: someone@example.com");
                mailTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }

        }

        private void typeComboBox_Validating(object sender, CancelEventArgs e)
        {

            if (!validateUserType() && !typeComboBox.Text.Contains("choose type..."))
            {

                validationSetError(e, typeComboBox, "User type you have enterd is not in the list!");
                typeComboBox.SelectAll();

            } else {
                clearErrorProvider();
            }

        }

        private void telephoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            // regular expression
            string[] patterns = {@"^\d+$", @"^\d{3}-\d{4}$", @"^\d{4}-\d{3}$", @"^\d{3}-\d{2}-{2}$"};
            string phone = telephoneTextBox.Text;
            bool ok = false;

            foreach (string pattern in patterns)
            {
                if (Regex.IsMatch(phone, pattern)) {
                    // if phone matches one pattern from pattern list
                    // of regular expression than break
                    ok = true;
                    break;
                }
            }

            if (phone.Length != 0 && !ok)
            {
                validationSetError(e, telephoneTextBox, "Telephone doesn't match regular expression");
                telephoneTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }

        }


        private void registerButton_Click(object sender, EventArgs e)
        {
            if (!validateUserType() || !validateEmailAddress())
            {
                // if both required filds are empty
                // or both are inputed wrong
                MessageBox.Show("You haven't entered all required fileds!", "Error");

                if (!validateUserType()) typeComboBox.Focus();
                else mailTextBox.Focus();
            }
            else
            {                
                // for connection string
                project.Properties.Settings s = new project.Properties.Settings();

                SqlConnection connection = null;
                SqlDataReader rdr = null;

                try
                {
                    string sqlCommand = "INSERT INTO New_Users(userType, name, surname, title, office, phone, mail)" +
                        " VALUES (@userType, @name, @surname, @title, @office, @phone, @mail)";

                    string[] atValues = { "@userType", "@name", "@surname", "@title", "@office", "@phone", "@mail" };
                    string[] values = {typeComboBox.Text, nameTextBox.Text, surnameTextBox.Text,
                        titleTextBox.Text, officeTextBox.Text, telephoneTextBox.Text, mailTextBox.Text};

                    // create new connection
                    connection = new System.Data.SqlClient.SqlConnection();

                    // set connection string
                    connection.ConnectionString = s.TESTConnectionString;

                    // open connection
                    connection.Open();

                    // create command
                    SqlCommand insertCommand = new SqlCommand(sqlCommand, connection);
                    SqlParameter param = null;

                    for (int i = 0; i < atValues.Length; i++)
                    {
                        param = new SqlParameter(atValues[i], values[i]);
                        insertCommand.Parameters.Add(param);
                    }

                    // send data to admin
                    int row = insertCommand.ExecuteNonQuery();
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

                // clear form
                typeComboBox.Text = "choose type...";
                nameTextBox.Clear();
                surnameTextBox.Clear();
                titleTextBox.Clear();
                officeTextBox.Clear();
                telephoneTextBox.Clear();
                mailTextBox.Clear();

                this.Hide();
            }
        }

        // validates e-mail address
        // returns if validation is succesfull
        private bool validateEmailAddress()
        {
            // regular expression for email address
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                        + @"[a-zA-Z]{2,}))$";

            string mail = mailTextBox.Text;

            if (!Regex.IsMatch(mail, patternStrict))
            {
                return false;
            }

            return true;
        }

        // validates user type
        // returns if validation is succesfull
        private bool validateUserType()
        {
            string userType = typeComboBox.Text;

            if (userType.Length == 0 ||
                userType.Contains("choose type...") ||
                !(userType.ToLower().Equals("administrator") ||
                userType.ToLower().Equals("laboratory personnel") ||
                userType.ToLower().Equals("teacher")))
            {
                return false;
            }

            return true;
        }
    }
}
