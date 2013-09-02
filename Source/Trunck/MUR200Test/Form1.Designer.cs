namespace MUR200Test
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
            this.btnReadID = new System.Windows.Forms.Button();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReadData = new System.Windows.Forms.TextBox();
            this.btnReadBlock = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtBlock = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtSec = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnReadID
            // 
            this.btnReadID.Location = new System.Drawing.Point(12, 45);
            this.btnReadID.Name = "btnReadID";
            this.btnReadID.Size = new System.Drawing.Size(75, 23);
            this.btnReadID.TabIndex = 0;
            this.btnReadID.Text = "读ID号";
            this.btnReadID.UseVisualStyleBackColor = true;
            this.btnReadID.Click += new System.EventHandler(this.btnReadID_Click);
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(104, 47);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(138, 21);
            this.txtCardID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "扇区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "块号(0-3):";
            // 
            // txtReadData
            // 
            this.txtReadData.Location = new System.Drawing.Point(12, 129);
            this.txtReadData.Name = "txtReadData";
            this.txtReadData.Size = new System.Drawing.Size(402, 21);
            this.txtReadData.TabIndex = 6;
            // 
            // btnReadBlock
            // 
            this.btnReadBlock.Location = new System.Drawing.Point(228, 94);
            this.btnReadBlock.Name = "btnReadBlock";
            this.btnReadBlock.Size = new System.Drawing.Size(75, 23);
            this.btnReadBlock.TabIndex = 7;
            this.btnReadBlock.Text = "读取数据";
            this.btnReadBlock.UseVisualStyleBackColor = true;
            this.btnReadBlock.Click += new System.EventHandler(this.btnReadBlock_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(320, 94);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 8;
            this.btnWrite.Text = "写入数据";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(164, 96);
            this.txtBlock.MaxValue = 16;
            this.txtBlock.MinValue = 0;
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(47, 21);
            this.txtBlock.TabIndex = 3;
            this.txtBlock.Text = "1";
            // 
            // txtSec
            // 
            this.txtSec.Location = new System.Drawing.Point(45, 96);
            this.txtSec.MaxValue = 16;
            this.txtSec.MinValue = 0;
            this.txtSec.Name = "txtSec";
            this.txtSec.Size = new System.Drawing.Size(42, 21);
            this.txtSec.TabIndex = 2;
            this.txtSec.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "密钥：";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(59, 12);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(135, 21);
            this.txtKey.TabIndex = 10;
            this.txtKey.Text = "F1 FF FF FF FF F1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 198);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnReadBlock);
            this.Controls.Add(this.txtReadData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBlock);
            this.Controls.Add(this.txtSec);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.btnReadID);
            this.Name = "Form1";
            this.Text = "MUR200测试程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadID;
        private System.Windows.Forms.TextBox txtCardID;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtSec;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadData;
        private System.Windows.Forms.Button btnReadBlock;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKey;
    }
}

