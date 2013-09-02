using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 指以EPSON命令模式进行打印的小票打印机(一般通过串口发送命令)
    /// </summary>
    public class EpsonmodePrinter
    {
        #region 构造函数
        public EpsonmodePrinter(byte commport, int baudRate)
        {
            if (commport > 0)
            {
                Action action = delegate()
                {
                    CommPort = new CommPort(commport, baudRate);
                    CommPort.OnDataArrivedEvent += new DataArrivedDelegate(CommPort_OnDataArrivedEvent);
                };

                //说明,用另一个线程创建COMM组件,则此组件的onComm事件就会在非UI的线程上执行,
                Thread t = new Thread(new ThreadStart(action));
                t.Start();
                t.Join();
            }
        }
        #endregion

        #region 私有变量
        protected CommPort CommPort = null;
        private PrinterStatus _Status;
        private AutoResetEvent _GetStatusEvent = new AutoResetEvent(false);
        #endregion

        #region 私有方法
        private void CommPort_OnDataArrivedEvent(object sender, byte[] data)
        {
            OnPrinterStatusPacket(sender, data);
            _GetStatusEvent.Set();
        }
        /// <summary>
        /// 用于处理收到的打印机状态数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        protected virtual void OnPrinterStatusPacket(object sender, byte[] data)
        {
            if (data.Length == 1)
            {
                if ((data[0] & 0x60) == 0x60)
                {
                    Status = PrinterStatus.PaperAbsent;
                }
                else if ((data[0] & 0x10) == 0x10)
                {
                    if (EnablePaperWillEndSensor) //纸将尽当成缺纸
                    {
                        Status = PrinterStatus.PaperAbsent;
                    }
                    else
                    {
                        Status = PrinterStatus.Ok;
                    }
                }
                else
                {
                    Status = PrinterStatus.Ok;
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取打印机状态
        /// </summary>
        public PrinterStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置打印机是否安装有纸检测探头
        /// </summary>
        public bool EnablePaperWillEndSensor { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开打印机
        /// </summary>
        public virtual void Open()
        {
            if (CommPort != null && !CommPort.PortOpened)
            {
                CommPort.Open();
                if (!CommPort.PortOpened)
                {
                    Status = PrinterStatus.OffLine;
                }
            }
        }

        /// <summary>
        /// 关闭打印机，释放资源
        /// </summary>
        public virtual void Close()
        {
            CommPort.Close();
        }

        /// <summary>
        /// 初始化打印机
        /// </summary>
        public virtual void InitPrinter()
        {
            CommPort.SendData(new byte[] { 0x1b, 0x40 });
        }

        /// <summary>
        /// 打印并将纸向前推进N行
        /// </summary>
        /// <param name="lines"></param>
        public virtual void PrintAndFeedIn(byte lines)
        {
            CommPort.SendData(new byte[] { 0x1b, 0x64, lines });
        }

        /// <summary>
        /// 设置左边距(针式打印机不支持此命令)
        /// </summary>
        /// <param name="margin"></param>
        public virtual void SetLeftMargin(byte margin)
        {
            CommPort.SendData(new byte[] { 0x1d, 0x4c, margin, 0x0 });
        }

        /// <summary>
        /// 切纸
        /// </summary>
        public virtual void CutDown()
        {
            CommPort.SendData(new byte[] { 0x1d, 0x56, 0x0 });
        }

        /// <summary>
        /// 设置打印模式（普通，粗体，下划线)
        /// </summary>
        /// <param name="time"></param>
        public virtual void SetPrintMode(PrintMode mode)
        {
            CommPort.SendData(new byte[] { 0x1b, 0x21, (byte)mode });
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="j"></param>
        public virtual void SetJustification(Justification j)
        {
            byte[] data = new byte[] { 0x1b, 0x61, (byte)j };
            CommPort.SendData(data);
        }

        /// <summary>
        /// 设置打印颜色(目前只支持黑色和红色)
        /// </summary>
        /// <param name="color"></param>
        public virtual void SetPrintColor(Color color)
        {
            byte[] data = null;
            if (color == Color.Red)
            {
                data = new byte[] { 0x1b, 0x72, 0x1 };
            }
            else
            {
                data = new byte[] { 0x1b, 0x72, 0x0 };
            }
            CommPort.SendData(data);
        }

        /// <summary>
        /// 放大字体
        /// </summary>
        /// <param name="widthTime">字长放大倍数,为0时表示不放大,字长实际大小为普通字体的(2*widthTime)倍</param>
        /// <param name="heightTime">字宽放大倍数,为0时表示不放大,字宽实际大小为普通字体的(2*widthTime)倍</param>
        public virtual void EnlargeFont(byte widthTime, byte heightTime)
        {
            byte time = (byte)((widthTime << 4) + heightTime);
            CommPort.SendData(new byte[] { 0x1d, 0x21, time });
        }

        /// <summary>
        /// 打印一行中文字串
        /// </summary>
        /// <param name="msg"></param>
        public virtual void PrintChineseLine(string msg)
        {
            byte[] data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(msg);
            CommPort.SendData(data);
            PrintAndFeedIn(1);
        }

        /// <summary>
        /// 打印一行ASC字符
        /// </summary>
        /// <param name="msg"></param>
        public virtual void PrintAscLine(string msg)
        {
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(msg);
            CommPort.SendData(data);
            PrintAndFeedIn(1);
        }

        /// <summary>
        /// 打印位图
        /// </summary>
        /// <param name="img"></param>
        public virtual void PrintBitMap(Bitmap img)
        {
            List<byte> data = new List<byte>();
            data.AddRange(new byte[] { 0x1d, 0x76, 0x30, 0x0 });
        }

        /// <summary>
        /// 查询打印机状态,执行此方法后可以通过Status属性查看打印机状态
        /// </summary>
        public virtual void QueryStatus()
        {
            _GetStatusEvent.Reset();
            PrintStatusRequest();
            if (!_GetStatusEvent.WaitOne(2000, false))
            {
                Status = PrinterStatus.OffLine;
            }
        }

        /// <summary>
        /// 向打印机发送状态查询命令
        /// </summary>
        public virtual void PrintStatusRequest()
        {
            byte[] data = new byte[] { 0x10, 0x04, 0x04 };
            CommPort.SendData(data);
        }

        /// <summary>
        /// 打印停车场收费小票
        /// </summary>
        /// <param name="bill"></param>
        public void PrintParkBill(ParkBillInfo bill)
        {
            if (bill != null)
            {
                InitPrinter();
                QueryStatus();
                if (Status == PrinterStatus.Ok)
                {
                    SetJustification(Justification.center);
                    SetPrintMode(PrintMode.Bold);
                    PrintChineseLine(bill.CompanyName);

                    SetPrintMode(PrintMode.Default);
                    SetJustification(Justification.left);
                    SetPrintMode(PrintMode.Default);
                    PrintAndFeedIn(1);
                    PrintChineseLine("     卡号:  " + bill.CardID);
                    PrintChineseLine("  车牌号码:  " + bill.CarPlate);
                    PrintChineseLine("  入场时间:  " + bill.EnterDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    PrintChineseLine("  收费时间:  " + bill.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    PrintChineseLine("  停车时长:  " + bill.ParkInterval);
                    PrintChineseLine("  收费车型:  " + bill.CarType);
                    PrintChineseLine("  收费类型:  " + bill.TariffType);
                    PrintChineseLine("  应收费用:  " + bill.Accounts + " 元");
                    PrintChineseLine("  已经收取:  " + bill.HavePaid + " 元");
                    SetPrintColor(Color.Red);
                    PrintChineseLine("  本次收取:  " + bill.Paid + " 元");
                    SetPrintColor(Color.Black);
                    PrintChineseLine("  收费站点:  " + bill.StationID);
                    PrintChineseLine("  收费人员:  " + bill.Operator);
                    PrintAndFeedIn(8);
                    CutDown();
                }
            }
        }
        #endregion
    }
}

