using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 表示BX4K系列LED控制板的控制器类,一个控制器类可以管理多个控制板，控制板之间通过485协议通讯
    /// </summary>
    public class BX4KLEDControler
    {
        #region 构造函数
        public BX4KLEDControler(byte commport, int baudRate)
        {
            if (commport > 0)
            {
                Action action = delegate()
                {
                    CommPort = new CommPort(commport, baudRate);
                    CommPort.OnDataArrivedEvent += new DataArrivedDelegate(CommPort_OnDataArrivedEvent);
                };

                //说明,用另一个线程创建COMM组件,则此组件的onComm事件就会在非UI的线程上执行,
                Thread t = new Thread(new ThreadStart(action));
                t.Start();
                t.Join();
            }
        }
        #endregion

        #region 私有变量
        protected CommPort CommPort = null;
        #endregion

        #region 私有方法
        private void CommPort_OnDataArrivedEvent(object sender, byte[] data)
        {

        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取串口号
        /// </summary>
        public int Port
        {
            get { return CommPort.Port; }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开控制器
        /// </summary>
        public bool Open()
        {
            if (!CommPort.PortOpened)
            {
                CommPort.Open();
                return CommPort.PortOpened;
            }
            return true;
        }

        /// <summary>
        /// 关闭控制器
        /// </summary>
        public void Close()
        {
            if (CommPort.PortOpened)
            {
                CommPort.Close();
            }
        }

        /// <summary>
        /// 动态显示区域内容
        /// </summary>
        /// <param name="address">控制板地址</param>
        /// <param name="deviceType">控制板类型 0x51=bx4k1 0x52=bx4k2</param>
        /// <param name="clear">是否清空之前的动态区域内容</param>
        /// <param name="areas">区域及区域内容</param>
        public void DynamicDisplay(short address, byte deviceType, bool clear, List<BX4KDynamicArea> areas)
        {
            if (areas == null || areas.Count <= 0) return;
            List<byte> frame = new List<byte>();
            List<byte> packet = new List<byte>();
            List<byte> cmd = new List<byte>();
            for (int i = areas.Count - 1; i >= 0; i--)
            {
                BX4KDynamicArea area = areas[i];
                List<byte> areaData = new List<byte>();
                byte[] content = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(area.Text);
                if (content != null && content.Length > 0) areaData.InsertRange(0, content); //内容
                areaData.InsertRange(0, SEBinaryConverter.IntToBytes(content == null ? 0 : content.Length)); //内容长度
                areaData.Insert(0, area.StayTime);  //
                areaData.Insert(0, area.Speed);
                areaData.Insert(0, 0x0);  //exitcode
                areaData.Insert(0, (byte)area.DisplayMode);
                areaData.Insert(0, (byte)(area.NewLine ? 0x1 : 0x2));
                areaData.Insert(0, (byte)(area.SingleLine ? 0x1 : 0x2));
                areaData.Insert(0, area.FontCode);  //
                areaData.InsertRange(0, new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff });
                areaData.Insert(0, (byte)i);  //areaID
                areaData.InsertRange(0, SEBinaryConverter.ShortToBytes(area.Height));
                areaData.InsertRange(0, SEBinaryConverter.ShortToBytes(area.Width));
                areaData.InsertRange(0, SEBinaryConverter.ShortToBytes(area.Y));
                areaData.InsertRange(0, SEBinaryConverter.ShortToBytes(area.X));
                areaData.Insert(0, area.AreaType);
                areaData.InsertRange(0, SEBinaryConverter.ShortToBytes((short)areaData.Count)); //区域数据长度
                cmd.InsertRange(0, areaData);
            }
            cmd.Insert(0, (byte)areas.Count);  //区域数量
            cmd.Insert(0, (byte)(clear ? 0xff : 0x0));  //删除区域个数
            cmd.InsertRange(0, SEBinaryConverter.ShortToBytes((short)cmd.Count));
            cmd.Insert(0, 0x02);  //Response 0x01=命令须返回 0x02=命令不须返回
            cmd.Insert(0, 0x06);  //cmd
            cmd.Insert(0, 0xA3);  //CmdGroup

            packet.InsertRange(0, cmd);
            packet.InsertRange(0, SEBinaryConverter.ShortToBytes((short)cmd.Count));
            packet.Insert(0, 0x0); //协议版本号
            packet.Insert(0, deviceType);  //设备类型
            packet.InsertRange(0, new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });//保留
            packet.InsertRange(0, new byte[] { 0x0, 0x80 }); //源地址 0x8000
            packet.InsertRange(0, SEBinaryConverter.ShortToBytes(address)); //目标地址

            frame.AddRange(new byte[] { 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5 });//帧头
            frame.AddRange(packet);  //包数据
            frame.AddRange(SEBinaryConverter.ShortToBytes(BX4KCRCCalculator.CalCRC(packet.ToArray())));
            frame.Add(0x5A); //帧尾

            if (CommPort.PortOpened)
            {
                CommPort.SendData(frame.ToArray());
            }
        }
        /// <summary>
        /// 设置亮度
        /// </summary>
        /// <param name="address"></param>
        /// <param name="deviceType"></param>
        /// <param name="brightness"></param>
        public void SetBrightness(short address, byte deviceType, byte brightness)
        {
            List<byte> frame = new List<byte>();
            List<byte> packet = new List<byte>();
            List<byte> cmd = new List<byte>();

            cmd.Insert(0, brightness); //亮度
            cmd.Insert(0, 0x01);  //强制调节亮度
            cmd.InsertRange(0, SEBinaryConverter.ShortToBytes(0x02));  //数据长度
            cmd.Insert(0, 0x02);  //Response 0x01=命令须返回 0x02=命令不须返回
            cmd.Insert(0, 0x02);  //命令编号
            cmd.Insert(0, 0xA3);  //CmdGroup

            packet.InsertRange(0, cmd);
            packet.InsertRange(0, SEBinaryConverter.ShortToBytes((short)cmd.Count));
            packet.Insert(0, 0x0); //协议版本号
            packet.Insert(0, deviceType);  //设备类型
            packet.InsertRange(0, new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });//保留
            packet.InsertRange(0, new byte[] { 0x0, 0x80 }); //源地址 0x8000
            packet.InsertRange(0, SEBinaryConverter.ShortToBytes(address)); //目标地址

            frame.AddRange(new byte[] { 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5, 0xA5 });//帧头
            frame.AddRange(packet);  //包数据
            frame.AddRange(SEBinaryConverter.ShortToBytes(BX4KCRCCalculator.CalCRC(packet.ToArray())));
            frame.Add(0x5A); //帧尾

            if (CommPort.PortOpened)
            {
                CommPort.SendData(frame.ToArray());
            }
        }

        #endregion
    }
}
