﻿namespace SoftDogWrite
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
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkZhongkao = new System.Windows.Forms.CheckBox();
            this.chkACS = new System.Windows.Forms.CheckBox();
            this.chkTA = new System.Windows.Forms.CheckBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProjectID = new LJH.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "软件类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "项目编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "开始日期";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(120, 124);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(140, 21);
            this.dtStart.TabIndex = 5;
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Location = new System.Drawing.Point(204, 94);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(60, 16);
            this.chkInventory.TabIndex = 7;
            this.chkInventory.Text = "进销存";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkZhongkao
            // 
            this.chkZhongkao.AutoSize = true;
            this.chkZhongkao.Location = new System.Drawing.Point(120, 62);
            this.chkZhongkao.Name = "chkZhongkao";
            this.chkZhongkao.Size = new System.Drawing.Size(102, 16);
            this.chkZhongkao.TabIndex = 9;
            this.chkZhongkao.Text = "中考/体质测试";
            this.chkZhongkao.UseVisualStyleBackColor = true;
            // 
            // chkACS
            // 
            this.chkACS.AutoSize = true;
            this.chkACS.Location = new System.Drawing.Point(120, 94);
            this.chkACS.Name = "chkACS";
            this.chkACS.Size = new System.Drawing.Size(48, 16);
            this.chkACS.TabIndex = 10;
            this.chkACS.Text = "门禁";
            this.chkACS.UseVisualStyleBackColor = true;
            // 
            // chkTA
            // 
            this.chkTA.AutoSize = true;
            this.chkTA.Location = new System.Drawing.Point(286, 94);
            this.chkTA.Name = "chkTA";
            this.chkTA.Size = new System.Drawing.Size(48, 16);
            this.chkTA.TabIndex = 11;
            this.chkTA.Text = "考勤";
            this.chkTA.UseVisualStyleBackColor = true;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(197, 236);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(101, 33);
            this.btnWrite.TabIndex = 12;
            this.btnWrite.Text = "写狗";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(67, 236);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(101, 33);
            this.btnRead.TabIndex = 13;
            this.btnRead.Text = "读狗";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(120, 157);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(140, 21);
            this.dtEnd.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "结束日期";
            // 
            // txtProjectID
            // 
            this.txtProjectID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtProjectID.Location = new System.Drawing.Point(120, 22);
            this.txtProjectID.MaxValue = 2147483647;
            this.txtProjectID.MinValue = 0;
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(140, 21);
            this.txtProjectID.TabIndex = 4;
            this.txtProjectID.Text = "1000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 296);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.chkTA);
            this.Controls.Add(this.chkACS);
            this.Controls.Add(this.chkZhongkao);
            this.Controls.Add(this.chkInventory);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.txtProjectID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "加密狗写狗工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private LJH.GeneralLibrary.WinformControl.IntergerTextBox txtProjectID;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkZhongkao;
        private System.Windows.Forms.CheckBox chkACS;
        private System.Windows.Forms.CheckBox chkTA;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label4;
    }
}
