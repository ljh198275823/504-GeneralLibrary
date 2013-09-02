namespace BX4KTest
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
            this.label6 = new System.Windows.Forms.Label();
            this.chkSingleLine = new System.Windows.Forms.CheckBox();
            this.chkNewLine = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.txtAddress = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.comport = new LJH.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.txtContent = new LJH.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "控制器串口号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "高度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "宽度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "控制板地址";
            // 
            // chkSingleLine
            // 
            this.chkSingleLine.AutoSize = true;
            this.chkSingleLine.Location = new System.Drawing.Point(13, 91);
            this.chkSingleLine.Name = "chkSingleLine";
            this.chkSingleLine.Size = new System.Drawing.Size(72, 16);
            this.chkSingleLine.TabIndex = 8;
            this.chkSingleLine.Text = "单行显示";
            this.chkSingleLine.UseVisualStyleBackColor = true;
            // 
            // chkNewLine
            // 
            this.chkNewLine.AutoSize = true;
            this.chkNewLine.Location = new System.Drawing.Point(110, 91);
            this.chkNewLine.Name = "chkNewLine";
            this.chkNewLine.Size = new System.Drawing.Size(72, 16);
            this.chkNewLine.TabIndex = 9;
            this.chkNewLine.Text = "自动换行";
            this.chkNewLine.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(197, 183);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "应用(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(295, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "显示内容";
            // 
            // numY
            // 
            this.numY.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numY.Location = new System.Drawing.Point(114, 58);
            this.numY.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(48, 21);
            this.numY.TabIndex = 20;
            // 
            // numHeight
            // 
            this.numHeight.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numHeight.Location = new System.Drawing.Point(306, 58);
            this.numHeight.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(48, 21);
            this.numHeight.TabIndex = 21;
            this.numHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // numX
            // 
            this.numX.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numX.Location = new System.Drawing.Point(30, 58);
            this.numX.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(48, 21);
            this.numX.TabIndex = 22;
            // 
            // numWidth
            // 
            this.numWidth.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numWidth.Location = new System.Drawing.Point(209, 58);
            this.numWidth.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(48, 21);
            this.numWidth.TabIndex = 23;
            this.numWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(273, 25);
            this.txtAddress.MaxValue = 65536;
            this.txtAddress.MinValue = 0;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(90, 21);
            this.txtAddress.TabIndex = 19;
            this.txtAddress.Text = "1";
            // 
            // comport
            // 
            this.comport.FormattingEnabled = true;
            this.comport.Location = new System.Drawing.Point(96, 25);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(96, 20);
            this.comport.TabIndex = 18;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(70, 114);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(293, 21);
            this.txtContent.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 219);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.comport);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.chkNewLine);
            this.Controls.Add(this.chkSingleLine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "BX4K测试工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkSingleLine;
        private System.Windows.Forms.CheckBox chkNewLine;
        private LJH.GeneralLibrary.WinformControl.DBCTextBox txtContent;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private LJH.GeneralLibrary.WinformControl.ComPortComboBox comport;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtAddress;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numWidth;
    }
}

