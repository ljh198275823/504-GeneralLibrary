using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace LJH.GeneralLibrary.CardReader
{
    /// <summary>
    /// 羊城通读卡器,波特率为9600
    /// </summary>
    public class YangChengTongReader
    {
        #region 构造函数
        public YangChengTongReader(byte comport, byte address)
        {
            this.Address = address;
            this.COMPort = comport;
            if (COMPort > 0)
            {
                //说明,用另一个线程创建COMM组件,则此组件的onComm事件就会在非UI的线程上执行,
                //此处之所以用这样的方法主要间因为让KPM150BarCodePrinter可以在UI线程中同步打印纸票
                Thread t = new Thread(CreateCommPortThread);
                t.Start();
                t.Join();
            }
        }
        #endregion

        #region 私有变量
        private const byte PACKET_HEAD = 0X0A;
        private const byte PACKET_TAIL = 0X0E;
        private const int MIN_PACKET_LENGTH = 8;
        private const int TIMEOUT = 2000;

        private CommPort _CommPort;
        private object _CommportLocker = new object();
        private List<byte> _RecievedData = new List<byte>();
        #endregion

        #region 私有方法
        private void CreateCommPortThread()
        {
            _CommPort = new CommPort(COMPort, 115200);
        }

        //处理读卡命令的返回消息
        private void _Commport_DataRecievedHandler(object sender, byte[] data)
        {
            _RecievedData.AddRange(data);
        }

        private YangChengTongCardInfo GetCardInfoFromRecieveData(List<byte> data)
        {
            //0x0A+[模块地址(2Byte)]+[01(2byte)]+[状态码(2Byte)]+[物理卡号(8Byte)]+[逻辑卡号(10Byte)]+[卡内余额(8Byte)]+[ BCC校验码(2Byte)]+0x0E
            YangChengTongCardInfo card = null;
            bool allZero = true;
            for (int i = 15; i < 25; i++)
            {
                if (data[i] != 0)
                {
                    allZero = false;
                    break;
                }
            }
            if (!allZero)
            {
                card = new YangChengTongCardInfo();
                card.CardID = System.Text.ASCIIEncoding.ASCII.GetString(_RecievedData.GetRange(7, 8).ToArray());
                string b = System.Text.ASCIIEncoding.ASCII.GetString(_RecievedData.GetRange(25, 8).ToArray());
                long balance;
                if (long.TryParse(b, System.Globalization.NumberStyles.HexNumber, null, out balance))
                {
                    card.Balance = (decimal)balance / 100;
                }
            }
            return card;
        }

        //保存羊城通扣款记录,保存在软件安装目录下的RECORD.TXT中
        private void SavePaymentRecord(byte[] recordData)
        {
            //状态码为[00]时：0x0A+[模块地址(2Byte)]+[03(2byte)]+[状态码(2Byte)]+[交易记录(132Byte)]+[ BCC校验码]+0x0E
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "RECORD.txt");
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter bw = new StreamWriter(fs, System.Text.ASCIIEncoding.ASCII))
                {
                    string sb = System.Text.ASCIIEncoding.ASCII.GetString(recordData);
                    bw.WriteLine(sb);
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置读卡器状态
        /// </summary>
        public ReaderState State { get; set; }

        /// <summary>
        /// 获取或设置读卡器串口号
        /// </summary>
        public byte COMPort { get; set; }
        /// <summary>
        /// 获取或设置读卡器地址
        /// </summary>
        public byte Address { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开读卡器
        /// </summary>
        public void Open()
        {
            if (this.COMPort > 0 && this.Address > 0)
            {
                _CommPort.Open();
                if (_CommPort.PortOpened)
                {
                    State = ReaderState.InWork;
                    _CommPort.OnDataArrivedEvent += _Commport_DataRecievedHandler;
                }
                else
                {
                    State = ReaderState.OutOfWork;
                }
            }
        }

        /// <summary>
        /// 关闭羊城通读卡器
        /// </summary>
        public void Close()
        {
            _CommPort.Close();
            _CommPort.OnDataArrivedEvent -= _Commport_DataRecievedHandler;
            State = ReaderState.OutOfWork;
        }

        /// <summary>
        /// 读取羊城通卡信息（带蜂鸣、如读到卡，则此卡必需离开读头模块一次才能被再次读取。）
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public YangChengTongOperationResult ReadCardWithLock(out YangChengTongCardInfo card)
        {
            //0x09+[模块地址(2Byte)]+[01]+[BCC校验码]+0x0D
            YangChengTongOperationResult result;
            lock (_CommportLocker)
            {
                _RecievedData.Clear();
                YangChengTongPacket packet = new YangChengTongPacket();
                packet.Address = Address.ToString("X2");
                packet.Code = "01";
                _CommPort.SendData(packet.GetBinaryCommand());
                Thread.Sleep(TIMEOUT);

                if (_RecievedData.Count > 0)
                {
                    if (_RecievedData.Count == 36)
                    {
                        card = GetCardInfoFromRecieveData(_RecievedData);
                        if (card != null)
                        {
                            result = YangChengTongOperationResult.Success;
                        }
                        else
                        {
                            result = YangChengTongOperationResult.OperationFail;
                            card = null;
                        }
                    }
                    else
                    {
                        result = YangChengTongOperationResult.OperationFail;
                        card = null;
                    }
                }
                else
                {
                    result = YangChengTongOperationResult.CommunicationError;
                    card = null;
                }
                return result;
            }
        }

        /// <summary>
        /// 读取羊城通信息（不带蜂鸣、卡片不需离开模块就可以连续读取）
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public YangChengTongOperationResult ReadCardWithNoLock(out YangChengTongCardInfo card)
        {
            //0x09+[模块地址(2Byte)]+[02]+[BCC校验码]+0x0D
            YangChengTongOperationResult result;
            lock (_CommportLocker)
            {
                _RecievedData.Clear();
                YangChengTongPacket packet = new YangChengTongPacket();
                packet.Address = Address.ToString("X2");
                packet.Code = "02";
                _CommPort.SendData(packet.GetBinaryCommand());
                Thread.Sleep(TIMEOUT);

                if (_RecievedData.Count > 0)
                {
                    if (_RecievedData.Count == 36)
                    {
                        card = GetCardInfoFromRecieveData(_RecievedData);
                        if (card != null)
                        {
                            result = YangChengTongOperationResult.Success;
                        }
                        else
                        {
                            result = YangChengTongOperationResult.OperationFail;
                            card = null;
                        }
                    }
                    else
                    {
                        result = YangChengTongOperationResult.OperationFail;
                        card = null;
                    }
                }
                else
                {
                    result = YangChengTongOperationResult.CommunicationError;
                    card = null;
                }
                return result;
            }
        }

        /// <summary>
        /// 卡片交易，从羊城通扣款
        /// </summary>
        /// <param name="payment">扣除的金额(元)</param>
        /// <returns></returns>
        public YangChengTongOperationResult CardPay(decimal payment)
        {
            //0x09+[模块地址(2Byte)]+[03]+[十六进制表示的以分为单位的金额(8Byte)][BCC校验码]+0x0D
            YangChengTongOperationResult result;
            lock (_CommportLocker)
            {
                _RecievedData.Clear();
                YangChengTongPacket packet = new YangChengTongPacket();
                packet.Address = Address.ToString("X2");
                packet.Code = "03";
                long lng = (long)(payment * 100);
                string sp = lng.ToString("X").PadLeft(8, '0');
                packet.AppendArray(System.Text.ASCIIEncoding.ASCII.GetBytes(sp));
                _CommPort.SendData(packet.GetBinaryCommand());
                Thread.Sleep(TIMEOUT);

                if (_RecievedData.Count > 0)
                {
                    if (_RecievedData.Count == 142)
                    {
                        try
                        {
                            SavePaymentRecord(_RecievedData.GetRange(7, 132).ToArray());
                            result = YangChengTongOperationResult.Success;
                        }
                        catch (Exception ex)
                        {
                            LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                            result = YangChengTongOperationResult.OperationFail;
                        }
                    }
                    else
                    {
                        result = YangChengTongOperationResult.OperationFail;
                    }
                }
                else
                {
                    result = YangChengTongOperationResult.CommunicationError;
                }
                return result;
            }
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        /// <returns></returns>
        public YangChengTongOperationResult SyncTime()
        {
            YangChengTongOperationResult result;
            //上位机指令：0x09+[模块地址(2Byte)]+[04]+[日期(yyyymmddhhnnss)(14Byte)]+[BCC校验码]+0x0D
            lock (_CommportLocker)
            {
                _RecievedData.Clear();
                YangChengTongPacket packet = new YangChengTongPacket();
                packet.Address = Address.ToString("X2");
                packet.Code = "04";
                string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(dt);
                packet.AppendArray(data);
                _CommPort.SendData(packet.GetBinaryCommand());
                Thread.Sleep(TIMEOUT);
                if (_RecievedData.Count == 10)
                {
                    result = YangChengTongOperationResult.Success;
                }
                else
                {
                    result = YangChengTongOperationResult.CommunicationError;
                }
                return result;
            }
        }
        #endregion
    }
}
