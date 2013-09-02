using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 纸票打印机状态
    /// </summary>
    public enum PrinterStatus
    {
        /// <summary>
        /// 未初始化
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 打印机串口没有打开
        /// </summary>
        COMPortNotOpen = 1,
        /// <summary>
        /// 打印机正常
        /// </summary>
        Ok = 2,
        /// <summary>
        /// 打印机缺纸
        /// </summary>
        PaperAbsent = 3,
        /// <summary>
        /// 打印机已断开
        /// </summary>
        OffLine = 4,
    }
}
