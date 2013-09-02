using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 表示BX4K控制板动态区域
    /// </summary>
    public class BX4KDynamicArea
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置区域类型(默认为0x00)
        /// </summary>
        public byte AreaType { get; set; }
        /// <summary>
        /// 获取或设置区域原点的X坐标(以8个像素点为单位)
        /// </summary>
        public short X { get; set; }
        /// <summary>
        /// 获取或设置区域原点的Y坐标
        /// </summary>
        public short Y { get; set; }
        /// <summary>
        /// 获取或设置区域的宽度(以8个像素点为单位)
        /// </summary>
        public short Width { get; set; }
        /// <summary>
        /// 获取或设置区域的高度
        /// </summary>
        public short Height { get; set; }
        /// <summary>
        /// 数据编码格式 0x01=GB2312
        /// </summary>
        public byte FontCode { get; set; }
        /// <summary>
        /// 获取或设置是否单行显示
        /// </summary>
        public bool SingleLine { get; set; }
        /// <summary>
        /// 获取或设置换行设置
        /// </summary>
        public bool NewLine { get; set; }
        /// <summary>
        /// 获取或设置显示模式
        /// </summary>
        public BX4KDisplayMode DisplayMode { get; set; }
        /// <summary>
        /// 获取或设置显示的移动速度(0x00=4字/秒 0x1=3字/秒 0x2=2.5字/秒 0x03=2字/秒 0x5=1字/秒)
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 获取或设置停留时间(以0.5秒为单位)
        /// </summary>
        public byte StayTime { get; set; }
        /// <summary>
        /// 获取或设置要显示的字符串
        /// </summary>
        public string Text { get; set; }
        #endregion
    }
}
