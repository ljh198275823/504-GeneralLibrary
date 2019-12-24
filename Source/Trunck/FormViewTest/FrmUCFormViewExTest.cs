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
    public partial class FrmUCFormViewExTest : Form
    {
        public FrmUCFormViewExTest()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ucFormViewEx1.AddAForm(new Form2());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.ucFormViewEx1.AddAForm(new Form3());
        }
    }
}
