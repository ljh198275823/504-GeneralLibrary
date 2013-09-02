using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 表示中山通读卡事件参数
    /// </summary>
    public class ZSTReaderEventArgs : EventArgs
    {
        #region 构造函数

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置消息类型 1:读卡事件 2:表示消费成功 3：表示消费不成功
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 获取或设置读到卡片的读卡器的IP地址
        /// </summary>
        public string ReaderIP { get; set; }
        /// <summary>
        /// 获取或设置读到的卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置卡类型代码
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 获取或设置卡片的余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置终端号
        /// </summary>
        public string TeminalNum { get; set; }
        /// <summary>
        /// 获取或设置原生消息
        /// </summary>
        public string RawMessage { get; set; }
        #endregion

        #region 重写基类方法
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion
    }
}
