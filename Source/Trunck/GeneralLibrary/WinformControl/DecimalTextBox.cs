using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    public partial class DecimalTextBox:TextBox
    {
        #region 构造函数
        public DecimalTextBox()
        {
            InitializeComponent();
            this.ImeMode = ImeMode.OnHalf;
            Init();
        }

        public DecimalTextBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.ImeMode = ImeMode.OnHalf;
            Init();
        }
        #endregion 

        #region 私有变量
        private string _PreText; //用于保存输入框内容改变之前的输入框中的内容

        private void Init()
        {
            this.Text = "0.00";
            this._PreText = "0.00";
            this.MinValue = decimal.MinValue;
            this.MaxValue = decimal.MaxValue;
            this.PointCount = -1;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置允许输入的最小值
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// 获取或设置允许输入的最大值
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// 获取或设置显示成文字时的小位点后位数,如果指定为-1则表示不强制显示小数点
        /// </summary>
        public int PointCount { get; set; }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal DecimalValue
        {
            get
            {
                string txt;
                decimal val;
                txt = this.Text.Trim();
                if (txt.Length > 0 && decimal.TryParse(txt, out val))
                {
                    return val;
                }
                return 0;
            }
            set
            {
                if (PointCount >= 0)
                {
                    this.Text = value.ToString("F" + PointCount);
                }
                else
                {
                    this.Text = value.ToString();
                }
            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnEnter(EventArgs e)
        {
            if (this.Text.Trim().Length > 0)
            {
                this.SelectionStart = 0;
                this.SelectionLength = this.Text.Length;
            }
            base.OnEnter(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            string text = StringHelper.ToDBC(this.Text);
            int position = this.SelectionStart;
            if (!string.IsNullOrEmpty(text.Trim()))
            {
                if (text.Trim() == "-")
                {
                    if (this.MinValue >= 0)
                    {
                        this.Text = _PreText;
                        this.SelectionStart = this.Text.Length;
                        return;
                    }
                }
                else
                {
                    decimal value;
                    if (!decimal.TryParse(text, out value))
                    {
                        this.Text = _PreText;
                        this.SelectionStart = this.Text.Length;
                        return;
                    }
                    else
                    {
                        if (value > MaxValue || value < MinValue)
                        {
                            this.Text = _PreText;
                            this.SelectionStart = this.Text.Length;
                            return;
                        }
                    }
                }
            }
            this.Text = text;
            _PreText = this.Text;
            this.SelectionStart = position;
            base.OnTextChanged(e);
        }
        #endregion
    }
}
