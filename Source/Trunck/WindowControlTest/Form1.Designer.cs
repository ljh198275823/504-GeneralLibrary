namespace WindowControlTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNone = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txt3 = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txt2 = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txt1 = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txt0 = new LJH.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "1位小数点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "2位小数点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "无限制";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "3位小数点";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "0位小数点";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "赋值";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNone
            // 
            this.txtNone.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNone.Location = new System.Drawing.Point(83, 163);
            this.txtNone.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtNone.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtNone.Name = "txtNone";
            this.txtNone.PointCount = -1;
            this.txtNone.Size = new System.Drawing.Size(169, 21);
            this.txtNone.TabIndex = 9;
            this.txtNone.Text = "0";
            // 
            // txt3
            // 
            this.txt3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt3.Location = new System.Drawing.Point(83, 129);
            this.txt3.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txt3.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txt3.Name = "txt3";
            this.txt3.PointCount = 3;
            this.txt3.Size = new System.Drawing.Size(169, 21);
            this.txt3.TabIndex = 3;
            this.txt3.Text = "0";
            // 
            // txt2
            // 
            this.txt2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt2.Location = new System.Drawing.Point(83, 98);
            this.txt2.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txt2.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txt2.Name = "txt2";
            this.txt2.PointCount = 2;
            this.txt2.Size = new System.Drawing.Size(169, 21);
            this.txt2.TabIndex = 2;
            this.txt2.Text = "0";
            // 
            // txt1
            // 
            this.txt1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt1.Location = new System.Drawing.Point(83, 65);
            this.txt1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txt1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txt1.Name = "txt1";
            this.txt1.PointCount = 1;
            this.txt1.Size = new System.Drawing.Size(169, 21);
            this.txt1.TabIndex = 1;
            this.txt1.Text = "0";
            // 
            // txt0
            // 
            this.txt0.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt0.Location = new System.Drawing.Point(83, 35);
            this.txt0.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txt0.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txt0.Name = "txt0";
            this.txt0.PointCount = 0;
            this.txt0.Size = new System.Drawing.Size(169, 21);
            this.txt0.TabIndex = 0;
            this.txt0.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 270);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt3);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.txt0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txt0;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txt1;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txt2;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txt3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private LJH.GeneralLibrary.WinformControl.DecimalTextBox txtNone;
        private System.Windows.Forms.Button button1;
    }
}

