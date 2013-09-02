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
    public partial class FrmAoDaLed : Form
    {
        public FrmAoDaLed()
        {
            InitializeComponent();
        }

        #region 私有变量
        private BX4KLEDControler _bx4kController;
        #endregion

        private List<BX4KDynamicArea> GetCarLedAreas()
        {
            int offset = 108;
            List<BX4KDynamicArea> areas = new List<BX4KDynamicArea>();
            BX4KDynamicArea carA = new BX4KDynamicArea()
            {
                X = (short)(0 + offset),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = false,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + CarA.IntergerValue.ToString("D3")
            };
            areas.Add(carA);
            BX4KDynamicArea carB = new BX4KDynamicArea()
            {
                X = (short)(56 / 8 + offset),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = false,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + CarB.IntergerValue.ToString("D3")
            };
            areas.Add(carB);
            BX4KDynamicArea carC = new BX4KDynamicArea()
            {
                X = (short)(112 / 8 + offset),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = false,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + CarC.IntergerValue.ToString("D3")
            };
            areas.Add(carC);
            return areas;
        }

        private List<BX4KDynamicArea> GetBikeLedAreas()
        {
            int offset =108;
            List<BX4KDynamicArea> areas = new List<BX4KDynamicArea>();
            BX4KDynamicArea bikeA = new BX4KDynamicArea()
            {
                X = (short)(offset + 0),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = true,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + BikeA.IntergerValue.ToString("D3")
            };
            areas.Add(bikeA);
            BX4KDynamicArea bikeB = new BX4KDynamicArea()
            {
                X = (short)(offset + 56 / 8),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = true,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + BikeB.IntergerValue.ToString("D3")
            };
            areas.Add(bikeB);
            BX4KDynamicArea bikeC = new BX4KDynamicArea()
            {
                X = (short)(offset + 112 / 8),
                Y = 0,
                Width = 48 / 8,
                Height = 16,
                SingleLine = false,
                NewLine = true,
                FontCode = 0x1,
                DisplayMode = BX4KDisplayMode.Static,
                StayTime = 0xA,
                Text = @"\FE000\C2" + BikeC.IntergerValue.ToString("D3")
            };
            areas.Add(bikeC);
            return areas;
        }

        private void btnShowOnLed_Click(object sender, EventArgs e)
        {
            if (comport.ComPort > 0)
            {
                if (_bx4kController != null) _bx4kController.Close();
                int baud = comBaud.SelectedIndex == 0 ? 9600 : 57600;
                _bx4kController = new BX4KLEDControler(comport.ComPort, baud);
                if (!_bx4kController.Open())
                {
                    MessageBox.Show("串口打开失败");
                    return;
                }
                if (txtAddress1.IntergerValue > 0)
                {
                    List<BX4KDynamicArea> areas = GetCarLedAreas();
                    _bx4kController.DynamicDisplay((short)txtAddress1.IntergerValue, 0x51, true, areas);
                    System.Threading.Thread.Sleep(100);
                }
                if (txtAddress2.IntergerValue > 0)
                {
                    List<BX4KDynamicArea> areas = GetBikeLedAreas();
                    _bx4kController.DynamicDisplay((short)txtAddress2.IntergerValue, 0x51, true, areas);
                }
            }
        }

        private void FrmAoDaLed_Load(object sender, EventArgs e)
        {
            this.comport.Init();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.txtAutoFresh.Checked && txtInterval.IntergerValue > 0)
            {
                timer1.Interval = txtInterval.IntergerValue * 1000;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comport.ComPort > 0)
            {
                int baud = comBaud.SelectedIndex == 0 ? 9600 : 57600;
                if (_bx4kController != null) _bx4kController.Close();
                _bx4kController = new BX4KLEDControler(comport.ComPort, baud);
                if (!_bx4kController.Open())
                {
                    MessageBox.Show("串口打开失败");
                    return;
                }
                for (int i = txtFrom.IntergerValue; i < txtTo.IntergerValue; i += 2)
                {
                    List<BX4KDynamicArea> areas = GetCarLedAreas();
                    foreach (BX4KDynamicArea area in areas)
                    {
                        Random rd = new Random();
                        int vacant = rd.Next(100, 800);
                        area.Text = @"\FE000\C3" + vacant.ToString("D3");
                    }
                    _bx4kController.DynamicDisplay((short)i, 0x51, true, areas);
                    System.Threading.Thread.Sleep(500);

                    areas = GetBikeLedAreas();
                    foreach (BX4KDynamicArea area in areas)
                    {
                        Random rd = new Random();
                        int vacant = rd.Next(100, 800);
                        area.Text = @"\FE000\C3" + vacant.ToString("D3");
                    }
                    _bx4kController.DynamicDisplay((short)(i + 1), 0x51, true, areas);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}
