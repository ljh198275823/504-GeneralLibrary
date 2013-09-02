using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary .LED ;

namespace DesktopLedTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IParkingLed _LED;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comPortComboBox1.Init();
            this.txtStorage.Text = "中央收费处";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (_LED != null) _LED.Close();
            if (this.comPortComboBox1.ComPort > 0 && this.comboBox1.SelectedIndex >= 0)
            {
                if (this.comboBox1.SelectedIndex == 0)
                {
                    _LED = new ZhongKuangLed(this.comPortComboBox1.ComPort);
                }
                else
                {
                    _LED = new YanseDesktopLed(this.comPortComboBox1.ComPort);
                }
                _LED.Open();
                _LED.PermanentSentence = txtStorage.Text;
                _LED.DisplayMsg(txtTemp.Text);
            }
        }
    }
}
