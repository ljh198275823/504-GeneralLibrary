namespace BX4KTest
{
    partial class FrmAoDaLed
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnShowOnLed = new System.Windows.Forms.Button();
            this.BikeC = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.BikeB = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.BikeA = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.CarC = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.CarB = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.CarA = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtAddress2 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtAddress1 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.comport = new LJH.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.txtAutoFresh = new System.Windows.Forms.CheckBox();
            this.txtInterval = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtFrom = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtTo = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comBaud = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "控制器串口号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "A类";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "B类";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "C类";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "控制板地址";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "小车";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 37;
            this.label5.Text = "电车";
            // 
            // btnShowOnLed
            // 
            this.btnShowOnLed.Location = new System.Drawing.Point(186, 145);
            this.btnShowOnLed.Name = "btnShowOnLed";
            this.btnShowOnLed.Size = new System.Drawing.Size(114, 23);
            this.btnShowOnLed.TabIndex = 39;
            this.btnShowOnLed.Text = "显示";
            this.btnShowOnLed.UseVisualStyleBackColor = true;
            this.btnShowOnLed.Click += new System.EventHandler(this.btnShowOnLed_Click);
            // 
            // BikeC
            // 
            this.BikeC.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BikeC.Location = new System.Drawing.Point(248, 109);
            this.BikeC.MaxValue = 999;
            this.BikeC.MinValue = 0;
            this.BikeC.Name = "BikeC";
            this.BikeC.Size = new System.Drawing.Size(52, 24);
            this.BikeC.TabIndex = 36;
            this.BikeC.Text = "888";
            this.BikeC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BikeB
            // 
            this.BikeB.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BikeB.Location = new System.Drawing.Point(186, 109);
            this.BikeB.MaxValue = 999;
            this.BikeB.MinValue = 0;
            this.BikeB.Name = "BikeB";
            this.BikeB.Size = new System.Drawing.Size(52, 24);
            this.BikeB.TabIndex = 35;
            this.BikeB.Text = "888";
            this.BikeB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BikeA
            // 
            this.BikeA.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BikeA.Location = new System.Drawing.Point(124, 109);
            this.BikeA.MaxValue = 999;
            this.BikeA.MinValue = 0;
            this.BikeA.Name = "BikeA";
            this.BikeA.Size = new System.Drawing.Size(52, 24);
            this.BikeA.TabIndex = 34;
            this.BikeA.Text = "888";
            this.BikeA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CarC
            // 
            this.CarC.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CarC.Location = new System.Drawing.Point(248, 81);
            this.CarC.MaxValue = 999;
            this.CarC.MinValue = 0;
            this.CarC.Name = "CarC";
            this.CarC.Size = new System.Drawing.Size(52, 24);
            this.CarC.TabIndex = 33;
            this.CarC.Text = "888";
            this.CarC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CarB
            // 
            this.CarB.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CarB.Location = new System.Drawing.Point(186, 81);
            this.CarB.MaxValue = 999;
            this.CarB.MinValue = 0;
            this.CarB.Name = "CarB";
            this.CarB.Size = new System.Drawing.Size(52, 24);
            this.CarB.TabIndex = 32;
            this.CarB.Text = "888";
            this.CarB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CarA
            // 
            this.CarA.Font = new System.Drawing.Font("Stencil", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CarA.Location = new System.Drawing.Point(124, 81);
            this.CarA.MaxValue = 999;
            this.CarA.MinValue = 0;
            this.CarA.Name = "CarA";
            this.CarA.Size = new System.Drawing.Size(52, 24);
            this.CarA.TabIndex = 31;
            this.CarA.Text = "888";
            this.CarA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(14, 109);
            this.txtAddress2.MaxValue = 65536;
            this.txtAddress2.MinValue = 0;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(52, 21);
            this.txtAddress2.TabIndex = 30;
            this.txtAddress2.Text = "2";
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(14, 81);
            this.txtAddress1.MaxValue = 65536;
            this.txtAddress1.MinValue = 0;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(52, 21);
            this.txtAddress1.TabIndex = 23;
            this.txtAddress1.Text = "1";
            // 
            // comport
            // 
            this.comport.FormattingEnabled = true;
            this.comport.Location = new System.Drawing.Point(16, 23);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(96, 20);
            this.comport.TabIndex = 22;
            // 
            // txtAutoFresh
            // 
            this.txtAutoFresh.AutoSize = true;
            this.txtAutoFresh.Location = new System.Drawing.Point(14, 192);
            this.txtAutoFresh.Name = "txtAutoFresh";
            this.txtAutoFresh.Size = new System.Drawing.Size(36, 16);
            this.txtAutoFresh.TabIndex = 40;
            this.txtAutoFresh.Text = "每";
            this.txtAutoFresh.UseVisualStyleBackColor = true;
            this.txtAutoFresh.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(52, 190);
            this.txtInterval.MaxValue = 60;
            this.txtInterval.MinValue = 0;
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(32, 21);
            this.txtInterval.TabIndex = 41;
            this.txtInterval.Text = "10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "秒钟更新控制板";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(179, 190);
            this.txtFrom.MaxValue = 100;
            this.txtFrom.MinValue = 0;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(32, 21);
            this.txtFrom.TabIndex = 43;
            this.txtFrom.Text = "1";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(235, 190);
            this.txtTo.MaxValue = 100;
            this.txtTo.MinValue = 0;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(38, 21);
            this.txtTo.TabIndex = 44;
            this.txtTo.Text = "40";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "--";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comBaud
            // 
            this.comBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBaud.FormattingEnabled = true;
            this.comBaud.Items.AddRange(new object[] {
            "9600",
            "57600"});
            this.comBaud.Location = new System.Drawing.Point(177, 23);
            this.comBaud.Name = "comBaud";
            this.comBaud.Size = new System.Drawing.Size(96, 20);
            this.comBaud.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(178, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 47;
            this.label10.Text = "波特率";
            // 
            // FrmAoDaLed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 225);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comBaud);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtAutoFresh);
            this.Controls.Add(this.btnShowOnLed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BikeC);
            this.Controls.Add(this.BikeB);
            this.Controls.Add(this.BikeA);
            this.Controls.Add(this.CarC);
            this.Controls.Add(this.CarB);
            this.Controls.Add(this.CarA);
            this.Controls.Add(this.txtAddress2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAddress1);
            this.Controls.Add(this.comport);
            this.Controls.Add(this.label1);
            this.Name = "FrmAoDaLed";
            this.Text = "澳大户外屏测试";
            this.Load += new System.EventHandler(this.FrmAoDaLed_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtAddress1;
        private LJH.GeneralLibrary.WinformControl.ComPortComboBox comport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtAddress2;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox CarA;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox CarB;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox CarC;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox BikeA;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox BikeB;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox BikeC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnShowOnLed;
        private System.Windows.Forms.CheckBox txtAutoFresh;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtInterval;
        private System.Windows.Forms.Label label6;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtFrom;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comBaud;
        private System.Windows.Forms.Label label10;
    }
}