using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LJH.GeneralLibrary.DAL
{
    public class DataContextFactory
    {
        /// <summary>
        /// 创建数据上下文
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static DataContext CreateDataContext(string connStr, string resourceUri)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(connStr), "没有找到有效的数据库连接!");
            Stream stream = typeof(DataContextFactory).Assembly.GetManifestResourceStream(resourceUri);
            MappingSource mappingSource = XmlMappingSource.FromStream(stream);
            DataContext inventory = new DataContext(connStr, mappingSource);
            inventory.Log = System.Console.Out;
            return inventory;
        }
    }
}
