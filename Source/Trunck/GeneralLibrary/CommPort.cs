using System;
using System.Windows .Forms ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace LJH.GeneralLibrary
{
    public delegate void DataArrivedDelegate(object sender,byte[] data);
    
    /// <summary>
    /// 串口操作
    /// </summary>
    public class CommPort
    {
        #region 构造方法
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portNum">串口号</param>
        /// <param name="baud">波特率</param>
        public CommPort(byte portNum, int baud)
        {
            _PortNum = portNum;
            InitCommPort(portNum, baud);
        }

        private void _Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_Port.BytesToRead > 0 && this.OnDataArrivedEvent != null)
                {
                    byte[] _buffer = new byte[_Port.BytesToRead];
                    _Port.Read(_buffer, 0, _buffer.Length);
                    if (this.OnDataArrivedEvent != null)
                    {
                        this.OnDataArrivedEvent(this, _buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 成员变量
        private SerialPort _Port;
        private byte _PortNum;
        #endregion 成员变量

        #region 属性
        /// <summary>
        /// 获取串口当前是否打开
        /// </summary>
        public bool PortOpened
        {
            get
            {
                return _Port.IsOpen;
            }
        }
        /// <summary>
        /// 获取串口号
        /// </summary>
        public byte Port
        {
            get
            {
                return _PortNum;
            }
        }
        /// <summary>
        /// 获取或设置串口的波特率
        /// </summary>
        public int BaudRate
        {
            get { return _Port.BaudRate; }
            set { _Port.BaudRate = value; }
        }
        /// <summary>
        /// 获取或设置一个值，该值在串行通信过程中启用数据终端就绪 (DTR) 信号。
        /// </summary>
        public bool DtrEnable
        {
            get
            {
                return _Port.DtrEnable;
            }
            set
            {
                _Port.DtrEnable = value;
            }
        }
        #endregion 属性

        #region 事件
        /// <summary>
        /// 数据到达事件（串口接收到数据后触发此事件将数据提供给上层应用）
        /// </summary>
        public event DataArrivedDelegate OnDataArrivedEvent;
        #endregion 事件

        #region 私有方法
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="_portNum">端口号</param>
        /// <param name="_settings">通信参数</param>
        /// <param name="_rThreshold">触发事件阀值</param>
        private void InitCommPort(short portNum, int baud)
        {
            try
            {
                _Port = new SerialPort("COM" + portNum, baud);
                _Port.ReceivedBytesThreshold = 1;
                _Port.DataReceived += new SerialDataReceivedEventHandler(_Port_DataReceived);
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion 私有方法

        #region 公开方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                if (!this._Port.IsOpen)
                {
                    _Port.Open();
                }
            }
            catch(Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                _Port.Close();
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 将输出缓冲区中的数据发送出去
        /// </summary>
        public void SendData(byte[] outPut)
        {
            try
            {
                if (this.PortOpened)
                {
                    _Port.Write(outPut, 0, outPut.Length);
                }
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion 公开方法
    }
}
