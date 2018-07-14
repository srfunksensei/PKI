namespace PKI_project
{
    partial class BaseReserve
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.forTextBox = new System.Windows.Forms.TextBox();
            this.coureseTextBox = new System.Windows.Forms.TextBox();
            this.reserveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.reservationErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.endTimeReserveTextBox = new System.Windows.Forms.TextBox();
            this.startTimeReserveTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.reservationErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "For";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Course";
            // 
            // forTextBox
            // 
            this.forTextBox.Location = new System.Drawing.Point(83, 17);
            this.forTextBox.Name = "forTextBox";
            this.forTextBox.Size = new System.Drawing.Size(151, 20);
            this.forTextBox.TabIndex = 0;
            this.helpToolTip.SetToolTip(this.forTextBox, "Input user for who reservation will be made");
            this.forTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.forTextBox_Validating);
            // 
            // coureseTextBox
            // 
            this.coureseTextBox.Location = new System.Drawing.Point(83, 48);
            this.coureseTextBox.Name = "coureseTextBox";
            this.coureseTextBox.Size = new System.Drawing.Size(151, 20);
            this.coureseTextBox.TabIndex = 1;
            this.helpToolTip.SetToolTip(this.coureseTextBox, "Input course for reservation to be made");
            this.coureseTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.coureseTextBox_Validating);
            // 
            // reserveButton
            // 
            this.reserveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reserveButton.Location = new System.Drawing.Point(44, 113);
            this.reserveButton.Name = "reserveButton";
            this.reserveButton.Size = new System.Drawing.Size(75, 24);
            this.reserveButton.TabIndex = 4;
            this.reserveButton.Text = "Reserve";
            this.helpToolTip.SetToolTip(this.reserveButton, "Click to make the reservation");
            this.reserveButton.UseVisualStyleBackColor = true;
            this.reserveButton.Click += new System.EventHandler(this.reserveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(131, 113);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 24);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.helpToolTip.SetToolTip(this.cancelButton, "Click to cancel reserving");
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // reservationErrorProvider
            // 
            this.reservationErrorProvider.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "-";
            // 
            // endTimeReserveTextBox
            // 
            this.endTimeReserveTextBox.Location = new System.Drawing.Point(176, 79);
            this.endTimeReserveTextBox.Name = "endTimeReserveTextBox";
            this.endTimeReserveTextBox.Size = new System.Drawing.Size(32, 20);
            this.endTimeReserveTextBox.TabIndex = 3;
            this.helpToolTip.SetToolTip(this.endTimeReserveTextBox, "Optionaly change reservation end time");
            this.endTimeReserveTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.endTimeReserveTextBox_Validating);
            // 
            // startTimeReserveTextBox
            // 
            this.startTimeReserveTextBox.Location = new System.Drawing.Point(122, 79);
            this.startTimeReserveTextBox.Name = "startTimeReserveTextBox";
            this.startTimeReserveTextBox.Size = new System.Drawing.Size(32, 20);
            this.startTimeReserveTextBox.TabIndex = 2;
            this.helpToolTip.SetToolTip(this.startTimeReserveTextBox, "Optionaly change reservation start time");
            this.startTimeReserveTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.startTimeReserveTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Time period";
            // 
            // BaseReserve
            // 
            this.AcceptButton = this.reserveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(261, 146);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endTimeReserveTextBox);
            this.Controls.Add(this.startTimeReserveTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.reserveButton);
            this.Controls.Add(this.coureseTextBox);
            this.Controls.Add(this.forTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(269, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(269, 180);
            this.Name = "BaseReserve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reserve";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.reservationErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TextBox forTextBox;
        protected System.Windows.Forms.TextBox coureseTextBox;
        protected System.Windows.Forms.Button reserveButton;
        protected System.Windows.Forms.Button cancelButton;
        protected System.Windows.Forms.ErrorProvider reservationErrorProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TextBox endTimeReserveTextBox;
        protected System.Windows.Forms.TextBox startTimeReserveTextBox;
        protected System.Windows.Forms.ToolTip helpToolTip;
    }
}