using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt0.DecimalValue = txtNone.DecimalValue;
            txt1.DecimalValue = txtNone.DecimalValue;
            txt2.DecimalValue = txtNone.DecimalValue;
            txt3.DecimalValue = txtNone.DecimalValue;
        }
    }
}
