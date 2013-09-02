using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.GeneralLibrary.CardReader
{
    public interface ICardReader : IDisposable
    {
        /// <summary>
        /// 获取或设置发卡机目前的状态
        /// </summary>
        WegenType WegenType { get; set; }
        /// <summary>
        /// 获取或设置发卡机的韦根类型
        /// </summary>
        ReaderState State { get; set; }
        /// <summary>
        /// 初始化发卡机
        /// </summary>
        void Init();
        /// <summary>
        /// 读卡片的ID号及数据
        /// </summary>
        /// <returns></returns>
        ReadCardResult ReadCard();
        /// <summary>
        /// 设置某个扇区的密钥,使用默认密钥，修改为设置好的密钥
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        CardOperationResultCode SetSectionKey(string cardID, int section, bool successBuz, bool failBuz);
        /// <summary>
        /// 设置某个扇区的密钥,如果此扇区已经被加密，返回false
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="originalKey">旧密钥（6字节）</param>
        /// <param name="key">新密钥（6字节）</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        CardOperationResultCode SetSectionKey(string cardID, int section, byte[] originalKey, byte[] key, bool successBuz, bool failBuz);
        /// <summary>
        /// 从卡片中读卡扇区某些块中的数据,失败返回空
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="count">读取块数(1~3)</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        ReadCardResult ReadSection(string cardID, int section, int block, byte count, byte[] key, bool successBuz, bool failBuz, bool initKey);
        /// <summary>
        /// 把数据写入卡片的某个扇区的某些块中
        /// </summary>
        /// <param name="cardID">需写入卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="pBuf">写入数据</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz, bool initKey);
        ///// <summary>
        ///// 把扇区中某些块的数据清零
        ///// </summary>
        ///// <param name="cardID">需清零卡片的卡号（为null或空时不检查卡号）</param>
        ///// <param name="section">扇区（0~15）</param>
        ///// <param name="block">起始块号（0~3）</param>
        ///// <param name="ucExt">写入块数(1~3)</param>
        ///// <param name="key">密钥</param>
        ///// <param name="successBuz">成功是否发出提示音</param>
        ///// <param name="failBuz">失败是否发出提示音</param>
        ///// <returns></returns>
        //CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz);
        /// <summary>
        /// 操作读卡器发出成功提示声音
        /// </summary>
        bool SucessBuz();
        /// <summary>
        /// 操作读卡器发出失败提示声音
        /// </summary>
        bool FailBuz();
    }
}
