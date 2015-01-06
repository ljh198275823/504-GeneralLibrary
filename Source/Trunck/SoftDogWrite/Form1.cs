﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LJH.GeneralLibrary;
using LJH.GeneralLibrary.SoftDog;

namespace SoftDogWrite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量
        private const string  _Key = "Ljh198275823@163.com";
        private SoftDogReader _Writer = new SoftDogReader(new DTEncrypt().Encrypt(_Key));
        private const int CHECKPOSITON = 70;
        private const string CHECKSTRING = "Ljh198275823";
        private DSEncrypt MydsEncrypt = new DSEncrypt(_Key);
        private Random _myRandom = new Random();
        #endregion

        #region 私有方法
        private void WriteDog()
        {
            int ret = -1;
            byte[] rdDate = new byte[100];
            _myRandom.NextBytes(rdDate);
            ret = _Writer.WriteData(0, rdDate, _Key);

            byte[] data = null;
            if (ret == 0)
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(CHECKSTRING));
                ret = _Writer.WriteData(CHECKPOSITON, data, _Key);
            }

            if (ret == 0)
            {
                data = SEBinaryConverter.IntToBytes(txtProjectID.IntergerValue);
                ret = _Writer.WriteData(61, data, _Key);
            }

            if (ret == 0)
            {
                SoftwareType sl = SoftwareType.None;
                if (chkACS.Checked) sl |= SoftwareType.TYPE_ACS;
                if (chkInventory.Checked) sl |= SoftwareType.TYPE_Inventory;
                if (chkTA.Checked) sl |= SoftwareType.TYPE_TA;
                if (chkZhongkao.Checked) sl |= SoftwareType.TYPE_ZHONGKAO;
                data = SEBinaryConverter.IntToBytes((int)sl);
                ret = _Writer.WriteData(31, data, _Key);
            }

            if (ret == 0)
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dtStart.Value.Date.ToString("yyMMdd")));
                ret = _Writer.WriteData(17, data, _Key);
            }

            if (ret == 0)
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dtEnd.Value.Date.ToString("yyMMdd")));
                ret = _Writer.WriteData(5, data, _Key);
            }
            if (ret != 0) throw new InvalidOperationException("写狗失败 errorcode=" + ret.ToString());
        }
        #endregion

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                SoftDogInfo info = _Writer.ReadDog();
                txtProjectID.IntergerValue = info.ProjectNo;
                chkZhongkao.Checked = (info.SoftwareList & SoftwareType.TYPE_ZHONGKAO) == SoftwareType.TYPE_ZHONGKAO;
                chkInventory.Checked = (info.SoftwareList & SoftwareType.TYPE_Inventory) == SoftwareType.TYPE_Inventory;
                chkACS.Checked = (info.SoftwareList & SoftwareType.TYPE_ACS) == SoftwareType.TYPE_ACS;
                chkTA.Checked = (info.SoftwareList & SoftwareType.TYPE_TA) == SoftwareType.TYPE_TA;
                dtStart.Value = info.StartDate;
                dtEnd.Value = info.ExpiredDate;
                MessageBox.Show("读狗成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (txtProjectID.IntergerValue <= 0)
            {
                MessageBox.Show("项目编号不正确");
                return;
            }
            if (!chkACS.Checked && !chkInventory.Checked && !chkTA.Checked && !chkZhongkao.Checked)
            {
                MessageBox.Show("请至少选择一种软件类型");
                return;
            }
            if (dtEnd.Value < dtStart.Value)
            {
                MessageBox.Show("结束日期不能小于开始日期");
                return;
            }
            try
            {
                WriteDog();
                MessageBox.Show("写狗成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}