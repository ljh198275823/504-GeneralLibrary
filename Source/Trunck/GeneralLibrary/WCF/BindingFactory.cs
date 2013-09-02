using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.WCF
{
    public class BindingFactory
    {
        /// <summary>
        /// 通过URI来创建一个双向绑定对象
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Binding CreateDualBinding(string uri)
        {
            if (uri.StartsWith("http", false, System.Globalization.CultureInfo.CurrentCulture))
            {
                WSDualHttpBinding binding = new WSDualHttpBinding();
                binding.Security.Mode = WSDualHttpSecurityMode.None;
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                binding.ReceiveTimeout = TimeSpan.FromDays(1);
                return binding;
            }
            else if (uri.StartsWith("net.tcp", false, System.Globalization.CultureInfo.CurrentCulture))
            {
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                binding.ReceiveTimeout = TimeSpan.FromDays(1);
                return binding;
            }
            else
            {
                return null;
            }
        }

        public static Binding CreateBinding(string uri)
        {
            if (uri.StartsWith("http", false, System.Globalization.CultureInfo.CurrentCulture))
            {
                BasicHttpBinding binding=new BasicHttpBinding ();
                binding.Security.Mode = BasicHttpSecurityMode.None;
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                return binding;
            }
            else if (uri.StartsWith("net.tcp", false, System.Globalization.CultureInfo.CurrentCulture))
            {
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                return binding;
            }
            else
            {
                return null;
            }
        }
    }
}
