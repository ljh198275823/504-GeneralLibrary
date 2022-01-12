namespace LJH.GeneralLibrary.WinformControl
{
    partial class UCPaging
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPaging));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalPages = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTotalCount = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtPageIndex = new System.Windows.Forms.ComboBox();
            this.txtPageSize = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtTotalPages
            // 
            resources.ApplyResources(this.txtTotalPages, "txtTotalPages");
            this.txtTotalPages.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalPages.Name = "txtTotalPages";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.ForeColor = System.Drawing.Color.Red;
            this.txtCount.Name = "txtCount";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtTotalCount
            // 
            resources.ApplyResources(this.txtTotalCount, "txtTotalCount");
            this.txtTotalCount.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalCount.Name = "txtTotalCount";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.txtPageIndex);
            this.panel1.Controls.Add(this.txtPageSize);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtTotalCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtTotalPages);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtCount);
            this.panel1.Controls.Add(this.label10);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnLast
            // 
            resources.ApplyResources(this.btnLast, "btnLast");
            this.btnLast.ForeColor = System.Drawing.Color.Blue;
            this.btnLast.Name = "btnLast";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ForeColor = System.Drawing.Color.Blue;
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.ForeColor = System.Drawing.Color.Blue;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevios_Click);
            // 
            // btnFirst
            // 
            resources.ApplyResources(this.btnFirst, "btnFirst");
            this.btnFirst.ForeColor = System.Drawing.Color.Blue;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtPageIndex
            // 
            resources.ApplyResources(this.txtPageIndex, "txtPageIndex");
            this.txtPageIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPageIndex.FormattingEnabled = true;
            this.txtPageIndex.Name = "txtPageIndex";
            // 
            // txtPageSize
            // 
            resources.ApplyResources(this.txtPageSize, "txtPageSize");
            this.txtPageSize.FormattingEnabled = true;
            this.txtPageSize.Items.AddRange(new object[] {
            resources.GetString("txtPageSize.Items"),
            resources.GetString("txtPageSize.Items1"),
            resources.GetString("txtPageSize.Items2"),
            resources.GetString("txtPageSize.Items3"),
            resources.GetString("txtPageSize.Items4"),
            resources.GetString("txtPageSize.Items5"),
            resources.GetString("txtPageSize.Items6")});
            this.txtPageSize.Name = "txtPageSize";
            // 
            // UCPaging
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCPaging";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtTotalPages;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label txtTotalCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox txtPageSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.ComboBox txtPageIndex;
    }
}
