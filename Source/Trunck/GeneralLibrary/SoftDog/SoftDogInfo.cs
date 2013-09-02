using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SoftDog
{
    /// <summary>
    /// 表示加密狗中保存的信息
    /// </summary>
    public class SoftDogInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int logID { get; set; }
        /// <summary>
        /// 写入日期
        /// </summary>
        public DateTime WriteDateTime { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int ProjectNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 软件列表
        /// </summary>
        public SoftwareType  SoftwareList { get; set; }
        /// <summary>
        /// 软件试用开始时间
        /// </summary>
        public DateTime TryPeriodStartDate { get; set; }
        /// <summary>
        /// 软件试用第一次时长(天)
        /// </summary>
        public int TryPeriodDays1 { get; set; }
        /// <summary>
        /// 软件试用第二次时长(天)目前暂不用,为0
        /// </summary>
        public int TryPeriodDays2 { get; set; }
        /// <summary>
        /// 消费系统消费点数量
        /// </summary>
        public int PayPointQuantity { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExpiredDate
        {
            get
            {
                return TryPeriodStartDate.AddDays(TryPeriodDays1);
            }
        }

    }

    /// <summary>
    /// 加密狗支持的软件类型
    /// </summary>
    [Flags()]
    public enum SoftwareType
    {
        /// <summary>
        /// 进销存软件
        /// </summary>
        TYPE_Inventory = 1,
    }
}
