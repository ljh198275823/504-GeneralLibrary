namespace LJH.GeneralLibrary.WinForm
{
    partial class FrmPagingMasterBase<T>
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
            this.ucPaging1 = new WinformControl.UCPaging();
            this.SuspendLayout();
            // 
            // ucPaging1
            // 
            this.ucPaging1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucPaging1.Location = new System.Drawing.Point(0, 369);
            this.ucPaging1.Name = "ucPaging1";
            this.ucPaging1.Size = new System.Drawing.Size(977, 30);
            this.ucPaging1.TabIndex = 22;
            // 
            // FrmPagingMasterBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(977, 399);
            this.Controls.Add(this.ucPaging1);
            this.Name = "FrmPagingMasterBase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmMasterBase";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMasterBase_FormClosed);
            this.Load += new System.EventHandler(this.FrmMasterBase_Load);
            this.ResumeLayout(false);
        }

        #endregion
        private WinformControl.UCPaging ucPaging1;
    }
}