namespace FormViewTest
{
    partial class Form2
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
            this.dbcTextBox1 = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.SuspendLayout();
            // 
            // dbcTextBox1
            // 
            this.dbcTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dbcTextBox1.Location = new System.Drawing.Point(51, 88);
            this.dbcTextBox1.Name = "dbcTextBox1";
            this.dbcTextBox1.Size = new System.Drawing.Size(165, 21);
            this.dbcTextBox1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.dbcTextBox1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用于测试的窗体";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LJH.GeneralLibrary.WinformControl.DBCTextBox dbcTextBox1;
    }
}