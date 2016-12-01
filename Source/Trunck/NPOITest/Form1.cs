using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPOITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable _SourceTable = null;
            try
            {
                OpenFileDialog saveFileDialog1 = new OpenFileDialog();
                saveFileDialog1.Filter = "Excel文档|*.xls;*.xlsx;*.csv|所有文件(*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog1.FileName;
                    if (System.IO.Path.GetExtension(path).ToUpper() == ".CSV")
                    {
                        _SourceTable = LJH.GeneralLibrary.Core.UI.CsvHelper.Import(path, Encoding.Default);
                    }
                    else
                    {
                        _SourceTable = LJH.GeneralLibrary.Core.UI.NPOIExcelHelper.Import(path);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
