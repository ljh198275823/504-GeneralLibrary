using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 表示羊城通通信时下发到读卡器的数据包
    /// </summary>
    public class YangChengTongPacket
    {
        private List<byte> _Data = new List<byte>();

        /// <summary>
        /// 获取或设置包的地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 获取或设置包的指令码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 在包数据中增加一个字节
        /// </summary>
        /// <param name="b"></param>
        public void AppendByte(byte data)
        {
            _Data.Add(data);
        }
        
        /// <summary>
        /// 在包数据中增加一个短整数
        /// </summary>
        /// <param name="data"></param>
        public void AppendShort(short data)
        {
            _Data.AddRange(SEBinaryConverter.ShortToBytes(data));
        }

        /// <summary>
        /// 在包的数据中增加一个整数
        /// </summary>
        /// <param name="data"></param>
        public void AppendInt(int data)
        {
            _Data.AddRange(SEBinaryConverter.IntToBytes(data));
        }

        /// <summary>
        /// 在包的数据中增加一个长整数
        /// </summary>
        /// <param name="data"></param>
        public void AppendLong(long data)
        {
            _Data.AddRange(SEBinaryConverter.LongToBytes(data));
        }

        /// <summary>
        /// 在包的数据中增加一个字节数组
        /// </summary>
        /// <param name="data"></param>
        public void AppendArray(byte[] data)
        {
            _Data.AddRange(data);
        }

        /// <summary>
        /// 生成字节指令
        /// </summary>
        /// <returns></returns>
        public byte[] GetBinaryCommand()
        {
            List<byte> data = new List<byte>();
            data.Add((byte)0x09);
            data.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(Address));
            data.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(Code));
            data.AddRange(_Data);
            data.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(GetBCC(data, 1, data.Count)));
            data.Add((byte)0x0d);
            return data.ToArray();
        }

        private string GetBCC(List<byte> data, int beginIndex, int endIndex)
        {
            int total = 0;
            for (int i = beginIndex; i < endIndex; i++)
            {
                total += data[i];
            }
            total &= 0xff;
            return total.ToString("X2");
        }
    }
}
