using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.DAL
{
    /// <summary>
    /// 表示数据库查询条件
    /// </summary>
    public abstract class SearchCondition
    {
        /// <summary>
        /// 获取或设置每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 获取或设置当前页,从1开始递增
        /// </summary>
        public int PageIndex { get; set; }
    }
}
