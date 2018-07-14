namespace PKI_project
{
    partial class CreateUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cancelButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.mailTextBox = new System.Windows.Forms.TextBox();
            this.telephoneTextBox = new System.Windows.Forms.TextBox();
            this.officeTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.surnameTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.addClassroomButton = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.createUserErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.createUserErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(195, 458);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.helpToolTip.SetToolTip(this.cancelButton, "Click here to cancel user creation");
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createButton.Location = new System.Drawing.Point(98, 458);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 10;
            this.createButton.Text = "Create";
            this.helpToolTip.SetToolTip(this.createButton, "Click here to create the user");
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // passTextBox
            // 
            this.passTextBox.Location = new System.Drawing.Point(162, 100);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.Size = new System.Drawing.Size(121, 20);
            this.passTextBox.TabIndex = 2;
            this.helpToolTip.SetToolTip(this.passTextBox, "Input password for user you\r\nwant to create");
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(162, 72);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(121, 20);
            this.usernameTextBox.TabIndex = 1;
            this.helpToolTip.SetToolTip(this.usernameTextBox, "Input username for user you\r\nwant to create");
            this.usernameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.usernameTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 125;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 124;
            this.label1.Text = "Username";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(12, 126);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(58, 17);
            this.label28.TabIndex = 123;
            this.label28.Text = "Details";
            // 
            // label38
            // 
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label38.Location = new System.Drawing.Point(2, 143);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(355, 2);
            this.label38.TabIndex = 122;
            // 
            // mailTextBox
            // 
            this.mailTextBox.Location = new System.Drawing.Point(162, 314);
            this.mailTextBox.Name = "mailTextBox";
            this.mailTextBox.Size = new System.Drawing.Size(121, 20);
            this.mailTextBox.TabIndex = 8;
            this.helpToolTip.SetToolTip(this.mailTextBox, "Input mail for user you\r\nwant to create");
            this.mailTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.mailTextBox_Validating);
            // 
            // telephoneTextBox
            // 
            this.telephoneTextBox.Location = new System.Drawing.Point(162, 284);
            this.telephoneTextBox.Name = "telephoneTextBox";
            this.telephoneTextBox.Size = new System.Drawing.Size(121, 20);
            this.telephoneTextBox.TabIndex = 7;
            this.helpToolTip.SetToolTip(this.telephoneTextBox, "Input telephone for user you\r\nwant to create");
            this.telephoneTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.telephoneTextBox_Validating);
            // 
            // officeTextBox
            // 
            this.officeTextBox.Location = new System.Drawing.Point(162, 254);
            this.officeTextBox.Name = "officeTextBox";
            this.officeTextBox.Size = new System.Drawing.Size(121, 20);
            this.officeTextBox.TabIndex = 6;
            this.helpToolTip.SetToolTip(this.officeTextBox, "Input office for user you\r\nwant to create");
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(162, 224);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(121, 20);
            this.titleTextBox.TabIndex = 5;
            this.helpToolTip.SetToolTip(this.titleTextBox, "Input title for user you\r\nwant to create");
            // 
            // surnameTextBox
            // 
            this.surnameTextBox.Location = new System.Drawing.Point(162, 194);
            this.surnameTextBox.Name = "surnameTextBox";
            this.surnameTextBox.Size = new System.Drawing.Size(121, 20);
            this.surnameTextBox.TabIndex = 4;
            this.helpToolTip.SetToolTip(this.surnameTextBox, "Input surname for user you\r\nwant to create");
            this.surnameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.surnameTextBox_Validating);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(162, 164);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(121, 20);
            this.nameTextBox.TabIndex = 3;
            this.helpToolTip.SetToolTip(this.nameTextBox, "Input name for user you\r\nwant to create");
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(105, 315);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(35, 15);
            this.label29.TabIndex = 121;
            this.label29.Text = "mail";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(65, 285);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(75, 15);
            this.label30.TabIndex = 120;
            this.label30.Text = "Telephone";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(96, 255);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(44, 15);
            this.label31.TabIndex = 119;
            this.label31.Text = "Office";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(105, 225);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 15);
            this.label32.TabIndex = 118;
            this.label32.Text = "Title";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(75, 195);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 15);
            this.label33.TabIndex = 117;
            this.label33.Text = "Surname";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(95, 165);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(45, 15);
            this.label34.TabIndex = 116;
            this.label34.Text = "Name";
            // 
            // typeComboBox
            // 
            this.typeComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Administrator",
            "Laboratory personnel",
            "Teacher"});
            this.typeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.typeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Administrator",
            "Laboratory personnel",
            "Teacher"});
            this.typeComboBox.Location = new System.Drawing.Point(162, 43);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 0;
            this.typeComboBox.Text = "choose type...";
            this.helpToolTip.SetToolTip(this.typeComboBox, "Select user type for user you\r\nwant to create");
            this.typeComboBox.TextChanged += new System.EventHandler(this.typeComboBox_TextChanged);
            this.typeComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.typeComboBox_Validating);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(55, 45);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(85, 15);
            this.label35.TabIndex = 115;
            this.label35.Text = "Type of user";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(12, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(141, 17);
            this.label26.TabIndex = 114;
            this.label26.Text = "Basic informations";
            // 
            // label27
            // 
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label27.Location = new System.Drawing.Point(2, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(355, 2);
            this.label27.TabIndex = 113;
            // 
            // addClassroomButton
            // 
            this.addClassroomButton.Enabled = false;
            this.addClassroomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addClassroomButton.Location = new System.Drawing.Point(30, 407);
            this.addClassroomButton.Name = "addClassroomButton";
            this.addClassroomButton.Size = new System.Drawing.Size(75, 23);
            this.addClassroomButton.TabIndex = 9;
            this.addClassroomButton.Text = "Add";
            this.helpToolTip.SetToolTip(this.addClassroomButton, "If you are creating laboratory personnel\r\nyou can choose classroom for which he/s" +
        "he\r\nwill be responsible");
            this.addClassroomButton.UseVisualStyleBackColor = true;
            this.addClassroomButton.Click += new System.EventHandler(this.addClassroomButton_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Red;
            this.label55.Location = new System.Drawing.Point(27, 373);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(200, 15);
            this.label55.TabIndex = 126;
            this.label55.Text = "Allowed only for laboratory personel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 129;
            this.label3.Text = "Classrooms";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(2, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(355, 2);
            this.label4.TabIndex = 128;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(2, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(355, 2);
            this.label5.TabIndex = 130;
            // 
            // createUserErrorProvider
            // 
            this.createUserErrorProvider.ContainerControl = this;
            // 
            // CreateUserForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(363, 490);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addClassroomButton);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.passTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.mailTextBox);
            this.Controls.Add(this.telephoneTextBox);
            this.Controls.Add(this.officeTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.surnameTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(371, 524);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(371, 524);
            this.Name = "CreateUserForm";
            this.ShowInTaskbar = false;
            this.Text = "Create User Form";
            ((System.ComponentModel.ISupportInitialize)(this.createUserErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox mailTextBox;
        private System.Windows.Forms.TextBox telephoneTextBox;
        private System.Windows.Forms.TextBox officeTextBox;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox surnameTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button addClassroomButton;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider createUserErrorProvider;
        private System.Windows.Forms.ToolTip helpToolTip;
    }
}