using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    [Serializable]
    public class BarCodePrinterStatusChangedEventArgs:EventArgs
    {
        public BarCodePrinterStatusChangedEventArgs()
        {
        }

        public BarCodePrinterStatusChangedEventArgs(PrinterStatus s)
        {
            Status = s;
        }

        /// <summary>
        /// 获取或设置打印机状态
        /// </summary>
        public PrinterStatus  Status { get; set; }
    }

    public delegate void BarCodePrinterStatusChangedHandler(object sender,BarCodePrinterStatusChangedEventArgs e);
}
