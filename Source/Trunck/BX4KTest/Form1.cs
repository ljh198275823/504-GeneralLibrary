using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.LED;

namespace BX4KTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量
        private BX4KLEDControler _bx4kController;
        #endregion

        #region 私有方法
        private void CreateBX4KController(byte commport)
        {

        }

        private bool CheckInput()
        {
            return true;
        }

        private BX4KDynamicArea GetAreaFromInput()
        {
            BX4KDynamicArea area = new BX4KDynamicArea();
            area.X = (short)(numX.Value / 8);
            area.Y = (short)numY.Value;
            area.Width = (short)(numWidth.Value / 8);
            area.Height = (short)numHeight.Value;
            area.SingleLine = chkSingleLine.Checked;
            area.NewLine = chkNewLine.Checked;
            area.FontCode = 0x1;
            area.DisplayMode = BX4KDisplayMode.Static;
            area.StayTime = 0xA;
            area.Text = txtContent.Text;
            return area;
        }
        #endregion

        #region 事件处理程序
        private void Form1_Load(object sender, EventArgs e)
        {
            this.comport.Init();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (_bx4kController != null) _bx4kController.Close();
            if (!CheckInput()) return;
            _bx4kController = new BX4KLEDControler(comport.ComPort, 57600);
            if (!_bx4kController.Open())
            {
                MessageBox.Show("串口打开失败");
                return;
            }
            BX4KDynamicArea area = GetAreaFromInput();
            List<BX4KDynamicArea> areas = new List<BX4KDynamicArea>();
            areas.Add(area);
            _bx4kController.DynamicDisplay((short)txtAddress.IntergerValue, 0x51, true, areas);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_bx4kController != null) _bx4kController.Close();
            this.Close();
        }
        #endregion
    }
}
