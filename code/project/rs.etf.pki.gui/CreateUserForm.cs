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
    public partial class CreateUserForm : Form
    {
        BaseApplication parent;

        public CreateUserForm(BaseApplication parent)
        {
            this.parent = parent;

            InitializeComponent();
        }

        private void clearForm()
        {
            this.typeComboBox.Text = "choose type...";
            this.usernameTextBox.Clear();
            this.passTextBox.Clear();
            this.nameTextBox.Clear();
            this.surnameTextBox.Clear();
            this.titleTextBox.Clear();
            this.officeTextBox.Clear();
            this.telephoneTextBox.Clear();
            this.mailTextBox.Clear();
        }

        public void fillForm(string name, string surname, string title, string office, string telephone,
            string mail, string userType)
        {
            this.typeComboBox.Text = userType;
            this.nameTextBox.Text = name;
            this.surnameTextBox.Text = surname;
            this.titleTextBox.Text = title;
            this.officeTextBox.Text = office;
            this.telephoneTextBox.Text = telephone;
            this.mailTextBox.Text = mail;
        }

        //
        // CLICK METHODS
        //

        private void cancelButton_Click(object sender, EventArgs e)
        {
            clearForm();
            this.Hide();
        }

        private void addClassroomButton_Click(object sender, EventArgs e)
        {
            if (((AdminApplication)parent).AddClassroomsToPersonnel == null ||
                ((AdminApplication)parent).AddClassroomsToPersonnel.IsDisposed)
            {
                ((AdminApplication)parent).AddClassroomsToPersonnel = new AddClassrooms();
            }
            ((AdminApplication)parent).AddClassroomsToPersonnel.Show();

            string username = usernameTextBox.Text;

            // fill all classrooms
            ((AdminApplication)parent).AddClassroomsToPersonnel.setAllItemsinAllListBox(parent.ClassroomNums);
            // set caller 
            ((AdminApplication)parent).AddClassroomsToPersonnel.setCaller((AdminApplication)parent);
            // set Cancel button enabled to false and set username
            ((AdminApplication)parent).AddClassroomsToPersonnel.setCancelButtonEnabled(true, username, -1);
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!usernameTextBox.Text.Equals(string.Empty) &&
                (typeComboBox.Text.Length != 0 &&
                !typeComboBox.Text.Contains("choose type...") &&
                (typeComboBox.Text.ToLower().Equals("administrator") ||
                typeComboBox.Text.ToLower().Equals("laboratory personnel") ||
                typeComboBox.Text.ToLower().Equals("teacher"))))
            {
                // sql query
                string sqlCommandText =
                    "INSERT All_Users (username, pass, userType, name, surname, title, office, phone, mail, isLogin, isBlocked)" +
                    "VALUES (@username, @pass, @userType, @name, @surname, @title, @office, @phone, @mail, 0, 0)";

                // make command
                SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                sql.Parameters.Add(new SqlParameter("@username", usernameTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@pass", passTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@userType", typeComboBox.Text));
                sql.Parameters.Add(new SqlParameter("@name", nameTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@surname", surnameTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@title", titleTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@office", officeTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@phone", telephoneTextBox.Text));
                sql.Parameters.Add(new SqlParameter("@mail", mailTextBox.Text));

                // execute command
                sql.ExecuteNonQuery();

                // delete user from new users
                sqlCommandText = "DELETE FROM New_Users WHERE userType = @userType AND mail = @mail";
                sql.Parameters.Clear();
                sql.CommandText = sqlCommandText;
                sql.Parameters.Add(new SqlParameter("@userType", typeComboBox.Text));
                sql.Parameters.Add(new SqlParameter("@mail", mailTextBox.Text));

                sql.ExecuteNonQuery();

                ((AdminApplication)parent).setNewUsersForAutoComplete();
                if (typeComboBox.Text.ToLower().Equals("laboratory personnel")) ((AdminApplication)parent).getLaboratoryPersonnel();

                ((AdminApplication)parent).refreshAllUsersTabPage();

                ((AdminApplication)parent).deleteRowFromRegistrationDataGridView();
                ((AdminApplication)parent).refreshCreateNewUserTab();

                //hide form
                this.Hide();
            }
            else
            {
                MessageBox.Show("Required fields are: \nUser type and Username", "Warning");
            }
        }


        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            createUserErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            createUserErrorProvider.SetError(c, msgError);
        }

        private void typeComboBox_Validating(object sender, CancelEventArgs e)
        {
            string userType = typeComboBox.Text;

            if (userType.Length != 0 &&
                !userType.Contains("choose type...") &&
                !(userType.ToLower().Equals("administrator") ||
                userType.ToLower().Equals("laboratory personnel") ||
                userType.ToLower().Equals("teacher")))
            {

                validationSetError(e, typeComboBox, "User type you have enterd is not in the list!");
                typeComboBox.SelectAll();

            }
            else
            {
                clearErrorProvider();
            }
        }

        private void usernameTextBox_Validating(object sender, CancelEventArgs e)
        {
            // find if there is another user with that user name
            SqlDataReader rdr = null;

            try
            {
                // sql query
                string sqlCommandText = "SELECT isLogin FROM All_Users WHERE username = @username";

                // make command
                SqlCommand sql = new SqlCommand(sqlCommandText, parent.Connection);
                sql.Parameters.Add(new SqlParameter("@username", usernameTextBox.Text));

                // execute command
                rdr = sql.ExecuteReader();

                if (rdr.Read())
                {
                    validationSetError(e, usernameTextBox, "User with this username already exist!");
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
            }
            else
            {
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

        private void telephoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            // regular expression
            string[] patterns = { @"^\d+$", @"^\d{3}-\d{4}$", @"^\d{4}-\d{3}$", @"^\d{3}-\d{2}-{2}$" };
            string phone = telephoneTextBox.Text;
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
                validationSetError(e, telephoneTextBox, "Telephone doesn't match regular expression");
                telephoneTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void mailTextBox_Validating(object sender, CancelEventArgs e)
        {
            // regular expression for email address
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                        + @"[a-zA-Z]{2,}))$";

            string mail = mailTextBox.Text;

            if (mail.Length != 0 && !Regex.IsMatch(mail, patternStrict))
            {
                validationSetError(e, mailTextBox, "Not recognizable e-mail address!");
                mailTextBox.SelectAll();
            }
            else
            {
                clearErrorProvider();
            }
        }

        private void typeComboBox_TextChanged(object sender, EventArgs e)
        {
            string userType = typeComboBox.Text;

            if (userType.ToLower().Equals("laboratory personnel"))
            {
                addClassroomButton.Enabled = true;
            }
            else
            {
                addClassroomButton.Enabled = false;
            }
        }

        
    }
}
