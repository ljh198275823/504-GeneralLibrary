﻿namespace LJH.GeneralLibrary.WinformControl
{
    partial class UCFormViewEx
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
            this.pHeader = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pHeader
            // 
            this.pHeader.AllowDrop = true;
            this.pHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(501, 38);
            this.pHeader.TabIndex = 0;
            this.pHeader.Resize += new System.EventHandler(this.pHeader_Resize);
            // 
            // UCFormViewEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pHeader);
            this.Name = "UCFormViewEx";
            this.Size = new System.Drawing.Size(501, 38);
            this.Load += new System.EventHandler(this.UCFormView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pHeader;
    }
}
