namespace LJH.GeneralLibrary.WinformControl
{
    partial class UCIPTextBox
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
            this.components = new System.ComponentModel.Container();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ip2 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip4 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip3 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip1 = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(161, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 152;
            this.label14.Text = ".";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(102, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 151;
            this.label13.Text = ".";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 150;
            this.label12.Text = ".";
            // 
            // ip2
            // 
            this.ip2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip2.Location = new System.Drawing.Point(58, 3);
            this.ip2.MaxValue = 255;
            this.ip2.MinValue = 0;
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(39, 21);
            this.ip2.TabIndex = 147;
            this.ip2.Text = "0";
            this.ip2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ip1_KeyUp);
            // 
            // ip4
            // 
            this.ip4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip4.Location = new System.Drawing.Point(173, 3);
            this.ip4.MaxValue = 255;
            this.ip4.MinValue = 0;
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(39, 21);
            this.ip4.TabIndex = 149;
            this.ip4.Text = "0";
            this.ip4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ip1_KeyUp);
            // 
            // ip3
            // 
            this.ip3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip3.Location = new System.Drawing.Point(116, 3);
            this.ip3.MaxValue = 255;
            this.ip3.MinValue = 0;
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(39, 21);
            this.ip3.TabIndex = 148;
            this.ip3.Text = "0";
            this.ip3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ip1_KeyUp);
            // 
            // ip1
            // 
            this.ip1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip1.Location = new System.Drawing.Point(0, 3);
            this.ip1.MaxValue = 255;
            this.ip1.MinValue = 0;
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(39, 21);
            this.ip1.TabIndex = 146;
            this.ip1.Text = "0";
            this.ip1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ip1_KeyUp);
            // 
            // UCIPTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ip2);
            this.Controls.Add(this.ip4);
            this.Controls.Add(this.ip3);
            this.Controls.Add(this.ip1);
            this.Name = "UCIPTextBox";
            this.Size = new System.Drawing.Size(219, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private LJH.GeneralLibrary .WinformControl .IntergerTextBox  ip2;
        private LJH.GeneralLibrary .WinformControl .IntergerTextBox ip4;
        private LJH.GeneralLibrary .WinformControl .IntergerTextBox ip3;
        private LJH.GeneralLibrary .WinformControl .IntergerTextBox ip1;
    }
}
