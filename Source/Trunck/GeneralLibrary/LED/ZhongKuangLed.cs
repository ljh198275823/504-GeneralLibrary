using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 表示一种停车场串口显示屏
    /// </summary>
    public class ZhongKuangLed:IParkingLed 
    {
        #region 构造函数
        public ZhongKuangLed(byte comPort)
        {
            if (comPort > 0)
            {
                _CommPort = new CommPort(comPort,9600);
            }
        }
        #endregion

        #region 私有变量
        private CommPort _CommPort;
        private string _PermanentSentence;
        private Thread _ResetThread;
        private DateTime _LastShowDT = DateTime.Now;
        private int _PersistSeconds = 10;
        private AutoResetEvent _Event;
        #endregion

        #region 私有方法
        /// <summary>
        /// 把语句保存到屏中,并显示出来
        /// </summary>
        /// <param name="msg"></param>
        private void SaveSentence(string msg)
        {
            if (_CommPort.PortOpened)
            {
                byte[] code = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(msg);
                _CommPort.SendData(new byte[] { 0x16 });
                _CommPort.SendData(code);
                _CommPort.SendData(new byte[] { 0x17, 0x1d, 0x0, 0x0 });
            }
        }

        private void ResetThread()
        {
            try
            {
                while (_Event.WaitOne(int.MaxValue))
                {
                    TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _LastShowDT.Ticks);
                    while (true)
                    {
                        if (ts.TotalSeconds >= _PersistSeconds)
                        {
                            SetDateTime(DateTime.Now);
                            SetDisplayMode(2);
                            _Event.Reset();
                            break;
                        }
                        Thread.Sleep(2000);
                        ts = new TimeSpan(DateTime.Now.Ticks - _LastShowDT.Ticks);
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 设置屏的显示模式
        ///0   实时显示输入字符
        ///1   循环显示中文日期+时间+保存的语句内容
        ///2   循环显示保存的语句内容 + 数字日期 + 时间
        ///3   循环显示数字日期+ 时间
        ///4   循环数字显示跳动时间
        /// </summary>
        /// <param name="mode"></param>
        private void SetDisplayMode(byte mode)
        {
            if (_CommPort.PortOpened)
            {
                byte[] data = new byte[] { 0x19, 0x0, mode };
                _CommPort.SendData(data);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取或设置屏上保存的语句
        /// </summary>
        public string PermanentSentence
        {
            get
            {
                return _PermanentSentence;
            }
            set
            {
                _PermanentSentence = value;
                SaveSentence(value);
            }
        }
        /// <summary>
        /// 设置显示屏上的时间
        /// </summary>
        /// <param name="dt"></param>
        public void SetDateTime(DateTime dt)
        {
            string s = dt.ToString("ssmmHHddMMyy");
            byte[] data = new byte[12];
            for (int i = 0; i < s.Length; i++)
            {
                data[i] = byte.Parse(s.Substring(i, 1));
            }
            _CommPort.SendData(new byte[] { 0x1a });
            _CommPort.SendData(data);
        }

        /// <summary>
        /// 实时显示语句,显示时长为指定的秒数
        /// </summary>
        /// <param name="msg"></param>
        public void DisplayMsg(string msg,int persistSeconds)
        {
            if (_CommPort.PortOpened)
            {
                SetDisplayMode(0);
                _CommPort.SendData(new byte[] { 0x13 }); //清屏换行
                byte[] data = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(msg);
                _CommPort.SendData(data);

                //显示几秒后恢复成原来的模式,如果多次调用这个方法，则以最后一次调用时开始算起恢复时间
                _LastShowDT = DateTime.Now;
                _PersistSeconds = persistSeconds;
                _Event.Set();
            }
        }

        /// <summary>
        /// 显示语句，时长为10S
        /// </summary>
        /// <param name="msg"></param>
        public void DisplayMsg(string msg)
        {
            DisplayMsg(msg, 10);
        }

        public void Open()
        {
            _CommPort.Open();
            SetDateTime(DateTime.Now);
            SetDisplayMode(2);

            _Event = new AutoResetEvent(false);
            _ResetThread = new Thread(ResetThread);
            _ResetThread.Start();
        }

        public void Close()
        {
            _CommPort.Close();
            _ResetThread.Abort();
        }
        #endregion
    }
}
