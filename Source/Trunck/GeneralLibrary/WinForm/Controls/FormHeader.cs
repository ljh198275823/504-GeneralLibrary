using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class FormHeader : UserControl
    {
        public FormHeader()
        {
            InitializeComponent();
        }

        #region 私有变量
        private bool _Active;
        #endregion

        /// <summary>
        /// 当点击控析时产生此事件
        /// </summary>
        public event EventHandler Click;

        #region 公共属性
        /// <summary>
        /// 获取或设置要显示的窗体
        /// </summary>
        public Form RenderForm
        {
            get
            {
                return this.Tag as Form;
            }
            set
            {
                this.Tag = value;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置成当前活动状态
        /// </summary>
        public void Active()
        {
            _Active = true;
            this.lblCaption.BackColor = Color.White;
            this.lblCaption.ForeColor = Color.Black;
            this.btnClose.BackColor = this.lblCaption.BackColor;
            this.btnClose.ForeColor = this.lblCaption.ForeColor;
        }

        /// <summary>
        /// 设置成当前为非活动状态
        /// </summary>
        public void Deactive()
        {
            _Active = false;
            this.lblCaption.BackColor = Parent.BackColor;
            this.lblCaption.ForeColor = Color.White;
            this.btnClose.BackColor = this.lblCaption.BackColor;
            this.btnClose.ForeColor = this.lblCaption.BackColor;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Fresh()
        {
            Form frm = this.RenderForm;
            Graphics g = this.CreateGraphics();
            string caption = frm.Text;
            int charLen = frm.Text.Length;
            while (g.MeasureString(caption, this.lblCaption.Font).Width > this.lblCaption.Width)
            {
                charLen--;
                caption = frm.Text.Substring(0, charLen) + "...";
            }
            lblCaption.Text = caption;
        }
        #endregion

        #region 事件处理程序
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.RenderForm.Close();
        }

        private void lblCaption_MouseEnter(object sender, EventArgs e)
        {
            if (!_Active)
            {
                this.lblCaption .BackColor = Color.Gray;
                this.btnClose.BackColor = this.lblCaption.BackColor;
                this.btnClose.ForeColor = Color.Black;
            }

        }

        private void lblCaption_MouseLeave(object sender, EventArgs e)
        {
            if (!_Active)
            {
                this.lblCaption.BackColor = Parent.BackColor;
                this.btnClose.BackColor = this.lblCaption.BackColor;
                this.btnClose.ForeColor = this.lblCaption.BackColor;
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Orange;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = this.lblCaption.BackColor;
        }

        private void lblCaption_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Click != null) this.Click(this, e);
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }
        #endregion
    }
}
