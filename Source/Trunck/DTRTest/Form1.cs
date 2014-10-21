using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary;

namespace DTRTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCommport.Init();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CommPort c = new CommPort(cmbCommport.ComPort, 2400);
            c.Open();
            if (c.PortOpened)
            {
                while (true)
                {
                    c.DtrEnable = true;
                    System.Threading.Thread.Sleep(2000);
                    c.DtrEnable = false;
                    System.Threading.Thread.Sleep(2000);
                }
            }
            else
            {
                MessageBox.Show("串口打开失败");
            }
        }
    }
}
