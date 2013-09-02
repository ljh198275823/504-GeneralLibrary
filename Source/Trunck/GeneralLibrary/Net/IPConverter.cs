using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.Net
{
    public  class IPConverter
    {
        public static string IPtoStr(byte[] ip)
        {
            string s = "";
            foreach (byte b in ip)
            {
                s += b.ToString() + ".";
            }
            return s.Substring(0, s.Length - 1);
        }

        public static string IPtoStr(int ip)
        {
            return IPtoStr(SEBinaryConverter.IntToBytes(ip));
        }

        /// <summary>
        /// 将整形IP转化成字符串型的IP地址,semode表示整形是否是小端模式的整数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="semode"></param>
        /// <returns></returns>
        public static string IPtoStr(int ip,bool semode)
        {
            if (semode)
            {
                return IPtoStr(SEBinaryConverter.IntToBytes(ip));
            }
            else
            {
                byte[] b = SEBinaryConverter.IntToBytes(ip);
                byte[] d = new byte[] { b[3], b[2], b[1], b[0] };
                return IPtoStr(d);
            }
        }

        public static  byte[] IPtoBytes(string ip)
        {
            string[] temp = ip.Split('.');
            byte[] ret = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                ret[i] = Convert.ToByte(temp[i]);
            }
            return ret;
        }

        public static string MACtoStr(byte[] mac)
        {
            string ret = "";
            foreach (byte b in mac)
            {
                ret += b.ToString("X2");
            }
            return ret;
        }

        public static byte[] MACtoBytes(string mac)
        {
            string temp = "";
            byte[] b = new byte[mac.Length / 2];

            for (int i = 0; i < mac.Length; i += 2)
            {
                temp = mac.Substring(i, 2);
                b[i / 2] = Convert.ToByte(temp, 16);
            }
            return b;
        }
    }
}
