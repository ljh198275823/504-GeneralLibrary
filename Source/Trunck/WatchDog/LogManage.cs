using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WatchDog
{
    public class LogManage
    {        
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="message">文本描述</param>
        public static void WriteLog(string message)
        {
            try
            {
                string dir = Path.Combine(Application.StartupPath, @"TextFile");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string file = Path.Combine(dir, "WatchDog.txt");
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch { }
        }
    }
}
