using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.Speech;

namespace SpeechTest
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSpeech_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMsg.Text))
            {
                TTSSpeech.Instance.Speek(this.txtMsg.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            TTSSpeech.Instance.Pause();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            TTSSpeech.Instance.Resume();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TTSSpeech.Instance.Skip(5);
        }
    }
}
