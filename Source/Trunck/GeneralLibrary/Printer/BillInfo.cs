using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 停车场收费小票
    /// </summary>
    [Serializable ]
    public class ParkBillInfo
    {
        /// <summary>
        /// 获取或设置公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置入场时间
        /// </summary>
        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置出场时间
        /// </summary>
        public DateTime ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        public string TariffType { get; set; }
        /// <summary>
        /// 获取或设置卡片上次收费时的应收费用
        /// </summary>
        public decimal LastAccounts { get; set; }
        /// <summary>
        /// 获取或设置卡片入场以来已经收取的停车费用(卡片入场后可能会有多条收费记录)
        /// </summary>
        public decimal HavePaid { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        public decimal Paid { get; set; }

        /// <summary>
        /// 获取或设置收费操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 获取或设置收费工作站
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string ParkInterval
        {
            get
            {
                string ret = string.Empty;
                TimeSpan span = new TimeSpan(ChargeDateTime.Ticks - EnterDateTime.Ticks);
                return string.Format(Resources.Resource1.EpsonMT532_Bill_ParkInterval, span.Days * 24 + span.Hours, span.Minutes);
            }
        }

        //add by Jan 2012-9-11
        public string SerialNumber { get; set; }
        //end
    }
}
