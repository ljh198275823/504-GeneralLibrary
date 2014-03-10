using System;
using System.IO;
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
    public partial class FrmCreateDataBase : Form
    {
        public FrmCreateDataBase()
        {
            InitializeComponent();
        }

        public SqlClient DBClient { get; set; }

        private string _Path = null;

        private void FrmCreateDataBase_Load(object sender, EventArgs e)
        {
            _Path = DBClient.GetDefaultDataBasePath();

            string[] files = Directory.GetFiles(Application.StartupPath, "*.sql");
            if (files != null && files.Length > 0)
            {
                this.txtSqlPath.Text = Path.Combine(Application.StartupPath, files[0]);
            }
        }

        private void txtDataBase_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtDataBase.Text))
            {
                this.txtDataPath.Text = System.IO.Path.Combine(_Path, txtDataBase.Text.Trim() + ".mdf");
                this.txtLogPath.Text = System.IO.Path.Combine(_Path, txtDataBase.Text.Trim() + "_log.ldf");
            }
        }

        private void btnSqlPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = this.txtSqlPath.Text;
            openFileDialog1.Filter = "sql文件(*.sql)|*.sql|所有文件(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtSqlPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnDataPath_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = this.txtDataPath.Text;
            saveFileDialog1.Filter = "mdf文件|*.mdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtDataPath.Text = saveFileDialog1.FileName;
            }
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = this.txtLogPath.Text;
            saveFileDialog1.Filter = "ldf文件|*.ldf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtLogPath.Text = saveFileDialog1.FileName;
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtDataBase.Text))
            {
                MessageBox.Show("数据库名称不能为空!");
                return false;
            }
            if (string.IsNullOrEmpty(txtSqlPath.Text))
            {
                MessageBox.Show("SQL文件不能为空!");
                return false;
            }
            if (File.Exists(txtSqlPath.Text) == false)
            {
                MessageBox.Show("指定的SQL文件不存在!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDataPath.Text))
            {
                MessageBox.Show("数据库文件不能为空!");
                return false;
            }
            if (string.IsNullOrEmpty(txtLogPath.Text))
            {
                MessageBox.Show("日志文件不能为空!");
                return false;
            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string msg = DBClient.CreateDataBase(txtDataBase.Text.Trim(), txtSqlPath.Text, txtDataPath.Text, txtLogPath.Text);
            if (string.IsNullOrEmpty(msg))
            {
                MessageBox.Show("创建数据库成功!");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
    }
}
