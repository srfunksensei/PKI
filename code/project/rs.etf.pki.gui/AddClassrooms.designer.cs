namespace PKI_project
{
    partial class AddClassrooms
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
            this.helpToolTip.SetToolTip(this.selectedListBox, "Selected classrooms for personnel");
            // 
            // allListBox
            // 
            this.helpToolTip.SetToolTip(this.allListBox, "All classrooms in the system");
            // 
            // label10
            // 
            this.label10.Size = new System.Drawing.Size(190, 17);
            this.label10.Text = "Select classrooms to add";
            // 
            // AddClassrooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(405, 306);
            this.Name = "AddClassrooms";
            this.Text = "Add classrooms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
