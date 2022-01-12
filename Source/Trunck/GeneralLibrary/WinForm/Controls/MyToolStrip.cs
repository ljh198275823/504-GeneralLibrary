using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms ;

namespace LJH.GeneralLibrary.WinformControl
{
    /// <summary>
    /// 这个类继承自ToolStrip,用于解决窗体中使用UCFormView显示窗体时，工具栏需要按两次才有反应的问题。
    /// 只需要将ClickThrough设置成True就可以了。具体原因可能与WINDOW的消息机制有关。
    /// </summary>
    public partial class MyToolStrip : System.Windows.Forms.ToolStrip
    {
        public MyToolStrip()
        {
            InitializeComponent();
        }

        public MyToolStrip(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置当包含它的窗休没有获取焦点时工具栏是否响应
        /// </summary>
        public bool ClickThrough { get; set; }

        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);
            if (ClickThrough &&

                m.Msg == NativeConstants.WM_MOUSEACTIVATE &&

                m.Result == (IntPtr)NativeConstants.MA_ACTIVATEANDEAT)
            {

                m.Result = (IntPtr)NativeConstants.MA_ACTIVATE;

            }

        }
    }



    internal sealed class NativeConstants
    {

        private NativeConstants()
        {

        }

        internal const uint WM_MOUSEACTIVATE = 0x21;

        internal const uint MA_ACTIVATE = 1;

        internal const uint MA_ACTIVATEANDEAT = 2;

        internal const uint MA_NOACTIVATE = 3;

        internal const uint MA_NOACTIVATEANDEAT = 4;

    }
}
