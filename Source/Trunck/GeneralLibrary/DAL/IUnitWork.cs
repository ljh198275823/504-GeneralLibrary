using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LJH.GeneralLibrary.DAL
{
    /// <summary>
    /// 表示一个单元操作,用于把多个操作组成一个事务操作.
    /// </summary>
    public interface IUnitWork
    {
        /// <summary>
        /// 提交操作
        /// </summary>
        CommandResult Commit();
        /// <summary>
        /// 获取数据上下文
        /// </summary>
        /// <returns></returns>
        DataContext DataContext { get; }
    }
}
