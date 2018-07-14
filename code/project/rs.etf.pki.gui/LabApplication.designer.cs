namespace PKI_project
{
    partial class LabApplication
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
            this.manageClassroomTabPage = new System.Windows.Forms.TabPage();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.addRemoveNoticeButton = new System.Windows.Forms.Button();
            this.label81 = new System.Windows.Forms.Label();
            this.addRemoveCommentlButton = new System.Windows.Forms.Button();
            this.label80 = new System.Windows.Forms.Label();
            this.changeInfoClassroomButton = new System.Windows.Forms.Button();
            this.label79 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.blackboardCheckBox = new System.Windows.Forms.CheckBox();
            this.label85 = new System.Windows.Forms.Label();
            this.computersCheckBox = new System.Windows.Forms.CheckBox();
            this.searchClassroomDataGridView = new System.Windows.Forms.DataGridView();
            this.classroomNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blackboardColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.laboratoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workingHoursColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectorCheckBox = new System.Windows.Forms.CheckBox();
            this.searchClassroomButton = new System.Windows.Forms.Button();
            this.laboratoryCheckBox = new System.Windows.Forms.CheckBox();
            this.capacityTextBox = new System.Windows.Forms.TextBox();
            this.classroomNumberTextBox = new System.Windows.Forms.TextBox();
            this.searchTabPage.SuspendLayout();
            this.baseTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.manageClassroomTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchClassroomDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // baseTabControl
            // 
            this.baseTabControl.Controls.Add(this.manageClassroomTabPage);
            this.baseTabControl.SelectedIndexChanged += new System.EventHandler(this.baseTabControl_SelectedIndexChanged);
            this.baseTabControl.Controls.SetChildIndex(this.manageClassroomTabPage, 0);
            this.baseTabControl.Controls.SetChildIndex(this.uInfoTabPage, 0);
            this.baseTabControl.Controls.SetChildIndex(this.searchTabPage, 0);
            // 
            // reserveButton
            // 
            this.helpToolTip.SetToolTip(this.reserveButton, "Click here to reserve selected time period");
            this.reserveButton.Click += new System.EventHandler(this.reserveButton_Click1);
            // 
            // reserveAllButton
            // 
            this.helpToolTip.SetToolTip(this.reserveAllButton, "Click here to reserve all time periods");
            this.reserveAllButton.Click += new System.EventHandler(this.reserveAllButton_Click1);
            // 
            // unreserveAllOccupiedButton
            // 
            this.helpToolTip.SetToolTip(this.unreserveAllOccupiedButton, "Click here to unreserve all time periods");
            this.unreserveAllOccupiedButton.Click += new System.EventHandler(this.unreserveAllOccupiedButton_Click1);
            // 
            // unreserveOccupiedButton
            // 
            this.helpToolTip.SetToolTip(this.unreserveOccupiedButton, "Click here to unreserve selected time period");
            this.unreserveOccupiedButton.Click += new System.EventHandler(this.unreserveOccupiedButton_Click1);
            // 
            // manageClassroomTabPage
            // 
            this.manageClassroomTabPage.Controls.Add(this.label82);
            this.manageClassroomTabPage.Controls.Add(this.label83);
            this.manageClassroomTabPage.Controls.Add(this.addRemoveNoticeButton);
            this.manageClassroomTabPage.Controls.Add(this.label81);
            this.manageClassroomTabPage.Controls.Add(this.addRemoveCommentlButton);
            this.manageClassroomTabPage.Controls.Add(this.label80);
            this.manageClassroomTabPage.Controls.Add(this.changeInfoClassroomButton);
            this.manageClassroomTabPage.Controls.Add(this.label79);
            this.manageClassroomTabPage.Controls.Add(this.label84);
            this.manageClassroomTabPage.Controls.Add(this.blackboardCheckBox);
            this.manageClassroomTabPage.Controls.Add(this.label85);
            this.manageClassroomTabPage.Controls.Add(this.computersCheckBox);
            this.manageClassroomTabPage.Controls.Add(this.searchClassroomDataGridView);
            this.manageClassroomTabPage.Controls.Add(this.projectorCheckBox);
            this.manageClassroomTabPage.Controls.Add(this.searchClassroomButton);
            this.manageClassroomTabPage.Controls.Add(this.laboratoryCheckBox);
            this.manageClassroomTabPage.Controls.Add(this.capacityTextBox);
            this.manageClassroomTabPage.Controls.Add(this.classroomNumberTextBox);
            this.manageClassroomTabPage.Location = new System.Drawing.Point(4, 22);
            this.manageClassroomTabPage.Name = "manageClassroomTabPage";
            this.manageClassroomTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.manageClassroomTabPage.Size = new System.Drawing.Size(919, 645);
            this.manageClassroomTabPage.TabIndex = 2;
            this.manageClassroomTabPage.Text = "Manage Classrooms";
            this.manageClassroomTabPage.ToolTipText = "Manage my classrooms";
            this.manageClassroomTabPage.UseVisualStyleBackColor = true;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(30, 12);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(216, 17);
            this.label82.TabIndex = 110;
            this.label82.Text = "Select filter/filters for search";
            // 
            // label83
            // 
            this.label83.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label83.Location = new System.Drawing.Point(20, 32);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(765, 2);
            this.label83.TabIndex = 109;
            // 
            // addRemoveNoticeButton
            // 
            this.addRemoveNoticeButton.Enabled = false;
            this.addRemoveNoticeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addRemoveNoticeButton.Location = new System.Drawing.Point(474, 536);
            this.addRemoveNoticeButton.Name = "addRemoveNoticeButton";
            this.addRemoveNoticeButton.Size = new System.Drawing.Size(115, 43);
            this.addRemoveNoticeButton.TabIndex = 10;
            this.addRemoveNoticeButton.Text = "Add/Remove Notice";
            this.helpToolTip.SetToolTip(this.addRemoveNoticeButton, "Add/remove notice for \r\nselected classroom");
            this.addRemoveNoticeButton.UseVisualStyleBackColor = true;
            this.addRemoveNoticeButton.Click += new System.EventHandler(this.addRemoveNoticeButton_Click);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(43, 45);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(128, 15);
            this.label81.TabIndex = 111;
            this.label81.Text = "Classroom number";
            // 
            // addRemoveCommentlButton
            // 
            this.addRemoveCommentlButton.Enabled = false;
            this.addRemoveCommentlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addRemoveCommentlButton.Location = new System.Drawing.Point(352, 536);
            this.addRemoveCommentlButton.Name = "addRemoveCommentlButton";
            this.addRemoveCommentlButton.Size = new System.Drawing.Size(115, 43);
            this.addRemoveCommentlButton.TabIndex = 9;
            this.addRemoveCommentlButton.Text = "Add/Remove Comment";
            this.helpToolTip.SetToolTip(this.addRemoveCommentlButton, "Add/remove comment for\r\nselected classroom");
            this.addRemoveCommentlButton.UseVisualStyleBackColor = true;
            this.addRemoveCommentlButton.Click += new System.EventHandler(this.addRemoveCommentlButton_Click);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(43, 72);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(105, 15);
            this.label80.TabIndex = 112;
            this.label80.Text = "Classroom type";
            // 
            // changeInfoClassroomButton
            // 
            this.changeInfoClassroomButton.Enabled = false;
            this.changeInfoClassroomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInfoClassroomButton.Location = new System.Drawing.Point(230, 536);
            this.changeInfoClassroomButton.Name = "changeInfoClassroomButton";
            this.changeInfoClassroomButton.Size = new System.Drawing.Size(115, 43);
            this.changeInfoClassroomButton.TabIndex = 8;
            this.changeInfoClassroomButton.Text = "Change Classroom Info";
            this.helpToolTip.SetToolTip(this.changeInfoClassroomButton, "Click to change classroom info of selected classroom");
            this.changeInfoClassroomButton.UseVisualStyleBackColor = true;
            this.changeInfoClassroomButton.Click += new System.EventHandler(this.changeInfoClassroomButton_Click);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(43, 176);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(61, 15);
            this.label79.TabIndex = 113;
            this.label79.Text = "Capacity";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(30, 245);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(113, 17);
            this.label84.TabIndex = 123;
            this.label84.Text = "Search results";
            // 
            // blackboardCheckBox
            // 
            this.blackboardCheckBox.AutoSize = true;
            this.blackboardCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackboardCheckBox.Location = new System.Drawing.Point(199, 71);
            this.blackboardCheckBox.Name = "blackboardCheckBox";
            this.blackboardCheckBox.Size = new System.Drawing.Size(135, 19);
            this.blackboardCheckBox.TabIndex = 1;
            this.blackboardCheckBox.Text = "Work on blackboard";
            this.helpToolTip.SetToolTip(this.blackboardCheckBox, "Classroom type for search");
            this.blackboardCheckBox.UseVisualStyleBackColor = true;
            this.blackboardCheckBox.Validating += new System.ComponentModel.CancelEventHandler(this.blackboardCheckBox_Validating);
            // 
            // label85
            // 
            this.label85.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label85.Location = new System.Drawing.Point(20, 262);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(765, 2);
            this.label85.TabIndex = 122;
            // 
            // computersCheckBox
            // 
            this.computersCheckBox.AutoSize = true;
            this.computersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computersCheckBox.Location = new System.Drawing.Point(199, 97);
            this.computersCheckBox.Name = "computersCheckBox";
            this.computersCheckBox.Size = new System.Drawing.Size(140, 19);
            this.computersCheckBox.TabIndex = 2;
            this.computersCheckBox.Text = "Work with computers";
            this.helpToolTip.SetToolTip(this.computersCheckBox, "Classroom type for search");
            this.computersCheckBox.UseVisualStyleBackColor = true;
            this.computersCheckBox.Validating += new System.ComponentModel.CancelEventHandler(this.computersCheckBox_Validating);
            // 
            // searchClassroomDataGridView
            // 
            this.searchClassroomDataGridView.AllowUserToAddRows = false;
            this.searchClassroomDataGridView.AllowUserToDeleteRows = false;
            this.searchClassroomDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchClassroomDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classroomNumberColumn,
            this.blackboardColumn,
            this.computersColumn,
            this.projectorColumn,
            this.laboratoryColumn,
            this.capacityColumn,
            this.workingHoursColumn});
            this.searchClassroomDataGridView.Location = new System.Drawing.Point(64, 280);
            this.searchClassroomDataGridView.MultiSelect = false;
            this.searchClassroomDataGridView.Name = "searchClassroomDataGridView";
            this.searchClassroomDataGridView.ReadOnly = true;
            this.searchClassroomDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.searchClassroomDataGridView.Size = new System.Drawing.Size(699, 250);
            this.searchClassroomDataGridView.TabIndex = 7;
            this.searchClassroomDataGridView.SelectionChanged += new System.EventHandler(this.searchClassroomDataGridView_SelectionChanged);
            // 
            // classroomNumberColumn
            // 
            this.classroomNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.classroomNumberColumn.HeaderText = "Classroom number";
            this.classroomNumberColumn.Name = "classroomNumberColumn";
            this.classroomNumberColumn.ReadOnly = true;
            this.classroomNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.classroomNumberColumn.Width = 108;
            // 
            // blackboardColumn
            // 
            this.blackboardColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.blackboardColumn.HeaderText = "Blackboard";
            this.blackboardColumn.Name = "blackboardColumn";
            this.blackboardColumn.ReadOnly = true;
            this.blackboardColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blackboardColumn.Width = 86;
            // 
            // computersColumn
            // 
            this.computersColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.computersColumn.HeaderText = "Computers";
            this.computersColumn.Name = "computersColumn";
            this.computersColumn.ReadOnly = true;
            this.computersColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.computersColumn.Width = 82;
            // 
            // projectorColumn
            // 
            this.projectorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.projectorColumn.HeaderText = "Projector";
            this.projectorColumn.Name = "projectorColumn";
            this.projectorColumn.ReadOnly = true;
            this.projectorColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.projectorColumn.Width = 74;
            // 
            // laboratoryColumn
            // 
            this.laboratoryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.laboratoryColumn.HeaderText = "Laboratory";
            this.laboratoryColumn.Name = "laboratoryColumn";
            this.laboratoryColumn.ReadOnly = true;
            this.laboratoryColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.laboratoryColumn.Width = 82;
            // 
            // capacityColumn
            // 
            this.capacityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.capacityColumn.HeaderText = "Capacity";
            this.capacityColumn.Name = "capacityColumn";
            this.capacityColumn.ReadOnly = true;
            this.capacityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.capacityColumn.Width = 73;
            // 
            // workingHoursColumn
            // 
            this.workingHoursColumn.HeaderText = "Working hours";
            this.workingHoursColumn.Name = "workingHoursColumn";
            this.workingHoursColumn.ReadOnly = true;
            this.workingHoursColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.workingHoursColumn.Width = 150;
            // 
            // projectorCheckBox
            // 
            this.projectorCheckBox.AutoSize = true;
            this.projectorCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectorCheckBox.Location = new System.Drawing.Point(199, 123);
            this.projectorCheckBox.Name = "projectorCheckBox";
            this.projectorCheckBox.Size = new System.Drawing.Size(156, 19);
            this.projectorCheckBox.TabIndex = 3;
            this.projectorCheckBox.Text = "Teching with a projector";
            this.helpToolTip.SetToolTip(this.projectorCheckBox, "Classroom type for search");
            this.projectorCheckBox.UseVisualStyleBackColor = true;
            this.projectorCheckBox.Validating += new System.ComponentModel.CancelEventHandler(this.projectorCheckBox_Validating);
            // 
            // searchClassroomButton
            // 
            this.searchClassroomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchClassroomButton.Location = new System.Drawing.Point(133, 205);
            this.searchClassroomButton.Name = "searchClassroomButton";
            this.searchClassroomButton.Size = new System.Drawing.Size(90, 23);
            this.searchClassroomButton.TabIndex = 6;
            this.searchClassroomButton.Text = "Search";
            this.helpToolTip.SetToolTip(this.searchClassroomButton, "Click to see search results");
            this.searchClassroomButton.UseVisualStyleBackColor = true;
            this.searchClassroomButton.Click += new System.EventHandler(this.searchClassroomButton_Click);
            // 
            // laboratoryCheckBox
            // 
            this.laboratoryCheckBox.AutoSize = true;
            this.laboratoryCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laboratoryCheckBox.Location = new System.Drawing.Point(199, 149);
            this.laboratoryCheckBox.Name = "laboratoryCheckBox";
            this.laboratoryCheckBox.Size = new System.Drawing.Size(139, 19);
            this.laboratoryCheckBox.TabIndex = 4;
            this.laboratoryCheckBox.Text = "Laboratory exercises";
            this.helpToolTip.SetToolTip(this.laboratoryCheckBox, "Classroom type for search");
            this.laboratoryCheckBox.UseVisualStyleBackColor = true;
            this.laboratoryCheckBox.Validating += new System.ComponentModel.CancelEventHandler(this.laboratoryCheckBox_Validating);
            // 
            // capacityTextBox
            // 
            this.capacityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capacityTextBox.Location = new System.Drawing.Point(199, 175);
            this.capacityTextBox.Name = "capacityTextBox";
            this.capacityTextBox.Size = new System.Drawing.Size(35, 20);
            this.capacityTextBox.TabIndex = 5;
            this.helpToolTip.SetToolTip(this.capacityTextBox, "Capacity for search");
            this.capacityTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.capacityTextBox_Validating);
            // 
            // classroomNumberTextBox
            // 
            this.classroomNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classroomNumberTextBox.Location = new System.Drawing.Point(199, 44);
            this.classroomNumberTextBox.Name = "classroomNumberTextBox";
            this.classroomNumberTextBox.Size = new System.Drawing.Size(35, 20);
            this.classroomNumberTextBox.TabIndex = 0;
            this.helpToolTip.SetToolTip(this.classroomNumberTextBox, "Classroom number for search");
            this.classroomNumberTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.classroomNumberTextBox_Validating);
            // 
            // LabApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(927, 695);
            this.Name = "LabApplication";
            this.searchTabPage.ResumeLayout(false);
            this.baseTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.manageClassroomTabPage.ResumeLayout(false);
            this.manageClassroomTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchClassroomDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage manageClassroomTabPage;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Button addRemoveNoticeButton;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Button addRemoveCommentlButton;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Button changeInfoClassroomButton;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.CheckBox blackboardCheckBox;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.CheckBox computersCheckBox;
        private System.Windows.Forms.DataGridView searchClassroomDataGridView;
        private System.Windows.Forms.CheckBox projectorCheckBox;
        private System.Windows.Forms.Button searchClassroomButton;
        private System.Windows.Forms.CheckBox laboratoryCheckBox;
        private System.Windows.Forms.TextBox capacityTextBox;
        private System.Windows.Forms.TextBox classroomNumberTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn classroomNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blackboardColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn computersColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn laboratoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workingHoursColumn;
    }
}
