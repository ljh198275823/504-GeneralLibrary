using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Data.SqlClient;

namespace LJH.GeneralLibrary.Core.DAL
{
    public class ConnectionHelper
    {
        /// <summary>
        /// 检测连接字符串是否可以连接到指定的数据库
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static bool CanConnectTo(string connStr)
        {
            try
            {
                AutoResetEvent ae = new AutoResetEvent(false);
                Action action = delegate()
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection())
                        {
                            con.ConnectionString = connStr;
                            con.Open();
                            con.Close();
                            ae.Set();
                        }
                    }
                    catch (ThreadAbortException)
                    {
                    }
                    catch (Exception)
                    {

                    }
                };
                Thread t = new Thread(new ThreadStart(action));
                t.IsBackground = true;
                t.Start();
                if (ae.WaitOne(5 * 1000))
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}
