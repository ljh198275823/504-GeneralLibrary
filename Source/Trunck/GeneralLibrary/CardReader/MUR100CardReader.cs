using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime .InteropServices ;

namespace LJH.GeneralLibrary.CardReader
{
    public class MUR100CardReader : ICardReader
    {
        #region DLL动态库调用
        /// <summary>
        /// 函数功能：此函数的功能是打开USB。
        /// </summary>
        /// <returns></returns>
        [DllImport("RC500USB.dll")]
        private static extern byte RC500USB_init();

        /// <summary>
        /// 函数功能：此函数发送Request 命令检查在有效范围内是否有卡的存在。返回值0 表示成功，否则返回错误码。
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="tagType"></param>
        /// <returns></returns>
        [DllImport("RC500USB.dll")]
        private static extern byte RC500USB_request(byte Mode, ref ushort tagType);

        /// <summary>
        /// 函数功能：此函数开始防冲突操作。返回值0 表示成功，否则返回错误码。
        /// </summary>
        /// <param name="bcnt"></param>
        /// <param name="snr"></param>
        /// <returns></returns>
        [DllImport("RC500USB.Dll")]
        private static extern byte RC500USB_anticoll(byte bcnt, ref ulong snr);

        /// <summary>
        /// 函数功能：此函数使被选择的卡产生一个暂停，即使之处于Halt 模式，处于Halt 模式的卡只能用ALL方式进行选择，即RC500USB_request 函数中的mode 参数值设为1
        /// 返回值0 表示成功，否则返回错误码
        /// </summary>
        /// <returns></returns>
        [DllImport("RC500USB.dll")]
        private static extern byte RC500USB_halt();

        /// <summary>
        /// 函数功能：此函数输出一驱动信号，可驱动蜂鸣器和绿色发光管持续时间，间隙时间和重复次数可调。
        /// 返回值0 表示成功，否则返回错误码。
        /// </summary>
        /// <param name="contrl">控制字如下表相应位为1 时该器件动作,contrl^1为绿灯,contrl^0 为蜂鸣器</param>
        /// <param name="opentm">低电平持续时间取值0-255 10ms 的分辨率</param>
        /// <param name="closetm">高电平间隙时间取值0-255 10ms 的分辨率</param>
        /// <param name="repcnt">重复次数</param>
        /// <returns></returns>
        [DllImport("RC500USB.dll")]
        private static extern byte RC500USB_buzzer(byte contrl, byte opentm, byte closetm, byte repcnt);

        /// <summary>
        /// 函数功能：此函数的功能是关闭USB。
        /// </summary>
        [DllImport("RC500USB.dll")]
        private static extern void RC500USB_exit();
        #endregion

        #region 私有属性
        private bool InvalidReading = false;//无效读卡，读到卡片到卡片离开这段时间，读到的卡片为无效读卡
        #endregion

        #region 公共方法和属性
        public ReaderState State { get; set; }
        public WegenType WegenType { get; set; }
        public void Init()
        {
            try
            {
                RC500USB_init();
                State = ReaderState.InWork;
            }
            catch (Exception err)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(err);
            }
        }

        public void Dispose()
        {
            State = ReaderState.OutOfWork;
            RC500USB_exit();
        }


        public ReadCardResult ReadCard()
        {
            ReadCardResult result = new ReadCardResult();
            try
            {
                long cardID = RCReadCard();

                if (cardID > 0)
                {
                    result.CardID = cardID.ToString();
                    return result;
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion

        #region ICardReader接口

        public CardOperationResultCode SetSectionKey(string cardID, int section, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public CardOperationResultCode SetSectionKey(string cardID, int section, byte[] originalKey, byte[] key, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public ReadCardResult ReadSection(string cardID, int section, int block, byte count, byte[] key, bool successBuz, bool failBuz, bool initKey) { return null; }

        public CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz, bool initKey) { return CardOperationResultCode.Fail; }

        //public CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public bool SucessBuz() { return RC500USB_buzzer(3, 50, 20, 1) == 0; ;}

        public bool FailBuz() { return RC500USB_buzzer(3, 50, 20, 3) == 0; }
        #endregion

        private long RCReadCard()
        {
            ushort s = 0;
            ulong value = 0;
            if (RC500USB_init() == 0)
            {
                if (RC500USB_request(0, ref s) == 0)
                {
                    if (!InvalidReading)
                    {
                        InvalidReading = true;
                        if (RC500USB_anticoll(0, ref value) == 0)
                        {
                            if (WegenType == WegenType.Wengen26)
                            {
                                value &= 0xffffff;
                            }
                            else if (WegenType == WegenType.Wengen34)
                            {
                                value &= 0xffffffff;
                            }
                            RC500USB_buzzer(3, 50, 20, 1);
                            RC500USB_halt();
                        }
                    }
                }
                else
                {
                    InvalidReading = false;
                }
                RC500USB_exit();
            }
            return (long)value;
        }
    }
}

//        '/************************************************************
//        '                   函数返回值定义:
//        '       0: 操作成功;
//        '       1: 在有效区域内没有卡;(Check Write出错)
//        '       2: 从卡中接收到了错误的CRC校验和;(Check Write:写出错（比较出错）)
//        '       3: 值溢出;
//        '       4: 不能验证;
//        '       5: 从卡中接收到了错误的校验位;
//        '       6: 通信错误;
//        '       8: 在防冲突时读到了错误的串行码;
//        '       10: 卡没有验证;
//        '       11: 从卡中接收到了错误数量的位;
//        '       12: 从卡中接收了错误数量的字节;
//        '       14: 调用Transfer函数出错;
//        '       15: 调用Write函数出错。
//        '       16: 调用Increment函数出错
//        '       17: 调用Decrment函数出错
//        '       18: 调用Read函数出错
//        '       24: 冲突错
//        '       30: 上一次了送命令时被打断
//        '       255:串行通信错误
//        '/************************************************************
