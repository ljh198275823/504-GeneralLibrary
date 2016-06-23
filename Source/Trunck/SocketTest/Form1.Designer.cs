namespace SocketTest
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
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtRecive = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdTCP = new System.Windows.Forms.RadioButton();
            this.rdUDP = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPort = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtIP = new LJH.GeneralLibrary.WinformControl.UCIPTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdAsc = new System.Windows.Forms.RadioButton();
            this.rdHex = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(30, 84);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(439, 123);
            this.txtSend.TabIndex = 2;
            // 
            // txtRecive
            // 
            this.txtRecive.Location = new System.Drawing.Point(30, 213);
            this.txtRecive.Multiline = true;
            this.txtRecive.Name = "txtRecive";
            this.txtRecive.Size = new System.Drawing.Size(439, 123);
            this.txtRecive.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(477, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(477, 84);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "远程IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "端口号";
            // 
            // rdTCP
            // 
            this.rdTCP.AutoSize = true;
            this.rdTCP.Location = new System.Drawing.Point(16, 12);
            this.rdTCP.Name = "rdTCP";
            this.rdTCP.Size = new System.Drawing.Size(41, 16);
            this.rdTCP.TabIndex = 8;
            this.rdTCP.Text = "TCP";
            this.rdTCP.UseVisualStyleBackColor = true;
            // 
            // rdUDP
            // 
            this.rdUDP.AutoSize = true;
            this.rdUDP.Checked = true;
            this.rdUDP.Location = new System.Drawing.Point(80, 12);
            this.rdUDP.Name = "rdUDP";
            this.rdUDP.Size = new System.Drawing.Size(41, 16);
            this.rdUDP.TabIndex = 9;
            this.rdUDP.TabStop = true;
            this.rdUDP.Text = "UDP";
            this.rdUDP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdUDP);
            this.groupBox1.Controls.Add(this.rdTCP);
            this.groupBox1.Location = new System.Drawing.Point(184, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 34);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // txtPort
            // 
            this.txtPort.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPort.Location = new System.Drawing.Point(78, 52);
            this.txtPort.MaxValue = 65535;
            this.txtPort.MinValue = 0;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "0";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(80, 16);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(219, 28);
            this.txtIP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdAsc);
            this.groupBox2.Controls.Add(this.rdHex);
            this.groupBox2.Location = new System.Drawing.Point(475, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(111, 123);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // rdAsc
            // 
            this.rdAsc.AutoSize = true;
            this.rdAsc.Checked = true;
            this.rdAsc.Location = new System.Drawing.Point(16, 43);
            this.rdAsc.Name = "rdAsc";
            this.rdAsc.Size = new System.Drawing.Size(41, 16);
            this.rdAsc.TabIndex = 9;
            this.rdAsc.TabStop = true;
            this.rdAsc.Text = "ASC";
            this.rdAsc.UseVisualStyleBackColor = true;
            // 
            // rdHex
            // 
            this.rdHex.AutoSize = true;
            this.rdHex.Location = new System.Drawing.Point(16, 12);
            this.rdHex.Name = "rdHex";
            this.rdHex.Size = new System.Drawing.Size(41, 16);
            this.rdHex.TabIndex = 8;
            this.rdHex.Text = "HEX";
            this.rdHex.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 342);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtRecive);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LJH.GeneralLibrary.WinformControl.UCIPTextBox txtIP;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtPort;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.TextBox txtRecive;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdTCP;
        private System.Windows.Forms.RadioButton rdUDP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdAsc;
        private System.Windows.Forms.RadioButton rdHex;
    }
}

