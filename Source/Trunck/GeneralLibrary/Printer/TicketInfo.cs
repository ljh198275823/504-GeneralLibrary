using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    public class TicketInfo
    {
        /// <summary>
        /// 获取和设置纸票的卡号
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        ///获取或设置入场时间
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// 获取或设置入场通道
        /// </summary>
        public string Entrance { get; set; }

        /// <summary>
        /// 获取或设置纸票的公司名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 获取或设置纸票的供应商
        /// </summary>
        public string Producter { get; set; }

        /// <summary>
        /// 获取和设置纸票的欢迎话语
        /// </summary>
        public string Reguard { get; set; }
    }
}
