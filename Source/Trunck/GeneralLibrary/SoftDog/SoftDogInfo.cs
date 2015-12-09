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
        /// 项目编号
        /// </summary>
        public int ProjectNo { get; set; }
        /// <summary>
        /// 软件列表
        /// </summary>
        public SoftwareType SoftwareList { get; set; }
        /// <summary>
        /// 软件试用开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 软件试用第一次时长(天)
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExpiredDate
        {
            get
            {
                return StartDate.AddDays(Days);
            }
            set
            {
                TimeSpan ts = new TimeSpan(value.Ticks - StartDate.Ticks);
                Days = (int)(Math.Floor(ts.TotalDays));
            }
        }
        /// <summary>
        /// 获取或设置是否是主机加密狗
        /// </summary>
        public bool IsHost { get; set; }
    }

    /// <summary>
    /// 加密狗支持的软件类型
    /// </summary>
    [Flags()]
    public enum SoftwareType
    {
        /// <summary>
        /// 
        /// </summary>
        None=0,
        /// <summary>
        /// 进销存软件
        /// </summary>
        TYPE_Inventory = 0x01,
        /// <summary>
        /// 考勤软件
        /// </summary>
        TYPE_TA = 0x02,
        /// <summary>
        /// 门禁软件
        /// </summary>
        TYPE_ACS=0x04,
        /// <summary>
        /// 中考/体质测试软件
        /// </summary>
        TYPE_ZHONGKAO = 0x08,
        /// <summary>
        /// 停车场
        /// </summary>
        TYPE_PARK=0x10
    }
}
