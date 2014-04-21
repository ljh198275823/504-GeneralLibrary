using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LJH.GeneralLibrary.Core.DAL
{
    /// <summary>
    /// 表示数据库提供者的工厂类
    /// </summary>
    public interface IProviderFactory
    {
        /// <summary>
        /// 创建一个数据提供者实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repUri"></param>
        /// <returns></returns>
        T CreateProvider<T>(string connStr) where T : class;
    }
}
