using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.CardReader
{
    [Serializable]
    public class BarCodeReadEventArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置读到的条码
        /// </summary>
        public string BarCode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void BarCodeReadEventHandler(object sender, BarCodeReadEventArgs e);

    /// <summary>
    /// 条码扫描枪
    /// </summary>
    public class BarCodeReader
    {
        #region 构造函数
        public BarCodeReader(byte commport)
        {
            _comm = new CommPort(commport, 9600);
            _comm.OnDataArrivedEvent += new DataArrivedDelegate(_comm_OnDataArrivedEvent);
        }
        #endregion

        #region 私有变量
        private CommPort _comm;
        private List<Byte> _msg = new List<byte>();
        #endregion

        #region 私有方法
        private void _comm_OnDataArrivedEvent(object sender, byte[] _data)
        {
            foreach (byte b in _data)
            {
                if (b != 0x0a && b != 0x0d)
                {
                    _msg.Add(b);
                }
                else
                {
                    if (_msg.Count > 0)
                    {
                        string barcode = ASCIIEncoding.ASCII.GetString(_msg.ToArray());
                        if (barcode.Contains('?')) barcode = barcode.Split('?')[1]; // add by aBit 提取有效条码信息，如将2?1234567890转换成1234567890 
                        if (BarCodeRead != null)
                        {
                            BarCodeReadEventArgs args = new BarCodeReadEventArgs();
                            args.BarCode = barcode;
                            _msg.Clear();
                            BarCodeRead(this, args);
                        }
                    }
                }
            }
        }
        #endregion

        #region 公共事件
        /// <summary>
        /// 条码枪扫描到条码后产生的事件
        /// </summary>
        public event BarCodeReadEventHandler BarCodeRead;
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开扫瞄枪
        /// </summary>
        public void Open()
        {
            _comm.Open();
        }
        /// <summary>
        /// 关闭扫瞄枪
        /// </summary>
        public void Close()
        {
            _comm.Close();
        }
        #endregion
    }
}
