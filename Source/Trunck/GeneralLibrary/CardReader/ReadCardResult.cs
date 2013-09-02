using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 读卡结果类
    /// </summary>
    public class ReadCardResult
    {
        #region 构造函数
        public ReadCardResult()
        {
            CardID = string.Empty;
            _dataList = new Dictionary<int, byte[]>();
            ResultCode = CardOperationResultCode.Success;
        }
        #endregion

        #region 私有变量
        private Dictionary<int, byte[]> _dataList;//扇区数据列表
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置操作结果代码
        /// </summary>
        public CardOperationResultCode ResultCode { get; set; }

        /// <summary>
        /// 获取或设置读到的条码
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置读到的写卡停车场数据，为扇区2数据
        /// </summary>
        public byte[] ParkingDate
        {
            get
            {
                return this[2];
            }
            set
            {
                this[2] = value;
            }
        }

        /// <summary>
        /// 获取或设置section扇区的数据,扇区号（0~15）超过返回null
        /// </summary>
        /// <param name="section">扇区号（0~15）</param>
        /// <returns></returns>
        public byte[] this[int section]
        {
            get
            {
                if (section > -1 && section < 16)
                {
                    if (_dataList.ContainsKey(section))
                    {
                        return _dataList[section];
                    }
                }
                return null;
            }
            set
            {
                if (section > -1 && section < 16)
                {
                    if (_dataList.ContainsKey(section))
                    {
                        _dataList[section] = value;
                    }
                    else
                    {
                        _dataList.Add(section, value);
                    }
                }
            }
 
        }
        #endregion
    }
}
