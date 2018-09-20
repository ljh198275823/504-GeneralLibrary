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

namespace LJH.GeneralLibrary.Core.UI
{
    public partial class FrmMasterBaseEX<T> : Form, IOperatorRender where T : class
    {
        public FrmMasterBaseEX()
        {
            InitializeComponent();
        }

        #region 私有变量
        protected List<T> _ShowingItems;
        protected List<T> _Items;
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
                GridView.CellMouseDown += GridView_CellMouseDown;
                GridView.Sorted += new EventHandler(GridView_Sorted);
                GridView.CellValueNeeded += GridView_CellValueNeeded;
                GridView.ColumnHeaderMouseClick += GridView_ColumnHeaderMouseClick;

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

        protected virtual List<T> FullTextSearch(List<T> data, string keyword)
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
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置窗体是否是用来做选择用的,(此属性需要在窗体显示之前指定)
        /// </summary>
        public bool ForSelect { get; set; }
        /// <summary>
        /// 获取或设置窗体在选择模式下的选择项
        /// </summary>
        public object SelectedItem { get; set; }
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
            _Items = GetDataSource();
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
            catch (Exception ex)
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
                if (this.GridView.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("确实要删除所选项吗?", "确定", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in this.GridView.SelectedRows)
                        {
                            T item = GetRowTag(row);
                            if (item != null && DeletingItem(item))
                            {
                                row.Selected = false;
                                _ShowingItems.Remove(item);
                                _Items.Remove(item);
                            }
                        }
                        GridView.RowCount = _ShowingItems.Count;
                        GridView.Invalidate();
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
                FrmDetailBase detailForm = GetDetailForm();
                if (detailForm != null)
                {
                    detailForm.IsAdding = true;
                    detailForm.ItemAdded += delegate(object obj, ItemAddedEventArgs args)
                    {
                        var item = args.AddedItem as T;
                        if (_ShowingItems == null) _ShowingItems = new List<T>();
                        _ShowingItems.Add(item);
                        if (_Items == null) _Items = new List<T>();
                        _Items.Add(item);
                        if (GridView != null)
                        {
                            GridView.RowCount++;
                            FreshStatusBar();
                            if (_ShowingItems.Count > GridView.DisplayedRowCount(false))
                            {
                                GridView.FirstDisplayedScrollingRowIndex = _ShowingItems.Count - GridView.DisplayedRowCount(false) + 1; //5是随便取的一数字
                            }
                            else
                            {
                                GridView.FirstDisplayedScrollingRowIndex = 0;
                            }
                            GridView.Rows[_ShowingItems.Count - 1].Selected = true;
                        }
                    };
                    detailForm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                    {
                        if (GridView != null) GridView.Invalidate();
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
        /// 进行修改数据操作
        /// </summary>
        protected virtual void PerformUpdateData()
        {
            if (this.GridView != null && this.GridView.SelectedRows != null && this.GridView.SelectedRows.Count > 0)
            {
                var row = this.GridView.SelectedRows[0];
                object pre = row.Tag;
                if (pre != null)
                {
                    FrmDetailBase detailForm = GetDetailForm();
                    if (detailForm != null)
                    {
                        detailForm.IsAdding = false;
                        detailForm.UpdatingItem = pre;

                        detailForm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                        {
                            if (GridView != null) GridView.Invalidate();
                        };
                        detailForm.ShowDialog();
                    }
                }
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
                    LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
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
                LJH.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
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
        protected virtual FrmDetailBase GetDetailForm()
        {
            return null;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> GetDataSource()
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

        protected virtual List<T> FilterData(List<T> items)
        {
            return items;
        }

        protected virtual object GetCellValue(T item, string colName)
        {
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
        protected virtual bool DeletingItem(T item)
        {
            return false;
        }

        protected virtual T GetRowTag(DataGridViewRow r)
        {
            if (_ShowingItems.Count > r.Index) return _ShowingItems[r.Index];
            return null;
        }

        protected virtual Color GetRowForColor(DataGridViewRow row)
        {
            return Color.Black;
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
            foreach (DataGridViewRow row in GridView.SelectedRows)
            {
                var o = GetRowTag(row);
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
                    this.SelectedItem = this.GridView.Rows[e.RowIndex].Tag;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ItemSelectedEventArgs args = new ItemSelectedEventArgs() { SelectedItem = this.GridView.Rows[e.RowIndex].Tag };
                    if (this.ItemSelected != null) this.ItemSelected(this, args);
                    this.GridView.Rows.Remove(this.GridView.Rows[e.RowIndex]);
                }
            }
        }

        private void GridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < _ShowingItems.Count)
                {
                    T item = _ShowingItems[e.RowIndex];
                    GridView.Rows[e.RowIndex].Tag = item;
                    GridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = (e.RowIndex % 2 == 1) ? Color.FromArgb(230, 230, 230) : Color.White;
                    GridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = GetRowForColor(GridView.Rows[e.RowIndex]);
                    var cell = GridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell is DataGridViewLinkCell) cell.Style.ForeColor = cell.Selected ? Color.White : Color.Blue;
                    e.Value = GetCellValue(item, GridView.Columns[e.ColumnIndex].Name);
                }
                else
                {
                    GridView.Rows[e.RowIndex].Visible = false;
                }
            }
            catch (Exception ex)
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

        private void GridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1)
                {
                    foreach (DataGridViewRow row in GridView.SelectedRows)
                    {
                        row.Selected = false;
                    }
                    if (!GridView.Rows[e.RowIndex].Selected)
                    {
                        GridView.Rows[e.RowIndex].Selected = true;
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
