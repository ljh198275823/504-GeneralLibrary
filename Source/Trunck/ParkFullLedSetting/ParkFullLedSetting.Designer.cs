namespace ParkFullLedTest
{
    partial class FrmParkFullLedSetting
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtGreeting = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtGreetingID = new System.Windows.Forms.TextBox();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.txtComport = new System.Windows.Forms.TextBox();
            this.txtVacantID = new System.Windows.Forms.TextBox();
            this.txtVacant = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(192, 206);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtGreeting
            // 
            this.txtGreeting.Location = new System.Drawing.Point(208, 121);
            this.txtGreeting.Name = "txtGreeting";
            this.txtGreeting.Size = new System.Drawing.Size(152, 21);
            this.txtGreeting.TabIndex = 1;
            this.txtGreeting.Text = "欢迎光临一号车场";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "串口号(1-255):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "屏每行字符数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "屏行数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "余位屏ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "欢迎屏显示字符:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "欢迎屏ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "余位屏字符:";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(284, 206);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 11;
            this.btnView.Text = "显示测试";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(19, 73);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(152, 21);
            this.txtWidth.TabIndex = 12;
            this.txtWidth.Text = "8";
            // 
            // txtGreetingID
            // 
            this.txtGreetingID.Location = new System.Drawing.Point(19, 121);
            this.txtGreetingID.Name = "txtGreetingID";
            this.txtGreetingID.Size = new System.Drawing.Size(152, 21);
            this.txtGreetingID.TabIndex = 13;
            this.txtGreetingID.Text = "257";
            // 
            // txtRow
            // 
            this.txtRow.Location = new System.Drawing.Point(208, 73);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(152, 21);
            this.txtRow.TabIndex = 14;
            this.txtRow.Text = "1";
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(208, 24);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(152, 21);
            this.txtBaud.TabIndex = 15;
            this.txtBaud.Text = "9600";
            // 
            // txtComport
            // 
            this.txtComport.Location = new System.Drawing.Point(19, 24);
            this.txtComport.Name = "txtComport";
            this.txtComport.Size = new System.Drawing.Size(152, 21);
            this.txtComport.TabIndex = 16;
            this.txtComport.Text = "1";
            // 
            // txtVacantID
            // 
            this.txtVacantID.Location = new System.Drawing.Point(19, 168);
            this.txtVacantID.Name = "txtVacantID";
            this.txtVacantID.Size = new System.Drawing.Size(152, 21);
            this.txtVacantID.TabIndex = 17;
            this.txtVacantID.Text = "258";
            // 
            // txtVacant
            // 
            this.txtVacant.Location = new System.Drawing.Point(208, 168);
            this.txtVacant.Name = "txtVacant";
            this.txtVacant.Size = new System.Drawing.Size(152, 21);
            this.txtVacant.TabIndex = 18;
            this.txtVacant.Text = "车位余:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 244);
            this.Controls.Add(this.txtVacant);
            this.Controls.Add(this.txtVacantID);
            this.Controls.Add(this.txtComport);
            this.Controls.Add(this.txtBaud);
            this.Controls.Add(this.txtRow);
            this.Controls.Add(this.txtGreetingID);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGreeting);
            this.Controls.Add(this.btnSave);
            this.Name = "Form1";
            this.Text = "停车场余位屏设置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtGreeting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtGreetingID;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.TextBox txtComport;
        private System.Windows.Forms.TextBox txtVacantID;
        private System.Windows.Forms.TextBox txtVacant;
    }
}

