using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.WinForm
{
    public partial class FrmViewBase<TID, TEntity> : Form where TEntity : IEntity<TID>
    {
        #region 构造函数
        public FrmViewBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        protected List<TEntity> _Items;
        private DataGridView _gridView;
        private string _ColumnsConfig = System.IO.Path.Combine(Application.StartupPath, "ColumnsConf.xml");
        #endregion.

        #region 私有方法
        private void InitGridView()
        {
            if (GridView != null)
            {
                GridView.BorderStyle = BorderStyle.FixedSingle;
                GridView.BackgroundColor = Color.White;
                GridView.Sorted += new EventHandler(GridView_Sorted);
                GridView.SelectionChanged += new EventHandler(GridView_SelectionChanged);
                if (GridView.ContextMenuStrip != null)
                {
                    ContextMenuStrip menu = GridView.ContextMenuStrip;
                    if (menu.Items["cMnu_Export"] != null) menu.Items["cMnu_Export"].Click += btnExport_Click;
                    if (menu.Items["cMnu_SelectColumns"] != null) menu.Items["cMnu_SelectColumns"].Click += btnSelectColumns_Click;
                    if (menu.Items["cMnu_Fresh"] != null) menu.Items["cMnu_Fresh"].Click += btnFresh_Click;
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
                    row.DefaultCellStyle.BackColor = (count % 2 == 1) ? Color.FromArgb(220, 220, 220) : Color.White;
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
                DataGridView view = this.GridView;
                if (view != null)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel文档|*.xls|所有文件(*.*)|*.*";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        if (DataGridViewExporter.Export(view, path))
                        {
                            MessageBox.Show("导出成功");
                        }
                        else
                        {
                            MessageBox.Show("保存到电子表格时出现错误!");
                        }
                    }
                }
            }
            catch
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
        /// 显示数据
        /// </summary>
        /// <param name="items">要显示的数据</param>
        /// <param name="reload">是否重新加载数据，如果为真，则表示先会清空之前的数据，否则保留旧有数据</param>
        protected virtual void ShowItemsOnGrid(List<TEntity> items)
        {
            GridView.Rows.Clear();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    int row = GridView.Rows.Add();
                    ShowItemInGridViewRow(GridView.Rows[row], item);
                    GridView.Rows[row].Tag = item;
                }
                ShowRowBackColor();
                this.GridView.Rows[0].Selected = false;
            }
            this.toolStripStatusLabel1.Text = string.Format("总共 {0} 项", items?.Count);
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
            InitGridView();
            InitGridViewColumns();
        }
        /// <summary>
        /// 显示操作的权限
        /// </summary>
        public virtual void ShowOperatorRights()
        {

        }

        protected virtual void ReFreshData()
        {
            _Items = null;
            var ret = GetDataSource();
            if (ret.Result != ResultCode.Successful)
            {
                MessageBox.Show(ret.Message);
            }
            else if (ret.QueryObjects != null && ret.QueryObjects.Count > 0)
            {
                _Items = ret.QueryObjects;
            }
            FreshView();
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
            if (_Items != null && _Items.Count > 0)
            {
                var items = FilterData(_Items.ToList());
                ShowItemsOnGrid(items);
            }
            else
            {
                ShowItemsOnGrid(null);
            }
        }

        protected virtual List<TEntity> FilterData(List<TEntity> items)
        {
            return items;
        }

        /// <summary>
        /// 在网格行中显示单个数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        protected virtual void ShowItemInGridViewRow(DataGridViewRow row, TEntity item)
        {

        }
        #endregion

        #region 事件处理
        private void FrmViewBase_Load(object sender, EventArgs e)
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

        private void btnFresh_Click(object sender, EventArgs e)
        {
            ReFreshData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.GridView != null)
            {
                NPOIExcelHelper.Export(GridView, true);
            }
        }

        private void btnSelectColumns_Click(object sender, EventArgs e)
        {
            SelectColumns();
        }

        private void FrmViewBase_Resize(object sender, EventArgs e)
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
        #endregion
    }
}
