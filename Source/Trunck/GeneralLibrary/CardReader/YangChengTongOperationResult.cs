using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 羊城通读卡器操作返回结果
    /// </summary>
    public enum YangChengTongOperationResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 通讯错误
        /// </summary>
        CommunicationError = 4,
        /// <summary>
        /// 没有卡片
        /// </summary>
        NoCard = 66,
        /// <summary>
        /// 黑名单
        /// </summary>
        BlackList =77,
        /// <summary>
        /// 操作失败
        /// </summary>
        OperationFail=88,
        /// <summary>
        /// 校验和错误
        /// </summary>
        DataError = 99,
    }

   // [00]：操作成功
   //[66]：没有发现卡
   //[77]：黑名单卡
   //[88]：操作失败
   //[99]：校验和错误

}
