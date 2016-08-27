using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using LJH.GeneralLibrary.Net;

namespace SocketTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private LJHSocket _Socket = null;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_Socket != null) _Socket.Close();
            _Socket = new LJHSocket(txtIP.IP, txtPort.IntergerValue, rdUDP.Checked ? ProtocolType.Udp : ProtocolType.Tcp);
            _Socket.Open();
            _Socket.OnDataArrivedEvent += new LJH.GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
        }

        private void _Socket_OnDataArrivedEvent(object sender, byte[] data)
        {
            Action action = delegate()
            {
                if (data != null && data.Length > 0)
                {
                    if (rdHex.Checked)
                    {
                        txtRecive.Text = LJH.GeneralLibrary.HexStringConverter.HexToString(data, " ");
                    }
                    else
                    {
                        txtRecive.Text = System.Text.ASCIIEncoding.ASCII.GetString(data);
                    }
                }
            };
            this.Invoke(action);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIP.IP = "127.0.0.1";
            txtPort.IntergerValue = 60000;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSend.Text))
            {
                byte[] data = null;
                if (rdHex.Checked)
                {
                    data = LJH.GeneralLibrary.HexStringConverter.StringToHex(txtSend.Text);
                }
                else
                {
                    data = System.Text.ASCIIEncoding.ASCII.GetBytes(txtSend.Text);
                }
                if (_Socket != null && _Socket.IsConnected) _Socket.SendData(data);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Socket != null) _Socket.Close();
        }
    }
}
