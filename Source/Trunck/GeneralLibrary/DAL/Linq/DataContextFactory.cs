using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace LJH.GeneralLibrary.Core.DAL.Linq
{
    public class DataContextFactory
    {
        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <param name="sqlURI"></param>
        /// <returns></returns>
        public static DataContext CreateDataContext(SQLConnectionURI sqlURI, MappingSource ms, bool Log = false)
        {
            System.Diagnostics.Debug.Assert(sqlURI != null, "没有找到有效的数据库连接!");
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(sqlURI.ConnectString), "没有找到有效的数据库连接!");
            IDbConnection connection = null;
            if (!string.IsNullOrEmpty(sqlURI.SQLType) && sqlURI.SQLType.ToUpper() == "SQLITE")
            {
                connection = new SQLiteConnection(sqlURI.ConnectString);
            }
            else if (!string.IsNullOrEmpty(sqlURI.SQLType) && sqlURI.SQLType.ToUpper() == "MSSQL")
            {
                connection = new SqlConnection(sqlURI.ConnectString);
            }
            else //如果没有标明数据库类型，默认采用mssql数据库
            {
                connection = new SqlConnection(sqlURI.ConnectString);
            }
            DataContext dc = new DataContext(connection, ms);
            if (Log) dc.Log = System.Console.Out;
            return dc;
        }

        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static DataContext CreateDataContext(string sqlUri, MappingSource ms, bool Log = false)
        {
            var uri = string.IsNullOrEmpty(sqlUri) ? null : new SQLConnectionURI(sqlUri);
            return CreateDataContext(uri, ms, Log);
        }
    }
}
