using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary .CardReader ;
namespace IDR210Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IDR210Reader _Reader = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (_Reader != null) _Reader.Close();
            int port=0;
            if (int.TryParse(textBox1.Text, out port))
            {
                _Reader = new IDR210Reader(port);
                bool open = _Reader.Open();
                MessageBox.Show("打开" + (open ? "成功" : "失败"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_Reader != null) _Reader.Close();
            _Reader = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_Reader != null)
            {
                IdentityCardInfo id = _Reader.ReadInfo();

            }
        }
    }
}
