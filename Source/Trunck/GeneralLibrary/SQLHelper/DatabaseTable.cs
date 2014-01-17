using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.SQLHelper
{
    /// <summary>
    /// 表示一个数据表
    /// </summary>
    public class DatabaseTable:DatabaseObject 
    {
        private List<DatabaseColumn> _Columns = new List<DatabaseColumn>();
        /// <summary>
        /// 是否是用户自建的表,否则是系统创建的表
        /// </summary>
        public bool IsUserTable { get; set; }

        /// <summary>
        /// 表的所有列
        /// </summary>
        public List<DatabaseColumn> Columns
        {
            get
            {
                return _Columns;
            }
        }

    }
}
