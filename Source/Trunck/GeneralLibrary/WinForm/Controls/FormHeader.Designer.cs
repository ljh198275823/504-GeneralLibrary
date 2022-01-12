namespace LJH.GeneralLibrary.WinformControl
{
    partial class FormHeader
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHeader));
            this.lblCaption = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCaption
            // 
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.ForeColor = System.Drawing.Color.Black;
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblCaption_MouseDown);
            this.lblCaption.MouseEnter += new System.EventHandler(this.lblCaption_MouseEnter);
            this.lblCaption.MouseLeave += new System.EventHandler(this.lblCaption_MouseLeave);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // FormHeader
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.btnClose);
            this.Name = "FormHeader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label   btnClose;
        private System.Windows.Forms.Label lblCaption;
    }
}
