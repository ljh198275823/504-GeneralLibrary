using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.UI
{
    public class ItemUpdatedEventArgs : EventArgs
    {
        public ItemUpdatedEventArgs()
        {
        }

        public ItemUpdatedEventArgs(object updatedItem)
        {
            this.UpdatedItem = updatedItem;
        }

        public ItemUpdatedEventArgs(object updatedItem, object preItem)
        {
            UpdatedItem = updatedItem;
            PreUpdatingItem = preItem;
        }

        /// <summary>
        /// 获取或设置更新后的对象
        /// </summary>
        public object UpdatedItem { get; set; }
        /// <summary>
        /// 获取或设置更新之前的对象
        /// </summary>
        public object PreUpdatingItem { get; set; }
    }
}
