using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int count;

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.SetText(string.Format("这是第{0}个窗体", count++));
            this.ucFormView1.AddAForm(frm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.SetText(string.Format("这是第{0}个窗体", count++));
            this.ucFormView2.AddAForm(frm);
        }
    }
}
