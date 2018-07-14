namespace PKI_project
{
    partial class ChangeClassroomInfo
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
            this.changeNoticeTextBox = new System.Windows.Forms.TextBox();
            this.changeCommentTextBox = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.changeEndWorkingTextBox = new System.Windows.Forms.TextBox();
            this.changeStartWorkingTextBox = new System.Windows.Forms.TextBox();
            this.changeCapacityTextBox = new System.Windows.Forms.TextBox();
            this.changeCNumTextBox = new System.Windows.Forms.TextBox();
            this.changeLaboratoryCheckBox = new System.Windows.Forms.CheckBox();
            this.changeProjectorCheckBox = new System.Windows.Forms.CheckBox();
            this.changeComputersCheckBox = new System.Windows.Forms.CheckBox();
            this.changeBlackboardCheckBox = new System.Windows.Forms.CheckBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.changeClassroomInforErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.changeClassroomInforErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // changeNoticeTextBox
            // 
            this.changeNoticeTextBox.Location = new System.Drawing.Point(28, 399);
            this.changeNoticeTextBox.Multiline = true;
            this.changeNoticeTextBox.Name = "changeNoticeTextBox";
            this.changeNoticeTextBox.Size = new System.Drawing.Size(500, 50);
            this.changeNoticeTextBox.TabIndex = 9;
            this.helpToolTip.SetToolTip(this.changeNoticeTextBox, "Input/remove notice for classroom");
            // 
            // changeCommentTextBox
            // 
            this.changeCommentTextBox.Location = new System.Drawing.Point(28, 302);
            this.changeCommentTextBox.Multiline = true;
            this.changeCommentTextBox.Name = "changeCommentTextBox";
            this.changeCommentTextBox.Size = new System.Drawing.Size(500, 50);
            this.changeCommentTextBox.TabIndex = 8;
            this.helpToolTip.SetToolTip(this.changeCommentTextBox, "Input/remove comment for classroom");
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(25, 381);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(48, 15);
            this.label75.TabIndex = 144;
            this.label75.Text = "Notice";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(25, 284);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(68, 15);
            this.label74.TabIndex = 143;
            this.label74.Text = "Comment";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(12, 249);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(163, 17);
            this.label72.TabIndex = 142;
            this.label72.Text = "Comments and notice";
            // 
            // label73
            // 
            this.label73.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label73.Location = new System.Drawing.Point(2, 269);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(540, 2);
            this.label73.TabIndex = 141;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(190, 210);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(10, 13);
            this.label67.TabIndex = 140;
            this.label67.Text = "-";
            // 
            // changeEndWorkingTextBox
            // 
            this.changeEndWorkingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeEndWorkingTextBox.Location = new System.Drawing.Point(206, 207);
            this.changeEndWorkingTextBox.Name = "changeEndWorkingTextBox";
            this.changeEndWorkingTextBox.Size = new System.Drawing.Size(20, 20);
            this.changeEndWorkingTextBox.TabIndex = 7;
            this.helpToolTip.SetToolTip(this.changeEndWorkingTextBox, "Input end working hour \r\nyou wish to change");
            this.changeEndWorkingTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.changeEndWorkingTextBox_Validating);
            // 
            // changeStartWorkingTextBox
            // 
            this.changeStartWorkingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeStartWorkingTextBox.Location = new System.Drawing.Point(164, 207);
            this.changeStartWorkingTextBox.Name = "changeStartWorkingTextBox";
            this.changeStartWorkingTextBox.Size = new System.Drawing.Size(20, 20);
            this.changeStartWorkingTextBox.TabIndex = 6;
            this.helpToolTip.SetToolTip(this.changeStartWorkingTextBox, "Input start working hour\r\nyou wish to change");
            this.changeStartWorkingTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.changeStartWorkingTextBox_Validating);
            // 
            // changeCapacityTextBox
            // 
            this.changeCapacityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeCapacityTextBox.Location = new System.Drawing.Point(163, 176);
            this.changeCapacityTextBox.Name = "changeCapacityTextBox";
            this.changeCapacityTextBox.Size = new System.Drawing.Size(35, 20);
            this.changeCapacityTextBox.TabIndex = 5;
            this.helpToolTip.SetToolTip(this.changeCapacityTextBox, "Input classroom capacity you wish to change");
            this.changeCapacityTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.changeCapacityTextBox_Validating);
            // 
            // changeCNumTextBox
            // 
            this.changeCNumTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeCNumTextBox.Location = new System.Drawing.Point(163, 41);
            this.changeCNumTextBox.Name = "changeCNumTextBox";
            this.changeCNumTextBox.Size = new System.Drawing.Size(35, 20);
            this.changeCNumTextBox.TabIndex = 0;
            this.helpToolTip.SetToolTip(this.changeCNumTextBox, "Input classroom number you wish to change");
            this.changeCNumTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.changeCNumTextBox_KeyDown);
            this.changeCNumTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.changeCNumTextBox_Validating);
            // 
            // changeLaboratoryCheckBox
            // 
            this.changeLaboratoryCheckBox.AutoSize = true;
            this.changeLaboratoryCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeLaboratoryCheckBox.Location = new System.Drawing.Point(163, 147);
            this.changeLaboratoryCheckBox.Name = "changeLaboratoryCheckBox";
            this.changeLaboratoryCheckBox.Size = new System.Drawing.Size(139, 19);
            this.changeLaboratoryCheckBox.TabIndex = 4;
            this.changeLaboratoryCheckBox.Text = "Laboratory exercises";
            this.helpToolTip.SetToolTip(this.changeLaboratoryCheckBox, "Select/deselect type of classroom\r\nyou wish to change");
            this.changeLaboratoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeProjectorCheckBox
            // 
            this.changeProjectorCheckBox.AutoSize = true;
            this.changeProjectorCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeProjectorCheckBox.Location = new System.Drawing.Point(163, 122);
            this.changeProjectorCheckBox.Name = "changeProjectorCheckBox";
            this.changeProjectorCheckBox.Size = new System.Drawing.Size(156, 19);
            this.changeProjectorCheckBox.TabIndex = 3;
            this.changeProjectorCheckBox.Text = "Teching with a projector";
            this.helpToolTip.SetToolTip(this.changeProjectorCheckBox, "Select/deselect type of classroom\r\nyou wish to change");
            this.changeProjectorCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeComputersCheckBox
            // 
            this.changeComputersCheckBox.AutoSize = true;
            this.changeComputersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeComputersCheckBox.Location = new System.Drawing.Point(163, 97);
            this.changeComputersCheckBox.Name = "changeComputersCheckBox";
            this.changeComputersCheckBox.Size = new System.Drawing.Size(140, 19);
            this.changeComputersCheckBox.TabIndex = 2;
            this.changeComputersCheckBox.Text = "Work with computers";
            this.helpToolTip.SetToolTip(this.changeComputersCheckBox, "Select/deselect type of classroom\r\nyou wish to change");
            this.changeComputersCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeBlackboardCheckBox
            // 
            this.changeBlackboardCheckBox.AutoSize = true;
            this.changeBlackboardCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeBlackboardCheckBox.Location = new System.Drawing.Point(163, 72);
            this.changeBlackboardCheckBox.Name = "changeBlackboardCheckBox";
            this.changeBlackboardCheckBox.Size = new System.Drawing.Size(135, 19);
            this.changeBlackboardCheckBox.TabIndex = 1;
            this.changeBlackboardCheckBox.Text = "Work on blackboard";
            this.helpToolTip.SetToolTip(this.changeBlackboardCheckBox, "Select/deselect type of classroom\r\nyou wish to change");
            this.changeBlackboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(25, 210);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(99, 15);
            this.label68.TabIndex = 131;
            this.label68.Text = "Working hours";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(25, 177);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(61, 15);
            this.label69.TabIndex = 130;
            this.label69.Text = "Capacity";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(25, 72);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(105, 15);
            this.label70.TabIndex = 129;
            this.label70.Text = "Classroom type";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(25, 42);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(128, 15);
            this.label71.TabIndex = 128;
            this.label71.Text = "Classroom number";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(12, 9);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(141, 17);
            this.label65.TabIndex = 127;
            this.label65.Text = "Basic informations";
            // 
            // label66
            // 
            this.label66.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label66.Location = new System.Drawing.Point(2, 29);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(540, 2);
            this.label66.TabIndex = 126;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(195, 468);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.helpToolTip.SetToolTip(this.saveButton, "Click here to save changes for classroom");
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(276, 468);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.helpToolTip.SetToolTip(this.cancelButton, "Cancel changing classroom info");
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // changeClassroomInforErrorProvider
            // 
            this.changeClassroomInforErrorProvider.ContainerControl = this;
            // 
            // ChangeClassroomInfo
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(546, 497);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.changeNoticeTextBox);
            this.Controls.Add(this.changeCommentTextBox);
            this.Controls.Add(this.label75);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.label72);
            this.Controls.Add(this.label73);
            this.Controls.Add(this.label67);
            this.Controls.Add(this.changeEndWorkingTextBox);
            this.Controls.Add(this.changeStartWorkingTextBox);
            this.Controls.Add(this.changeCapacityTextBox);
            this.Controls.Add(this.changeCNumTextBox);
            this.Controls.Add(this.changeLaboratoryCheckBox);
            this.Controls.Add(this.changeProjectorCheckBox);
            this.Controls.Add(this.changeComputersCheckBox);
            this.Controls.Add(this.changeBlackboardCheckBox);
            this.Controls.Add(this.label68);
            this.Controls.Add(this.label69);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.label71);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.label66);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(554, 531);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(554, 531);
            this.Name = "ChangeClassroomInfo";
            this.Text = "Change Classroom Info";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.changeClassroomInforErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox changeNoticeTextBox;
        private System.Windows.Forms.TextBox changeCommentTextBox;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox changeEndWorkingTextBox;
        private System.Windows.Forms.TextBox changeStartWorkingTextBox;
        private System.Windows.Forms.TextBox changeCapacityTextBox;
        private System.Windows.Forms.TextBox changeCNumTextBox;
        private System.Windows.Forms.CheckBox changeLaboratoryCheckBox;
        private System.Windows.Forms.CheckBox changeProjectorCheckBox;
        private System.Windows.Forms.CheckBox changeComputersCheckBox;
        private System.Windows.Forms.CheckBox changeBlackboardCheckBox;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider changeClassroomInforErrorProvider;
        private System.Windows.Forms.ToolTip helpToolTip;
    }
}