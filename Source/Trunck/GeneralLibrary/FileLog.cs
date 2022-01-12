using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 文件日志，把日志记录到文件中
    /// </summary>
    public class FileLog
    {
        private static object _FileLocker = new object();
        private static Dictionary<string, object> _Lockers = new Dictionary<string, object>();

        private static object GetLocker(string logName)
        {
            lock (_FileLocker)
            {
                if (!_Lockers.ContainsKey(logName))
                {
                    _Lockers.Add(logName, new object());
                }
                return _Lockers[logName];
            }
        }
        /// <summary>
        /// 把内容content记录到日志名称为name的日志中
        /// 本函数为线程串行访问，多线程并发时过多调用写日志功能会影响并发性能
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="content">要记录的内容</param>
        public static void Log(string logName, string content)
        {
            Log(Application.StartupPath, logName, content);
        }
        /// <summary>
        /// 把内容content记录到日志名称为name的日志中
        /// 本函数为线程串行访问，多线程并发时过多调用写日志功能会影响并发性能
        /// </summary>
        /// <param name="path">日志的路径</param>
        /// <param name="logName">日志名称</param>
        /// <param name="content">要记录的内容</param>
        public static void Log(string path, string logName, string content)
        {
            //不同的日志文件各用一把锁，以免写一个日志文件时同时锁住其它日志文件的写入
            object locker = GetLocker(logName);
            if (locker != null)
            {
                lock (locker)
                {
                    try
                    {
                        string message = string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), content);

                        string dir = Path.Combine(path, "Logs/" + DateTime.Today.ToString("yyyy-MM-dd"));
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        string file = Path.Combine(dir, logName + ".log");
                        using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.Write(message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionPolicy.HandleException(ex);
                    }
                }
            }
        }
    }
}
