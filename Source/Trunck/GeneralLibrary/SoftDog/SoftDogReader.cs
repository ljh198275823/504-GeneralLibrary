using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

namespace LJH.GeneralLibrary.SoftDog
{
    public class SoftDogReader
    {
        #region 动态库引用
        [DllImport("dt215.dll", CharSet = CharSet.Ansi, EntryPoint = "DogRead")]
        private static extern int DogRead_32(int idogBytes, int idogAddr, byte[] pdogData);
        [DllImport("dt215.dll", CharSet = CharSet.Ansi, EntryPoint = "DogWrite")]
        private static extern int DogWrite_32(int idogBytes, int idogAddr, byte[] pdogData);

        [DllImport("dt215L.dll", CharSet = CharSet.Ansi, EntryPoint = "DogRead")]
        private static extern int DogRead_64(int idogBytes, int idogAddr, byte[] pdogData);
        [DllImport("dt215L.dll", CharSet = CharSet.Ansi, EntryPoint = "DogWrite")]
        private static extern int DogWrite_64(int idogBytes, int idogAddr, byte[] pdogData);
        #endregion

        #region 构造函数
        public SoftDogReader(string encriptFactor)
        {
            if (!string.IsNullOrEmpty(encriptFactor))
            {
                _key = encriptFactor;
                string temp = new DTEncrypt().DSEncrypt(encriptFactor);
                MydsEncrypt = new DSEncrypt(temp);
            }
        }
        #endregion

        #region 私有变量
        private string _key = null;
        private const int CHECKPOSITON = 70;
        private const string CHECKSTRING = "Ljh198275823";
        private DSEncrypt MydsEncrypt = null;
        private string _SystemBits = null;
        #endregion

        #region 私有方法
        private string Distinguish64or32System()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope("//localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return addressWidth;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
        }
        #endregion

        #region 公共方法
        public void CheckDog()
        {
            string temp = ReadString(CHECKPOSITON, CHECKSTRING.Length);
            if (CHECKSTRING != MydsEncrypt.Encrypt(temp))
            {
                throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
            }
        }

        public string ReadString(int addr, int len)
        {
            byte[] data = ReadData(addr, len);
            if (data != null && data.Length == len)
            {
                return ASCIIEncoding.GetEncoding("GB2312").GetString(data);
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        public int ReadInteger(int addr, int len)
        {
            if (len > 4) throw new InvalidCastException("超过最大的整形数");
            byte[] data = ReadData(addr, len);
            if (data != null)
            {
                return SEBinaryConverter.BytesToInt(data);
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        public byte[] ReadData(int addr, int len)
        {
            byte[] data = new byte[len];
            int ret = -1;
            //if (string.IsNullOrEmpty(_SystemBits)) _SystemBits = Distinguish64or32System();
            ret = DogRead_32(len, addr, data);
            if (ret == 0)
            {
                return data;
            }
            throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
        }

        public int WriteData(int addr, byte[] data, string key)
        {
            int ret = -1;
            if (!string.IsNullOrEmpty(_key) && new DTEncrypt().DSEncrypt(_key) == key)
            {
                //if (string.IsNullOrEmpty(_SystemBits)) _SystemBits = Distinguish64or32System();
                ret = DogWrite_32(data.Length, addr, data);
            }
            return ret;
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
            info.ProjectNo = ReadInteger(61, 4);
            //适用的软件列表
            info.SoftwareList = (SoftwareType)ReadInteger(31, 4);
            //有效期
            string d = MydsEncrypt.Encrypt(ReadString(17, 6));
            info.StartDate = DateTime.Parse("20" + d.Substring(0, 2) + "-" + d.Substring(2, 2) + "-" + d.Substring(4, 2));
            d = MydsEncrypt.Encrypt(ReadString(5, 6));
            info.ExpiredDate = DateTime.Parse("20" + d.Substring(0, 2) + "-" + d.Substring(2, 2) + "-" + d.Substring(4, 2));
            //是否是主机加密狗
            info.IsHost = ReadInteger(37, 1) == 1;
            string temp = ReadString(40, 10).Trim();
            if (!string.IsNullOrEmpty(temp)) info.DBUser = MydsEncrypt.Encrypt(temp);
            temp = ReadString(50, 10).Trim();
            if (!string.IsNullOrEmpty(temp)) info.DBPassword = MydsEncrypt.Encrypt(temp);
            return info;
        }
        #endregion
    }
}
