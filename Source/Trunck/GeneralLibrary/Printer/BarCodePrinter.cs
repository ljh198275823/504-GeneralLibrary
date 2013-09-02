using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 表示一个可以打印条码的EPSON模式打印机
    /// </summary>
    public abstract class BarCodePrinter:EpsonmodePrinter
    {
        #region 构造函数
        public BarCodePrinter(byte commport, int baudRate)
            : base(commport, baudRate)
        {
        }
        #endregion

        #region 基类要重写的方法
        public abstract void PrintBarCode(string barcode);

        public abstract void SetBarCodeWidthMagnification(byte magnification);

        public abstract void SetBarCodeSubtitle(bool enable);

        public abstract void SetBarCodeHeight(byte height);

        public abstract bool PrintTicket(TicketInfo ticket);
        #endregion
    }
}
