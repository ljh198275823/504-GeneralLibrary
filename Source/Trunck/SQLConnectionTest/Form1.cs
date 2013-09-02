using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLConnectionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string GetConnectStrFromInput()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = this.txtServer.Text;
            sb.InitialCatalog = this.txtDataBase.Text;
            sb.IntegratedSecurity = rdSystem.Checked;
            sb.UserID = this.txtUserID.Text;
            sb.Password = this.txtPasswd.Text;
            sb.PersistSecurityInfo = true;
            return sb.ConnectionString;
        }

        private void rdUser_CheckedChanged(object sender, EventArgs e)
        {
            txtUserID.Enabled = rdUser.Checked;
            txtPasswd.Enabled = rdUser.Checked;
        }

        private void btnCreatConStr_Click(object sender, EventArgs e)
        {
            txtConstr.Text = GetConnectStrFromInput();
        }

        private void btnCreateEncryptConStr_Click(object sender, EventArgs e)
        {
            string temp = (new LJH.GeneralLibrary.SoftDog.DTEncrypt()).Encrypt(GetConnectStrFromInput());
            temp = temp.Replace(">", "&gt;");
            temp = temp.Replace("<", "&lt;");
            this.txtConstr.Text = temp;
        }

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = GetConnectStrFromInput();
                    con.Open();
                    con.Close();
                    MessageBox.Show("连接成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUserID.Enabled = rdUser.Checked;
            txtPasswd.Enabled = rdUser.Checked;
            this.txtServer.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtConstr.Text = (new LJH.GeneralLibrary.SoftDog.DTEncrypt()).DSEncrypt(txtConstr.Text);
        }
    }
}
