using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
        private const string _Key = "Ljh198275823@163.com";
        private SoftDogReader _Writer = new SoftDogReader(new DTEncrypt().Encrypt(_Key));
        private const int CHECKPOSITON = 70;
        private const string CHECKSTRING = "Ljh198275823";
        private DSEncrypt MydsEncrypt = new DSEncrypt(_Key);
        private Random _myRandom = new Random();
        #endregion

        #region 私有方法
        private bool CheckInput()
        {
            if (txtProjectID.IntergerValue <= 0)
            {
                MessageBox.Show("项目编号不正确");
                return false;
            }
            if (!chkACS.Checked && !chkInventory.Checked && !chkTA.Checked && !chkPark.Checked && !chkTYPE_SteelRollInventory_COST.Checked)
            {
                MessageBox.Show("请至少选择一种软件类型");
                return false;
            }
            if (dtEnd.Value < dtStart.Value)
            {
                MessageBox.Show("结束日期不能小于开始日期");
                return false;
            }
            return true;
        }

        private SoftDogInfo GetDogInfoFromInput()
        {
            SoftDogInfo dog = new SoftDogInfo();
            dog.ProjectNo = txtProjectID.IntergerValue;
            dog.SoftwareList = SoftwareType.None;
            if (chkACS.Checked) dog.SoftwareList |= SoftwareType.TYPE_ACS;
            if (chkInventory.Checked) dog.SoftwareList |= SoftwareType.TYPE_Inventory;
            if (chkTA.Checked) dog.SoftwareList |= SoftwareType.TYPE_TA;
            if (chkPark.Checked) dog.SoftwareList |= SoftwareType.TYPE_PARK;
            if (chkTYPE_SteelRollInventory_COST.Checked) dog.SoftwareList |= SoftwareType.TYPE_SteelRollInventory_COST;
            dog.StartDate = dtStart.Value.Date;
            dog.ExpiredDate = dtEnd.Value.Date;
            dog.IsHost = chkHost.Checked;
            dog.DBServer = txtDBServer.Text.Trim();
            dog.DBName = txtDBName.Text.Trim();
            dog.DBUser = txtUser.Text.Trim();
            dog.DBPassword = txtPassword.Text.Trim();
            dog.MAC = txtMAC.Text.Trim();
            if (!string.IsNullOrEmpty(dog.MAC)) dog.MAC = dog.MAC.ToUpper();
            return dog;
        }

        private void WriteDog(SoftDogInfo dog)
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
                data = SEBinaryConverter.IntToBytes(dog.ProjectNo);
                ret = _Writer.WriteData(61, data, _Key);
            }

            if (ret == 0)
            {
                data = SEBinaryConverter.IntToBytes((int)dog.SoftwareList);
                ret = _Writer.WriteData(31, data, _Key);
            }

            if (ret == 0)
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dog.StartDate.ToString("yyMMdd")));
                ret = _Writer.WriteData(17, data, _Key);
            }

            if (ret == 0)
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dog.ExpiredDate.ToString("yyMMdd")));
                ret = _Writer.WriteData(5, data, _Key);
            }
            if (ret == 0)
            {
                ret = _Writer.WriteData(37, new byte[] { (byte)(dog.IsHost ? 1 : 0) }, _Key);
            }
            if (ret == 0 && !string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dog.DBUser));
                byte[] temp = new byte[10];
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i < data.Length) temp[i] = data[i];
                    else temp[i] = 0x20;
                }
                ret = _Writer.WriteData(40, temp, _Key);
            }
            if (ret == 0 && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                data = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(MydsEncrypt.Encrypt(dog.DBPassword));
                byte[] temp = new byte[10];
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i < data.Length) temp[i] = data[i];
                    else temp[i] = 0x20;
                }
                ret = _Writer.WriteData(50, temp, _Key);
            }
            if (ret != 0) throw new InvalidOperationException("写狗失败 errorcode=" + ret.ToString());
        }

        private void WriteLIC(SoftDogInfo dog, string licFile)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("ProjectNo:{0};", dog.ProjectNo));
            sb.Append(string.Format("SoftwareList:{0};", (int)dog.SoftwareList));
            sb.Append(string.Format("StartDate:{0};", dog.StartDate.ToString("yyyy-MM-dd")));
            sb.Append(string.Format("ExpiredDate:{0};", dog.ExpiredDate.ToString("yyyy-MM-dd")));
            sb.Append(string.Format("IsHost:{0};", dog.IsHost.ToString()));
            sb.Append(string.Format("DBName:{0};", dog.DBName));
            sb.Append(string.Format("DBUser:{0};", dog.DBUser));
            if (!string.IsNullOrEmpty(dog.DBServer)) sb.Append(string.Format("DBServer:{0};", dog.DBServer));
            sb.Append(string.Format("DBPassword:{0};", dog.DBPassword));
            sb.Append(string.Format("MAC:{0}", dog.MAC));
            using (FileStream fs = new FileStream(licFile, FileMode.Create, FileAccess.ReadWrite))
            {
                var data = System.Text.ASCIIEncoding.ASCII.GetBytes(new DTEncrypt().Encrypt(sb.ToString()));
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
        }
        #endregion
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                SoftDogInfo info = _Writer.ReadDog();
                if (info != null)
                {
                    ShowDog(info);
                    MessageBox.Show("读狗成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowDog(SoftDogInfo info)
        {
            txtProjectID.IntergerValue = info.ProjectNo;
            chkInventory.Checked = (info.SoftwareList & SoftwareType.TYPE_Inventory) == SoftwareType.TYPE_Inventory;
            chkACS.Checked = (info.SoftwareList & SoftwareType.TYPE_ACS) == SoftwareType.TYPE_ACS;
            chkTA.Checked = (info.SoftwareList & SoftwareType.TYPE_TA) == SoftwareType.TYPE_TA;
            chkPark.Checked = (info.SoftwareList & SoftwareType.TYPE_PARK) == SoftwareType.TYPE_PARK;
            chkTYPE_SteelRollInventory_COST.Checked = ((info.SoftwareList & SoftwareType.TYPE_SteelRollInventory_COST) == SoftwareType.TYPE_SteelRollInventory_COST);
            dtStart.Value = info.StartDate;
            dtEnd.Value = info.ExpiredDate;
            chkHost.Checked = info.IsHost;
            txtDBName.Text = info.DBName;
            txtDBServer.Text = info.DBServer;
            txtUser.Text = info.DBUser;
            txtPassword.Text = info.DBPassword;
            txtMAC.Text = info.MAC;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    var dog = GetDogInfoFromInput();
                    WriteDog(dog);
                    MessageBox.Show("写狗成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWriteLic_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dig = new FolderBrowserDialog();
                if (dig.ShowDialog() == DialogResult.OK)
                {
                    if (CheckInput())
                    {
                        var dog = GetDogInfoFromInput();
                        string lic = Path.Combine(dig.SelectedPath, "ljh.lic");
                        WriteLIC(dog, lic);
                        MessageBox.Show("写LIC成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadLic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog();
            if (dig.ShowDialog() == DialogResult.OK)
            {
                SoftDogInfo dog = LICReader.ReadDog(dig.FileName);
                if (dog != null)
                {
                    ShowDog(dog);
                    MessageBox.Show("读LIC成功");
                }
            }
        }
    }
}
