using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class UCIPTextBox : UserControl
    {
        public UCIPTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IP
        {
            get
            {
                byte b;
                if (!byte.TryParse(ip1.Text, out b) || !byte.TryParse(ip2.Text, out b) || !byte.TryParse(ip3.Text, out b) || !byte.TryParse(ip4.Text, out b))
                {
                    return "0.0.0.0";
                }
                else
                {
                    return string.Format("{0}.{1}.{2}.{3}", byte.Parse(ip1.Text), byte.Parse(ip2.Text), byte.Parse(ip3.Text), byte.Parse(ip4.Text));
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] ip;
                    ip = value.Split('.');
                    this.ip1.Text = ip[0];
                    this.ip2.Text = ip[1];
                    this.ip3.Text = ip[2];
                    this.ip4.Text = ip[3];
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var ip = LJH.GeneralLibrary.Net.NetTool.GetLocalIP();
            if (ip != null)
            {
                var strIP = ip.ToString();
                if (!string.IsNullOrEmpty(strIP))
                {
                    string[] temp = strIP.Split('.');
                    if (temp.Length == 4)
                    {
                        IP = string.Format("{0}.{1}.{2}.1", temp[0], temp[1], temp[2]);
                        ip4.SelectAll();
                    }
                }
            }
        }

        private void ip1_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                byte b = 0;
                string txt = tb.Text;
                if (txt.Length == 3 && byte.TryParse(txt, out b))
                {
                    if (tb == this.ip1)
                    {
                        ip2.Focus();
                        ip2.SelectAll();
                    }
                    else if (tb == this.ip2)
                    {
                        ip3.Focus();
                        ip3.SelectAll();
                    }
                    else if (tb == this.ip3)
                    {
                        ip4.Focus();
                        ip4.SelectAll();
                    }
                }
            }
        }
    }
}
