using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.LED
{
    /// <summary>
    /// 中智LED屏
    /// </summary>
    public class ZhongZhiLed
    {
        /// <summary>
        /// 生成一个在LED上显示相应字串的消息包
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] CreateShowMessagePacket(byte addrees,string msg)
        {
            List<byte> data = new List<byte>();
            byte[] msgDate = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(msg);
            data.Add(0x02); //开始
            data.Add((byte)(0x20 + addrees));
            data.Add(0x25); //下载扇区
            data.Add(0x21); //第一扇区
            data.Add(0x0c); //行起始
            data.Add (0x21); //进入模式
            data.Add(0x20);  //停留模式
            data.Add(0x24);  //停留时间
            data.Add(0x20);  //推出模式
            data.Add((byte)(0x20 + msgDate.Length)); //字符长度
            data.AddRange(msgDate);  //字符
            data.Add(0x03);  //结束

            //显示扇区指令
            data.Add(0x02);
            data.Add((byte)(0x20 + addrees));
            data.Add(0x24);  //显示扇区
            data.Add(0x21);  //第一扇区
            data.Add(0x03);  //结束
            return data.ToArray();
        }
    }
}
