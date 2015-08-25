using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using LJH.GeneralLibrary.ExceptionHandling;
using LJH.GeneralLibrary.Core.DAL;

namespace LJH.GeneralLibrary.Core.DAL.Linq
{
    /// <summary>
    /// 表示LINQ单元操作
    /// </summary>
    public class LinqUnitWork : IUnitWork
    {
        #region 构造函数
        public LinqUnitWork(SQLConnectionURI connStr, MappingSource ms)
        {
            _DataContext = DataContextFactory.CreateDataContext(connStr, ms);
        }
        #endregion

        #region 私有变量
        private DataContext _DataContext;
        #endregion

        #region 实现IUnitWork接口
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public CommandResult Commit()
        {
            try
            {
                _DataContext.SubmitChanges();
                return new CommandResult(ResultCode.Successful, ResultCode.Successful.ToString());

            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "UnitWork.Commit()");
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 获取执行单元的数据上下文
        /// </summary>
        public DataContext DataContext
        {
            get { return _DataContext; }
        }
        #endregion
    }
}
