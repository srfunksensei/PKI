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
    public partial class ChangeUserInfo : Form
    {
        private string password;
        private BaseApplication parent;

        public ChangeUserInfo(BaseApplication parent)
        {
            InitializeComponent();

            this.parent = parent;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            oldPassTextBox.Focus();
            this.clearAllComponents();
            this.Hide();
        }

        public void clearAllComponents()
        {
            this.typeComboBox.SelectedItem = null;
            this.typeComboBox.Text = "choose type...";
            this.usernameTextBox.Clear();
            this.oldPassTextBox.Clear();
            this.newPassTextBox.Clear();
            this.newPassAgainTextBox.Clear();
            this.nameTextBox.Clear();
            this.surnameTextBox.Clear();
            this.titleTextBox.Clear();
            this.officeTextBox.Clear();
            this.telephoneTextBox.Clear();
            this.mailTextBox.Clear();
        }

        public void fillAllComponents(string type, string username, string pass, string name, string surname, string title, 
            string office, string phone, string mail)
        {
            typeComboBox.Text = type;
            typeComboBox.Enabled = ((type.ToLower()).Equals("administrator") ?  true : false);
            usernameTextBox.Text = username;
            nameTextBox.Text = name;
            surnameTextBox.Text = surname;
            titleTextBox.Text = title;
            officeTextBox.Text = office;
            telephoneTextBox.Text = phone;
            mailTextBox.Text = mail;

            password = pass;
        }

        //
        // VALIDATING METHODS
        //

        // clears error provider
        private void clearErrorProvider()
        {
            changeUserInfoErrorProvider.Clear();
        }

        // set error on error provider
        protected void validationSetError(CancelEventArgs e, Control c, String msgError)
        {
            e.Cancel = true;
            changeUserInfoErrorProvider.SetError(c, msgError);
        }

        private void oldPassTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (oldPassTextBox.Modified && !(password).Equals(oldPassTextBox.Text))
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            string sqlCommand = "UPDATE All_Users " +
                "SET userType = @usrType" +
                (oldPassTextBox.Modified ? ", pass = @newPass" : string.Empty) +
                (nameTextBox.Modified ? ", name = @name" : string.Empty) +
                (surnameTextBox.Modified ? ", surname = @surname" : string.Empty) +
                (titleTextBox.Modified ? ", title = @title" : string.Empty) +
                (officeTextBox.Modified ? ", office = @office" : string.Empty) +
                (telephoneTextBox.Modified ? ", phone = @phone" : string.Empty) +
                (mailTextBox.Modified ? ", mail = @mail" : string.Empty) +
                " WHERE username = @usrName";

            // create command
            SqlCommand sql = new SqlCommand(sqlCommand, parent.Connection);
            sql.Parameters.Add(new SqlParameter("@usrName", usernameTextBox.Text));
            if(oldPassTextBox.Modified) sql.Parameters.Add(new SqlParameter("@newPass", newPassTextBox.Text));
            sql.Parameters.Add(new SqlParameter("@usrType", typeComboBox.Text));
            if(nameTextBox.Modified) sql.Parameters.Add(new SqlParameter("@name", nameTextBox.Text));
            if(surnameTextBox.Modified) sql.Parameters.Add(new SqlParameter("@surname", surnameTextBox.Text));
            if(titleTextBox.Modified) sql.Parameters.Add(new SqlParameter("@title", titleTextBox.Text));
            if(officeTextBox.Modified) sql.Parameters.Add(new SqlParameter("@office", officeTextBox.Text));
            if(telephoneTextBox.Modified) sql.Parameters.Add(new SqlParameter("@phone", telephoneTextBox.Text));
            if(mailTextBox.Modified) sql.Parameters.Add(new SqlParameter("@mail", mailTextBox.Text));

            sql.ExecuteNonQuery();

            // if user info tab selected
            if (parent.baseTabControl.SelectedIndex == 0) parent.fillUserInfo();
            if (parent is AdminApplication)
            {
                ((AdminApplication)parent).refreshAllUsersTabPage();

                if (parent.Username.Equals(usernameTextBox.Text)) parent.fillUserInfo();
            }

            this.clearAllComponents();
            this.Hide();
            
        }
    }
}
