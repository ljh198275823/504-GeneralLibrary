using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace LJH.GeneralLibrary.SQLHelper
{
    /// <summary>
    /// SQL客户端
    /// </summary>
    public class SqlClient
    {
        private SqlConnection _Con = null;
        private string _ConnectString = null;
 
        public SqlClient(string connStr)
        {
            _ConnectString = connStr;
            _Con = new SqlConnection(_ConnectString);
        }

        public SqlClient(string server, bool integratedSecurity)
        {
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b.DataSource = server;
            b.IntegratedSecurity = integratedSecurity;
            _ConnectString = b.ToString();
            _Con = new SqlConnection(_ConnectString);
        }

        public SqlClient (string server,string userid,string passwd)
        {
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b.DataSource = server;
            b.UserID = userid;
            b.Password = passwd;
            _ConnectString = b.ToString();
            _Con = new SqlConnection(_ConnectString);
        }


        public SqlClient(string server, string dataBase)
        {
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b.DataSource = server;
            b.InitialCatalog = dataBase;
            b.IntegratedSecurity = true;
            _ConnectString = b.ToString();
            _Con = new SqlConnection(_ConnectString);
        }

        public SqlClient(string server, string dataBase, string userID, string passwd)
        {
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b.DataSource = server;
            b.InitialCatalog = dataBase;
            b.UserID = userID;
            b.Password = passwd;
            _ConnectString = b.ToString();
            _Con = new SqlConnection(_ConnectString);
        }

        #region 公共属性
        /// <summary>
        /// 获取当前连接的数据库
        /// </summary>
        public string DataBase
        {
            get
            {
                if (_Con != null && _Con.State == ConnectionState.Open)
                {
                    return _Con.Database;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ConnectionState State
        {
            get
            {
                return _Con.State;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 连接SQL服务器
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                _Con.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public void Close()
        {
            if (_Con.State == ConnectionState.Open)
            {
                _Con.Close();
            }
        }

        /// <summary>
        /// 改变当前连接的数据库
        /// </summary>
        /// <param name="newDateBase"></param>
        public void ChangeDataBase(string newDateBase)
        {
            _Con.ChangeDatabase(newDateBase);
        }

        /// <summary>
        /// 获取所有数据库名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDataBase()
        {
            List<string> result = new List<string>();
            string curDb = DataBase;
            ChangeDataBase("master");
            string sql = "SELECT name FROM SYSDATABASES ORDER BY Name";
            DataTable dt = Query(sql);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row[0] as string);
            }
            ChangeDataBase(curDb);
            return result;
        }

        /// <summary>
        /// 获取或设置是否存在指定名称的数据库
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasDataBase(string name)
        {
            List<string> result = new List<string>();
            string curDb = DataBase;
            if (DataBase != "master") ChangeDataBase("master");
            string sql = string.Format("SELECT count(name) FROM SYSDATABASES where name='{0}'", name);
            var count = Convert.ToInt32(ExecuteScalar(sql));
            return count > 0;
        }

        /// <summary>
        /// 获取数据库默认的路径
        /// </summary>
        /// <returns></returns>
        public string GetDefaultDataBasePath()
        {
            string curDb = DataBase;
            ChangeDataBase("master");
            string sql = "select filename from sysfiles where filename like '%master%'";
            string filename = ExecuteScalar(sql) as string;
            string path = Path.GetDirectoryName(filename);
            ChangeDataBase(curDb);
            return path;
        }

        /// <summary>
        /// 创建数据库,不成功的话返回原因,成功返回空
        /// </summary>
        /// <param name="name">创建的数据库名称</param>
        /// <param name="sqlFile">创建数据库的脚本文件</param>
        /// <param name="dataFile">创建的数据库的数据文件路径</param>
        /// <param name="logFile">创建的数据库的日志文件路径</param>
        /// <returns></returns>
        public string CreateDataBase(string name, string sqlFile, string dataFile, string logFile)
        {
            try
            {
                string sql = "create database " + name + " " +
                             "on (name=" + name + "_dat,filename='" + dataFile + "') " +
                             "log on (name=" + name + "_log,filename='" + logFile + "')";
                ExecuteNoQuery(sql);

                string database = DataBase; //暂时保存当前数据库
                ChangeDataBase(name);

                ExecuteSQLFile(sqlFile);

                ChangeDataBase(database);// 返回到初始数据库
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 执行文件中的所有语句
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void ExecuteSQLFile(string sqlFile)
        {
            List<string> commands = (new SQLStringExtractor()).ExtractFromFile(sqlFile);
            if (commands != null && commands.Count > 0)
            {
                foreach (string command in commands)
                {
                    ExecuteNoQuery(command);
                }
            }
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bakFile"></param>
        /// <returns></returns>
        public string BackupDataBase(string name, string bakFile)
        {
            try
            {
                string sql = "BACKUP DATABASE " + name + " TO DISK='" + bakFile + "'";
                ExecuteNoQuery(sql);
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bakFile"></param>
        /// <returns></returns>
        public string RestoreDataBase(string name, string bakFile)
        {
            try
            {
                string sql = "restore database " + name + " from disk='" + bakFile  + "'";
                ExecuteNoQuery(sql);
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Close();
        }

        #endregion

        #region 私有方法
        public DataTable Query(string sql)
        {
            using (SqlDataAdapter adp = new SqlDataAdapter(sql, _Con))
            {
                DataTable dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, _Con))
            {
                cmd.CommandTimeout = int.MaxValue;
                if (_Con.State != ConnectionState.Open)
                {
                    _Con.Open();
                }
                return cmd.ExecuteScalar();
            }
        }

        public int ExecuteNoQuery(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, _Con))
            {
                cmd.CommandTimeout = int.MaxValue;
                if (_Con.State != ConnectionState.Open)
                {
                    _Con.Open();
                }
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
