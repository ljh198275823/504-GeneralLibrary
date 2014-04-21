using System;
using System.Linq;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using LJH.GeneralLibrary.ExceptionHandling;

namespace LJH.GeneralLibrary.Core.DAL
{
    /// <summary>
    /// 表示所有数据提供者的基类
    /// </summary>
    /// <typeparam name="TInfo"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public abstract class ProviderBase<TInfo, TID> : IProvider<TInfo, TID> where TInfo : class,IEntity<TID>, new()
    {
        #region 构造函数
        public ProviderBase(string conStr, MappingSource ms)
        {
            this.ConnectStr = conStr;
            _MappingResource = ms;
        }
        #endregion

        #region 私有字段
        protected readonly string successMsg = "ok";
        protected MappingSource _MappingResource = null;
        #endregion

        #region 公共属性
        public string ConnectStr { get; set; }
        #endregion

        #region IProvider <TInfo> 成员
        /// <summary>
        /// 通过ID获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QueryResult<TInfo> GetByID(TID id)
        {

            QueryResult<TInfo> result;
            try
            {
                DataContext dc = DataContextFactory.CreateDataContext(ConnectStr, _MappingResource);
                TInfo info = GetingItemByID(id, dc);
                if (info != null)
                {
                    result = new QueryResult<TInfo>(ResultCode.Successful, successMsg, info);
                }
                else
                {
                    result = new QueryResult<TInfo>(ResultCode.Fail, string.Format("没有找到ID={0}的数据!", id.ToString()), info);
                }
            }
            catch (Exception ex)
            {
                result = new QueryResult<TInfo>(ResultCode.Fail, ex.Message, null);
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "GetByID()");
            }
            return result;
        }
        /// <summary>
        /// 通过查询条件获取实体对象
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<TInfo> GetItems(SearchCondition search)
        {
            QueryResultList<TInfo> result;
            try
            {
                DataContext dc = DataContextFactory.CreateDataContext(ConnectStr, _MappingResource);
                List<TInfo> infoes;
                infoes = GetingItems(dc, search);
                result = new QueryResultList<TInfo>(ResultCode.Successful, successMsg, infoes);
            }
            catch (Exception ex)
            {
                result = new QueryResultList<TInfo>(ResultCode.Fail, ex.Message, new List<TInfo>());
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "GetItems()");
            }
            return result;
        }
        /// <summary>
        /// 插入实体对象
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(TInfo info)
        {
            CommandResult result;
            try
            {
                DataContext dc = DataContextFactory.CreateDataContext(ConnectStr, _MappingResource);
                InsertingItem(info, dc);
                dc.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, ex.Message);
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "Insert()");
            }
            return result;
        }
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="newVal"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public CommandResult Update(TInfo newVal, TInfo original)
        {
            CommandResult result;
            try
            {
                DataContext dc = DataContextFactory.CreateDataContext(ConnectStr, _MappingResource);
                UpdatingItem(newVal, original, dc);
                dc.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, ex.Message);
                ExceptionPolicy.HandleException(ex, this.GetType().FullName + "." + "Update()");
            }
            return result;
        }
        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Delete(TInfo info)
        {
            CommandResult result;
            try
            {
                DataContext dc = DataContextFactory.CreateDataContext(ConnectStr, _MappingResource);
                DeletingItem(info, dc);
                dc.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, ex.Message);
                ExceptionPolicy.HandleException(ex, this.GetType().FullName + "." + "Delete()");
            }
            return result;
        }
        /// <summary>
        /// 通过工作单元插入实体对象，实际插入发生在工作单元提交时
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unitWork"></param>
        public void Insert(TInfo info, IUnitWork unitWork)
        {
            if (unitWork != null && unitWork is ILinqUnitWork)
            {
                DataContext dc = (unitWork as ILinqUnitWork).DataContext;
                InsertingItem(info, dc);
            }
            else
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
        }
        /// <summary>
        /// 通过工作单元更新实体对象，实际更新发生在工作单元提交时
        /// </summary>
        /// <param name="newVal"></param>
        /// <param name="originalVal"></param>
        /// <param name="unitWork"></param>
        public void Update(TInfo newVal, TInfo originalVal, IUnitWork unitWork)
        {
            if (unitWork != null && unitWork is ILinqUnitWork)
            {
                DataContext dc = (unitWork as ILinqUnitWork).DataContext;
                UpdatingItem(newVal, originalVal, dc);
            }
            else
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
        }
        /// <summary>
        /// 通过工作单元删除实体对象，实际删除发生在工作单元提交时
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unitWork"></param>
        public void Delete(TInfo info, IUnitWork unitWork)
        {
            if (unitWork != null && unitWork is ILinqUnitWork)
            {
                DataContext dc = (unitWork as ILinqUnitWork).DataContext;
                DeletingItem(info, dc);
            }
            else
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
        }
        #endregion

        #region 模板方法
        protected virtual TInfo GetingItemByID(TID id, DataContext dc)
        {
            //每一个子类都要重写这个方法
            throw new Exception("子类没有重写GetingItemByID方法!");
        }
        protected virtual List<TInfo> GetingItems(DataContext dc, SearchCondition search)
        {
            //如果要实现这个功能,子类一定要重写这个方法
            return dc.GetTable<TInfo>().ToList();
        }
        protected virtual void InsertingItem(TInfo info, DataContext dc)
        {
            dc.GetTable<TInfo>().InsertOnSubmit(info);
        }
        protected virtual void UpdatingItem(TInfo newVal, TInfo original, DataContext dc)
        {
            //所有实体都可以用这个方法来更新数据
            dc.GetTable<TInfo>().Attach(newVal, original);
        }
        protected virtual void DeletingItem(TInfo info, DataContext dc)
        {
            //如果删除实体时要删除其关联数据,就得重写这个方法
            dc.GetTable<TInfo>().Attach(info);
            dc.GetTable<TInfo>().DeleteOnSubmit(info);
        }
        #endregion
    }
}
