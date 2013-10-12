using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LJH.GeneralLibrary.WinformControl
{
    /// <summary>
    /// 表示一个显示百分比的进度条
    /// </summary>
    public partial class PercentageProgressBar : ProgressBar
    {
        #region 构造函数
        /// <summary>
        /// 表示一个显示百分比的进度条
        /// </summary>
        public PercentageProgressBar()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 表示一个显示百分比的进度条
        /// </summary>
        /// <param name="container"></param>
        public PercentageProgressBar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region DLL调用函数
        [DllImport("user32.dll ")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll ")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        #endregion

        #region 私有变量
        private Color _TextColor = System.Drawing.Color.Black;
        private Font _TextFont = new System.Drawing.Font("宋体", 9);
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置百分比颜色
        /// </summary>
        public Color TextColor
        {

            get { return _TextColor; }

            set { _TextColor = value; this.Invalidate(); }

        }
        /// <summary>
        /// 获取或设置百分比字体
        /// </summary>
        public Font TextFont
        {

            get { return _TextFont; }

            set { _TextFont = value; this.Invalidate(); }

        }
        #endregion

        #region 重写方法
        /// <summary>
        /// 处理Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref   Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                //拦截系统消息，获得当前控件进程以便重绘。  

                //一些控件（如TextBox、Button等）是由系统进程绘制，重载OnPaint方法将不起作用.  

                //所有这里并没有使用重载OnPaint方法绘制TextBox边框。  

                //  

                //MSDN:重写   OnPaint   将禁止修改所有控件的外观。  

                //那些由   Windows   完成其所有绘图的控件（例如   Textbox）从不调用它们的   OnPaint   方法，  

                //因此将永远不会使用自定义代码。请参见您要修改的特定控件的文档，  

                //查看   OnPaint   方法是否可用。如果某个控件未将   OnPaint   作为成员方法列出，  

                //则您无法通过重写此方法改变其外观。  

                //  

                //MSDN:要了解可用的   Message.Msg、Message.LParam   和   Message.WParam   值，  

                //请参考位于   MSDN   Library   中的   Platform   SDK   文档参考。可在   Platform   SDK（“Core   SDK”一节）  

                //下载中包含的   windows.h   头文件中找到实际常数值，该文件也可在   MSDN   上找到。  

                IntPtr hDC = GetWindowDC(m.HWnd);

                if (hDC.ToInt32() == 0)
                {

                    return;

                }

                //base.OnPaint(e);

                System.Drawing.Graphics g = Graphics.FromHdc(hDC);

                SolidBrush brush = new SolidBrush(_TextColor);

                string s = this.Maximum == 0 ? "100%" : string.Format("{0}%", this.Value * 100 / this.Maximum);

                SizeF size = g.MeasureString(s, _TextFont);

                float x = (this.Width - size.Width) / 2;

                float y = (this.Height - size.Height) / 2;

                g.DrawString(s, _TextFont, brush, x, y);

                //返回结果  

                m.Result = IntPtr.Zero;

                //释放  

                ReleaseDC(m.HWnd, hDC);

            }

        }

        #endregion
    }
}
