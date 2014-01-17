using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.SQLHelper;

namespace DatabaseToolUI
{
    public partial class FrmSqlLogin : Form
    {
        public FrmSqlLogin()
        {
            InitializeComponent();
        }

        public SqlClient SQLClient { get; set; }

        private void rdUser_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUserID.Enabled = rdUser.Checked;
            this.txtPasswd.Enabled = rdUser.Checked;
            this.txtUserID.BackColor = rdUser.Checked ? Color.White : SystemColors.Control;
            this.txtPasswd.BackColor = rdUser.Checked ? Color.White : SystemColors.Control;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                MessageBox.Show("服务器名称不能为空!");
                return false;
            }
            if (rdUser.Checked && string.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("用户验证模式下用户名不能为空!");
                return false;
            }
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                SQLClient = null;
                bool su = false;
                if (rdSystem.Checked)
                {
                    SQLClient =new SqlClient ( txtServer.Text,true);
                    su = SQLClient.Connect();
                }
                else
                {
                    SQLClient =new SqlClient (txtServer.Text,txtUserID.Text,txtPasswd.Text);
                    su = SQLClient.Connect();
                }
                if (su)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("连接数据库失败！");
                }
            }
        }
    }
}
