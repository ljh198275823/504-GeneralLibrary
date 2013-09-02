using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary .Printer ;

namespace PrinterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量
        private BarCodePrinter _TicketPrinter;
        private EpsonmodePrinter _BillPrinter;
        private bool _IsPrinting;
        private int _PrintCount;
        #endregion

        #region 私有方法
        private BarCodePrinter CreatePrinter(byte comPort)
        {
            BarCodePrinter printer;
            if (rdKPM150.Checked)
            {
                printer = new KPM150BarCodePrinter(comPort, 115200);
            }
            else
            {
                printer = new X56TicketPrinter(comPort, 115200);
            }
            printer.Open();
            return printer;
        }

        private bool PrintTicket(BarCodePrinter printer, string barcode)
        {
            TicketInfo ticket = new TicketInfo();
            ticket.EventDateTime = DateTime.Now;
            ticket.Entrance = "一号入口";
            ticket.CardID = barcode;
            ticket.CompanyName = "太古汇地下停车场";
            ticket.Producter = "广州瑞立德";
            if (printer is KPM150BarCodePrinter)
            {
                return (printer as KPM150BarCodePrinter).PrintTicketOfTyko(ticket);
            }
            else
            {
                return printer.PrintTicket(ticket);
            }
        }

        private void Print_Thread()
        {
            for (int i = 0; i < _PrintCount; i++)
            {
                bool ret = PrintTicket(_TicketPrinter, string.Format("23955{0:D2}", i));
                if (!ret)
                {
                    MessageBox.Show("打印失败，原因是:" + _TicketPrinter.Status.ToString());
                }
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region 事件处理程序
        private void Form1_Load(object sender, EventArgs e)
        {
            this.comTicket.Init();
            this.comTicket.ComPort = 1;
            this.comBill.Init();
            this.comBill.ComPort = 1;
        }

        private void btnPrintTest1_Click(object sender, EventArgs e)
        {
            if (_TicketPrinter == null)
            {
                if (this.comTicket.ComPort > 0)
                {
                    _TicketPrinter = CreatePrinter(this.comTicket.ComPort);
                }
            }
            if (_TicketPrinter != null)
            {
                bool ret = PrintTicket(_TicketPrinter, "2339555");
                if (!ret)
                {
                    MessageBox.Show("打印失败，原因是:" + _TicketPrinter.Status.ToString());
                }
            }
        }

        private void btnPrintUntil1_Click(object sender, EventArgs e)
        {
            if (txtCounts1.IntergerValue <= 0)
            {
                MessageBox.Show("指定的打印张数不正确，请重新输入");
                return;
            }
            if (_TicketPrinter == null)
            {
                if (this.comTicket.ComPort > 0)
                {
                    _TicketPrinter = CreatePrinter(this.comTicket.ComPort);
                }
            }
            if (_TicketPrinter != null)
            {
                _PrintCount = txtCounts1.IntergerValue;
                Print_Thread();
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            if (_BillPrinter == null)
            {
                if (this.comBill.ComPort > 0)
                {
                    _BillPrinter = new EpsonmodePrinter(comBill.ComPort, 9600);
                    _BillPrinter.Open();
                }
            }
            if (_BillPrinter != null)
            {
                ParkBillInfo bill = new ParkBillInfo();
                bill.CardID = "233955501";
                bill.CarPlate = "粤A332QW";
                bill.CompanyName = "小票测试页";
                bill.CarType = "小车";
                bill.ChargeDateTime = DateTime.Now;
                bill.EnterDateTime = DateTime.Now.AddHours(-2).AddMinutes(-15);
                bill.OwnerName = "测试卡";
                bill.Accounts = 15;
                bill.LastAccounts = 10;
                bill.HavePaid = 10;
                bill.Paid = 5;
                bill.TariffType = "节假日收费";
                bill.StationID = "管理中心";
                bill.Operator = "操作员一";
                _BillPrinter.PrintParkBill(bill);
            }
        }
        #endregion
    }
}
