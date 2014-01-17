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
    public partial class FrmCommand : Form
    {
        public FrmCommand()
        {
            InitializeComponent();
        }

        public SqlClient DBClient { get; set; }

        private void btnCreatDB_Click(object sender, EventArgs e)
        {
            FrmCreateDataBase frm = new FrmCreateDataBase();
            frm.DBClient = DBClient;
            frm.ShowDialog();
        }

        private void FrmCommand_Load(object sender, EventArgs e)
        {
            FrmSqlLogin frm = new FrmSqlLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DBClient = frm.SQLClient;
                this.Show();
                this.Activate();
            }
            else
            {
                this.Close();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            FrmBackUp frm = new FrmBackUp();
            frm.DBClient = DBClient;
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmExeScript frm = new FrmExeScript();
            frm.DBClient = DBClient;
            frm.ShowDialog();
        }
    }
}
