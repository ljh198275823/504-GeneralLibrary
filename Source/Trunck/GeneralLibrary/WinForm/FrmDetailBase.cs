using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary;

namespace LJH.GeneralLibrary.WinForm
{
    public partial class FrmDetailBase<TID, TEntity> : Form where TEntity :class, IEntity<TID>
    {
        public FrmDetailBase()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置是否是用于增加
        /// </summary>
        public bool IsAdding { get; set; }
        /// <summary>
        /// 获取或设置是否是用于查看项目的明细
        /// </summary>
        public bool IsForView { get; set; }
        /// <summary>
        /// 获取或设置要查看项的明细
        /// </summary>
        public TEntity UpdatingItem { get; set; }
        #endregion

        #region 事件
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        public event EventHandler<ItemAddedEventArgs> ItemAdded;
        #endregion

        #region 模板方法
        protected virtual void InitControls()
        {
            if (IsForView)
            {
                btnOk.Enabled = false;
            }
            ShowOperatorRights();
        }

        /// <summary>
        /// 显示操作的权限
        /// </summary>
        public virtual void ShowOperatorRights()
        {

        }

        protected virtual void ItemShowing(TEntity info)
        {

        }

        protected virtual bool CheckInput()
        {
            throw new NotImplementedException("子类没有重写CheckInput方法");
        }

        protected virtual TEntity GetItemFromInput()
        {
            throw new NotImplementedException("子类没有重写GetItemFromInput方法");
        }
        /// <summary>
        /// 清空上一次保存后的输入数据
        /// </summary>
        protected virtual void ClearInput()
        {
            this.DialogResult = DialogResult.OK;
        }

        protected virtual CommandResult<TEntity> AddItem(TEntity addingItem)
        {
            throw new NotImplementedException("子类没有重写AddItem方法");
        }

        protected virtual CommandResult<TEntity> UpdateItem(TEntity updatingItem)
        {
            throw new NotImplementedException("子类没有重写UpdateItem方法");
        }

        protected void OnItemUpdated(ItemUpdatedEventArgs e)
        {
            if (this.ItemUpdated != null) this.ItemUpdated(this, e);
        }

        protected void OnItemAdded(ItemAddedEventArgs e)
        {
            if (this.ItemAdded != null) ItemAdded(this, e);
        }
        
        #endregion

        #region 事件处理程序
        private void FrmDetailBase_Load(object sender, EventArgs e)
        {
            InitControls();
            if (!IsAdding && UpdatingItem != null) ItemShowing(UpdatingItem);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            var item = GetItemFromInput();
            if (item == null) return;
            CommandResult<TEntity> ret = null;
            if (IsAdding)
            {
                ret = AddItem(item);
                if (ret.Result == ResultCode.Successful)
                {
                    OnItemAdded(new ItemAddedEventArgs(ret.Value != null ? ret.Value : item));
                    ClearInput();
                }
                else
                {
                    MessageBox.Show(ret.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ret = UpdateItem(item);
                if (ret.Result == ResultCode.Successful)
                {
                    OnItemUpdated(new ItemUpdatedEventArgs(ret.Value != null ? ret.Value : item));
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(ret.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
