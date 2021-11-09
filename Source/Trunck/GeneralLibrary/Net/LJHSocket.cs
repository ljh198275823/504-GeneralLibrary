using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.Net
{
    public class LJHSocket
    {
        #region 构造函数
        public LJHSocket(string ip, int port, ProtocolType pt, string localIP = null)
        {
            this.RemoteIP = ip;
            this.RemotePort = port;
            this.ProtocolType = pt;
            this.LocalIP = localIP;
        }
        #endregion

        #region 私有变量
        private Socket _Client = null;
        private Thread _ReadDataTread = null;
        #endregion

        #region 私有方法
        private void ReadDataTask()
        {
            byte[] buffer = new byte[1024];
            try
            {
                int count = _Client.Receive(buffer);
                while (count > 0)
                {
                    if (count > 0)
                    {
                        byte[] data = new byte[count];
                        Array.Copy(buffer, 0, data, 0, count); //将每次收到的数据放到
                        if (this.OnDataArrivedEvent != null) this.OnDataArrivedEvent(this, data);
                    }
                    Thread.Sleep(20); //这里有一点睡眠时间是希望收多一点数据再接收
                    count = _Client.Receive(buffer);
                }
                _ReadDataTread = null;
                Close();
            }
            catch (Exception ex)
            {
                _ReadDataTread = null;
                Close();
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 公共属性
        public string RemoteIP { get; set; }

        public int RemotePort { get; set; }

        public string LocalIP { get; set; }

        public ProtocolType ProtocolType { get; set; }

        public bool IsConnected
        {
            get { return _Client != null && _Client.Connected; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 数据到达事件
        /// </summary>
        public event DataArrivedDelegate OnDataArrivedEvent;

        public event EventHandler OnClosed;
        #endregion

        #region 公开方法
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                if (!string.IsNullOrEmpty(RemoteIP) && RemotePort > 0)
                {
                    IPEndPoint iep = new IPEndPoint(IPAddress.Parse(RemoteIP), RemotePort);
                    if (ProtocolType == ProtocolType.Tcp)
                    {
                        _Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else if (ProtocolType == ProtocolType.Udp)
                    {
                        _Client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    }
                    if (_Client != null)
                    {
                        if (!string.IsNullOrEmpty(LocalIP)) _Client.Bind(new IPEndPoint(IPAddress.Parse(LocalIP), 0)); //指定了本地地址的，绑定到本地地址
                        _Client.Connect(iep);
                        if (_ReadDataTread == null)
                        {
                            _ReadDataTread = new Thread(new ThreadStart(ReadDataTask));
                            _ReadDataTread.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void SendData(byte[] data)
        {
            try
            {
                if (IsConnected)
                {
                    _Client.Send(data);
                }
            }
            catch (SocketException ex)
            {
                Close();
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            try
            {
                if (IsConnected)
                {
                    _Client.Shutdown(SocketShutdown.Both);
                    _Client.Close();
                }
                if (this.OnClosed != null) this.OnClosed(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion 公开方法
    }
}
