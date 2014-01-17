namespace DatabaseToolUI
{
    partial class FrmCreateDataBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateDataBase));
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataPath = new System.Windows.Forms.TextBox();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.txtSqlPath = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSqlPath = new System.Windows.Forms.Button();
            this.btnDataPath = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDataBase
            // 
            resources.ApplyResources(this.txtDataBase, "txtDataBase");
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.TextChanged += new System.EventHandler(this.txtDataBase_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtDataPath
            // 
            resources.ApplyResources(this.txtDataPath, "txtDataPath");
            this.txtDataPath.Name = "txtDataPath";
            // 
            // txtLogPath
            // 
            resources.ApplyResources(this.txtLogPath, "txtLogPath");
            this.txtLogPath.Name = "txtLogPath";
            // 
            // txtSqlPath
            // 
            resources.ApplyResources(this.txtSqlPath, "txtSqlPath");
            this.txtSqlPath.Name = "txtSqlPath";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSqlPath
            // 
            resources.ApplyResources(this.btnSqlPath, "btnSqlPath");
            this.btnSqlPath.Name = "btnSqlPath";
            this.btnSqlPath.UseVisualStyleBackColor = true;
            this.btnSqlPath.Click += new System.EventHandler(this.btnSqlPath_Click);
            // 
            // btnDataPath
            // 
            resources.ApplyResources(this.btnDataPath, "btnDataPath");
            this.btnDataPath.Name = "btnDataPath";
            this.btnDataPath.UseVisualStyleBackColor = true;
            this.btnDataPath.Click += new System.EventHandler(this.btnDataPath_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // btnLogPath
            // 
            resources.ApplyResources(this.btnLogPath, "btnLogPath");
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // FrmCreateDataBase
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnLogPath);
            this.Controls.Add(this.btnDataPath);
            this.Controls.Add(this.btnSqlPath);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSqlPath);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.txtDataPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDataBase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "FrmCreateDataBase";
            this.Load += new System.EventHandler(this.FrmCreateDataBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.TextBox txtSqlPath;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSqlPath;
        public System.Windows.Forms.Button btnDataPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.Button btnLogPath;
    }
}