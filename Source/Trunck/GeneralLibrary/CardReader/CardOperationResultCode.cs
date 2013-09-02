using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 卡片操作结果代码枚举
    /// </summary>
    public enum CardOperationResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success=0x00,

        /// <summary>
        /// 失败
        /// </summary>
        Fail=0x01,

        /// <summary>
        /// 初始化读卡器失败
        /// </summary>
        InitFail=0x02,

        /// <summary>
        /// 打开读卡器失败
        /// </summary>
        OpenFail=0x03,

        /// <summary>
        /// 没有卡片
        /// </summary>
        NoCard=0x04,

        /// <summary>
        /// 卡号不一致
        /// </summary>
        CardIDError=0x05,

        /// <summary>
        /// 扇区验证失败
        /// </summary>
        AuthFail=0x06,

        /// <summary>
        /// 初始化密钥失败
        /// </summary>
        InitKeyFail=0x07,
    }
}
