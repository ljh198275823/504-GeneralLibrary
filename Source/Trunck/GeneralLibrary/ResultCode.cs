﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 查询或执行操作的结果枚举
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Successful = 0, 
        /// <summary>
        ///失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 连接失败
        /// </summary>
        NotConnected=2,
    }
}
