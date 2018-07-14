namespace PKI_project
{
    partial class ChangeReservation
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
            this.label4 = new System.Windows.Forms.Label();
            this.specificDateChangeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.reservationErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // forTextBox
            // 
            this.forTextBox.Location = new System.Drawing.Point(122, 17);
            this.helpToolTip.SetToolTip(this.forTextBox, "Input user for who reservation will be made");
            // 
            // coureseTextBox
            // 
            this.coureseTextBox.Location = new System.Drawing.Point(122, 49);
            this.helpToolTip.SetToolTip(this.coureseTextBox, "Input course for reservation to be made");
            // 
            // reserveButton
            // 
            this.reserveButton.Location = new System.Drawing.Point(62, 149);
            this.reserveButton.TabIndex = 5;
            this.reserveButton.Text = "Change";
            this.helpToolTip.SetToolTip(this.reserveButton, "Click to make the reservation");
            this.reserveButton.Click += new System.EventHandler(this.reserveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(149, 149);
            this.cancelButton.TabIndex = 6;
            this.helpToolTip.SetToolTip(this.cancelButton, "Click to cancel reserving");
            // 
            // endTimeReserveTextBox
            // 
            this.helpToolTip.SetToolTip(this.endTimeReserveTextBox, "Optionaly change reservation end time");
            // 
            // startTimeReserveTextBox
            // 
            this.helpToolTip.SetToolTip(this.startTimeReserveTextBox, "Optionaly change reservation start time");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Specific date";
            // 
            // specificDateChangeDateTimePicker
            // 
            this.specificDateChangeDateTimePicker.Checked = false;
            this.specificDateChangeDateTimePicker.CustomFormat = "dd.MM.yyyy";
            this.specificDateChangeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.specificDateChangeDateTimePicker.Location = new System.Drawing.Point(122, 113);
            this.specificDateChangeDateTimePicker.Name = "specificDateChangeDateTimePicker";
            this.specificDateChangeDateTimePicker.Size = new System.Drawing.Size(114, 20);
            this.specificDateChangeDateTimePicker.TabIndex = 4;
            this.helpToolTip.SetToolTip(this.specificDateChangeDateTimePicker, "Change reservation for specific date");
            // 
            // ChangeReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(286, 187);
            this.Controls.Add(this.specificDateChangeDateTimePicker);
            this.Controls.Add(this.label4);
            this.MaximumSize = new System.Drawing.Size(294, 221);
            this.MinimumSize = new System.Drawing.Size(294, 221);
            this.Name = "ChangeReservation";
            this.Text = "Change Reservation";
            this.Controls.SetChildIndex(this.startTimeReserveTextBox, 0);
            this.Controls.SetChildIndex(this.endTimeReserveTextBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.reserveButton, 0);
            this.Controls.SetChildIndex(this.forTextBox, 0);
            this.Controls.SetChildIndex(this.coureseTextBox, 0);
            this.Controls.SetChildIndex(this.specificDateChangeDateTimePicker, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reservationErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker specificDateChangeDateTimePicker;
    }
}
