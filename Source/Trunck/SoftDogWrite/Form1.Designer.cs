namespace SoftDogWrite
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkACS = new System.Windows.Forms.CheckBox();
            this.chkTA = new System.Windows.Forms.CheckBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkPark = new System.Windows.Forms.CheckBox();
            this.chkHost = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReadLic = new System.Windows.Forms.Button();
            this.btnWriteLic = new System.Windows.Forms.Button();
            this.txtPassword = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtUser = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtProjectID = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtDBName = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtMAC = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.chkTYPE_SteelRollInventory_COST = new System.Windows.Forms.CheckBox();
            this.txtDBServer = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "软件类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "项目编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "有效期限";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(120, 163);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(140, 21);
            this.dtStart.TabIndex = 5;
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Location = new System.Drawing.Point(120, 99);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(60, 16);
            this.chkInventory.TabIndex = 7;
            this.chkInventory.Text = "进销存";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkACS
            // 
            this.chkACS.AutoSize = true;
            this.chkACS.Location = new System.Drawing.Point(120, 131);
            this.chkACS.Name = "chkACS";
            this.chkACS.Size = new System.Drawing.Size(48, 16);
            this.chkACS.TabIndex = 10;
            this.chkACS.Text = "门禁";
            this.chkACS.UseVisualStyleBackColor = true;
            // 
            // chkTA
            // 
            this.chkTA.AutoSize = true;
            this.chkTA.Location = new System.Drawing.Point(192, 131);
            this.chkTA.Name = "chkTA";
            this.chkTA.Size = new System.Drawing.Size(48, 16);
            this.chkTA.TabIndex = 11;
            this.chkTA.Text = "考勤";
            this.chkTA.UseVisualStyleBackColor = true;
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWrite.Location = new System.Drawing.Point(367, 331);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(101, 33);
            this.btnWrite.TabIndex = 12;
            this.btnWrite.Text = "写狗";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRead.Location = new System.Drawing.Point(253, 331);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(101, 33);
            this.btnRead.TabIndex = 13;
            this.btnRead.Text = "读狗";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(311, 163);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(140, 21);
            this.dtEnd.TabIndex = 15;
            this.dtEnd.Value = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "--";
            // 
            // chkPark
            // 
            this.chkPark.AutoSize = true;
            this.chkPark.Location = new System.Drawing.Point(257, 131);
            this.chkPark.Name = "chkPark";
            this.chkPark.Size = new System.Drawing.Size(60, 16);
            this.chkPark.TabIndex = 16;
            this.chkPark.Text = "停车场";
            this.chkPark.UseVisualStyleBackColor = true;
            // 
            // chkHost
            // 
            this.chkHost.AutoSize = true;
            this.chkHost.ForeColor = System.Drawing.Color.Red;
            this.chkHost.Location = new System.Drawing.Point(272, 24);
            this.chkHost.Name = "chkHost";
            this.chkHost.Size = new System.Drawing.Size(84, 16);
            this.chkHost.TabIndex = 17;
            this.chkHost.Text = "主机加密狗";
            this.chkHost.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "用户名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "密码";
            // 
            // btnReadLic
            // 
            this.btnReadLic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReadLic.Location = new System.Drawing.Point(13, 331);
            this.btnReadLic.Name = "btnReadLic";
            this.btnReadLic.Size = new System.Drawing.Size(101, 33);
            this.btnReadLic.TabIndex = 22;
            this.btnReadLic.Text = "读LIC";
            this.btnReadLic.UseVisualStyleBackColor = true;
            this.btnReadLic.Click += new System.EventHandler(this.btnReadLic_Click);
            // 
            // btnWriteLic
            // 
            this.btnWriteLic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWriteLic.Location = new System.Drawing.Point(122, 331);
            this.btnWriteLic.Name = "btnWriteLic";
            this.btnWriteLic.Size = new System.Drawing.Size(101, 33);
            this.btnWriteLic.TabIndex = 23;
            this.btnWriteLic.Text = "写LIC";
            this.btnWriteLic.UseVisualStyleBackColor = true;
            this.btnWriteLic.Click += new System.EventHandler(this.btnWriteLic_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPassword.Location = new System.Drawing.Point(313, 201);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(140, 21);
            this.txtPassword.TabIndex = 19;
            // 
            // txtUser
            // 
            this.txtUser.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtUser.Location = new System.Drawing.Point(120, 201);
            this.txtUser.MaxLength = 10;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(140, 21);
            this.txtUser.TabIndex = 18;
            // 
            // txtProjectID
            // 
            this.txtProjectID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtProjectID.Location = new System.Drawing.Point(120, 22);
            this.txtProjectID.MaxValue = 2147483647;
            this.txtProjectID.MinValue = 0;
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(140, 21);
            this.txtProjectID.TabIndex = 4;
            this.txtProjectID.Text = "1000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "数据库名";
            // 
            // txtDBName
            // 
            this.txtDBName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDBName.Location = new System.Drawing.Point(120, 232);
            this.txtDBName.MaxLength = 50;
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(140, 21);
            this.txtDBName.TabIndex = 25;
            // 
            // txtMAC
            // 
            this.txtMAC.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtMAC.Location = new System.Drawing.Point(120, 271);
            this.txtMAC.MaxLength = 50;
            this.txtMAC.Name = "txtMAC";
            this.txtMAC.Size = new System.Drawing.Size(331, 21);
            this.txtMAC.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 274);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "MAC";
            // 
            // chkTYPE_SteelRollInventory_COST
            // 
            this.chkTYPE_SteelRollInventory_COST.AutoSize = true;
            this.chkTYPE_SteelRollInventory_COST.Location = new System.Drawing.Point(192, 99);
            this.chkTYPE_SteelRollInventory_COST.Name = "chkTYPE_SteelRollInventory_COST";
            this.chkTYPE_SteelRollInventory_COST.Size = new System.Drawing.Size(180, 16);
            this.chkTYPE_SteelRollInventory_COST.TabIndex = 28;
            this.chkTYPE_SteelRollInventory_COST.Text = "带成本核算铁皮卷进销存软件";
            this.chkTYPE_SteelRollInventory_COST.UseVisualStyleBackColor = true;
            // 
            // txtDBServer
            // 
            this.txtDBServer.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDBServer.Location = new System.Drawing.Point(313, 232);
            this.txtDBServer.MaxLength = 50;
            this.txtDBServer.Name = "txtDBServer";
            this.txtDBServer.Size = new System.Drawing.Size(140, 21);
            this.txtDBServer.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(266, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "服务器";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(120, 61);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(333, 21);
            this.txtProjectName.TabIndex = 32;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 390);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDBServer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkTYPE_SteelRollInventory_COST);
            this.Controls.Add(this.txtMAC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnWriteLic);
            this.Controls.Add(this.btnReadLic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.chkHost);
            this.Controls.Add(this.chkPark);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.chkTA);
            this.Controls.Add(this.chkACS);
            this.Controls.Add(this.chkInventory);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.txtProjectID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "加密狗写狗工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtProjectID;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkACS;
        private System.Windows.Forms.CheckBox chkTA;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkPark;
        private System.Windows.Forms.CheckBox chkHost;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtUser;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReadLic;
        private System.Windows.Forms.Button btnWriteLic;
        private System.Windows.Forms.Label label7;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtDBName;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtMAC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkTYPE_SteelRollInventory_COST;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtDBServer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtProjectName;
    }
}

