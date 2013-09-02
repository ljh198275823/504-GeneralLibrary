namespace PrinterTest
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
            this.btnPrintTest1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCounts1 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.btnPrintUntil1 = new System.Windows.Forms.Button();
            this.comTicket = new LJH.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comBill = new LJH.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rdKPM150 = new System.Windows.Forms.RadioButton();
            this.rdX56 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrintTest1
            // 
            this.btnPrintTest1.Location = new System.Drawing.Point(18, 83);
            this.btnPrintTest1.Name = "btnPrintTest1";
            this.btnPrintTest1.Size = new System.Drawing.Size(243, 23);
            this.btnPrintTest1.TabIndex = 0;
            this.btnPrintTest1.Text = "打印测试";
            this.btnPrintTest1.UseVisualStyleBackColor = true;
            this.btnPrintTest1.Click += new System.EventHandler(this.btnPrintTest1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdX56);
            this.groupBox1.Controls.Add(this.rdKPM150);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCounts1);
            this.groupBox1.Controls.Add(this.btnPrintUntil1);
            this.groupBox1.Controls.Add(this.comTicket);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPrintTest1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 150);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "纸票打印机 KPM150测试";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "张";
            // 
            // txtCounts1
            // 
            this.txtCounts1.Location = new System.Drawing.Point(163, 115);
            this.txtCounts1.Name = "txtCounts1";
            this.txtCounts1.Size = new System.Drawing.Size(38, 21);
            this.txtCounts1.TabIndex = 4;
            this.txtCounts1.Text = "10";
            // 
            // btnPrintUntil1
            // 
            this.btnPrintUntil1.Location = new System.Drawing.Point(18, 114);
            this.btnPrintUntil1.Name = "btnPrintUntil1";
            this.btnPrintUntil1.Size = new System.Drawing.Size(138, 23);
            this.btnPrintUntil1.TabIndex = 3;
            this.btnPrintUntil1.Text = "连续打印";
            this.btnPrintUntil1.UseVisualStyleBackColor = true;
            this.btnPrintUntil1.Click += new System.EventHandler(this.btnPrintUntil1_Click);
            // 
            // comTicket
            // 
            this.comTicket.FormattingEnabled = true;
            this.comTicket.Location = new System.Drawing.Point(126, 29);
            this.comTicket.Name = "comTicket";
            this.comTicket.Size = new System.Drawing.Size(135, 20);
            this.comTicket.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "打印机串口号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comBill);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 86);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EPSON小票打印机测试";
            // 
            // comBill
            // 
            this.comBill.FormattingEnabled = true;
            this.comBill.Location = new System.Drawing.Point(105, 29);
            this.comBill.Name = "comBill";
            this.comBill.Size = new System.Drawing.Size(156, 20);
            this.comBill.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "打印机串口号:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBill_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "纸票打印机型号:";
            // 
            // rdKPM150
            // 
            this.rdKPM150.AutoSize = true;
            this.rdKPM150.Checked = true;
            this.rdKPM150.Location = new System.Drawing.Point(126, 61);
            this.rdKPM150.Name = "rdKPM150";
            this.rdKPM150.Size = new System.Drawing.Size(59, 16);
            this.rdKPM150.TabIndex = 7;
            this.rdKPM150.TabStop = true;
            this.rdKPM150.Text = "KPM150";
            this.rdKPM150.UseVisualStyleBackColor = true;
            // 
            // rdX56
            // 
            this.rdX56.AutoSize = true;
            this.rdX56.Location = new System.Drawing.Point(209, 61);
            this.rdX56.Name = "rdX56";
            this.rdX56.Size = new System.Drawing.Size(41, 16);
            this.rdX56.TabIndex = 8;
            this.rdX56.Text = "X56";
            this.rdX56.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 281);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "打印机测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrintTest1;
        private System.Windows.Forms.GroupBox groupBox1;
        private LJH.GeneralLibrary.WinformControl.ComPortComboBox comTicket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private LJH.GeneralLibrary.WinformControl.ComPortComboBox comBill;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrintUntil1;
        private System.Windows.Forms.Label label4;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtCounts1;
        private System.Windows.Forms.RadioButton rdX56;
        private System.Windows.Forms.RadioButton rdKPM150;
        private System.Windows.Forms.Label label3;
    }
}

