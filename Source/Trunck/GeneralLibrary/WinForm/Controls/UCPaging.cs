using System;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinformControl
{
    /// <summary>
    /// 委托，查询指定页面的数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="pagesize"></param>
    public delegate void HandleEventGetPageData(int pageInex, int pageSize);

    public partial class UCPaging : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCPaging()
        {
            InitializeComponent();
        }

        private int _PageIndex = 0;
        private int _TotalPages = 0;

        #region 公共属性
        public void Init()
        {
            txtPageIndex.Items.Clear();
            _PageIndex = 0;
            _TotalPages = 0;
            txtTotalPages.Text = "0";
            txtCount.Text = "0";
            txtTotalCount.Text = "0";
        }

        public void ShowState(int pageSize, int pageIndex, int count, int totalCount)
        {
            if (totalCount > 0) SetPageSize(pageSize);
            txtPageSize.Enabled = true;
            _PageIndex = pageIndex == 0 ? 1 : pageIndex;
            txtCount.Text = count.ToString();
            txtTotalCount.Text = totalCount.ToString();
            if (GetPageSize() == 0) _TotalPages = 1;
            else
            {
                _TotalPages = (int)(Math.Ceiling((decimal)totalCount / GetPageSize()));
            }
            txtTotalPages.Text = _TotalPages.ToString();

            txtPageIndex.SelectedIndexChanged -= txtPageIndex_SelectedIndexChanged;
            txtPageIndex.Items.Clear();
            for (int i = 1; i <= _TotalPages; i++)
            {
                txtPageIndex.Items.Add(i.ToString());
            }
            txtPageIndex.Text = _PageIndex.ToString();
            txtPageIndex.SelectedIndexChanged += txtPageIndex_SelectedIndexChanged;
        }

        public int GetPageSize()
        {
            int pageSize = 0;
            if (string.IsNullOrEmpty(txtPageSize.Text) || !int.TryParse(txtPageSize.Text, out pageSize) || pageSize < 0) return 0;
            return pageSize;
        }

        public void SetPageSize(int pageSize)
        {
            if (pageSize == 0) txtPageSize.SelectedIndex = 0;
            else txtPageSize.Text = pageSize.ToString();
        }
        #endregion

        public event HandleEventGetPageData GetPageData;

        #region 窗体事件
        //第一页
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (_PageIndex > 1) GetPageData(1, GetPageSize());
        }

        //上一页
        private void btnPrevios_Click(object sender, EventArgs e)
        {
            if (_PageIndex > 1) GetPageData(_PageIndex - 1, GetPageSize());
        }

        //下一页
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PageIndex < _TotalPages) GetPageData(_PageIndex + 1, GetPageSize());
        }

        //最后页
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (_PageIndex != _TotalPages) GetPageData(_TotalPages, GetPageSize());
        }

        //指定到那一页
        private void txtPageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gotoindex = 0;
            if (int.TryParse(txtPageIndex.Text, out gotoindex) && Convert.ToInt32(txtPageIndex.Text) > 0 && Convert.ToInt32(txtPageIndex.Text) <= _TotalPages)
            {
                GetPageData(gotoindex, GetPageSize());
            }
        }
        #endregion
    }
}
