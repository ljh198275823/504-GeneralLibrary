using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 表示X56型号的纸票打印机
    /// </summary>
    public class X56TicketPrinter :BarCodePrinter
    {
        #region 构造函数
        public X56TicketPrinter(byte commport, int baudRate)
            : base(commport, baudRate)
        {
        }
        #endregion

        #region 重写基类方法
        /// <summary>
        /// 设置打印模式（普通，粗体，下划线)
        /// </summary>
        /// <param name="time"></param>
        public virtual void SetPrintMode(PrintMode mode)
        {
            if ((mode & PrintMode.Bold) == PrintMode.Bold)
            {
                CommPort.SendData(new byte[] { 0x1b, 0x45, 0x01 });
            }
            else
            {
                CommPort.SendData(new byte[] { 0x1b, 0x45, 0x00 });
            }
            if ((mode & PrintMode.UnderLine) == PrintMode.UnderLine)
            {
                CommPort.SendData(new byte[] { 0x1b, 0x2d, 0x01 });
            }
            else
            {
                CommPort.SendData(new byte[] { 0x1b, 0x2d, 0x00 });
            }
        }

        public override void PrintStatusRequest()
        {
            byte[] data = new byte[] { 0x1d, 0x61, 0x04 };
            CommPort.SendData(data);
        }

        public override void CutDown()
        {
            CommPort.SendData(new byte[] { 0x1B, 0xF0, 0x06, 0x01, 0x02 });
        }

        /// <summary>
        /// X56打印机只能将字体的长或高放大一倍，参数为O时表示不放大，否则放大一倍
        /// </summary>
        /// <param name="widthTime"></param>
        /// <param name="heightTime"></param>
        public override void EnlargeFont(byte widthTime, byte heightTime)
        {
            byte n = 2;
            if (widthTime > 0) n += 0x20;
            if (heightTime > 0) n += 0x10;
            CommPort.SendData(new byte[] { 0x1b, 0x21, n });
        }
        #endregion

        #region 事件处理
        protected override void OnPrinterStatusPacket(object sender, byte[] data)
        {
            //base.OnPrinterStatusPacket(sender, data);
            this.Status = PrinterStatus.Ok;
        }
        #endregion

        #region 实现IBarCodePrinter 接口
        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="barcode"></param>
        public override  void PrintBarCode(string barcode)
        {
            byte[] body = System.Text.ASCIIEncoding.ASCII.GetBytes(barcode);
            byte[] head = new byte[] { 0x1d, 0x6b, 0x49, (byte)(body.Length + 1), 0x67 };

            byte[] data = new byte[body.Length + head.Length];
            head.CopyTo(data, 0);
            body.CopyTo(data, head.Length);
            CommPort.SendData(data);
        }

        /// <summary>
        /// 启用或禁用条码标题
        /// </summary>
        /// <param name="enable"></param>
        public override  void SetBarCodeSubtitle(bool enable)
        {
            // 0x1B, 0xF0, 0x08, 0x01, [n]
            if (enable)
            {
                CommPort.SendData(new byte[] { 0x1B, 0xF0, 0x08, 0x01, 0x05 });
            }
            else
            {
                CommPort.SendData(new byte[] { 0x1B, 0xF0, 0x08, 0x01, 0x04 });
            }
        }

        /// <summary>
        /// 设置条码的放大系数(1-4)
        /// </summary>
        public override  void SetBarCodeWidthMagnification(byte magnification)
        {
            if (magnification >= 1 && magnification <= 4)
            {
                CommPort.SendData(new byte[] { 0x1d, 0x77, magnification });
            }
        }

        /// <summary>
        /// 设置条码高度以点为单位
        /// </summary>
        public override  void SetBarCodeHeight(byte height)
        {
            CommPort.SendData(new byte[] { 0x1d, 0x68, height });    //设置条码的高度以点为单位 1d 68 55
        }

        /// <summary>
        /// 打印纸票
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public override  bool PrintTicket(TicketInfo ticket)
        {
            if (CommPort != null && CommPort.PortOpened)
            {
                if (ticket != null)
                {
                    InitPrinter();
                    QueryStatus();
                    if (Status == PrinterStatus.Ok)
                    {
                        SetPrintMode(PrintMode.Default);
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintAndFeedIn(1);
                        PrintChineseLine(ticket.CompanyName);

                        EnlargeFont(0, 0);
                        PrintAndFeedIn(1);
                        SetBarCodeHeight(0x80);
                        SetBarCodeWidthMagnification(1);
                        SetBarCodeSubtitle(true);
                        PrintBarCode(ticket.CardID);
                        PrintAndFeedIn(2);

                        SetJustification(Justification.left);
                        SetLeftMargin((byte)20);
                        PrintChineseLine("入场:" + ticket.EventDateTime.ToString("yyyy-MM-dd HH:mm"));
                        PrintChineseLine("通道:" + ticket.Entrance);
                        PrintChineseLine("厂商:" + ticket.Producter);
                        PrintAndFeedIn(1);
                        SetLeftMargin(0);

                        SetJustification(Justification.center);
                        PrintChineseLine("▲");
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintChineseLine("先到收费处交费，再取车出场");

                        EnlargeFont(0, 0);
                        PrintAndFeedIn(1);

                        PrintAscLine("--------------------");
                        PrintChineseLine("出场凭证,请勿折损!");
                        PrintChineseLine("出口回收：手握此处，正面朝上塞卡");
                        PrintAndFeedIn(1);
                        //切纸之前用一秒时间等待打印机状态变化,如果状态OK就表示之前的打印都是成功的
                        Thread.Sleep(1000);
                        QueryStatus();

                        if (Status == PrinterStatus.Ok)
                        {
                            CutDown();
                        }
                    }
                }
            }
            return Status == PrinterStatus.Ok;
        }
        #endregion
    }
}

