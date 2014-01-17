using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SQLHelper
{
    /// <summary>
    /// 表示数据表的一个例
    /// </summary>
    public  class DatabaseColumn:DatabaseObject 
    {
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DBType{get;set;}
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanBeNull{get;set;}
    }
}
