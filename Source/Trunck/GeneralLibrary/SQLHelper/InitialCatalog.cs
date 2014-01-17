using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SQLHelper
{
    /// <summary>
    /// 表示一个数据库
    /// </summary>
    public class InitialCatalog:DatabaseObject 
    {
        private List<DatabaseTable> _Tables = new List<DatabaseTable>();
        /// <summary>
        /// 是否是系统数据库
        /// </summary>
        public bool IsUserDatabase { get; set; }

        /// <summary>
        /// 数据库的所有表
        /// </summary>
        public List<DatabaseTable> Tables
        {
            get
            {
                return _Tables;
            }
        }
    }
}
