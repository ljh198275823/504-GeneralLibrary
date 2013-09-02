using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    /// <summary>
    /// 表示一个网格导出类，用于把网格数据导出到文件
    /// </summary>
    public class DataGridViewExporter
    {
        /// <summary>
        /// 从网络中把数据导出到文件中（可以用电子表格软件打开)
        /// </summary>
        /// <param name="view"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool Export(DataGridView view, string file, bool exportHidden = false)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                using (fs = new FileStream(file, FileMode.Create))
                {
                    using (writer = new StreamWriter(fs, Encoding.Unicode))
                    {
                        StringBuilder header = new StringBuilder();
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            header.Append(view.Columns[i].HeaderText + "\t");
                        }
                        writer.WriteLine(header.ToString());
                        foreach (DataGridViewRow row in view.Rows)
                        {
                            if (row.Visible || exportHidden)
                            {
                                StringBuilder rowText = new StringBuilder();
                                for (int i = 0; i < view.Columns.Count; i++)
                                {
                                    if (row.Cells[i].Value != null)
                                    {
                                        rowText.Append(row.Cells[i].Value.ToString() + "\t");
                                    }
                                    else
                                    {
                                        rowText.Append(string.Empty + "\t");
                                    }
                                }
                                writer.WriteLine(rowText.ToString());
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return false;
            }
        }
    }
}
