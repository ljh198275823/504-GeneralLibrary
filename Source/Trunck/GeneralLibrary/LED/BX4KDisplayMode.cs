using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 获取或设置区域的显示模式
    /// </summary>
    public enum BX4KDisplayMode
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None=0x0,
        /// <summary>
        /// 静止显示
        /// </summary>
        Static=0x1,
        /// <summary>
        /// 快速打出
        /// </summary>
        Rapid=0x2,
        /// <summary>
        /// 向左移动
        /// </summary>
        Leftward=0x3,
        /// <summary>
        /// 向右移动
        /// </summary>
        Rightward=0x4,
        /// <summary>
        /// 向上移动
        /// </summary>
        Upward=0x5,
        /// <summary>
        /// 向下移动
        /// </summary>
        Downward=0x6,
    }
}
