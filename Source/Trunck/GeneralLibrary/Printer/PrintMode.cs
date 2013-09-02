using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 打印模式
    /// </summary>
    [Flags]
    public enum PrintMode
    {
        /// <summary>
        /// 普通
        /// </summary>
        Default=0,
        /// <summary>
        /// 粗体
        /// </summary>
        Bold=8,
        /// <summary>
        /// 下划线
        /// </summary>
        UnderLine=128,
    }
}
