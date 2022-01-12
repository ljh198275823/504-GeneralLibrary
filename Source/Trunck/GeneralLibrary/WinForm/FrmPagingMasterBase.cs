using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;
using LJH.GeneralLibrary.Core.DAL;

namespace LJH.GeneralLibrary.WinForm
{
    public partial class FrmPagingMasterBase<T> : Form, IFormMaster where T:class
    {
        public FrmPagingMasterBase()
        {
            InitializeComponent();
        }

        #region 私有变量
        private DataGridView _gridView;
        private Panel _PnlLeft;
        private string _ColumnsConfig = System.IO.Path.Combine(Application.StartupPath, "ColumnsConf.xml");
        private string _PnlLeftWidthConfig = System.IO.Path.Combine(Application.StartupPath, "PnlLeftWidthConf.xml");
        private string _PageSizeConfig = System.IO.Path.Combine(Application.StartupPath, "PageSize.xml");
        #endregion

        #region 私有方法
        private void InitToolbar()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is ToolStrip && ctrl.Name == "menu")  //初始化子窗体的菜单，如果有的话
                {
                    MenuStrip menu = ctrl as MenuStrip;
                    if (menu.Items["btn_Add"] != null) menu.Items["btn_Add"].Click += btnAdd_Click;
                    if (menu.Items["btn_Delete"] != null) menu.Items["btn_Delete"].Click += btnDelete_Click;
                    if (menu.Items["btn_Export"] != null) menu.Items["btn_Export"].Click += btnExport_Click;
                    if (menu.Items["btn_Fresh"] != null) menu.Items["btn_Fresh"].Click += btnFresh_Click;
                    if (menu.Items["btn_SelectColumns"] != null) menu.Items["btn_SelectColumns"].Click += btnSelectColumns_Click;
                    break;
                }
            }
        }

        private void InitGridView()
        {
            if (GridView != null)
            {
                if (ForSelect)
                {
                    GridView.CellDoubleClick += GridView_DoubleClick1;
                }
                else
                {
                    GridView.CellDoubleClick += GridView_DoubleClick;
                }
                GridView.Sorted += new EventHandler(GridView_Sorted);
                GridView.SelectionChanged += new EventHandler(GridView_SelectionChanged);

                if (GridView.ContextMenuStrip != null)
                {
                    ContextMenuStrip menu = GridView.ContextMenuStrip;
                    if (menu.Items["cMnu_Add"] != null) menu.Items["cMnu_Add"].Click += btnAdd_Click;
                    if (menu.Items["cMnu_Edit"] != null) menu.Items["cMnu_Edit"].Click += btnEdit_Click;
                    if (menu.Items["cMnu_Delete"] != null) menu.Items["cMnu_Delete"].Click += btnDelete_Click;
                    if (menu.Items["cMnu_Export"] != null) menu.Items["cMnu_Export"].Click += btnExport_Click;
                    if (menu.Items["cMnu_Fresh"] != null) menu.Items["cMnu_Fresh"].Click += btnFresh_Click;
                    if (menu.Items["cMnu_SelectColumns"] != null) menu.Items["cMnu_SelectColumns"].Click += btnSelectColumns_Click;
                    if (menu.Items["cMnu_SelectRows"] != null)
                    {
                        menu.Items["cMnu_SelectRows"].Visible = ForSelect;
                        menu.Items["cMnu_SelectRows"].Click += cMnu_SelectRows_Click;
                    }
                }
            }
        }

        private void InitGridViewColumns()
        {
            DataGridView grid = this.GridView;
            if (grid == null) return;
            string temp = GetConfig(_ColumnsConfig, string.Format("{0}_Columns", this.GetType().Name));
            if (string.IsNullOrEmpty(temp)) return;
            string[] cols = temp.Split(',');
            int displayIndex = 0;
            for (int i = 0; i < cols.Length; i++)
            {
                string[] col_Temp = cols[i].Split(':');
                if (col_Temp.Length >= 1 && grid.Columns.Contains(col_Temp[0]))
                {
                    grid.Columns[col_Temp[0]].DisplayIndex = displayIndex;
                    displayIndex++;
                    if (col_Temp.Length >= 2 && col_Temp[1].Trim() == "0")
                    {
                        grid.Columns[col_Temp[0]].Visible = false;
                    }
                    else
                    {
                        grid.Columns[col_Temp[0]].Visible = true;
                    }
                }
            }
        }

        private string[] GetAllVisiableColumns()
        {
            if (GridView != null)
            {
                List<string> cols = new List<string>();
                foreach (DataGridViewColumn col in GridView.Columns)
                {
                    if (col.Visible) cols.Add(col.Name);
                }
                return cols.ToArray();
            }
            return null;
        }

        private void InitPnlLeft()
        {
            if (PnlLeft != null)
            {
                if (File.Exists(_PnlLeftWidthConfig))
                {
                    int temp = 0;
                    string value = GetConfig(_PnlLeftWidthConfig, string.Format("{0}_PnlLeftWidth", this.GetType().Name));
                    if (!string.IsNullOrEmpty(value) && int.TryParse(value, out temp) && temp > 0) PnlLeft.Width = temp;
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置窗体是否是用来做选择用的,(此属性需要在窗体显示之前指定)
        /// </summary>
        public bool ForSelect { get; set; }
        /// <summary>
        /// 获取或设置窗体在选择模式下的选择项
        /// </summary>
        public T SelectedItem { get; set; }
        /// <summary>
        /// 获取或设置是否支持多选
        /// </summary>
        public bool MultiSelect { get; set; }
        /// <summary>
        /// 获取或设置查询条件
        /// </summary>
        public SearchCondition SearchCondition { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 显示操作的权限
        /// </summary>
        public virtual void ShowOperatorRights()
        {

        }
        #endregion

        #region 事件
        public event EventHandler<ItemSelectedEventArgs> ItemSelected;
        #endregion

        #region 保护方法
        public virtual void ReFreshData()
        {
            var items = GetDataSource(ucPaging1.GetPageSize(), 1);
            if (ucPaging1.GetPageSize() > 0) SaveConfig(_PageSizeConfig, "PageSize", ucPaging1.GetPageSize().ToString());
            ShowItemsOnGrid(items);
        }

        /// <summary>
        /// 显示数据行的颜色
        /// </summary>
        protected virtual void ShowRowBackColor()
        {
            int count = 0;
            foreach (DataGridViewRow row in this.GridView.Rows)
            {
                if (row.Visible)
                {
                    count++;
                    row.DefaultCellStyle.BackColor = (count % 2 == 1) ? Color.FromArgb(230, 230, 230) : Color.White;
                }
            }
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        protected virtual void ExportData()
        {
            try
            {
                if (GridView == null) return;
                SaveFileDialog dig = new SaveFileDialog();
                dig.Filter = "Excel文档|*.xls;*.xlsx|所有文件(*.*)|*.*";
                dig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (dig.ShowDialog() == DialogResult.OK)
                {
                    string path = dig.FileName;
                    NPOIExcelHelper.Export(GridView, path, true);
                    MessageBox.Show("导出成功");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存到电子表格时出现错误!");
            }
        }
        /// <summary>
        /// 选择数据网格中要显示的列
        /// </summary>
        protected virtual void SelectColumns()
        {
            FrmColumnSelection frm = new FrmColumnSelection();
            frm.Selectee = this.GridView;
            frm.SelectedColumns = GetAllVisiableColumns();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string[] cols = frm.SelectedColumns;
                if (cols != null && cols.Length > 0)
                {
                    string temp = string.Join(",", cols);
                    SaveConfig(_ColumnsConfig, string.Format("{0}_Columns", this.GetType().Name), temp);
                    InitGridViewColumns();
                }
            }
        }
        /// <summary>
        /// 进行增加数据操作
        /// </summary>
        protected virtual void PerformAddData()
        {
            try
            {
                FrmDetailBase detailForm = GetDetailForm();
                if (detailForm != null)
                {
                    detailForm.IsAdding = true;
                    DataGridViewRow row = null;
                    detailForm.ItemAdded += delegate (object obj, ItemAddedEventArgs args)
                    {
                        row = Add_And_Show_Row(args.AddedItem as T);
                    };
                    detailForm.ItemUpdated += delegate (object obj, ItemUpdatedEventArgs args)
                    {
                        ShowItemInGridViewRow(row, args.UpdatedItem as T);
                    };
                    detailForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 增加并显示一行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected DataGridViewRow Add_And_Show_Row(T item)
        {
            int row = GridView.Rows.Add();
            ShowItemInGridViewRow(GridView.Rows[row], item);
            GridView.Rows[row].Tag = item;
            if (this.GridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in GridView.SelectedRows)
                {
                    r.Selected = false;
                }
            }
            GridView.Rows[row].Selected = true;
            if (row > GridView.DisplayedColumnCount(false))
            {
                GridView.FirstDisplayedScrollingRowIndex = row - GridView.DisplayedColumnCount(false) + 1;
            }
            return GridView.Rows[row];
        }
        /// <summary>
        /// 进行修改数据操作
        /// </summary>
        protected virtual void PerformUpdateData()
        {
            if (this.GridView != null && this.GridView.SelectedRows != null && this.GridView.SelectedRows.Count > 0)
            {
                object pre = this.GridView.SelectedRows[0].Tag;
                if (pre != null)
                {
                    FrmDetailBase detailForm = GetDetailForm();
                    if (detailForm != null)
                    {
                        detailForm.IsAdding = false;
                        detailForm.UpdatingItem = pre;

                        detailForm.ItemUpdated += delegate (object obj, ItemUpdatedEventArgs args)
                        {
                            ShowItemInGridViewRow(this.GridView.SelectedRows[0], args.UpdatedItem as T);
                        };
                        detailForm.ShowDialog();
                    }
                }
            }
        }
        /// <summary>
        /// 进行删除数据操作
        /// </summary>
        protected virtual void PerformDeleteData()
        {
            try
            {
                if (this.GridView.SelectedRows.Count > 0)
                {
                    List<DataGridViewRow> deletingRows = new List<DataGridViewRow>();
                    DialogResult result = MessageBox.Show("确实要删除所选项吗?", "确定", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        for (int i = GridView.Rows.Count - 1; i > -1; i--)
                        {
                            DataGridViewRow row = GridView.Rows[i];
                            if (row.Selected)
                            {
                                var deletingItem = row.Tag as T;
                                if (DeletingItem(deletingItem))
                                {
                                    deletingRows.Add(row);
                                }
                            }
                        }
                        if (deletingRows.Count > 0) btnFresh_Click(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("没有选择项!", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 从某个配置文件中读取键为key的项的值
        /// </summary>
        /// <param name="file"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetConfig(string file, string key)
        {
            string ret = null;
            if (File.Exists(file))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<MyKeyValuePair>));
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        List<MyKeyValuePair> items = ser.Deserialize(fs) as List<MyKeyValuePair>;
                        if (items != null && items.Count > 0)
                        {
                            MyKeyValuePair kv = items.SingleOrDefault(it => it.Key == key);
                            ret = kv != null ? kv.Value : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex);
                }
            }
            return ret;
        }
        /// <summary>
        /// 将某个配置保存到某个配置文件中
        /// </summary>
        /// <param name="file"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void SaveConfig(string file, string key, string value)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<MyKeyValuePair>));
                List<MyKeyValuePair> items = null;
                if (File.Exists(file))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        items = ser.Deserialize(fs) as List<MyKeyValuePair>;
                    }
                }
                if (items == null) items = new List<MyKeyValuePair>();
                MyKeyValuePair kv = items.SingleOrDefault(it => it.Key == key);
                if (kv != null)
                {
                    kv.Value = value;
                }
                else
                {
                    items.Add(new MyKeyValuePair { Key = key, Value = value });
                }
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    ser.Serialize(fs, items);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 子类要重写的方法
        protected virtual Panel PnlLeft
        {
            get
            {
                if (_PnlLeft == null)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is Panel && ctrl.Name == "pnlLeft")
                        {
                            _PnlLeft = ctrl as Panel;
                            break;
                        }
                    }
                }
                return _PnlLeft;
            }
        }

        protected virtual DataGridView GridView
        {
            get
            {
                if (_gridView == null)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            _gridView = ctrl as DataGridView;
                        }
                    }
                }
                return _gridView;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            ShowOperatorRights();
            InitToolbar();
            InitGridView();
            InitGridViewColumns();
            InitPnlLeft();

            this.ucPaging1.Init();
            this.ucPaging1.GetPageData += UcPaging1_GetPageData;
            var temp = GetConfig(_PageSizeConfig, "PageSize");
            int pageSize = 0;
            if (string.IsNullOrEmpty(temp) || !int.TryParse(temp, out pageSize) || pageSize == 0) pageSize = 30;
            this.ucPaging1.SetPageSize(pageSize);
        }
        /// <summary>
        /// 获取明细窗体
        /// </summary>
        /// <returns></returns>
        protected virtual FrmDetailBase GetDetailForm()
        {
            return null;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected virtual QueryResultList<T> GetDataSource(int pageSize, int pageIndex)
        {
            return null;
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="items">要显示的数据</param>
        /// <param name="reload">是否重新加载数据，如果为真，则表示先会清空之前的数据，否则保留旧有数据</param>
        protected virtual void ShowItemsOnGrid(QueryResultList<T> resultList)
        {
            GridView.Rows.Clear();
            if (resultList != null)
            {
                if (resultList.Result != ResultCode.Successful)
                {
                    MessageBox.Show(resultList.Message);
                    return;
                }
                var items = resultList.QueryObjects;
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        int row = GridView.Rows.Add();
                        ShowItemInGridViewRow(GridView.Rows[row], item);
                        GridView.Rows[row].Tag = item;
                    }
                }
                if (this.GridView.Rows.Count > 0)
                {
                    ShowRowBackColor();
                    this.GridView.Rows[0].Selected = false;
                }
                this.ucPaging1.ShowState(resultList.PageSize, resultList.PageIndex, (items != null ? items.Count : 0), resultList.TotalCount);
            }
            else
            {
                this.ucPaging1.Init();
            }
        }
        /// <summary>
        /// 在网格行中显示单个数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        protected virtual void ShowItemInGridViewRow(DataGridViewRow row, T item)
        {

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool DeletingItem(T item)
        {
            return false;
        }
        #endregion

        #region 事件处理
        private void FrmMasterBase_Load(object sender, EventArgs e)
        {
            Init();
            if (GridView != null) btnFresh_Click(null, null); //这一行不能少，如果没有这一行，窗体在设计时会出错
        }

        private void btnFresh_Click(object sender, EventArgs e)
        {
            var items = GetDataSource(ucPaging1.GetPageSize(), 1);
            if (ucPaging1.GetPageSize() > 0) SaveConfig(_PageSizeConfig, "PageSize", ucPaging1.GetPageSize().ToString());
            ShowItemsOnGrid(items);
        }

        private void UcPaging1_GetPageData(int pageInex, int pageSize)
        {
            SaveConfig(_PageSizeConfig, "PageSize", pageSize.ToString());
            var items = GetDataSource(pageSize, pageInex);
            ShowItemsOnGrid(items);
        }

        private void GridView_Sorted(object sender, EventArgs e)
        {
            ShowRowBackColor();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PerformAddData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PerformUpdateData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PerformDeleteData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void btnSelectColumns_Click(object sender, EventArgs e)
        {
            SelectColumns();
        }

        private void cMnu_SelectRows_Click(object sender, EventArgs e)
        {
            if (GridView == null) return;
            foreach (DataGridViewRow row in GridView.SelectedRows)
            {
                var o = row.Tag;
                if (o != null)
                {
                    ItemSelectedEventArgs args = new ItemSelectedEventArgs() { SelectedItem = o };
                    if (this.ItemSelected != null) this.ItemSelected(this, args);
                    row.Visible = false;
                }
            }
        }

        private void GridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PerformUpdateData();
        }

        private void GridView_DoubleClick1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!MultiSelect)
                {
                    this.SelectedItem = this.GridView.Rows[e.RowIndex].Tag as T;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ItemSelectedEventArgs args = new ItemSelectedEventArgs() { SelectedItem = this.GridView.Rows[e.RowIndex].Tag };
                    if (this.ItemSelected != null) this.ItemSelected(this, args);
                    if (!args.Canceled) this.GridView.Rows.Remove(this.GridView.Rows[e.RowIndex]);
                }
            }
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Rows[i].Cells[j] is DataGridViewLinkCell)
                    {
                        var cell = dgv.Rows[i].Cells[j] as DataGridViewLinkCell;
                        cell.LinkColor = cell.Selected ? Color.White : Color.Blue;
                    }
                }
            }
        }

        private void FrmMasterBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PnlLeft != null)
            {
                SaveConfig(_PnlLeftWidthConfig, string.Format("{0}_PnlLeftWidth", this.GetType().Name), PnlLeft.Width.ToString());
            }
        }
        #endregion
    }
}
