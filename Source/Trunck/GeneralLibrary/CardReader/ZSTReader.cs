using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LJH.GeneralLibrary.CardReader
{
    public class ZSTReader
    {
        #region 构造函数
        public ZSTReader()
        {
        }
        #endregion

        #region 私有变量
        private ZSTLib.IR50 _R50;
        private Thread _MessageThread;
        #endregion

        #region 私有方法
        private void Message_Thread()
        {
            try
            {
                string s = null;
                while (true)
                {
                    try
                    {
                        s = _R50.getMessage();
                        if (!string.IsNullOrEmpty(s) && s != "-1")
                        {
                            ZSTReaderEventArgs args = GetArgsFromMessage(s);
                            if (args != null && MessageRecieved != null)
                            {
                                MessageRecieved(this, args);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private ZSTReaderEventArgs GetArgsFromMessage(string msg)
        {
            ZSTReaderEventArgs args = new ZSTReaderEventArgs();
            args.RawMessage = msg;
            string[] lines = msg.Split('\n');
            foreach (string line in lines)
            {
                string[] strs = line.Split(':');
                if (strs != null && strs.Length == 2)
                {
                    if (strs[0] == "type")
                    {
                        args.MessageType = strs[1].Trim(' ', '\r');
                    }
                    if (strs[0] == "ip")
                    {
                        args.ReaderIP = strs[1].Trim(' ', '\r');
                    }
                    if (strs[0] == "卡号")
                    {
                        long cardID = 0;
                        if (long.TryParse(strs[1].Trim(' ', '\r'), out cardID))
                        {
                            args.CardID = cardID.ToString();
                        }
                    }
                    if (strs[0] == "终端号")
                    {
                        args.TeminalNum = strs[1].Trim(' ', '\r');
                    }
                    if (strs[0] == "余额")
                    {
                        decimal balance = 0;
                        if (decimal.TryParse(strs[1].Trim(' ', '\r', '元'), out balance))
                        {
                            args.Balance = balance;
                        }
                    }
                    if (strs[0] == "卡类型代码")
                    {
                        int type=0;
                        if (int.TryParse(strs[1].Trim(' ', '\r'), out type))
                        {
                            args.CardType = type;
                        }
                    }
                }
            }
            return args;
        }
        #endregion

        #region 公共事件
        /// <summary>
        /// 当收到消息时产生此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public event EventHandler<ZSTReaderEventArgs> MessageRecieved;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _R50 = new ZSTLib.R50();
            if (_R50.init() == 0)
            {
                _MessageThread  = new Thread(Message_Thread);
                _MessageThread.Start();
            }
        }
        /// <summary>
        /// 向某个IP地址的读卡器发送消息确认包(一般用于入口读到卡片信息)
        /// </summary>
        /// <param name="ip"></param>
        public void MessageConfirm(string ip)
        {
            _R50 .messageConfirm (ip);
        }
        /// <summary>
        /// 向某个IP地址的读卡器发送扣款指令
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="money"></param>
        public void Consumption(string ip, decimal money)
        {
            string str = Math.Floor(money * 100).ToString();
            if (str.Length % 2 == 1)
            {
                str = "0" + str;
            }
            _R50.consumption(ip, str);
        }
        /// <summary>
        /// 获取所有在线的中山通读卡器
        /// </summary>
        /// <returns></returns>
        public List<string> SearchReaders()
        {
            List<string> strs = new List<string>();
            string ips = _R50.getServerInfo();
            if (!string.IsNullOrEmpty(ips))
            {
                string[] temp = ips.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (temp != null && temp.Length > 0)
                {
                    foreach (string str in temp)
                    {
                        string[] fuck = str.Split(':');
                        if (fuck != null && fuck.Length == 2 && fuck[0].Contains("ip"))
                        {
                            strs.Add(fuck[1].Trim());
                        }
                    }
                }
            }
            return strs;
        }
        /// <summary>
        /// 脱机数据对账统计
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string ConsumptionReport(string ip, DateTime begin, DateTime end)
        {
            return _R50.comsumptionReport(ip, begin.ToString("yyyyMMddHHmmss") + end.ToString("yyyyMMddHHmmss"));
        }
        /// <summary>
        /// 上传消费记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string UploadFile(string ip)
        {
            return _R50.upLoadFile(ip);
        }
        /// <summary>
        /// 设置自动上传消费记录的时间点,最多可以设置8个
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="timers"></param>
        /// <returns></returns>
        public string SetUploadTimer(string ip, List<DateTime> timers)
        {
            if (timers == null || timers.Count == 0)
            {
                return _R50.setUpdateTimer(ip, "{}");
            }
            else if (timers.Count <= 8)
            {
                string temp = string.Empty;
                for (int i = 1; i <= timers.Count; i++)
                {
                    temp += i.ToString() + ":" + timers[i - 1].ToString("HHmm") + ",";
                }
                temp = temp.Substring(0, temp.Length - 1); //去掉最后一个逗号
                temp = "{" + temp + "}";
                return _R50.setUpdateTimer(ip, temp);
            }
            else
            {
                return "时间点数量不正确,最多能设置8个时间点";
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_MessageThread != null)
            {
                _MessageThread.Abort();
                _MessageThread = null;
            }
        }
        #endregion
    }
}
