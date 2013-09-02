namespace LJH.Park.ZSTTool
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInit = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnComsuption = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnMessageConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(323, 12);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(116, 25);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(317, 308);
            this.txtMessage.TabIndex = 1;
            // 
            // btnComsuption
            // 
            this.btnComsuption.Location = new System.Drawing.Point(323, 80);
            this.btnComsuption.Name = "btnComsuption";
            this.btnComsuption.Size = new System.Drawing.Size(116, 25);
            this.btnComsuption.TabIndex = 2;
            this.btnComsuption.Text = "扣款一分";
            this.btnComsuption.UseVisualStyleBackColor = true;
            this.btnComsuption.Click += new System.EventHandler(this.btnComsuption_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(323, 115);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(116, 25);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "获取所有读卡器";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMessageConfirm
            // 
            this.btnMessageConfirm.Location = new System.Drawing.Point(323, 47);
            this.btnMessageConfirm.Name = "btnMessageConfirm";
            this.btnMessageConfirm.Size = new System.Drawing.Size(116, 25);
            this.btnMessageConfirm.TabIndex = 4;
            this.btnMessageConfirm.Text = "读卡确认";
            this.btnMessageConfirm.UseVisualStyleBackColor = true;
            this.btnMessageConfirm.Click += new System.EventHandler(this.btnMessageConfirm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 311);
            this.Controls.Add(this.btnMessageConfirm);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnComsuption);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnInit);
            this.Name = "Form1";
            this.Text = "中山通读卡程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnComsuption;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnMessageConfirm;
    }
}

