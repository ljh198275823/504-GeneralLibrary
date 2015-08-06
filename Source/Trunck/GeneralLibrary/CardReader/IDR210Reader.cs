using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime .InteropServices ;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 表示精仑IDR210 二代身份证读卡器
    /// </summary>
    public class IDR210Reader
    {
        #region 导入函数
        private static readonly int SUCCESS = 1;
        [DllImport("sdtapi.dll")]
        private static extern int InitComm(int iPort); //初始化

        [DllImport("sdtapi.dll")]
        private static extern int CloseComm(); //关闭

        [DllImport("sdtapi.dll")]
        private static extern int Authenticate(); //本函数用于发现二代证

        [DllImport("sdtapi.dll")]
        private static extern int ReadBaseMsg(byte[] data, out int len);

        [DllImport("sdtapi.dll")]
        private static extern int ReadBaseMsgPhoto(byte[] data, out int len, string directory); //读取二代证信息和图片,图片放在directory目录下的photo.bmp文件
        #endregion

        #region 构造函数
        public IDR210Reader(int comport)
        {
            Comport = comport;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置读卡器串口, 其中1001表示USB接口
        /// </summary>
        public int Comport { get; set; }
        #endregion

        #region 私有方法
        private byte[] Slice(byte[] source, int index, int count)
        {
            byte[] ret = new byte[count];
            Array.Copy(source, index, ret, 0, count);
            return ret;
        }
        #endregion

        #region 公共方法
        public bool Open()
        {
            int ret = InitComm(Comport);
            return ret == SUCCESS;
        }

        public bool Close()
        {
            int ret = CloseComm();
            return ret == SUCCESS;
        }

        public IdentityCardInfo ReadInfo()
        {
            byte[] data = new byte[192];
            int len = 0;
            int ret = Authenticate();
            string path = System.IO.Path.Combine(Application.StartupPath, "photo.bmp");
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            if (ret == SUCCESS)
            {
                ret = ReadBaseMsg(data, out len);
                if (ret == SUCCESS)
                {
                    IdentityCardInfo idCard = new IdentityCardInfo()
                    {
                        Name = UnicodeEncoding.Default.GetString(Slice(data, 0, 31)).Trim('\0'),
                        Sex = UnicodeEncoding.Default.GetString(Slice(data, 31, 3)).Trim('\0'),
                        Nation = UnicodeEncoding.Default.GetString(Slice(data, 34, 10)).Trim('\0'),
                        BirthDay = UnicodeEncoding.Default.GetString(Slice(data, 44, 9)).Trim('\0'),
                        Address = UnicodeEncoding.Default.GetString(Slice(data, 53, 71)).Trim('\0'),
                        IDNumber = UnicodeEncoding.Default.GetString(Slice(data, 124, 19)).Trim('\0'),
                        Issuer = UnicodeEncoding.Default.GetString(Slice(data, 143, 31)).Trim('\0'),
                        ValidFrom = UnicodeEncoding.Default.GetString(Slice(data, 174, 9)).Trim('\0'),
                        ValidTo = UnicodeEncoding.Default.GetString(Slice(data, 183, 9)).Trim('\0'),
                        Photo = System.IO.File.Exists(path) ? System.IO.Path.Combine(Application.StartupPath, "photo.bmp") : null,
                    };
                    if (!string.IsNullOrEmpty(idCard.Nation) && idCard.Nation.Substring(idCard.Nation.Length - 1) != "族") idCard.Nation += "族";
                    return idCard;
                }
            }
            return null;
        }
        #endregion
    }

    public class IdentityCardInfo
    {
        public IdentityCardInfo()
        {
        }

        #region 公共属性
        public string IDNumber { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public string Nation { get; set; }

        public string BirthDay { get; set; }

        public string Address { get; set; }

        public string Issuer { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public string Photo { get; set; }
        #endregion
    }
}
