using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LJH.GeneralLibrary.SoftDog
{
    public class SoftDogReader
    {
        #region 动态库引用
        [DllImport("dt215.dll", CharSet = CharSet.Ansi)]
        private static extern int DogRead(int idogBytes, int idogAddr, byte[] pdogData);
        [DllImport("dt215.dll", CharSet = CharSet.Ansi)]
        private static extern int DogWrite(int idogBytes, int idogAddr, byte[] pdogData);
        #endregion

        #region 构造函数
        public SoftDogReader()
            : this(string.Empty)
        {

        }

        public SoftDogReader(string encriptFactor)
        {
            if (!string.IsNullOrEmpty(encriptFactor))
            {
                MydsEncrypt = new DSEncrypt(encriptFactor);
            }
            else
            {
                MydsEncrypt = new DSEncrypt();
            }
        }
        #endregion

        #region 私有变量
        private const int CHECKPOSITON = 70;
        private const string CHECKSTRING = "Ljh198275823";
        private DSEncrypt MydsEncrypt = null;
        #endregion

        #region 公共方法
        public void CheckDog()
        {
            string temp = ReadString(CHECKSTRING.Length, CHECKPOSITON);
            if (CHECKSTRING != MydsEncrypt.Encrypt(temp))
            {
                throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
            }
        }

        public string ReadString(int len, int addr)
        {
            byte[] data = ReadData(len, addr);
            if (data != null && data.Length == len)
            {
                return ASCIIEncoding.GetEncoding("GB2312").GetString(data);
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        public int ReadInteger(int len, int addr)
        {
            if (len > 4) throw new InvalidCastException("超过最大的整形数");
            byte[] data = ReadData(len, addr);
            if (data != null)
            {
                return SEBinaryConverter.BytesToInt(data);
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        public byte[] ReadData(int len, int addr)
        {
            byte[] data = new byte[len];
            int ret = DogRead(len, addr, data);
            if (ret == 0)
            {
                return data;
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        /// <summary>
        /// 读取加密狗信息
        /// </summary>
        public SoftDogInfo ReadDog()
        {
            SoftDogInfo info = null;
            CheckDog();
            info = new SoftDogInfo();
            //项目编号
            info.ProjectNo = ReadInteger(4, 61); // int.Parse(MydsEncrypt.Encrypt(ReadString(4, 61)));
            //适用的软件列表
            info.SoftwareList = (SoftwareType)ReadInteger(4, 31);
            //有效期
            string d = MydsEncrypt.Encrypt(ReadString(6, 17));
            info.StartDate = DateTime.Parse("20" + d.Substring(0, 2) + "-" + d.Substring(2, 2) + "-" + d.Substring(4, 2));
            d = MydsEncrypt.Encrypt(ReadString(6, 5));
            info.ExpiredDate = DateTime.Parse("20" + d.Substring(0, 2) + "-" + d.Substring(2, 2) + "-" + d.Substring(4, 2));
            return info;
        }

        internal void WriteDog(SoftDogInfo info)
        {

        }
        #endregion
    }
}
