using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 条码打印机,现型号为意大利CUSTOM
    /// </summary>
    public class KPM150BarCodePrinter : BarCodePrinter
    {
        #region 构造函数
        public KPM150BarCodePrinter(byte commport)
            : base(commport, 115200)
        {
        }

        public KPM150BarCodePrinter(byte commport, int baudRate)
            : base(commport, baudRate)
        {

        }
        #endregion

        #region 重写基类方法
        public override void CutDown()
        {
            CommPort.SendData(new byte[] { 0x1b, 0x69 });
        }
        #endregion

        #region 实现 BarCodePrinter 接口
        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="barcode"></param>
        public override  void PrintBarCode(string barcode)
        {
            byte[] body = System.Text.ASCIIEncoding.ASCII.GetBytes(barcode);
            byte[] head = new byte[] { 0x1d, 0x6b, 0x49, (byte)(body.Length + 2), 0x7b, 0x41 };   // 73=0x49=Code128
            byte[] data = new byte[body.Length + head.Length];
            head.CopyTo(data, 0);
            body.CopyTo(data, head.Length);
            CommPort.SendData(data);
        }

        /// <summary>
        /// 启用或禁用条码标题
        /// </summary>
        public override void SetBarCodeSubtitle(bool enable)
        {
            if (enable)
            {
                CommPort.SendData(new byte[] { 0x1d, 0x48, 0x02 }); //设置条码字符的位置,2表示在下,3表示上下都有,1表示在上,0表示无
            }
            else
            {
                CommPort.SendData(new byte[] { 0x1d, 0x48, 0x00 }); //设置条码字符的位置,2表示在下,3表示上下都有,1表示在上,0表示无
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
        /// 打印纸票,
        /// </summary>
        /// <param name="ticket"></param>
        public override bool PrintTicket(TicketInfo ticket)
        {
            if (CommPort != null && CommPort.PortOpened)
            {
                if (ticket != null)
                {
                    InitPrinter();
                    QueryStatus();
                    if (Status == PrinterStatus.Ok)
                    {
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintChineseLine(ticket.CompanyName);

                        EnlargeFont(0, 0);
                        SetBarCodeWidthMagnification(2);
                        SetBarCodeSubtitle(true);
                        SetBarCodeHeight(0x80);
                        PrintBarCode(ticket.CardID);
                        PrintAndFeedIn(1);

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

        #region 公共方法
        /// <summary>
        /// 以SVEL模式打印纸票
        /// </summary>
        /// <param name="ticket"></param>
        public bool PrintTicketBySVEL(TicketInfo ticket)
        {
            if (CommPort != null && CommPort.PortOpened)
            {
                if (ticket != null)
                {
                    InitPrinter();
                    QueryStatus();
                    if (Status == PrinterStatus.Ok)
                    {
                        //标头部分
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintChineseLine(ticket.CompanyName);
                        EnlargeFont(0, 0);

                        //条码和入场时间，入场口，供应商部分 用SVEL指令打
                        CommPort.SendData(new byte[] { 0x1c, 0x3c, 0x53, 0x56, 0x45, 0x4c, 0x3e });  //设置成SVEL打印模式
                        string cmd = null;
                        cmd = "<LHT380,400,0,40>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F50><HW1,1><RC45,20>入场时间：");
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F5><HW1,1><RC45,175>" + ticket.EventDateTime.ToString("yyyy-MM-dd HH:mm"));
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F50><HW1,1><RC80,20>入场通道：" + ticket.Entrance);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<X2,H><RC120,20><NP20>*{0}*", ticket.CardID);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F5><HW1,1><RC290,120>{0}", ticket.CardID);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F50><HW1,1><RC320,70>厂商：" + ticket.Producter);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = "<Q>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = "<EPOS>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd)); //设置回EPOS打印模式

                        //其它信息
                        SetLeftMargin(0);
                        SetJustification(Justification.center);
                        PrintChineseLine("▲");
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintChineseLine("先到收费处交费，再取车出场");
                        EnlargeFont(0, 0);
                        PrintAscLine("--------------------");
                        PrintChineseLine("出场凭证,请勿折损!");
                        PrintChineseLine("出口回收：手握此处，正面朝上塞卡");
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
        /// <summary>
        /// 太古汇纸票版面（条码竖打，支持中英文提示,由于条码竖打，所以条码字符设置为小于9个)
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public bool PrintTicketOfTyko(TicketInfo ticket)
        {
            if (CommPort != null && CommPort.PortOpened)
            {
                if (ticket != null)
                {
                    InitPrinter();
                    QueryStatus();
                    if (Status == PrinterStatus.Ok)
                    {
                        //标头部分
                        SetJustification(Justification.center);
                        EnlargeFont(0, 1);
                        PrintChineseLine(ticket.CompanyName);
                        EnlargeFont(0, 0);

                        //条码和入场时间，入场口，供应商部分 用SVEL指令打
                        CommPort.SendData(new byte[] { 0x1c, 0x3c, 0x53, 0x56, 0x45, 0x4c, 0x3e });  //设置成SVEL打印模式
                        string cmd = null;
                        cmd = "<LHT340,400,0,40>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F5><HW1,1><RC45,30>" + ticket.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F50><HW1,1><RC80,30>" + ticket.Entrance);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<X2,H><RC120,30><NP20>*{0}*", ticket.CardID);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F5><HW1,1><RC290,130>{0}", ticket.CardID);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = string.Format("<F50><HW1,1><RC320,30>厂商：" + ticket.Producter);
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = "<Q>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd));
                        cmd = "<EPOS>";
                        CommPort.SendData(System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(cmd)); //设置回EPOS打印模式

                        //其它信息
                        SetJustification(Justification.center);
                        EnlargeFont(0, 0);
                        PrintChineseLine("先到收费处交费，再取车出场");
                        PrintChineseLine("Please pay at the");
                        PrintChineseLine("Cashier Office before");
                        PrintChineseLine("departing in your");
                        PrintChineseLine("vehicle");
                        PrintChineseLine("出场凭证,请勿折损!");
                        PrintChineseLine("Do not fold or");
                        PrintChineseLine("damage the ticket");
                        PrintChineseLine("出口回收：手握此处，正面朝上塞卡");
                        PrintAscLine("Insert the ticket");
                        PrintChineseLine("into the card reader,");
                        PrintChineseLine("with the face up,");
                        PrintChineseLine("in order to exit");
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

