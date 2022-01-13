namespace LJH.GeneralLibrary.WinForm
{
    partial class FrmReportBaseWithPaging<TID, TEntity>
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
            this.btnColumn = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ucPaging1 = new WinformControl.UCPaging();
            this.SuspendLayout();
            // 
            // btnColumn
            // 
            this.btnColumn.Location = new System.Drawing.Point(688, 70);
            this.btnColumn.Name = "btnColumn";
            this.btnColumn.Size = new System.Drawing.Size(111, 23);
            this.btnColumn.TabIndex = 20;
            this.btnColumn.Text = "选择列(&C)";
            this.btnColumn.UseVisualStyleBackColor = true;
            this.btnColumn.Click += new System.EventHandler(this.btnSelectColumns_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(688, 41);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(111, 23);
            this.btnSaveAs.TabIndex = 19;
            this.btnSaveAs.Text = "另存为(&S)";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(688, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(111, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ucPaging1
            // 
            this.ucPaging1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucPaging1.Location = new System.Drawing.Point(0, 393);
            this.ucPaging1.Name = "ucPaging1";
            this.ucPaging1.Size = new System.Drawing.Size(913, 30);
            this.ucPaging1.TabIndex = 21;
            // 
            // FrmPagingReportBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 423);
            this.Controls.Add(this.ucPaging1);
            this.Controls.Add(this.btnColumn);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnSearch);
            this.Name = "FrmPagingReportBase";
            this.Text = "FrmReportBase";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPagingReportBase_FormClosed);
            this.Load += new System.EventHandler(this.FrmReportBase_Load);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button btnColumn;
        protected System.Windows.Forms.Button btnSaveAs;
        protected System.Windows.Forms.Button btnSearch;
        private WinformControl.UCPaging ucPaging1;
    }
}