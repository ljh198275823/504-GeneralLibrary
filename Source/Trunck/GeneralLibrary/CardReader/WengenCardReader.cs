using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LJH.GeneralLibrary.CardReader
{
    [StructLayout (LayoutKind .Sequential )]
    struct Card_Info
    {
        [MarshalAs (UnmanagedType.U1)]
        public byte Card_Tyte;			// Wiegand数目
        [MarshalAs (UnmanagedType.ByValArray ,SizeConst =8,ArraySubType =UnmanagedType .U1)]
        public byte[] Card_ID ;				//卡号数据
        [MarshalAs (UnmanagedType.ByValArray,SizeConst =4,ArraySubType =UnmanagedType .U2)]
        public ushort[]  Reserved;			//系统保留
    }

    public class WengenCardReader:ICardReader 
    {
        [DllImport("wiegreader.dll")]
        private static extern short ReadCardInfo(ref Card_Info cardInfo);

        public ReaderState State{get;set;}
        public WegenType WegenType { get; set; }

        public void Init()
        {
            Card_Info info = new Card_Info();
            try
            {
                ReadCardInfo(ref info);
                State = ReaderState.InWork;
            }
            catch
            {
                State = ReaderState.OutOfWork;
            }
        }

        public void Dispose()
        {
            State =ReaderState .OutOfWork ;
        }

        public ReadCardResult ReadCard()
        {
            ReadCardResult result = new ReadCardResult();
            long cardID=0;
            Card_Info info = new Card_Info();
            try
            {
                if (ReadCardInfo(ref info) == 1)
                {
                    if (WegenType == WegenType.Wengen26)
                    {
                        cardID = info.Card_ID[7] + info.Card_ID[6] * 256 + info.Card_ID[5] * 256 * 256;
                    }
                    else if (WegenType == WegenType.Wengen34)
                    {
                        cardID = info.Card_ID[7] + info.Card_ID[6] * 256 +
                            info.Card_ID[5] * 256 * 256 + info.Card_ID[4] * (long)(256 * 256 * 256);
                    }
                    if (cardID > 0)
                    {
                        result.CardID = cardID.ToString();
                        return result;
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        #region ICardReader接口

        public CardOperationResultCode SetSectionKey(string cardID, int section, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public CardOperationResultCode SetSectionKey(string cardID, int section, byte[] originalKey, byte[] key, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public ReadCardResult ReadSection(string cardID, int section, int block, byte count, byte[] key, bool successBuz, bool failBuz, bool initKey) { return null; }

        public CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz, bool initKey) { return CardOperationResultCode.Fail; }

        //public CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz) { return CardOperationResultCode.Fail; }

        public bool SucessBuz() { return false; }

        public bool FailBuz() { return false; }
        
        #endregion
    }
}



