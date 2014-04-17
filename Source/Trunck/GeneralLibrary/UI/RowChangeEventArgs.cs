using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.UI
{
    public  class RowChangeEventArgs:EventArgs
    {
        #region 构造函数
        public RowChangeEventArgs()
        {
        }

        public RowChangeEventArgs(bool isFirst, bool isLast)
        {
            IsFirstRow = isFirst;
            IsLastRow = isLast;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置是否是第一项
        /// </summary>
        public bool IsFirstRow { get; set; }
        /// <summary>
        /// 获取或设置是否是最后一项
        /// </summary>
        public bool IsLastRow { get; set; }
        /// <summary>
        /// 获取或设置当前行所指向的对象
        /// </summary>
        public object UpdatingItem { get; set; }
        #endregion
    }
}
