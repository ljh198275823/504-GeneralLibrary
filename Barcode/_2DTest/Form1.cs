using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace _2DTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            qrCodeGraphicControl1.Text = txtContent.Text;
            qrCodeImgControl1.Text = txtContent.Text;
        }

        private void ContrastCal()
        {
            //SolidBrush darkmoduleBrush = qrCodeImgControl1.DarkBrush as SolidBrush;
            //SolidBrush lightmoduleBrush = qrCodeImgControl1.LightBrush as SolidBrush;
            //Color darkmodule = darkmoduleBrush == null ? _darkModule : darkmoduleBrush.Color;
            //Color lightmodule = lightmoduleBrush == null ? _lightModule : lightmoduleBrush.Color;

            //Contrast ctrast = ColorContrast.GetContrast(new FormColor(lightmodule), new FormColor(darkmodule));

            //label1.Text = ctrast.Ratio.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            qrCodeGraphicControl1.Text = txtContent.Text;
            qrCodeImgControl1.Text = txtContent.Text;
        }
    }
}
