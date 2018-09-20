namespace SQLConnectionTest
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
            this.gpDB = new System.Windows.Forms.GroupBox();
            this.txtPasswd = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtDataBase = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtUserID = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtServer = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdUser = new System.Windows.Forms.RadioButton();
            this.rdSystem = new System.Windows.Forms.RadioButton();
            this.btnCreatConStr = new System.Windows.Forms.Button();
            this.btnCreateEncryptConStr = new System.Windows.Forms.Button();
            this.btnConnectionTest = new System.Windows.Forms.Button();
            this.txtConstr = new System.Windows.Forms.TextBox();
            this.btn解密 = new System.Windows.Forms.Button();
            this.gpDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpDB
            // 
            this.gpDB.Controls.Add(this.txtPasswd);
            this.gpDB.Controls.Add(this.txtDataBase);
            this.gpDB.Controls.Add(this.txtUserID);
            this.gpDB.Controls.Add(this.txtServer);
            this.gpDB.Controls.Add(this.label6);
            this.gpDB.Controls.Add(this.label5);
            this.gpDB.Controls.Add(this.label4);
            this.gpDB.Controls.Add(this.label3);
            this.gpDB.Controls.Add(this.rdUser);
            this.gpDB.Controls.Add(this.rdSystem);
            this.gpDB.Location = new System.Drawing.Point(12, 13);
            this.gpDB.Name = "gpDB";
            this.gpDB.Size = new System.Drawing.Size(436, 100);
            this.gpDB.TabIndex = 6;
            this.gpDB.TabStop = false;
            this.gpDB.Text = "数据库设置";
            // 
            // txtPasswd
            // 
            this.txtPasswd.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPasswd.Location = new System.Drawing.Point(272, 70);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.Size = new System.Drawing.Size(139, 21);
            this.txtPasswd.TabIndex = 3;
            // 
            // txtDataBase
            // 
            this.txtDataBase.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDataBase.Location = new System.Drawing.Point(272, 42);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(139, 21);
            this.txtDataBase.TabIndex = 1;
            // 
            // txtUserID
            // 
            this.txtUserID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtUserID.Location = new System.Drawing.Point(66, 70);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(139, 21);
            this.txtUserID.TabIndex = 2;
            // 
            // txtServer
            // 
            this.txtServer.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtServer.Location = new System.Drawing.Point(66, 42);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(139, 21);
            this.txtServer.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(234, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(222, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "服务器：";
            // 
            // rdUser
            // 
            this.rdUser.AutoSize = true;
            this.rdUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUser.Location = new System.Drawing.Point(175, 20);
            this.rdUser.Name = "rdUser";
            this.rdUser.Size = new System.Drawing.Size(107, 16);
            this.rdUser.TabIndex = 5;
            this.rdUser.Text = "用户名密码验证";
            this.rdUser.UseVisualStyleBackColor = true;
            this.rdUser.CheckedChanged += new System.EventHandler(this.rdUser_CheckedChanged);
            // 
            // rdSystem
            // 
            this.rdSystem.AutoSize = true;
            this.rdSystem.Checked = true;
            this.rdSystem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdSystem.Location = new System.Drawing.Point(41, 20);
            this.rdSystem.Name = "rdSystem";
            this.rdSystem.Size = new System.Drawing.Size(95, 16);
            this.rdSystem.TabIndex = 4;
            this.rdSystem.TabStop = true;
            this.rdSystem.Text = "系统集成验证";
            this.rdSystem.UseVisualStyleBackColor = true;
            this.rdSystem.CheckedChanged += new System.EventHandler(this.rdUser_CheckedChanged);
            // 
            // btnCreatConStr
            // 
            this.btnCreatConStr.Location = new System.Drawing.Point(13, 129);
            this.btnCreatConStr.Name = "btnCreatConStr";
            this.btnCreatConStr.Size = new System.Drawing.Size(96, 23);
            this.btnCreatConStr.TabIndex = 6;
            this.btnCreatConStr.Text = "生成连接字符串";
            this.btnCreatConStr.UseVisualStyleBackColor = true;
            this.btnCreatConStr.Click += new System.EventHandler(this.btnCreatConStr_Click);
            // 
            // btnCreateEncryptConStr
            // 
            this.btnCreateEncryptConStr.Location = new System.Drawing.Point(122, 129);
            this.btnCreateEncryptConStr.Name = "btnCreateEncryptConStr";
            this.btnCreateEncryptConStr.Size = new System.Drawing.Size(148, 23);
            this.btnCreateEncryptConStr.TabIndex = 7;
            this.btnCreateEncryptConStr.Text = "生成加密连接字符串";
            this.btnCreateEncryptConStr.UseVisualStyleBackColor = true;
            this.btnCreateEncryptConStr.Click += new System.EventHandler(this.btnCreateEncryptConStr_Click);
            // 
            // btnConnectionTest
            // 
            this.btnConnectionTest.Location = new System.Drawing.Point(356, 129);
            this.btnConnectionTest.Name = "btnConnectionTest";
            this.btnConnectionTest.Size = new System.Drawing.Size(81, 23);
            this.btnConnectionTest.TabIndex = 8;
            this.btnConnectionTest.Text = "测试连接";
            this.btnConnectionTest.UseVisualStyleBackColor = true;
            this.btnConnectionTest.Click += new System.EventHandler(this.btnConnectionTest_Click);
            // 
            // txtConstr
            // 
            this.txtConstr.Location = new System.Drawing.Point(13, 161);
            this.txtConstr.Multiline = true;
            this.txtConstr.Name = "txtConstr";
            this.txtConstr.Size = new System.Drawing.Size(435, 113);
            this.txtConstr.TabIndex = 9;
            // 
            // btn解密
            // 
            this.btn解密.Location = new System.Drawing.Point(283, 129);
            this.btn解密.Name = "btn解密";
            this.btn解密.Size = new System.Drawing.Size(61, 23);
            this.btn解密.TabIndex = 10;
            this.btn解密.Text = "解密";
            this.btn解密.UseVisualStyleBackColor = true;
            this.btn解密.Click += new System.EventHandler(this.btn解密_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 283);
            this.Controls.Add(this.btn解密);
            this.Controls.Add(this.txtConstr);
            this.Controls.Add(this.btnConnectionTest);
            this.Controls.Add(this.btnCreateEncryptConStr);
            this.Controls.Add(this.btnCreatConStr);
            this.Controls.Add(this.gpDB);
            this.Name = "Form1";
            this.Text = "数据库连接字符串生成器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gpDB.ResumeLayout(false);
            this.gpDB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpDB;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtPasswd;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtDataBase;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtUserID;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdUser;
        private System.Windows.Forms.RadioButton rdSystem;
        private System.Windows.Forms.Button btnCreatConStr;
        private System.Windows.Forms.Button btnCreateEncryptConStr;
        private System.Windows.Forms.Button btnConnectionTest;
        private System.Windows.Forms.TextBox txtConstr;
        private System.Windows.Forms.Button btn解密;
    }
}

