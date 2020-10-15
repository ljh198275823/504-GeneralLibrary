using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.DAL
{
    public enum ImportOption
    {
        /// <summary>
        /// 忽略
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// 覆盖
        /// </summary>
        Override = 1,
        /// <summary>
        /// 追加
        /// </summary>
        Append = 2,
    }
}
