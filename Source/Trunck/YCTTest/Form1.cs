using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.CardReader;

namespace YCTTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private YangChengTongReader reader = null;

        private void eventList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (comPort.ComPort > 0 && comAddress.EntranceAddress > 0)
            {
                reader = new YangChengTongReader(comPort.ComPort, comAddress.EntranceAddress);
                reader.Open();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comAddress.Init();
            this.comPort.Init(true);
        }

        private void btnReadCurCard_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                YangChengTongCardInfo card;
                YangChengTongOperationResult result = reader.ReadCardWithNoLock(out card);
                if (card != null)
                {
                    this.txtBalance.DecimalValue = card.Balance;
                }
                else
                {
                    MessageBox.Show("读羊城通失败");
                }
            }
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            if (reader.State == ReaderState.InWork)
            {
                if (reader.SyncTime() == YangChengTongOperationResult.Success)
                {
                    MessageBox.Show("同步时间成功");
                }
                else
                {
                    MessageBox.Show("同步时间失败");
                };
            }
        }

        private void btnReduceBalance_Click(object sender, EventArgs e)
        {
            if (reader.State == ReaderState.InWork)
            {
                if (reader.CardPay(txtAmount.DecimalValue) == YangChengTongOperationResult.Success)
                {
                    MessageBox.Show("扣款成功");
                }
                else
                {
                    MessageBox.Show("扣款失败");
                }
            }
        }
    }
}
