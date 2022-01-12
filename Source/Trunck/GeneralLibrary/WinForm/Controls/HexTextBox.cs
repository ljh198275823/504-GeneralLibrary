using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class HexTextBox : TextBox
    {
        public HexTextBox()
        {
            InitializeComponent();
        }

        public HexTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 私有变量
        private string _PreText;
        private Regex reg = new Regex("^[A-Fa-f0-9]+$");
        #endregion

        #region 公共属性
        /// <summary>
        /// 是否允许输入空格
        /// </summary>
        public bool InputSpace
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置16进制字节组
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Localizable(false)]
        public byte[] HexValue
        {
            get
            {
                string text = this.Text.Replace(" ", "");
                System.Collections.Generic.List<byte> list = new System.Collections.Generic.List<byte>();
                try
                {
                    if (text.Length % 2 == 0)
                    {
                        for (int i = 0; i < text.Length / 2; i++)
                        {
                            list.Add(byte.Parse(text.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
                        }
                    }
                }
                catch
                {
                }
                return list.ToArray();
            }
            set
            {
                string text = string.Empty;
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        byte b = value[i];
                        text += b.ToString("X2");
                        if (this.InputSpace)
                        {
                            text += " ";
                        }
                    }
                }
                this.Text = text.Trim();
            }
        }
        #endregion


        private void Init()
        {
            this.Text = string.Empty;
            this._PreText = string.Empty;
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            string text = StringHelper.ToDBC(this.Text).ToUpper();
            int selectionStart = base.SelectionStart;
            if (!string.IsNullOrEmpty(text.Trim()))
            {
                if (!this.reg.IsMatch(this.InputSpace ? text.Replace(" ", "") : text))
                {
                    this.Text = this._PreText;
                    base.SelectionStart = this.Text.Length;
                    return;
                }
            }
            this.Text = text;
            this._PreText = this.Text;
            base.SelectionStart = selectionStart;
            base.OnTextChanged(e);
        }

        protected override void OnEnter(System.EventArgs e)
        {
            if (this.Text.Trim().Length > 0)
            {
                base.SelectionStart = 0;
                this.SelectionLength = this.Text.Length;
            }
            base.OnEnter(e);
        }
    }
}
