using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 研色LED桌面屏
    /// </summary>
    public class YanseDesktopLed:IParkingLed 
    {
        #region 构造函数
        public YanseDesktopLed(byte comPort)
        {
            _CommPort = new CommPort(comPort, 57600);
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
        private void SendMessage(string msg)
        {
            //16字节头+ 32字节＋内容（unicode)+ 4字节尾 最少52字节
            byte[] data = GetStrBytes(msg);
            List<byte> packet = new List<byte>();
            //16头
            packet.Add(0xA0);
            packet.AddRange(SEBinaryConverter.ShortToBytes((short)(data.Length + 52)));
            packet.AddRange(SEBinaryConverter.ShortToBytes((short)1));
            packet.AddRange(new byte[8]);
            packet.Add(0x03);
            packet.Add(0x03);
            packet.Add(0x02);
            //32 指令
            packet.AddRange(SEBinaryConverter.ShortToBytes((short)1));
            packet.Add(0x00);
            packet.Add(0x00);
            packet.Add(0x0E);
            packet.Add(0x02);
            packet.Add(0x02);
            packet.AddRange(new byte[17]);
            packet.Add(0x01);
            packet.AddRange(new byte[3]);
            packet.Add(0x0A);
            packet.Add(0x00);
            packet.Add(0x01);
            packet.Add(0x00);
            //内容
            packet.AddRange(data);
            //尾
            packet.AddRange(SEBinaryConverter.ShortToBytes((short)packet.Sum(b => b)));
            packet.Add(0x00);
            packet.Add(0x50);

            _CommPort.SendData(packet.ToArray());
        }

        private byte[] GetStrBytes(string str)
        {
            List<byte> ret = new List<byte>();
            byte[] data = System.Text.Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < data.Length; i += 2)
            {
                int ch = SEBinaryConverter.BytesToInt(new byte[] { data[i], data[i + 1] });
                if (ch >= 0x80)
                {
                    if (ch <= 0xFF)
                        ch -= 0x80;
                    else if (ch >= 0x2000 && ch <= 0x266F)
                        ch = ch - 0x2000 + 128;
                    else if (ch >= 0x3000 && ch <= 0x33FF)
                        ch = ch - 0x3000 + 1648 + 128;
                    else if (ch >= 0x4E00 && ch <= 0x9FA5)
                        ch = ch - 0x4E00 + 1648 + 1024 + 128;
                    else if (ch >= 0xF900 && ch <= 0xFFFF)
                        ch = ch - 0xF900 + 1648 + 1024 + 20902 + 128;
                    ch += 128;
                }
                ret.AddRange(SEBinaryConverter.ShortToBytes((short)ch));
            }
            return ret.ToArray();
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
                            SendMessage(PermanentSentence);
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
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置屏上保存的语句(在屏空闲时显示)
        /// </summary>
        public string PermanentSentence
        {
            get { return _PermanentSentence; }
            set
            {
                if (value != _PermanentSentence)
                {
                    _PermanentSentence = value;
                    SendMessage(_PermanentSentence);
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 实时显示语句(显示时间为10S）
        /// </summary>
        /// <param name="msg"></param>
        public void DisplayMsg(string msg)
        {
            DisplayMsg(msg, 10);
        }

        /// <summary>
        /// 实时显示语句，显示时长为指定的秒数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="persistSeconds"></param>
        public void DisplayMsg(string msg, int persistSeconds)
        {
            SendMessage(msg);
            _LastShowDT = DateTime.Now;
            _PersistSeconds = persistSeconds;
            _Event.Set();
        }

        public void Open()
        {
            _CommPort.Open();

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
