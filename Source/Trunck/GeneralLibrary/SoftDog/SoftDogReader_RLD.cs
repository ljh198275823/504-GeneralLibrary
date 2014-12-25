using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .InteropServices ;

namespace LJH.GeneralLibrary.SoftDog
{
    public class SoftDogReader_RLD
    {
        #region DLL方法
        [DllImport("LJH.dll", EntryPoint = "R20894836291")]
        extern static private int ReadDog_Str(int DogBytes, int DogAddr, StringBuilder DogData, int lngVerify);

        [DllImport("LJH.dll", EntryPoint = "W68721738953")]
        extern private static int WriteDog_Str(int DogBytes, int DogAddr, string DogData, int lngVerify);

        [DllImport("LJH.dll", EntryPoint = "R20894836291")]
        extern private static int ReadDog_Int(int DogBytes, int DogAddr, ref int DogData, int lngVerify);

        [DllImport("LJH.dll", EntryPoint = "W68721738953")]
        extern private static int WriteDog_Int(int DogBytes, int DogAddr, int DogData, int lngVerify);

        [DllImport("LJH.dll", EntryPoint = "R20894836291")]
        extern private static int ReadDog_Double(int DogBytes, int DogAddr, ref double DogData, int lngVerify);

        [DllImport("LJH.dll", EntryPoint = "W68721738953")]
        extern private static int WriteDog_Double(int DogBytes, int DogAddr, double DogData, int lngVerify);

        [DllImport("LJH.dll")]
        extern private static int Q73728193(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q734324393(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q73213213(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q738768673(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q7483247(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q0829382(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q4738495494(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q902109219(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q268489350(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q72819839(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q21774939(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q0987635798(int DogBytes, int DogAddr, string DogData, int lngVerify);
        [DllImport("LJH.dll")]
        extern private static int Q1267173884(int DogBytes, int DogAddr, string DogData, int lngVerify);
        #endregion

        private const int READSTARTINT = 743899832;
        private const int WRITESTARTINT = 812374829;

        private const int CHECKPOSITON = 50;
        private const string CHECKSTRING = "RalidInfo";

        DSEncrypt MydsEncrypt = new DSEncrypt("61Curtis8686");

        private string ReadString(int len, int addr)
        {
            StringBuilder strTemp = new StringBuilder();
            int lngVerify = (int)(READSTARTINT + 1);
            int ret=ReadDog_Str(len, addr, strTemp, lngVerify);
            if (ret == 0)
            {
                return strTemp.ToString().Substring(0, len);
            }
            else
            {
                throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
            }
        }

        private int ReadInteger(int len, int addr)
        {
            int temp = 0;
            int lngVerify = (int)(READSTARTINT + 1);
            if (ReadDog_Int(len, addr, ref temp, lngVerify) == 0)
            {
                return temp;
            }
            else
            {
                throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
            }
        }

        private double ReadDouble(int len, int addr)
        {
            double temp = 0;
            int lngVerify = (int)(READSTARTINT + 1);
            if (ReadDog_Double(len, addr, ref temp, lngVerify) == 0)
            {
                return temp;
            }
            else
            {
                throw new InvalidOperationException("访问加密狗错误，请插入正确的加密狗重试！");
            }
        }

        //检测一下加密狗是否是RALID公司已经写过的
        private bool CheckDog()
        {
            string temp = ReadString(CHECKSTRING.Length, CHECKPOSITON);
            return (CHECKSTRING == MydsEncrypt.Encrypt(temp));
        }

        /// <summary>
        /// 读取加密狗信息,保存到info中,成功返回TRUE,如果加密狗信息错误会产生InvalidOperationException异常
        /// </summary>
        public SoftDogInfo ReadDog()
        {
            SoftDogInfo info = null;
            try
            {
                if (CheckDog())
                {
                    info = new SoftDogInfo();
                    //项目编号
                    info.ProjectNo = int.Parse(MydsEncrypt.Encrypt(ReadString(4, 61)));
                    //适用的软件列表
                    info.SoftwareList = (SoftwareType)ReadInteger(4, 31);
                    //有效期
                    string d = MydsEncrypt.Encrypt(ReadString(6, 71));
                    info.StartDate = DateTime.Parse("20" + d.Substring(0, 2) + "-" + d.Substring(2, 2) + "-" + d.Substring(4, 2));
                    info.Days = ReadInteger(2, 77);
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return info;
        }

        private int GetActualReaderQty(int intReaderQty)
        {
            switch (intReaderQty)
            {
                case 0:
                    return 1;
                case 1:
                    return 8;
                case 2:
                    return 16;
                case 3:
                    return 64;
                case 4:
                    return 128;
                case 5:
                    return 256;
                case 6:
                    return 512;
                case 7:
                    return 1024;
                case 8:
                    return 2048;
                default:
                    return 0;
            }
        }
    }
}
