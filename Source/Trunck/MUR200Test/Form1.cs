using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.CardReader;

namespace MUR200Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MUR200CardReader _MUR200 = new MUR200CardReader();
        byte[] key = new byte[] { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };

        private void btnReadID_Click(object sender, EventArgs e)
        {
            string cardid = _MUR200.ReadCard().CardID;
            if (string.IsNullOrEmpty(cardid))
            {
                MessageBox.Show("没有检测到卡片或读卡失败");
            }
            else
            {
                txtCardID.Text = cardid;
            }
        }

        private void btnReadBlock_Click(object sender, EventArgs e)
        {
            string[] strs = txtKey.Text.Split(' ');
            List<byte> keybytes = new List<byte>();
            foreach (string str in strs)
            {
                byte b = 0;
                if (byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out b))
                {
                    keybytes.Add(b);
                }
            }

            ReadCardResult result = _MUR200.ReadSection(txtCardID.Text.Trim(), txtSec.IntergerValue, txtBlock.IntergerValue, 1, keybytes.ToArray(), true, false, false);

            byte[] data = result[txtSec.IntergerValue];
            txtCardID.Text = result.CardID;
            txtReadData.Text = string.Empty;
            if (data != null)
            {
                foreach (byte d in data)
                {
                    txtReadData.Text += d.ToString("X2") + " ";
                }
                MessageBox.Show("读取成功");
            }
            else
            {
                if (result.ResultCode == CardOperationResultCode.CardIDError)
                {
                    MessageBox.Show("读取到的卡号与当前卡号不一致！");
                }
                else
                {
                    MessageBox.Show("读取失败");
                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string[] strs = txtKey.Text.Split(' ');
            List<byte> keybytes = new List<byte>();
            foreach (string str in strs)
            {
                byte b = 0;
                if (byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out b))
                {
                    keybytes.Add(b);
                }
            }

            strs = txtReadData.Text.Split(' ');
            List<byte> data = new List<byte>();
            foreach (string str in strs)
            {
                byte b = 0;
                if (byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out b))
                {
                    data.Add(b);
                }
            }
            if (data.Count > 0)
            {
                CardOperationResultCode result = _MUR200.WriteSection(txtCardID.Text.Trim(), txtSec.IntergerValue, txtBlock.IntergerValue, 1, data.ToArray(), keybytes.ToArray(), true, false, false);
                if (result == CardOperationResultCode.Success)
                {
                    MessageBox.Show("写入成功");
                }
                else if (result == CardOperationResultCode.CardIDError)
                {
                    MessageBox.Show("写入的卡号与当前卡号不一致！");
                }
                else
                {
                    MessageBox.Show("写入失败");

                }
            }

        }
    }
}
