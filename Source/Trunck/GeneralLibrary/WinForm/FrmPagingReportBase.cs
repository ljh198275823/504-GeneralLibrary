using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinForm
{
    public partial class FrmPagingReportBase<T> : Form, IFormMaster
    {
        #region 构造函数
        public FrmPagingReportBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private DataGridView _gridView;
        private Control _PnlLeft;
        private string _ColumnsConfig = System.IO.Path.Combine(Application.StartupPath, "ColumnsConf.xml");
        private string _PageSizeConfig = System.IO.Path.Combine(Application.StartupPath, "PageSize.xml");
        private string _PnlLeftWidthConfig = System.IO.Path.Combine(Application.StartupPath, "PnlLeftWidthConf.xml");
        #endregion.

        #region 私有方法
        private void InitGridView()
        {
            if (GridView != null)
            {
                GridView.BorderStyle = BorderStyle.FixedSingle;
                GridView.BackgroundColor = Color.White;
                GridView.Sorted += new EventHandler(GridView_Sorted);

                if (GridView.ContextMenuStrip != null)
                {
                    ContextMenuStrip menu = GridView.ContextMenuStrip;
                    if (menu.Items["cMnu_Export"] != null) menu.Items["cMnu_Export"].Click += btnSaveAs_Click;
                    if (menu.Items["cMnu_SelectColumns"] != null) menu.Items["cMnu_SelectColumns"].Click += btnSelectColumns_Click;
                }
            }
        }

        private void InitGridViewColumns()
        {
            DataGridView grid = this.GridView;
            if (grid == null) return;
            string temp = GetConfig(_ColumnsConfig, "Columns");
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

        #region 保护方法
        public virtual void ReFreshData()
        {
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
        protected virtual void PerformExportData()
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
                ExceptionPolicy.HandleException(ex);
                MessageBox.Show("保存到电子表格时出现错误!");
            }
        }
        /// <summary>
        /// 选择数据网格中要显示的列
        /// </summary>
        protected virtual void PerformSelectColumns()
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
                    SaveConfig(_ColumnsConfig, "Columns", temp);
                    InitGridViewColumns();
                }
            }
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
                            MyKeyValuePair kv = items.SingleOrDefault(it => it.Key == string.Format("{0}_{1}", this.GetType().Name, key));
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
                var mykey = string.Format("{0}_{1}", this.GetType().Name, key);
                MyKeyValuePair kv = items.SingleOrDefault(it => it.Key == mykey);
                if (kv != null)
                {
                    kv.Value = value;
                }
                else
                {
                    items.Add(new MyKeyValuePair { Key = mykey, Value = value });
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

        protected virtual Control PnlLeft
        {
            get
            {
                if (_PnlLeft == null)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl.Name == "pnlLeft")
                        {
                            _PnlLeft = ctrl;
                            break;
                        }
                    }
                }
                return _PnlLeft;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            ShowOperatorRights();
            InitGridView();
            InitGridViewColumns();
            InitPnlLeft();
        }
        /// <summary>
        /// 显示操作的权限
        /// </summary>
        public virtual void ShowOperatorRights()
        {

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
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected virtual QueryResultList<T> GetDataSource(int pageSize, int pageIndex)
        {
            return null;
        }
        #endregion

        #region 事件处理
        private void FrmReportBase_Load(object sender, EventArgs e)
        {
            Init();
            this.ucPaging1.Init();
            this.ucPaging1.GetPageData += UcPaging1_GetPageData;
            var temp = GetConfig(_PageSizeConfig, "PageSize");
            int pageSize = 0;
            if (string.IsNullOrEmpty(temp) || !int.TryParse(temp, out pageSize) || pageSize == 0) pageSize = 30;
            this.ucPaging1.SetPageSize(pageSize);
        }

        private void FrmPagingReportBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PnlLeft != null)
            {
                SaveConfig(_PnlLeftWidthConfig, string.Format("{0}_PnlLeftWidth", this.GetType().Name), PnlLeft.Width.ToString());
            }
        }

        private void UcPaging1_GetPageData(int pageInex, int pageSize)
        {
            SaveConfig(_PageSizeConfig, "PageSize", pageSize.ToString());
            var items = GetDataSource(pageSize, pageInex);
            ShowItemsOnGrid(items);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var items = GetDataSource(ucPaging1.GetPageSize(), 1);
            if (ucPaging1.GetPageSize() > 0) SaveConfig(_PageSizeConfig, "PageSize", ucPaging1.GetPageSize().ToString());
            ShowItemsOnGrid(items);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            PerformExportData();
        }

        private void GridView_Sorted(object sender, EventArgs e)
        {
            ShowRowBackColor();
        }

        private void btnSelectColumns_Click(object sender, EventArgs e)
        {
            PerformSelectColumns();
        }
        #endregion
    }
}
