using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace LJH.GeneralLibrary.SQLHelper
{
    /// <summary>
    /// 表示sql语句提取器,可以从一段文字或文件中提取可以执行的SQL语名,比如会去掉注释
    /// </summary>
    public class SQLStringExtractor
    {
        #region 构造函数
        public SQLStringExtractor()
        {
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 从文件中提取所有可执行的SQL语句
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<string> ExtractFromFile(string file)
        {
            string str = File.ReadAllText(file, System.Text.Encoding.Default);
            return ExtractFromStr(str);
        }

        /// <summary>
        /// 从字符串中提取所有可执行的SQL语句
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> ExtractFromStr(string str)
        {
            List<string> ret = null;
            string sql = string.Empty;
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\s*\b[Gg][Oo]\b\s*");  //用于匹配有GO的行
            System.Text.RegularExpressions.Regex comment = new System.Text.RegularExpressions.Regex(@"/\*.*\*/"); //用于匹配注释/*...*/

            //先将字符串中所有的 "/*...*/" 的注释换成一个空格
            str = comment.Replace(str, " ");
            string[] lines = str.Split('\n'); //分出字符串中所有的行
            if (lines != null && lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    if (reg.IsMatch(line))  //如果本行是GO,则执行GO之前的语句
                    {
                        if (!string.IsNullOrEmpty(sql))
                        {
                            if (ret == null) ret = new List<string>();
                            ret.Add(sql);
                            sql = string.Empty;
                        }
                    }
                    else
                    {
                        //如果有"--"注释,把注释内容换成一个空格
                        int ind = line.IndexOf("--");
                        if (ind >= 0)
                        {
                            sql += line.Substring(0, ind) + " ";
                        }
                        else
                        {
                            sql += line + " ";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(sql))  //
                {
                    if (ret == null) ret = new List<string>();
                    ret.Add(sql);
                }
            }
            return ret;
        }
        #endregion
    }
}
