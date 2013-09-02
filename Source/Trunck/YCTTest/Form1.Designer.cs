namespace YCTTest
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
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnReadCurCard = new System.Windows.Forms.Button();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.btnReduceBalance = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBalance = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtAmount = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.comAddress = new LJH.GeneralLibrary.WinformControl.AddressComboBox(this.components);
            this.comPort = new LJH.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(33, 60);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(129, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnReadCurCard
            // 
            this.btnReadCurCard.Location = new System.Drawing.Point(33, 100);
            this.btnReadCurCard.Name = "btnReadCurCard";
            this.btnReadCurCard.Size = new System.Drawing.Size(129, 23);
            this.btnReadCurCard.TabIndex = 1;
            this.btnReadCurCard.Text = "读取读卡器上的卡片";
            this.btnReadCurCard.UseVisualStyleBackColor = true;
            this.btnReadCurCard.Click += new System.EventHandler(this.btnReadCurCard_Click);
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(175, 60);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(138, 23);
            this.btnSetTime.TabIndex = 6;
            this.btnSetTime.Text = "同步时间";
            this.btnSetTime.UseVisualStyleBackColor = true;
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnReduceBalance
            // 
            this.btnReduceBalance.Location = new System.Drawing.Point(31, 141);
            this.btnReduceBalance.Name = "btnReduceBalance";
            this.btnReduceBalance.Size = new System.Drawing.Size(129, 23);
            this.btnReduceBalance.TabIndex = 8;
            this.btnReduceBalance.Text = "扣款";
            this.btnReduceBalance.UseVisualStyleBackColor = true;
            this.btnReduceBalance.Click += new System.EventHandler(this.btnReduceBalance_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "串口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "地址";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(181, 101);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(132, 21);
            this.txtBalance.TabIndex = 15;
            this.txtBalance.Text = "0.00";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(181, 143);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(132, 21);
            this.txtAmount.TabIndex = 10;
            this.txtAmount.Text = "0.00";
            // 
            // comAddress
            // 
            this.comAddress.FormattingEnabled = true;
            this.comAddress.Location = new System.Drawing.Point(175, 21);
            this.comAddress.Name = "comAddress";
            this.comAddress.Size = new System.Drawing.Size(73, 20);
            this.comAddress.TabIndex = 5;
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(64, 21);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(70, 20);
            this.comPort.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 175);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnReduceBalance);
            this.Controls.Add(this.btnSetTime);
            this.Controls.Add(this.comAddress);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.btnReadCurCard);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnReadCurCard;
        private LJH.GeneralLibrary.WinformControl.ComPortComboBox comPort;
        private LJH.GeneralLibrary.WinformControl.AddressComboBox comAddress;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.Button btnReduceBalance;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txtBalance;
    }
}

