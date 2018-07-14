namespace PKI_project
{
    partial class AddPersonnel
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
            this.SuspendLayout();
            // 
            // selectedListBox
            // 
            this.helpToolTip.SetToolTip(this.selectedListBox, "Selected personnel");
            // 
            // allListBox
            // 
            this.helpToolTip.SetToolTip(this.allListBox, "All personnel in the system");
            // 
            // label10
            // 
            this.label10.Size = new System.Drawing.Size(176, 17);
            this.label10.Text = "Select personell to add";
            // 
            // okButton
            // 
            this.helpToolTip.SetToolTip(this.okButton, "Click to save");
            // 
            // AddPersonnel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(405, 306);
            this.Name = "AddPersonnel";
            this.Text = "Add personnel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
