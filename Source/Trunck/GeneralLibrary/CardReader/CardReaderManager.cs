using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace LJH.GeneralLibrary.CardReader
{
    public class CardReaderManager:IDisposable
    {
        #region 静态方法和属性
        private static CardReaderManager _man;

        public static CardReaderManager GetInstance(WegenType readerType)
        {
            if (_man == null)
            {
                _man = new CardReaderManager();
                _man.InitReaders();
            }
            foreach (ICardReader reader in _man._Readers)
            {
                reader.WegenType = readerType;
            }
            return _man;
        }
        #endregion

        #region 构造函数
        private CardReaderManager()
        {
        }
        #endregion

        #region 私有变量
        private List<ICardReader> _Readers = new List<ICardReader>();
        private ICardReader _ActiveReader; //表示目前读到卡片的读卡器，
        private Stack<CardReadEventHandler> _CardReadCallBackStack=new Stack<CardReadEventHandler> ();  //读到卡片后的回调函数

        private Thread _ReadCardThread;
        private object _ThreadLocker = new object();
        private bool _Started;

        private bool _Reading;//表示读卡器是否正在进行读卡中
        #endregion 

        #region 私有方法
        private void InitReaders()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                if (t.IsClass && t.GetInterface("ICardReader") != null)
                {
                    try
                    {
                        ICardReader reader = Activator.CreateInstance(t) as ICardReader;
                        if (reader != null)
                        {
                            reader.Init();
                            if (reader.State == ReaderState.InWork)
                            {
                                _Readers.Add(reader);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        ExceptionPolicy.HandleException(err);
                    }
                }
            }
        }

        private ReadCardResult CardRead()
        {
            ReadCardResult result = new ReadCardResult();
            try
            {
                foreach (ICardReader reader in _Readers)
                {
                    result = reader.ReadCard();
                    if (!string.IsNullOrEmpty(result.CardID))
                    {
                        _ActiveReader = reader;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        private void ReadCard_Thread()
        {
            try
            {
                while (true)
                {
                    if (_Started)
                    {
                        _Reading = true;
                        ReadCardResult result = CardRead();
                        if (!string.IsNullOrEmpty(result.CardID) && _CardReadCallBackStack.Count > 0)
                        {
                            CardReadEventArgs args = new CardReadEventArgs()
                            {
                                CardID = result.CardID,
                                ParkingDate = result.ParkingDate
                            };
                            _CardReadCallBackStack.Peek().Invoke(this, args);
                        }
                        _Reading = false;
                    }
                    Thread.Sleep(500);
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            _Reading = false;
        }

        /// <summary>
        /// 等待读卡完成
        /// </summary>
        private void WaitForReading()
        {
            int wait = 0;
            while (wait < 10 && _Reading)//最多等待等待1000毫秒
            {
                wait++;
                Thread.Sleep(100);//正在读卡，等待100毫秒
            }
        }
        #endregion

        #region 公共只读属性

        /// <summary>
        /// 获取当前读到卡片的读卡器
        /// </summary>
        public ICardReader ActiveReader
        {
            get { return _ActiveReader; }
        }

        /// <summary>
        /// 获取是否有读到卡片的读卡器
        /// </summary>
        public bool HadActiveReader
        {
            get { return _ActiveReader != null; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 读卡请求入栈，读卡器读到卡号后调用读卡请求堆栈中的第一个回调函数(目前事件产生时不在UI线程,使用窗体编程时要注意用Invoke方法)
        /// </summary>
        /// <param name="cardReadCallBack"></param>
        public void PushCardReadRequest(CardReadEventHandler cardReadCallBack)
        {
            if (_CardReadCallBackStack.Count > 0 && _CardReadCallBackStack.Peek() == cardReadCallBack)
            {
                //do nothing
            }
            else
            {
                _CardReadCallBackStack.Push(cardReadCallBack);
                BeginReadCard();
            }
        }
        
        /// <summary>
        /// 读卡请求出栈
        /// </summary>
        public void PopCardReadRequest(CardReadEventHandler cardReadCallBack)
        {
            if (_CardReadCallBackStack.Count > 0 && _CardReadCallBackStack.Peek() != null && _CardReadCallBackStack.Peek() == cardReadCallBack)
            {
                _CardReadCallBackStack.Pop();
                if (_CardReadCallBackStack.Count == 0) StopReadCard();
            }
        }
        /// <summary>
        /// 开始读卡
        /// </summary>
        public void BeginReadCard()
        {
            lock (_ThreadLocker)
            {
                if (_ReadCardThread == null)
                {
                    _ReadCardThread = new Thread(ReadCard_Thread);
                    _ReadCardThread.IsBackground = true;
                    _ReadCardThread.Start();
                }
                _Started = true;
            }
        }
        /// <summary>
        /// 停止读卡
        /// </summary>
        public void StopReadCard()
        {
            lock (_ThreadLocker)
            {
                _Started = false;
            }
        }

        /// <summary>
        /// 设置某个扇区的密钥,使用默认密钥，修改为设置好的密钥
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        public CardOperationResultCode SetSectionKey(string cardID, int section, bool successBuz, bool failBuz)
        {

            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }

            CardOperationResultCode result = CardOperationResultCode.InitFail;

            try
            {
                if (HadActiveReader)
                {
                    result = _ActiveReader.SetSectionKey(cardID, section, successBuz, failBuz);
                }
                else
                {
                    foreach (ICardReader reader in _Readers)
                    {
                        result = reader.SetSectionKey(cardID, section, successBuz, failBuz);
                        if (result == CardOperationResultCode.Success)
                        {
                            _ActiveReader = reader;
                            break;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡

            return result;
        }

        /// <summary>
        /// 设置某个扇区的密钥,如果此扇区已经被加密，返回false
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="originalKey">旧密钥（6字节）</param>
        /// <param name="key">新密钥（6字节）</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        public CardOperationResultCode SetSectionKey(string cardID, int section, byte[] originalKey, byte[] key, bool successBuz, bool failBuz)
        {
            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }

            CardOperationResultCode result = CardOperationResultCode.InitFail;

            try
            {
                if (HadActiveReader)
                {
                    result = _ActiveReader.SetSectionKey(cardID, section, originalKey, key, successBuz, failBuz);
                }
                else
                {
                    foreach (ICardReader reader in _Readers)
                    {
                        result = reader.SetSectionKey(cardID, section, originalKey, key, successBuz, failBuz);
                        if (result == CardOperationResultCode.Success)
                        {
                            _ActiveReader = reader;
                            break;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡

            return result;            
        }

        /// <summary>
        /// 从卡片中读卡扇区某些块中的数据,失败返回空
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="count">读取块数(1~3)</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        public ReadCardResult ReadSection(string cardID, int section, int block, byte count, byte[] key, bool successBuz, bool failBuz, bool initKey)
        {
            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }

            ReadCardResult result = null;

            try
            {
                if (HadActiveReader)
                {
                    result = _ActiveReader.ReadSection(cardID, section, block, count, key, successBuz, failBuz, initKey);
                }
                else
                {
                    foreach (ICardReader reader in _Readers)
                    {
                        result = reader.ReadSection(cardID, section, block, count, key, successBuz, failBuz, initKey);
                        if (result != null && !string.IsNullOrEmpty(result.CardID))
                        {
                            _ActiveReader = reader;
                            break;
                        }
                    }
                    if (result == null)
                    {
                        result = new ReadCardResult();
                        result.ResultCode = CardOperationResultCode.InitFail;
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡

            return result; 
        }

        /// <summary>
        /// 把数据写入卡片的某个扇区的某些块中
        /// </summary>
        /// <param name="cardID">需写入卡片的卡号（为null或空时不检查卡号）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="pBuf">写入数据</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="loop">是否循环写入，直到写入成功或取消写入</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        public CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz, bool loop, bool initKey)
        {
            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }

            CardOperationResultCode result = CardOperationResultCode.InitFail;

            try
            {
                while (true)
                {
                    if (HadActiveReader)
                    {
                        result = _ActiveReader.WriteSection(cardID, section, block, ucExt, pBuf, key, successBuz, failBuz, initKey);
                    }
                    else
                    {
                        foreach (ICardReader reader in _Readers)
                        {
                            result = reader.WriteSection(cardID, section, block, ucExt, pBuf, key, successBuz, failBuz, initKey);
                            if (result == CardOperationResultCode.Success)
                            {
                                _ActiveReader = reader;
                                break;
                            }
                        }
                    }

                    if (!loop || result == CardOperationResultCode.Success)
                    {
                        break;//退出循环写入
                    }
                    else
                    {
                        string msg = "信息未能写入卡片，是否重新写入？";
                        switch (result)
                        {
                            case CardOperationResultCode.Success:
                                break;
                            case CardOperationResultCode.NoCard:
                                msg+=string.Format("\r\n原因：{0}","没有卡片！");
                                break;
                            case CardOperationResultCode.CardIDError:
                                msg += string.Format("\r\n原因：{0}", "卡片不是当前发行的卡片！");
                                break;
                            case CardOperationResultCode.AuthFail:
                            case CardOperationResultCode.Fail:
                                msg += string.Format("\r\n原因：{0}", "卡片不是有效卡片！");
                                break;
                            case CardOperationResultCode.InitFail:
                            case CardOperationResultCode.OpenFail:
                                msg += string.Format("\r\n原因：{0}", "打开读卡器失败！");
                                break;
                            default:
                                break;
                        }

                        if (System.Windows.Forms.MessageBox.Show(msg, "询问？",
                            System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        {
                            break;//退出循环写入
                        }
                    }
                }

            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡

            return result;
        }

        /// <summary>
        /// 把数据写入卡片的某个扇区的某些块中
        /// </summary>
        /// <param name="cardID">需写入卡片的卡号（为null或空时不检查卡号）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="pBuf">写入数据</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        public CardOperationResultCode WriteSection(string cardID, int section, int block, byte ucExt, byte[] pBuf, byte[] key, bool successBuz, bool failBuz)
        {
            return WriteSection(cardID, section, block, ucExt, pBuf, key, successBuz, failBuz, false, false);
        }

        /// <summary>
        /// 把扇区中某些块的数据清零
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <param name="loop">是否循环写入，直到写入成功或取消写入</param>
        /// <param name="initKey">密钥验证失败后，是否初始化扇出密钥</param>
        /// <returns></returns>
        public CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz, bool loop, bool initKey)
        {
            byte[] data = new byte[16 * ucExt];
            return WriteSection(cardID, section, block, ucExt, data, key, successBuz, failBuz, loop, initKey);
        }

        /// <summary>
        /// 把扇区中某些块的数据清零
        /// </summary>
        /// <param name="cardID">卡片的卡号（为null或空时不检查卡号是否一致）</param>
        /// <param name="section">扇区（0~15）</param>
        /// <param name="block">起始块号（0~3）</param>
        /// <param name="ucExt">写入块数(1~3)</param>
        /// <param name="key">密钥</param>
        /// <param name="successBuz">成功是否发出提示音</param>
        /// <param name="failBuz">失败是否发出提示音</param>
        /// <returns></returns>
        public CardOperationResultCode ResetSection(string cardID, int section, int block, byte ucExt, byte[] key, bool successBuz, bool failBuz)
        {
            return ResetSection(cardID, section, block, ucExt, key, successBuz, failBuz, false, false);
        }


        /// <summary>
        /// 操作读卡器发出成功提示声音
        /// </summary>
        public void SucessBuz()
        {
            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }
            
            try
            {
                if (HadActiveReader)
                {
                    _ActiveReader.SucessBuz();
                }
                else
                {
                    foreach (ICardReader reader in _Readers)
                    {
                        if (reader.SucessBuz())
                        {
                            _ActiveReader = reader;
                            break;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡
        }

        /// <summary>
        /// 操作读卡器发出失败提示声音
        /// </summary>
        public void FailBuz()
        {
            bool originalStarted = _Started;
            if (originalStarted)
            {
                StopReadCard();//正在读卡，先停止
                WaitForReading();//等待读卡完成
            }

            try
            {
                if (HadActiveReader)
                {
                    _ActiveReader.FailBuz();
                }
                else
                {
                    foreach (ICardReader reader in _Readers)
                    {
                        if (reader.FailBuz())
                        {
                            _ActiveReader = reader;
                            break;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            if (originalStarted) BeginReadCard();//恢复读卡
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            foreach (ICardReader reader in this._Readers)
            {
                reader.Dispose();
            }
        }

        #endregion
    }


    [Serializable]
    public class CardReadEventArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置读到的条码
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置读到的写卡停车场数据，为扇区2数据
        /// </summary>
        public byte[] ParkingDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void CardReadEventHandler(object sender, CardReadEventArgs e);
}
