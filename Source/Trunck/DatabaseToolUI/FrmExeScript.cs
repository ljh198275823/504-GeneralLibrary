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
    public partial class FrmExeScript : Form
    {
        public FrmExeScript()
        {
            InitializeComponent();
        }

        public SqlClient DBClient { get; set; }

        private void btnSqlPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "sql文件(*.sql)|*.sql|所有文件(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtSqlPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBox1.SelectedIndex >= 0)
                {
                    DBClient.ChangeDataBase(this.comboBox1.Text);
                    DBClient.ExecuteSQLFile(this.txtSqlPath.Text);
                    MessageBox.Show("执行成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmExeScript_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> allDB = DBClient.GetAllDataBase();
                this.comboBox1.DataSource = allDB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
