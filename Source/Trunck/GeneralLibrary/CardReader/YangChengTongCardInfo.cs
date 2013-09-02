using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 表示羊城卡片
    /// </summary>
    [Serializable]
    public class YangChengTongCardInfo
    {
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LogicalID { get; set; }
        /// <summary>
        /// 获取或设置卡片余额
        /// </summary>
        public decimal Balance { get; set; }
    }

}
