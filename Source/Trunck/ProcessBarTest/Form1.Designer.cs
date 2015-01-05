namespace ProcessBarTest
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.percentageProgressBar1 = new LJH.GeneralLibrary.WinformControl.PercentageProgressBar(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // percentageProgressBar1
            // 
            this.percentageProgressBar1.Location = new System.Drawing.Point(62, 50);
            this.percentageProgressBar1.Name = "percentageProgressBar1";
            this.percentageProgressBar1.Size = new System.Drawing.Size(535, 72);
            this.percentageProgressBar1.TabIndex = 0;
            this.percentageProgressBar1.TextColor = System.Drawing.Color.Black;
            this.percentageProgressBar1.TextFont = new System.Drawing.Font("宋体", 9F);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(453, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 234);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.percentageProgressBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private LJH.GeneralLibrary.WinformControl.PercentageProgressBar percentageProgressBar1;
        private System.Windows.Forms.Button button1;
    }
}

