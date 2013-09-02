using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    /// <summary>
    /// 表示一个用于放置窗口标题的面板
    /// </summary>
    public partial class FormPanel : UserControl
    {
        public FormPanel()
        {
            InitializeComponent();
            FormHeaderLength = 135;
        }

        #region 私有变量
        private List<FormHeader> _Headers = new List<FormHeader>();
        private readonly int _YOffset=5;
        private object _Locker = new object();
        #endregion

        #region 私有方法
        private void header_Click(object sender, EventArgs e)
        {
            FormHeader header = sender as FormHeader;
            HighLightForm(header.RenderForm);
        }

        private void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            foreach (FormHeader header in _Headers)
            {
                if (object.ReferenceEquals(header.RenderForm, sender))
                {
                    _Headers.Remove(header);
                    Controls.Remove(header);
                    Fresh();
                    break;
                }
            }
            Form frm = this.FindForm();
            if (!object.ReferenceEquals(frm.ActiveMdiChild, sender))
            {
                
            }
            else if (_Headers.Count > 0)
            {
                HighLightForm(_Headers[_Headers.Count - 1].RenderForm);
            }
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置每个窗体标题栏的长度
        /// </summary>
        public int FormHeaderLength { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加一个窗体
        /// </summary>
        /// <param name="frm"></param>
        public void AddAForm(Form frm)
        {
            FormHeader header = new FormHeader();
            header.RenderForm = frm;
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            this.Controls.Add(header);
            _Headers.Add(header);
            header.Click += new EventHandler(header_Click);
            HighLightForm(frm);
            Fresh();
        }

        /// <summary>
        /// 高亮显示窗体
        /// </summary>
        /// <param name="frm"></param>
        public void HighLightForm(Form frm)
        {
            foreach (FormHeader header in _Headers)
            {
                if (object.ReferenceEquals(header.RenderForm, frm))
                {
                    header.Active();
                    frm.Show();
                    frm.Activate();
                }
                else
                {
                    header.Deactive();
                }
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Fresh()
        {
            int x = 0;
            int width = 0;
            if (_Headers.Count > 0)
            {
                if ((this.Width / _Headers.Count) < FormHeaderLength)
                {
                    width = this.Width / _Headers.Count;
                }
                else
                {
                    width = FormHeaderLength;
                }
                foreach (FormHeader header in _Headers)
                {
                    header.Location = new Point(x, _YOffset);
                    header.Width = width;
                    header.Fresh();
                    x += header.Width;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Width > 0)
            {
                Fresh();
            }
            base.OnResize(e);
        }
        #endregion
    }
}
