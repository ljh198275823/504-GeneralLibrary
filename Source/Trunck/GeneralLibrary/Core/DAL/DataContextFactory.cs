using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace LJH.GeneralLibrary.Core.DAL
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
            IDbConnection connection = null;
            string sqlType = GetSQLType(connStr);
            if (sqlType == "SQLITE")
            {
                connection = new SQLiteConnection(GetConnectString(connStr));
            }
            else if (sqlType == "MSSQL")
            {
                connection = new SqlConnection(GetConnectString(connStr));
            }
            else //如果没有标明数据库类型，默认采用mssql数据库
            {
                connection = new SqlConnection(connStr);
            }
            DataContext dc = new DataContext(connection, ms);
            if (Log) dc.Log = System.Console.Out;
            return dc;
        }

        /// <summary>
        /// 从数据库连接资源uri中获取数据库类型
        /// </summary>
        /// <returns></returns>
        private static string GetSQLType(string connStr)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                int p = connStr.IndexOf(':');
                if (p > 0)
                {
                    return connStr.Substring(0, p);
                }
            }
            return string.Empty;
        }

        /// <summary>
        ///  从数据库连接资源uri获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnectString(string connStr)
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                int p = connStr.IndexOf(':');
                if (p > 0)
                {
                    return connStr.Substring(p + 1);
                }
            }
            return string.Empty;
        }
    }
}
