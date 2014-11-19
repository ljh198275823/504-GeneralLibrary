namespace LJH.GeneralLibrary.WinformControl
{
    partial class UCFormView
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
            this.pBody = new System.Windows.Forms.Panel();
            this.pHeader = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pBody
            // 
            this.pBody.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBody.Location = new System.Drawing.Point(0, 32);
            this.pBody.Margin = new System.Windows.Forms.Padding(0);
            this.pBody.Name = "pBody";
            this.pBody.Size = new System.Drawing.Size(391, 205);
            this.pBody.TabIndex = 1;
            this.pBody.Resize += new System.EventHandler(this.pBody_Resize);
            // 
            // pHeader
            // 
            this.pHeader.AllowDrop = true;
            this.pHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(391, 32);
            this.pHeader.TabIndex = 0;
            this.pHeader.DragDrop += new System.Windows.Forms.DragEventHandler(this.pHeader_DragDrop);
            this.pHeader.DragEnter += new System.Windows.Forms.DragEventHandler(this.pHeader_DragEnter);
            this.pHeader.Resize += new System.EventHandler(this.pHeader_Resize);
            // 
            // UCFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pBody);
            this.Controls.Add(this.pHeader);
            this.Name = "UCFormView";
            this.Size = new System.Drawing.Size(391, 237);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.Panel pBody;
    }
}
