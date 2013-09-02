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
    public partial class UCFormView : UserControl
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private extern static long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public UCFormView()
        {
            InitializeComponent();
            FormHeaderLength = 135;
        }

        #region 私有变量
        private List<FormHeader> _Headers = new List<FormHeader>();
        private readonly int _YOffset = 5;
        private object _Locker = new object();
        private FormHeader _ActiveHeader;
        #endregion

        #region 私有方法
        /// <summary>
        /// 高亮显示窗体
        /// </summary>
        /// <param name="header"></param>
        private void HighLightForm(FormHeader header)
        {
            foreach (FormHeader h in _Headers)
            {
                if (object.ReferenceEquals(h, header))
                {
                    _ActiveHeader = h;
                    h.Active();
                    header.RenderForm.Size = new Size(this.pBody.Size.Width, this.pBody.Height);
                    SetParent(header.RenderForm.Handle, this.pBody.Handle);
                }
                else
                {
                    h.Deactive();
                }
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void FreshHeader()
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
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowInTaskbar = false ;
            frm.StartPosition = FormStartPosition.Manual;
            FormHeader header = new FormHeader();
            header.FormView = this;
            header.RenderForm = frm;
            frm.FormClosed -= new FormClosedEventHandler(frm_FormClosed);
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.Show();
            this.pHeader.Controls.Add(header);
            _Headers.Add(header);
            header.Click -= new EventHandler(header_Click);
            header.Click += new EventHandler(header_Click);
            HighLightForm(header);
            FreshHeader();
        }

        /// <summary>
        /// 使某个窗体处理活动状态
        /// </summary>
        /// <param name="frm"></param>
        public void ActiveForm(Form frm)
        {
            foreach (FormHeader header in _Headers)
            {
                if (object.ReferenceEquals(header.RenderForm, frm))
                {
                    HighLightForm(header);
                    break;
                }
            }
        }

        /// <summary>
        /// 移除一个窗体，如果在列表中存在的话
        /// </summary>
        /// <param name="frm"></param>
        public void RemoveForm(Form frm)
        {
            foreach (FormHeader header in _Headers)
            {
                if (object.ReferenceEquals(header.RenderForm, frm))
                {
                    if (object.ReferenceEquals(header, _ActiveHeader)) //如果关闭的是当前的活动窗体
                    {
                        int index = _Headers.IndexOf(header);
                        //如果当前活动窗体处理列表末尾，则前一个窗体变成活动窗体,其它情况下右边一个窗体为活动窗体
                        if (index == _Headers.Count - 1)
                        {
                            index -= 1;
                        }
                        else
                        {
                            index += 1;
                        }
                        if (index >= 0)
                        {
                            if (_Headers.Count > 0) HighLightForm(_Headers[index]);
                        }
                        else
                        {
                            _ActiveHeader = null;
                        }
                    }
                    frm.FormClosed -= new FormClosedEventHandler(frm_FormClosed);
                    header.FormView = null;
                    _Headers.Remove(header);
                    header.Click -= new EventHandler(header_Click);
                    this.pHeader.Controls.Remove(header);
                    FreshHeader();
                    break;
                }
            }
        }
        #endregion

        #region 事件处理函数
        private void pBody_Resize(object sender, EventArgs e)
        {
            if (this.Width > 0)
            {
                if (_ActiveHeader != null)
                {
                    Form frm = _ActiveHeader.RenderForm;
                    frm.Size = new Size(this.pBody.Size.Width, this.pBody.Height);
                    SetParent(frm.Handle, this.pBody.Handle);
                }
            }
        }

        private void pHeader_Resize(object sender, EventArgs e)
        {
            if (this.Width > 0)
            {
                FreshHeader();
            }
        }


        private void header_Click(object sender, EventArgs e)
        {
            FormHeader header = sender as FormHeader;
            HighLightForm(header);
        }

        private void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            RemoveForm(sender as Form );
        }

        
        #endregion

        private void pHeader_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pHeader_DragDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            if (formats != null && formats.Length > 0)
            {
                object data = e.Data.GetData(formats[0]);
                FormHeader header1 = data as FormHeader;
                if (header1 != null && !_Headers.Exists(item => object.ReferenceEquals(item, header1)))
                {
                    if (header1.FormView != null) header1.FormView.RemoveForm(header1.RenderForm);

                    Form frm = header1.RenderForm;
                    FormHeader header = new FormHeader();
                    header.FormView = this;
                    header.RenderForm = frm;
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                    this.pHeader.Controls.Add(header);
                    _Headers.Add(header);
                    header.Click += new EventHandler(header_Click);
                    HighLightForm(header);
                    FreshHeader();
                }
            }
        }
    }
}
