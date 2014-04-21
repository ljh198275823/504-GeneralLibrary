using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJH.GeneralLibrary.Core.UI
{
    public partial class FrmColumnSelection : Form
    {
        public FrmColumnSelection()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置被选择列的网格控件
        /// </summary>
        public DataGridView Selectee { get; set; }
        /// <summary>
        /// 获取或设置选取的所有列
        /// </summary>
        public string[] SelectedColumns { get; set; }
        #endregion

        private void FrmColumnSelection_Load(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            if (Selectee != null)
            {
                List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
                foreach (DataGridViewColumn col in Selectee.Columns)
                {
                    cols.Add(col);
                }
                cols = (from col in cols
                        orderby col.DisplayIndex ascending
                        select col).ToList(); //按显示先后顺序排列
                foreach (DataGridViewColumn col in cols)
                {
                    TreeNode item = new TreeNode(col.HeaderText);
                    treeView1.Nodes.Add(item);
                    item.Tag = col.Name;
                    item.Checked = col.Visible;
                }
            }
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.BackColor = Color.Blue;
                treeView1.SelectedNode.ForeColor = Color.White;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool leastCheck = false; //指示是否至少选择了一列
            List<string> cols = new List<string>();
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Checked) leastCheck = true;
                cols.Add(string.Format("{0}:{1}", item.Tag.ToString(), item.Checked ? 1 : 0));
            }
            if (!leastCheck)
            {
                MessageBox.Show("没有选择任何列,请至少选择一列");
            }
            else
            {
                this.SelectedColumns = cols.ToArray();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exchange(TreeNode curNode, TreeNode node)
        {
            string curText = curNode.Text;
            object curObj = curNode.Tag;
            bool curVisiable = curNode.Checked;

            string preText = node.Text;
            object preObj = node.Tag;
            bool preVisiable = node.Checked;

            curNode.Text = preText;
            curNode.Tag = preObj;
            curNode.Checked = preVisiable;

            node.Text = curText;
            node.Tag = curObj;
            node.Checked = curVisiable;

            treeView1.SelectedNode = node;
            node.BackColor = curNode.BackColor;
            node.ForeColor = curNode.ForeColor;
            curNode.BackColor = Color.White;
            curNode.ForeColor = Color.Black;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.PrevNode != null)
            {
                Exchange(node, node.PrevNode);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.NextNode != null)
            {
                Exchange(node, node.NextNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!object.ReferenceEquals(treeView1.SelectedNode, e.Node))
            {
                if (treeView1.SelectedNode != null)
                {
                    treeView1.SelectedNode.BackColor = Color.White;
                    treeView1.SelectedNode.ForeColor = Color.Black;
                }
                treeView1.SelectedNode = e.Node;
                treeView1.SelectedNode.BackColor = Color.Blue;
                treeView1.SelectedNode.ForeColor = Color.White;
            }
        }
    }
}
