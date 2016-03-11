using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;

namespace LJH.GeneralLibrary.Net
{
    /// <summary>
    /// 网络编程常用工具类
    /// </summary>
    public class NetTool
    {
        /// <summary>
        /// 获取本机的IP地址,如果有多个IP，返回第一个IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIP()
        {
            try
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ipa in ipe.AddressList)
                {
                    if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) return ipa;
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        public static IPAddress[] GetLocalIPS()
        {
            try
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                if (ipe.AddressList != null)
                {
                    return ipe.AddressList.Where(it => it.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 获取本机的MAC地址,如果有多个网卡，返回第一个网卡的MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalMac()
        {
            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    if (mo["IPEnabled"].ToString() == "True") return mo["MacAddress"].ToString();
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 获取本机计算机名
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            try
            {
                return Dns.GetHostName();
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
    }
}
