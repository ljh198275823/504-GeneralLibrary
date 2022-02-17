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
    public partial class FrmMasterBase<TID,TEntity> : Form, IFormMaster where TEntity : class, IEntity<TID>
    {
        public FrmMasterBase()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// 当前正在显示的数据
        /// </summary>
        protected List<TEntity> _ShowingItems;
        /// <summary>
        /// 查询到的所有数据
        /// </summary>
        protected List<TEntity> _Items;
        private DataGridView _gridView;
        private Panel _PnlLeft;
        private Dictionary<string, int> _ColumnsSort = new Dictionary<string, int>(); //表示每一列的排序方式
        private string _ColumnsConfig = System.IO.Path.Combine(Application.StartupPath, "ColumnsConf.xml");
        private string _PnlLeftWidthConfig = System.IO.Path.Combine(Application.StartupPath, "PnlLeftWidthConf.xml");
        #endregion.

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
                GridView.CellValueNeeded += GridView_CellValueNeeded;
                GridView.ColumnHeaderMouseClick += GridView_ColumnHeaderMouseClick;
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
        public TEntity SelectedItem { get; set; }
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
        /// 从数据库重新获取数据并刷新数据显示
        /// </summary>
        public virtual void ReFreshData()
        {
            var ret = GetDataSource();
            if (ret.Result == ResultCode.Fail)
            {
                MessageBox.Show(ret.Message);
            }
            _Items = ret.QueryObjects;
            FreshView();
        }
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
        /// <summary>
        /// 获取选择的表格里面的数据
        /// </summary>
        /// <returns></returns>
        protected List<TEntity> GetSelectedItems()
        {
            var dic = new Dictionary<int, TEntity>();
            foreach (DataGridViewCell cell in GridView.SelectedCells)
            {
                if (dic.ContainsKey(cell.RowIndex) == false) dic.Add(cell.RowIndex, GetRowTag(GridView.Rows[cell.RowIndex]));
            }
            return dic.Values.ToList();
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
        /// 进行删除数据操作
        /// </summary>
        protected virtual void PerformDeleteData()
        {
            try
            {
                if (GridView == null) return;
                var items = GetSelectedItems();
                if (items != null && items.Count > 0)
                {
                    DialogResult result = MessageBox.Show("确实要删除所选项吗?", "确定", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        foreach (var item in items)
                        {
                            if (item == null) continue;
                            var ret = DeletingItem(item);
                            if (ret.Result == ResultCode.Successful)
                            {
                                _ShowingItems.Remove(item);
                                _Items.Remove(item);
                            }
                            else
                            {
                                MessageBox.Show(ret.Message);
                            }
                        }
                        GridView.RowCount = _ShowingItems.Count;
                        GridView.Invalidate();
                        foreach (DataGridViewCell cell in GridView.SelectedCells)
                        {
                            cell.Selected = false;
                        }
                        FreshStatusBar();
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
        /// 进行增加数据操作
        /// </summary>
        protected virtual void PerformAddData()
        {
            try
            {
                var detailForm = GetDetailForm();
                if (detailForm != null)
                {
                    detailForm.IsAdding = true;
                    detailForm.ItemAdded += FrmDetail_ItemAdded;
                    detailForm.ItemUpdated += FrmDetail_ItemUpdated;
                    detailForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        protected virtual void FrmDetail_ItemAdded(object obj, ItemAddedEventArgs args)
        {
            var item = args.AddedItem as TEntity;
            if (_ShowingItems == null) _ShowingItems = new List<TEntity>();
            _ShowingItems.Insert(0, item);
            if (_Items == null) _Items = new List<TEntity>();
            _Items.Insert(0, item);
            if (GridView != null)
            {
                GridView.RowCount++;
                FreshStatusBar();
                GridView.FirstDisplayedScrollingRowIndex = 0;
                foreach (DataGridViewCell r in GridView.SelectedCells)
                {
                    r.Selected = false;
                }
                GridView.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// 进行修改数据操作
        /// </summary>
        protected virtual void PerformUpdateData()
        {
            var items = GetSelectedItems();
            if (items != null && items.Count > 0)
            {
                var pre = items[0];
                if (pre != null)
                {
                    var detailForm = GetDetailForm();
                    if (detailForm != null)
                    {
                        detailForm.IsAdding = false;
                        detailForm.UpdatingItem = pre;
                        detailForm.ItemUpdated += FrmDetail_ItemUpdated;
                        detailForm.ShowDialog();
                    }
                }
            }
        }

        protected virtual void FrmDetail_ItemUpdated(object obj, ItemUpdatedEventArgs args)
        {
            if (!object.ReferenceEquals(args.PreUpdatingItem, args.UpdatedItem)) FreshItem(args.UpdatedItem as TEntity); //如果不是同一个对象
            if (GridView != null) GridView.Invalidate();
        }

        /// <summary>
        /// 更新本地数据里面的某个项
        /// </summary>
        /// <param name="newVal"></param>
        protected virtual void FreshItem(TEntity newVal)
        {
            try
            {
                var index = _ShowingItems.FindIndex(it => IsIDEquel(it.ID, newVal.ID));
                if (index >= 0)
                {
                    _ShowingItems.RemoveAt(index);
                    _ShowingItems.Insert(index, newVal);
                }

                var index1 = _Items.FindIndex(it => IsIDEquel(it.ID, newVal.ID));
                if (index1 >= 0)
                {
                    _Items.RemoveAt(index1);
                    _Items.Insert(index1, newVal);
                }
            }
            catch
            {
            }
        }

        private bool IsIDEquel(TID id1,TID id2)
        {
            if (typeof(TID) == typeof(string)) return (id1 as string) == (id2 as string);
            else if (typeof(TID) == typeof(Guid)) return GuidConverter.Equals(id1, id2);
            else if (typeof(TID) == typeof(int)) return Convert.ToInt32(id1) == Convert.ToInt32(id2);
            else if (typeof(TID) == typeof(long)) return Convert.ToInt64(id1) == Convert.ToInt64(id2);
            return false;
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
        }
        /// <summary>
        /// 获取明细窗体
        /// </summary>
        /// <returns></returns>
        protected virtual FrmDetailBase<TID,TEntity> GetDetailForm()
        {
            return null;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected virtual QueryResultList<TEntity> GetDataSource()
        {
            return null;
        }

        protected virtual void FreshView()
        {
            _ShowingItems = _Items != null ? FilterData(_Items.ToList()) : null;
            if (GridView != null)
            {
                this.GridView.VirtualMode = true;
                this.GridView.RowCount = 0;
                this.GridView.RowCount = _ShowingItems != null ? _ShowingItems.Count : 0;
                this.GridView.Invalidate();
                FreshStatusBar();
            }
        }


        protected virtual List<TEntity> FullTextSearch(List<TEntity> data, string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return data;
            if (this.GridView == null) return data;
            if (data == null) return data;
            data = data.Where(item =>
            {
                foreach (DataGridViewColumn col in GridView.Columns)
                {
                    if (col.Visible)
                    {
                        object o = GetCellValue(item, col.Name);
                        if (o != null && o.ToString().Contains(keyword)) return true;
                    }
                }
                return false;
            }).ToList();
            return data;
        }

        protected virtual List<TEntity> FilterData(List<TEntity> items)
        {
            return items;
        }

        protected virtual object GetCellValue(TEntity item, string colName)
        {
            if (colName == "col序号") return _ShowingItems.IndexOf(item) + 1;
            return null;
        }
        /// <summary>
        /// 刷新状态栏
        /// </summary>
        protected virtual void FreshStatusBar()
        {
            this.toolStripStatusLabel1.Text = string.Format("总共 {0} 项", GridView.Rows.Count);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual CommandResult DeletingItem(TEntity item)
        {
            return new CommandResult(ResultCode.Fail, "没有重写此方法");
        }

        protected virtual TEntity GetRowTag(DataGridViewRow r)
        {
            if (_ShowingItems.Count > r.Index) return _ShowingItems[r.Index];
            return null;
        }

        protected virtual void SetRowStyle(DataGridViewRow row)
        {
            
        }
        #endregion

        #region 事件处理
        private void FrmMasterBase_Load(object sender, EventArgs e)
        {
            Init();
            if (GridView != null) //这一行不能少，如果没有这一行，窗体在设计时会出错
            {
                btnFresh_Click(null, null);
            }
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

        private void btnFresh_Click(object sender, EventArgs e)
        {
            ReFreshData();
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
            var items = GetSelectedItems();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    ItemSelectedEventArgs args = new ItemSelectedEventArgs() { SelectedItem = item };
                    if (this.ItemSelected != null) this.ItemSelected(this, args);
                    _ShowingItems.Remove(item);  //选择完之后从表格里面删除
                    _Items.Remove(item);
                }
            }
            this.GridView.Invalidate();
            foreach (DataGridViewCell cell in GridView.SelectedCells)
            {
                cell.Selected = false;
            }
        }

        private void GridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) PerformUpdateData();
        }

        private void GridView_DoubleClick1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var item = GetRowTag(this.GridView.Rows[e.RowIndex]);
                ItemSelectedEventArgs args = new ItemSelectedEventArgs() { SelectedItem = item };
                if (this.ItemSelected != null) this.ItemSelected(this, args);
                if (!MultiSelect)
                {
                    this.SelectedItem = GetRowTag(this.GridView.Rows[e.RowIndex]);
                    if (this.Modal)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                    }
                }
                else
                {
                    _ShowingItems.Remove(item);
                    this.GridView.RowCount = _ShowingItems != null ? _ShowingItems.Count : 0;
                    this.GridView.Invalidate();
                    FreshStatusBar();
                }
            }
        }

        private void GridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < _ShowingItems.Count)
                {
                    var item = _ShowingItems[e.RowIndex];
                    e.Value = GetCellValue(item, GridView.Columns[e.ColumnIndex].Name);
                    GridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = (e.RowIndex % 2 == 1) ? Color.FromArgb(224, 224, 224) : Color.White;
                    SetRowStyle(GridView.Rows[e.RowIndex]);
                }
                else
                {
                    GridView.Rows[e.RowIndex].Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void GridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_ShowingItems == null) return;
            if (GridView.Columns[e.ColumnIndex].SortMode != DataGridViewColumnSortMode.Automatic) return;
            if (!_ColumnsSort.ContainsKey(GridView.Columns[e.ColumnIndex].Name)) _ColumnsSort.Add(GridView.Columns[e.ColumnIndex].Name, 0);
            int sort = _ColumnsSort[GridView.Columns[e.ColumnIndex].Name];
            if (sort != 1)
            {
                _ShowingItems = (from item in _ShowingItems
                                 let field = GetCellValue(item, GridView.Columns[e.ColumnIndex].Name)
                                 orderby field ascending
                                 select item).ToList();
                _ColumnsSort[GridView.Columns[e.ColumnIndex].Name] = 1;  //当前为升序
            }
            else
            {
                _ShowingItems = (from item in _ShowingItems
                                 let field = GetCellValue(item, GridView.Columns[e.ColumnIndex].Name)
                                 orderby field descending
                                 select item).ToList();
                _ColumnsSort[GridView.Columns[e.ColumnIndex].Name] = 2; //当前为降序
            }
            GridView.Invalidate();
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

        private void FrmMasterBase_Resize(object sender, EventArgs e)
        {
            try
            {
                if (GridView != null)
                {
                    foreach (DataGridViewColumn col in GridView.Columns)
                    {
                        if (col.Visible && col.AutoSizeMode != DataGridViewAutoSizeColumnMode.None && col.AutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
                        {
                            GridView.AutoResizeColumn(col.Index, col.AutoSizeMode);
                        }
                    }
                }
            }
            catch
            {
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
