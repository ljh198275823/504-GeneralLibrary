using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using LJH.GeneralLibrary.ExceptionHandling;

namespace LJH.GeneralLibrary.Core.DAL
{
    /// <summary>
    /// 单元操作
    /// </summary>
    public interface ILinqUnitWork : LJH.GeneralLibrary.Core.DAL.IUnitWork
    {
        /// <summary>
        /// 获取执行单元的数据上下文
        /// </summary>
        DataContext DataContext { get; }
    }
}
