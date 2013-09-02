using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace LJH.GeneralLibrary.CardReader
{
    //add by Jan 2012-3-16
    public class MUR200CardReader : ICardReader
    {
        #region 底层库DLL
        /// <summary>
        /// 密钥A
        /// </summary>
        private const byte KEY_TYPE_A = 0x60;           // 密钥A
        /// <summary>
        /// 密钥B
        /// </summary>
        private const byte KEY_TYPE_B = 0x61;           // 密钥B
        /// <summary>
        /// 使用外部输入的密钥验证
        /// </summary>
        private const byte KEY_SOURCE_EXT = 0x00;		// 使用外部输入的密钥验证
        /// <summary>
        /// 使用内部E2的密钥验证
        /// </summary>
        private const byte KEY_SOURCE_E2 = 0x80;		// 使用内部E2的密钥验证

        private struct ACTIVEPARAA   //ISO14443A卡
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] ATQ;   // ISO14443A卡请求回应
            public byte UIDLen;  // ISO14443A卡UID的字节数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] UID;   // ISO14443A卡UID
            public byte SAK;     // ISO14443A卡选择应答
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:	int __stdcall MUR200Init(void)
        // 函数功能:	MUR200初始化
        // 入口参数:	-					
        // 出口参数:	-
        // 返 回 值:	当前存在MUR-200的个数
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern int MUR200Init();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:	unsigned char MUR200Open(unsigned char ucDvcIndex)
        // 函数功能:	打开MUR-200
        // 入口参数:	unsigned char ucDvcIndex		// 设备索引号					
        // 出口参数:	-
        // 返 回 值:	STATUS_COMM_OK -- 打开成功；	其他值 -- 失败
        // 说    明:	若有多个MUR-200设备，则ucDvcIndex分别为0、1、2......
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MUR200Open(byte ucDvcIndex);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char MFActivate( unsigned char ucMode, unsigned char ucReqCode,  
        //											ACTIVEPARAA *pAInfo)
        // 函数功能:   	A型卡激活
        // 入口参数:    unsigned char ucMode			// 防碰撞模式
        //				unsigned char ucReqCode			// 请求代码
        // 出口参数:    ACTIVEPARAA *pAInfo				// ISO14443 A卡激活信息
        // 返 回 值:    STATUS_SUCCESS -- 成功；其它值 -- 失败。  
        // 说    明:	该函数自动识别M1 和PLUS CPU卡的SL1、SL2和SL3
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MFActivate(byte ucMode, byte ucReqCode, ref ACTIVEPARAA pAInfo);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char MFRead(unsigned char ucBNr, unsigned char ucExt, unsigned char *pBuf)
        // 函数功能:   	MF卡多块读
        // 入口参数:    unsigned char ucBNr				// 读的起始块号
        //				unsigned char ucExt				// 读的块数(取值范围1～3)
        // 出口参数:    unsigned char *pBuf				// 读出的数据
        // 返 回 值:    STATUS_SUCCESS -- 成功；其它值 -- 失败。
        // 说    明:	该函数自动识别M1 和PLUS CPU卡的SL1、SL2和SL3
        //				对于MF1和PLUS SL1的卡，所有被操作的块需要在同一扇区内，否则会出错
        //				对于PLUS SL2/SL3的卡，建议所操作的块均在同一扇区内(因为会自动跳过配置块)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MFRead(byte ucBNr, byte ucExt, byte[] pBuf);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char MFWrite(unsigned char ucBNr, unsigned char ucExt, const unsigned char *pBuf)
        // 函数功能:   	MF卡多块写
        // 入口参数:    unsigned char ucBNr				// 写的起始块号
        //				unsigned char ucExt				// 写的块数(取值范围1～3)
        // 出口参数:    unsigned char *pBuf				// 写入的数据
        // 返 回 值:    STATUS_SUCCESS -- 成功；其它值 -- 失败。
        // 说    明:	该函数自动识别M1 和PLUS CPU卡的SL1、SL2和SL3
        //				对于MF1和PLUS SL1的卡，所有被操作的块需要在同一扇区内，否则会出错
        //				对于PLUS SL2/SL3的卡，建议所操作的块均在同一扇区内(因为会自动跳过配置块)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MFWrite(byte ucBNr, byte ucExt, byte[] pBuf);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char MFHaltA(void)
        // 函数功能:   	将卡置为HALT状态
        // 入口参数:    -
        // 出口参数:    -
        // 返 回 值:    STATUS_SUCCESS -- 成功；其它值 -- 失败。   
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MFHalt();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char SetIO(unsigned char ucContrl, unsigned char ucOpenTm,
        //									unsigned char ucCloseTm, unsigned char ucRepCnt)
        // 函数功能:    打开ucContrl中置1的位
        // 入口参数:    unsigned char ucContrl			// Contrl^0 蜂鸣器,	Contrl^1 红灯,
        //												// Contrl^2 绿灯,   Contrl^3 蓝灯,	
        //				unsigned char ucOpenTm;			// 打开时间，取值0-255， 10ms 的分辨率(0--一直打开)
        //				unsigned char ucCloseTm;		// 关闭时间，取值0-255， 10ms 的分辨率
        //				unsigned char ucRepCnt;			// 重复次数
        // 出口参数:	-
        // 返 回 值:    STATUS_SUCCESS -- 操作成功，其他值 -- 操作失败
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte SetIO(byte ucContrl, byte ucOpenTm, byte ucCloseTm, byte ucRepCnt);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:    unsigned char MFAuth(unsigned char ucAuthMode, unsigned char ucBlock, 
        //									 const unsigned char *pKey)
        // 函数功能:   	Mifare卡密钥验证
        // 入口参数:    unsigned char ucAuthMode		// 验证模式 
        //													KEY_TYPE_A | KEY_SOURCE_EXT	外部输入的密钥A验证
        //													KEY_TYPE_A | KEY_SOURCE_E2	内部E2的密钥A验证
        //													KEY_TYPE_B | KEY_SOURCE_EXT	外部输入的密钥B验证
        //													KEY_TYPE_B | KEY_SOURCE_E2	内部E2的密钥B验证
        //				unsigned char ucBlock			// 验证的块号
        //				unsigned char *pKey				// 使用KEY_SOURCE_E2模式时，pKey[0]为密钥存放的扇区,其它无效
        //												// 使用KEY_SOURCE_EXT模式时，pKey为16字节密钥
        // 出口参数:    -
        // 返 回 值:    STATUS_SUCCESS -- 成功；其它值 -- 失败。
        // 说    明:	该函数自动识别M1 和PLUS CPU卡的SL1、SL2和SL3
        //				对于只有6字节密钥的情况(如M1 S50/70卡)则截取输入的16字节密钥的前6字节作为密钥
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MFAuth(byte ucAuthMode, byte ucBNr, byte[] pKey);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 函数原型:	int __stdcall MUR200Exit(void)
        // 函数功能:	MUR-200关闭退出
        // 入口参数:	-				
        // 出口参数:	-
        // 返 回 值:	STATUS_COMM_OK -- 打开成功；	其他值 -- 失败
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("MUR200.dll")]
        private static extern byte MUR200Exit();
        #endregion

        #region 构造函数
        public MUR200CardReader()
        {
        }
        #endregion

        #region 私有只读属性
        private readonly byte[] KeyBlock = { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1, 0xff, 0x07, 0x80, 0x69, 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };
        private readonly byte[] DefaultKey = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
        private readonly byte[] RalidKey = { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };
        #endregion

        #region 私有属性
        private bool InvalidReading = false;//无效读卡，读到卡片到卡片离开这段时间，读到的卡片为无效读卡
        #endregion
        
        #region 公共属性
        /// <summary>
        /// 获取或设置发卡机目前的状态
        /// </summary>
        public ReaderState State { get; set; }

        /// <summary>
        /// 获取或设置发卡机的韦根类型
        /// </summary>
        public WegenType WegenType { get; set; }

        #endregion

        #region 私有方法
        /// <summary>
        /// 获取UID
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="UIDLen"></param>
        /// <returns></returns>
        private string GetUID(byte[] UID, byte UIDLen)
        {
            ulong value = 0;
            byte[] data = new byte[UIDLen];
            Array.Copy(UID, data, UIDLen);
            value = (ulong)SEBinaryConverter.BytesToLong(data);
            if (WegenType == WegenType.Wengen26)
            {
                value &= 0xffffff;
            }
            else if (WegenType == WegenType.Wengen34)
            {
                value &= 0xffffffff;
            }
            if (value > 0)
            {
                return value.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 操作成功提示
        /// </summary>
        private bool _SucessBuz()
        {
            return SetIO(0x0F, 0x32, 0x14, 0x01) == 0;//LED白色闪烁，蜂鸣器响1次
        }

        /// <summary>
        /// 操作失败提示
        /// </summary>
        private bool _FailBuz()
        {
            return SetIO(0x03, 0x0A, 0x0A, 0x03) == 0;//LED红色闪烁，蜂鸣器响3次
        }

        /// <summary>
        /// 初始化扇区密钥
        /// </summary>
        /// <param name="section"></param>
        /// <param name="originalKey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private CardOperationResultCode _InitKey(int section, byte[] originalKey, byte[] key)
        {
            CardOperationResultCode result = CardOperationResultCode.Success;

            int ret = 1;
            byte[] keyBuf = new byte[16];
            ret = MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (byte)(section * 4 + 3), originalKey);
            if (ret == 0)
            {
                ret = MFRead((byte)(section * 4 + 3), 1, keyBuf);
            }
            else if (result == CardOperationResultCode.Success)
            {
                result = CardOperationResultCode.AuthFail;
            }

            if (ret == 0)
            {
                //字节0~5为A密钥
                keyBuf[0] = key[0];
                keyBuf[1] = key[1];
                keyBuf[2] = key[2];
                keyBuf[3] = key[3];
                keyBuf[4] = key[4];
                keyBuf[5] = key[5];
                //字节6~9为控制字，不需要改
                //字节10~15为密码B
                keyBuf[10] = key[0];
                keyBuf[11] = key[1];
                keyBuf[12] = key[2];
                keyBuf[13] = key[3];
                keyBuf[14] = key[4];
                keyBuf[15] = key[5];
                ret = MFWrite((byte)(section * 4 + 3), 1, keyBuf);
            }

            if (ret != 0 && result == CardOperationResultCode.Success)
            {
                result = CardOperationResultCode.InitKeyFail;
            }

            return result;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化发卡机
        /// </summary>
        public void Init()
        {
            try
            {
                MUR200Init();
                MUR200Open(0);
                State = ReaderState.InWork;
            }
            catch (Exception err)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(err);
            }
        }

        /// <summary>
        /// 释放发卡机的资源
        /// </summary>
        public void Dispose()
        {
            State = ReaderState.OutOfWork;
            MUR200Exit();
        }

        /// <summary>
        /// 读卡片的ID号及数据
        /// </summary>
        /// <returns></returns>
        public ReadCardResult ReadCard()
        {
            ReadCardResult result = new ReadCardResult();
            try
            {
                string value = string.Empty;
                ACTIVEPARAA pAInfo = new ACTIVEPARAA();
                if (MUR200Init() > 0)
                {
                    if (MUR200Open(0) == 0)
                    {
                        if (MFActivate(0, 0x52, ref pAInfo) == 0)
                        {
                            if (!InvalidReading)
                            {
                                InvalidReading = true;
                                value = GetUID(pAInfo.UID, pAInfo.UIDLen);
                                //读取扇区2数据，读取成功将数据返回,失败不作处理
                                if (MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (2 * 4 + 3), RalidKey) == 0)
                                {
                                    byte[] sectiondata = new byte[16 * 3];
                                    if (MFRead((2 * 4 + 0), 3, sectiondata) == 0)
                                    {
                                        result.ParkingDate = new byte[16 * 3];
                                        Array.Copy(sectiondata, result.ParkingDate, 16 * 3);
                                    }
                                }
                                _SucessBuz();
                            }
                            MFHalt();
                        }
                        else
                        {
                            InvalidReading = false;
                        }
                        MUR200Exit();
                    }
                }
                if (!string.IsNullOrEmpty(value))
                {
                    result.CardID = value.ToString();
                }
            }
            catch(Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// 设置某个扇区的密钥,使用默认密钥，修改为设置好的密钥
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        public CardOperationResultCode SetSectionKey(string cardID, int section, bool successBuz, bool failBuz)
        {
            return SetSectionKey(cardID, section, DefaultKey, RalidKey, successBuz, failBuz);
        }

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
        public CardOperationResultCode SetSectionKey(string cardID, int section, byte[] originalKey, byte[] key, bool successBuz, bool failBuz)
        {
            try
            {
                CardOperationResultCode result = CardOperationResultCode.Success;
                int ret = 1;
                ACTIVEPARAA pAInfo = new ACTIVEPARAA();
                if (MUR200Init() > 0)
                {
                    ret = MUR200Open(0);
                }
                else if(result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.InitFail;
                }
                if (ret == 0)
                {
                    ret = MFActivate(0, 0x52, ref pAInfo);
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.OpenFail;
                }
                if (ret == 0)
                {
                    //cardID不为空时，需要检查卡号是否一致，不一致时，返回写卡失败
                    if (!string.IsNullOrEmpty(cardID) && cardID != GetUID(pAInfo.UID, pAInfo.UIDLen))
                    {
                        ret = 1;
                    }
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.NoCard;
                }
                if (ret == 0)
                {
                    result = _InitKey(section, originalKey, key);
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.CardIDError;
                }
                if (ret == 0 && successBuz)
                {
                    _SucessBuz();
                }
                else if (ret != 0 && failBuz)
                {
                    _FailBuz();
                }
                MUR200Exit();
                return result;
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

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
        public ReadCardResult ReadSection(string cardID, int section, int block, byte count, byte[] key, bool successBuz, bool failBuz, bool initKey)
        {
            ReadCardResult result = new ReadCardResult();

            try
            {
                int ret = 1;
                byte[] readbytes = null;
                ACTIVEPARAA pAInfo = new ACTIVEPARAA();
                if (MUR200Init() > 0)
                {
                    ret = MUR200Open(0);
                }
                else if (result.ResultCode == CardOperationResultCode.Success)
                {
                    result.ResultCode = CardOperationResultCode.InitFail;
                }
                if (ret == 0)
                {
                    ret = MFActivate(0, 0x52, ref pAInfo);
                }
                else if (result.ResultCode == CardOperationResultCode.Success)
                {
                    result.ResultCode = CardOperationResultCode.OpenFail;
                }
                if (ret == 0)
                {
                    result.CardID = GetUID(pAInfo.UID, pAInfo.UIDLen);

                    //cardID不为空时，需要检查卡号是否一致，不一致时，返回写卡失败
                    if (!string.IsNullOrEmpty(cardID) && cardID != result.CardID)
                    {
                        ret = 1;
                    }
                }
                else if (result.ResultCode == CardOperationResultCode.Success)
                { 
                    result.ResultCode = CardOperationResultCode.NoCard;
                }
                if (ret == 0)
                {
                    ret = MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (byte)(section * 4 + 3), key);
                }
                else if (result.ResultCode == CardOperationResultCode.Success)
                {
                    result.ResultCode = CardOperationResultCode.CardIDError;
                }
                //密钥验证失败后，如果需要初始化密钥
                if (initKey && ret != 0 && result.ResultCode == CardOperationResultCode.Success)
                {
                    MFActivate(0, 0x52, ref pAInfo);
                    result.ResultCode = _InitKey(section, DefaultKey, key);
                    if (result.ResultCode == CardOperationResultCode.Success)
                    {
                        ret = MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (byte)(section * 4 + 3), key);//再次验证密钥
                    }
                }
                if (ret == 0)
                {
                    readbytes = new byte[16 * count];
                    ret = MFRead((byte)(section * 4 + block), count, readbytes);
                    if (ret == 0)
                    {
                        result[section] = readbytes;
                        result.ResultCode = CardOperationResultCode.Success;
                    }
                    else
                    {
                        result.ResultCode = CardOperationResultCode.Fail;
                    }
                }
                else if (result.ResultCode == CardOperationResultCode.Success)
                {
                    result.ResultCode = CardOperationResultCode.AuthFail;
                }
                if (ret == 0 && successBuz)
                {
                    _SucessBuz();
                }
                else if (ret != 0 && failBuz)
                {
                    _FailBuz();
                }
                MUR200Exit();
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// 把数据写入卡片的某个扇区的某些块中
        /// </summary>
        /// <param name="cardID">需写入卡片的卡号（为null或空时不检查卡号）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="pBuf">写入数据</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        public CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz, bool initKey)
        {
            try
            {
                CardOperationResultCode result = CardOperationResultCode.Success;
                int ret = 1;
                ACTIVEPARAA pAInfo = new ACTIVEPARAA();
                if (MUR200Init() > 0)
                {
                    ret = MUR200Open(0);
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.InitFail;
                }
                if (ret == 0)
                {
                    ret = MFActivate(0, 0x52, ref pAInfo);
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.OpenFail;
                }
                if (ret == 0)
                {
                    //cardID不为空时，需要检查卡号是否一致，不一致时，返回写卡失败
                    if (!string.IsNullOrEmpty(cardID) && cardID != GetUID(pAInfo.UID, pAInfo.UIDLen))
                    {
                        ret = 1;
                    }
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.NoCard;
                }
                if (ret == 0)
                {
                    ret = MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (byte)(section * 4 + 3), key);
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.CardIDError;
                }
                //密钥验证失败后，如果需要初始化密钥
                if (initKey && ret != 0 && result == CardOperationResultCode.Success)
                {
                    MFActivate(0, 0x52, ref pAInfo);
                    result = _InitKey(section, DefaultKey, key);
                    if (result == CardOperationResultCode.Success)
                    {
                        ret = MFAuth(KEY_TYPE_A | KEY_SOURCE_EXT, (byte)(section * 4 + 3), key);//再次验证密钥
                    }
                }
                if (ret == 0)
                {
                    ret = MFWrite((byte)(section * 4 + block), ucExt, pBuf);
                    if (ret != 0) result = CardOperationResultCode.Fail;
                }
                else if (result == CardOperationResultCode.Success)
                {
                    result = CardOperationResultCode.AuthFail;
                }
                if (ret == 0 && successBuz)
                {
                    _SucessBuz();
                }
                else if (ret != 0 && failBuz)
                {
                    _FailBuz();
                }
                MUR200Exit();
                return result;
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

        ///// <summary>
        ///// 把扇区中某些块的数据清零
        ///// </summary>
        ///// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        ///// <param name="section">扇区（0~15）</param>
        ///// <param name="block">起始块号（0~3）</param>
        ///// <param name="ucExt">写入块数(1~3)</param>
        ///// <param name="key">密钥</param>
        ///// <param name="successBuz">成功是否发出提示音</param>
        ///// <param name="failBuz">失败是否发出提示音</param>
        ///// <returns></returns>
        //public CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz)
        //{
        //    byte[] data = new byte[16 * ucExt];
        //    return WriteSection(cardID, section, block, ucExt, data, key, successBuz, failBuz);
        //}

        /// <summary>
        /// 操作读卡器发出成功提示声音
        /// </summary>
        public bool SucessBuz()
        {
            bool result = false;
            try
            {
                if (MUR200Init() > 0)
                {
                    if (MUR200Open(0) == 0)
                    {
                        result = _SucessBuz();
                        MUR200Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// 操作读卡器发出失败提示声音
        /// </summary>
        public bool FailBuz()
        {
            bool result = false;
            try
            {
                if (MUR200Init() > 0)
                {
                    if (MUR200Open(0) == 0)
                    {
                        result = _FailBuz();
                        MUR200Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }
        #endregion
    }
}