using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LJH.GeneralLibrary.DAL
{
    public class DataContextFactory
    {
        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static DataContext CreateDataContext(string connStr, MappingSource ms, bool Log = false)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(connStr), "没有找到有效的数据库连接!");
            DataContext dc = new DataContext(connStr, ms);
            if (Log) dc.Log = System.Console.Out;
            return dc;
        }
    }
}
