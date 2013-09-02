using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.CardReader;

namespace LJH.Park.ZSTTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ZSTReader reader = new ZSTReader();
        private string _IP;

        private void btnInit_Click(object sender, EventArgs e)
        {
            reader.MessageRecieved += new EventHandler<ZSTReaderEventArgs>(reader_MessageRecieved);
            reader.Init();
        }

        private void reader_MessageRecieved(object sender, ZSTReaderEventArgs e)
        {
            Action action = delegate()
            {
                if (e.MessageType == "1")
                {
                    _IP = e.ReaderIP;
                }
                else
                {
                    _IP = null;
                }
                this.txtMessage.Text += "\r\n";
                this.txtMessage.Text += e.RawMessage;
                this.txtMessage.Text += "----------------------------------------";
                this.txtMessage.Refresh();
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void btnComsuption_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_IP))
            {
                reader.Consumption(_IP, 0.01m);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<string> ips = reader.SearchReaders();
        }

        private void btnMessageConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_IP))
            {
                reader.MessageConfirm(_IP);
            }
        }
    }
}
