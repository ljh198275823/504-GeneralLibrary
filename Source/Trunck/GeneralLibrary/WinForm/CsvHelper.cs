using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace LJH.GeneralLibrary.WinForm
{
    public static class CsvHelper
    {
        #region 私有方法
        private static char[] chSplit = new char[] { ',' };

        private static string ReadTxtFile(string path)
        {
            string str = string.Empty;
            StreamReader reader = new StreamReader(path, System.Text.Encoding.Default);
            str = reader.ReadToEnd();
            //再通过查询解析出来的的字符串有没有GB2312 的字段，来判断是否是GB2312格式的，如果是，则重新以GB2312的格式解析
            Regex reGB = new Regex("UTF-8", RegexOptions.IgnoreCase);
            Match mcGB = reGB.Match(str);
            if (mcGB.Success)
            {
                StreamReader reader2 = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8"));
                str = reader2.ReadToEnd();
            }
            return str;
        }
        #endregion

        #region public function
        /// <summary>
        /// 从文件中导出数据
        /// </summary>
        public static DataTable Import(string RecoveryPath, Encoding encode)
        {
            int intColCount = 0;
            bool blnFlag = true;
            DataTable mydt = new DataTable("Employees");
            DataColumn mydc;
            DataRow mydr;
            string strline;
            string[] aryline;

            using (System.IO.StreamReader mysr = new System.IO.StreamReader(RecoveryPath, encode))
            {
                while ((strline = mysr.ReadLine()) != null)
                {
                    strline = strline.Trim();
                    if (string.IsNullOrEmpty(strline)) continue; //如果遇到空白行，
                    aryline = strline.Split(chSplit);
                    if (blnFlag) //第一行用作表头
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            mydc = new DataColumn(aryline[i].ToString());
                            mydc.ColumnName = aryline[i].ToString();
                            mydt.Columns.Add(mydc);
                        }
                    }
                    else
                    {
                        if (aryline.Length < intColCount) continue; //如果某一行的列对不上，忽略这一行
                        mydr = mydt.NewRow();
                        for (int i = 0; i < intColCount; i++)
                        {
                            mydr[i] = aryline[i].Trim('"', '\'').Trim();
                        }
                        mydt.Rows.Add(mydr);
                    }
                }
            }
            return mydt;
        }
        #endregion
    }
}
