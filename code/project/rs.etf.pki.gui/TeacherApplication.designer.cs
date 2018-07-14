namespace PKI_project
{
    partial class TeacherApplication
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
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.searchTabPage.SuspendLayout();
            this.baseTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // reserveButton
            // 
            this.helpToolTip.SetToolTip(this.reserveButton, "Click here to reserve selected time period");
            // 
            // reserveAllButton
            // 
            this.helpToolTip.SetToolTip(this.reserveAllButton, "Click here to reserve all time periods");
            // 
            // unreserveAllOccupiedButton
            // 
            this.helpToolTip.SetToolTip(this.unreserveAllOccupiedButton, "Click here to unreserve all time periods");
            this.unreserveAllOccupiedButton.Click += new System.EventHandler(this.unreserveAllOccupiedButton_Click_1);
            // 
            // unreserveOccupiedButton
            // 
            this.helpToolTip.SetToolTip(this.unreserveOccupiedButton, "Click here to unreserve selected time period");
            this.unreserveOccupiedButton.Click += new System.EventHandler(this.unreserveOccupiedButton_Click_1);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(232, 218);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(10, 13);
            this.label44.TabIndex = 54;
            this.label44.Text = "-";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(67, 279);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(100, 15);
            this.label45.TabIndex = 44;
            this.label45.Text = "Wanted period";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(26, 363);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(113, 17);
            this.label39.TabIndex = 63;
            this.label39.Text = "Search results";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(26, 14);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(216, 17);
            this.label40.TabIndex = 62;
            this.label40.Text = "Select filter/filters for search";
            // 
            // label41
            // 
            this.label41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label41.Location = new System.Drawing.Point(16, 34);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(765, 2);
            this.label41.TabIndex = 61;
            // 
            // label42
            // 
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label42.Location = new System.Drawing.Point(16, 382);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(765, 2);
            this.label42.TabIndex = 60;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(331, 281);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(10, 13);
            this.label43.TabIndex = 57;
            this.label43.Text = "-";
            // 
            // TeacherApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(927, 695);
            this.Name = "TeacherApplication";
            this.searchTabPage.ResumeLayout(false);
            this.baseTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
    }
}
