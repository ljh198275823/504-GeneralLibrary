using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 表示一个修改项
    /// </summary>
    public class UpdateItem<TID>
    {
        public TID ID { get; set; }
        /// <summary>
        /// 获取或设置修改项的ID, 一般者了实体类的属性名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 获取或设置修改后的新值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 获取或设置修改前的值 
        /// </summary>
        public string OriginalValue { get; set; }
    }
}
