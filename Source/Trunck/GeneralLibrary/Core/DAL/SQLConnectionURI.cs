using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.DAL
{
    public class SQLConnectionURI
    {
        #region 构造函数
        public SQLConnectionURI(string sqlUri)
        {
            if (!string.IsNullOrEmpty(sqlUri))
            {
                int p = sqlUri.IndexOf(__SPLIT);
                if (p > 0)
                {
                    _SQLType = sqlUri.Substring(0, p);
                    _ConnectString = sqlUri.Substring(p + 1);
                }
                else
                {
                    _SQLType = __DEFAULTSQL;
                    _ConnectString = sqlUri;
                }
            }
        }

        public SQLConnectionURI(string sqlType, string conStr)
        {
            _SQLType = sqlType;
            _ConnectString = conStr;
        }

        #endregion

        #region 私有变量
        private readonly string __SPLIT = ":";
        private readonly string __DEFAULTSQL = "MSSQL";
        private string _SQLType; //比如 MSSQL, SQLITE等
        private string _ConnectString;
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取或设置数据库类型
        /// </summary>
        /// <returns></returns>
        public string SQLType
        {
            get
            {
                return _SQLType;
            }
            set
            {
                _SQLType = value;
            }
        }

        /// <summary>
        ///  获取或设置数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public string ConnectString
        {
            get
            {
                return _ConnectString;
            }
            set
            {
                _ConnectString = value;
            }
        }
        #endregion

        #region 重写基类方法
        public override string ToString()
        {
            return (string.IsNullOrEmpty(_SQLType) ? __DEFAULTSQL : _SQLType) + __SPLIT + ConnectString;
        }
        #endregion
    }
}
