using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 地下空间满位显示屏
    /// </summary>
    [Serializable]
    public class ParkFullLed
    {
        #region  静态方法
        /// <summary>
        /// 根据配置信息生成满位屏对象
        /// </summary>
        /// <param name="configXml">保存配置信息的文件</param>
        /// <returns></returns>
        public static ParkFullLed Create(string xmlFile)
        {
            ParkFullLed led = null;
            using (FileStream stream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ParkFullLed));
                led = (ParkFullLed)xs.Deserialize(stream);
            }
            return led;
        }

        public static void SaveToFile(ParkFullLed led, string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ParkFullLed));
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                xs.Serialize(stream, led);
            }
        }
        #endregion

        #region 构造函数
        public ParkFullLed()
        {
            this.COMPort = 0;
            this.Baud = 9600;
            this.Greeting = "欢迎光临一号车场";
            this.GreetingLedID = 257;
            this.VacantLedID = 258;
            this.VacantText = "车位余:";
            this.Mark = 16;
            this.ShowMode = 4;
            this.MoveSpeed = 2;
            this.Pause =2;
            this.Append = 0;
            this.RowCharCount =16;
            this.Rows=1;
            this.Color=1;
            this.Reserve =0;
            this.Interval = 500;
        }
        #endregion

        #region 私有变量
        private CommPort _commport;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置串口号
        /// </summary>
        public byte COMPort { get; set; }
        /// <summary>
        /// 获取或设置波特率
        /// </summary>
        public int Baud { get; set; }
        /// <summary>
        /// 获取或设置停车场欢迎屏的ID
        /// </summary>
        public short GreetingLedID { get; set; }
        /// <summary>
        /// 获取或设置在欢迎屏上显示的字符
        /// </summary>
        public string Greeting { get; set; }
        /// <summary>
        /// 获取或设置余位屏ID
        /// </summary>
        public short VacantLedID { get; set; }
        /// <summary>
        /// 获取或设置余位屏显示字符
        /// </summary>
        public string VacantText { get; set; }
        /// <summary>
        /// 获取或设置条屏规格(默认为16)
        /// </summary>
        public byte Mark { get; set; }
        /// <summary>
        /// 获取或设置显示方式
        /// </summary>
        public byte ShowMode { get; set; }
        /// <summary>
        /// 获取或设置移动速度
        /// </summary>
        public byte MoveSpeed { get; set; }
        /// <summary>
        /// 获取或设置停留时间
        /// </summary>
        public byte Pause { get; set; }
        /// <summary>
        /// 获取或设置显示扩展方式
        /// </summary>
        public byte Append { get; set; }
        /// <summary>
        /// 获取或设置条屏每行字符数
        /// </summary>
        public byte RowCharCount { get; set; }
        /// <summary>
        /// 获取或设置屏显示字符行数
        /// </summary>
        public byte Rows { get; set; }
        /// <summary>
        /// 获取或设置屏的颜色
        /// </summary>
        public byte Color { get; set; }
        /// <summary>
        /// 获取或设置保留字节
        /// </summary>
        public byte Reserve { get; set; }
        /// <summary>
        /// 获取或设置两条指令下发间隔(ms)
        /// </summary>
        public int Interval { get; set; }
        #endregion

        #region 私有方法
        //计算校验和
        private short cksum(byte[] data)
        {
            int lenght = data.Length;
            uint sum = 0;

            for (int i = 0; i < lenght; i++)
            {
                if (i % 2 == 0)  //如果是奇数字节
                {
                    sum += (uint)data[i] * 256;
                }
                else
                {
                    sum += (uint)data[i];
                }
            }
            sum = (sum & 0xffff) + ((sum >> 16) & 0xffff);//高16位和低16位相加
            if ((sum & 0xffff0000) > 0) //表示有进位
            {
                sum++;
            }
            return (short)((sum) & 0xffff);
        }

        // 把Short值转换成字节数组(低位字节在前)
        private byte[] ShortToBytes(short t)
        {
            byte[] bytes = new byte[2];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(t >> (i * 8) & 0xFF);
            }
            return bytes;
        }

        private void DisplayMsg(short deviceID, string msg)
        {
            if (_commport != null && _commport.PortOpened)
            {
                byte[] content = UnicodeEncoding.GetEncoding("GB2312").GetBytes(msg);

                List<byte> bytes = new List<byte>();

                //参数11字节
                bytes.AddRange(ShortToBytes(deviceID));
                bytes.Add(Mark);
                bytes.Add(ShowMode);
                bytes.Add(MoveSpeed);
                bytes.Add(Pause);
                bytes.Add(Append);
                bytes.Add(RowCharCount);
                bytes.Add(Rows);
                bytes.Add(Color);
                bytes.Add(Reserve);
                //这两个参数不知何用
                bytes.Add(0x5c);
                bytes.Add(0x52);
                //内容
                bytes.AddRange(content);
                if (content.Length < 16)
                {
                    for (int i = content.Length; i < 16; i++)
                    {
                        bytes.Add(0x20);
                    }
                }

                //长度Length，长度为2字节,不包括Header，Length和End；
                short length = (short)(2 + bytes.Count);
                bytes.InsertRange(0, ShortToBytes(length));
                //头字节
                bytes.Insert(0, 0x55);

                //此前所有数据的校样和 2字节
                byte[] xdata = bytes.ToArray();
                bytes.AddRange(ShortToBytes(cksum(xdata)));
                //尾
                bytes.Add(0xaa);

                byte[] data = bytes.ToArray();
                _commport.SendData(data);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开显示屏通讯
        /// </summary>
        public void Open()
        {
            if (COMPort > 0)
            {
                _commport = new CommPort(COMPort, Baud);
                _commport.Open();
            }
        }

        /// <summary>
        /// 关闭显示屏通讯
        /// </summary>
        public void Close()
        {
            if (_commport != null && _commport.PortOpened)
            {
                _commport.Close();
            }
        }

        /// <summary>
        /// 显示字串
        /// </summary>
        /// <param name="vacant"></param>
        public void DisplayVacantInfo(int vacant)
        {
            if (GreetingLedID > 0 && !string.IsNullOrEmpty(Greeting))
            {
                DisplayMsg(GreetingLedID, Greeting);
            }
            System.Threading.Thread.Sleep(Interval);
            if (VacantLedID > 0)
            {
                DisplayMsg(VacantLedID, string.Format("{0}{1}", string.IsNullOrEmpty(VacantText) ? string.Empty : VacantText, vacant));
            }
        }
        #endregion
    }
}
