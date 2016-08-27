﻿using System;
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
        /// <summary>
        /// 获取或设置数据库名称
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 获取或设置数据库用户名
        /// </summary>
        public string DBUser { get; set; }
        /// <summary>
        /// 获取或设置数据库密码
        /// </summary>
        public string DBPassword { get; set; }
        /// <summary>
        /// 获取或设置授权可用的电脑的MAC,如果有多个，用逗号隔开
        /// </summary>
        public string MAC { get; set; }
        /// <summary>
        /// 获取或设置DOGINFO是否是从硬件加密狗中获取而来
        /// </summary>
        public bool DogDevice { get; set; }
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
        None = 0,
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
        TYPE_ACS = 0x04,
        /// <summary>
        /// 汇海体育软件
        /// </summary>
        TYPE_HHTIYU = 0x08,
        /// <summary>
        /// 汇海体育软件（WIFI版）
        /// </summary>
        TYPE_HHTIYU_WIFI = 0x10,
        /// <summary>
        /// 停车场
        /// </summary>
        TYPE_PARK = 0x20,
    }
}
