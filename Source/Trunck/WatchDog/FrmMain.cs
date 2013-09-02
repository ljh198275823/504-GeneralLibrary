using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace WatchDog
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private bool _Restart = false;

        private void btnKillPro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要执行此项操作吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ProcessManage.GetInstance.DoKill();
                ProcessManage.GetInstance.DoKill("AVTRSrv");
            }
        }


        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要执行此项操作吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ProcessManage.GetInstance.DoRestart();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要执行此项操作吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ProcessManage.GetInstance.DoStart();
            }
        }


        private void DoCheck()
        {
            if (this.listBox1.Items.Count > 200)
            {
                this.listBox1.Items.Clear();
            }

            string status = ProcessManage.GetInstance.GetProcessStatus();
            string msg = string.Format("【{0}】，监听进程名称：{1}，该进程状态：{2}。", DateTime.Now,
                ProcessManage.GetInstance.Processes_Name, status);
            this.listBox1.Items.Insert(0, msg);

            if (status.Contains(ProcessManage.GetInstance.Active_Status) == false)
            {
                msg = msg + "系统执行重启。";
                this.listBox1.Items.Insert(0, msg);
                ProcessManage.GetInstance.DoRestart();
                LogManage.WriteLog(msg);
            }
            this.listBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DoCheck();
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            StartListen();
        }

        private void StartListen()
        {
            this.timer1.Enabled = true;
            RefreshUI();
        }

        private void RefreshUI()
        {
            this.btnStartTimer.Enabled = !this.timer1.Enabled;
            this.btnStopTimer.Enabled = this.timer1.Enabled;
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要执行此项操作吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.timer1.Enabled = false;
                RefreshUI();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chkRestart.Checked = AppSettings.CurrentSetting.GetConfigContent("AutoReboot") == "True";
            this.timer1.Interval = int.Parse(this.numericUpDown1.Value.ToString()) * 1000;
            StartListen();
            DeleteDebugRegistryKey();
            tmrRestart.Enabled = chkRestart.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.timer1.Interval = int.Parse(this.numericUpDown1.Value.ToString()) * 1000;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要“退出监测程序”吗？点击确定将关闭此程序，点击取消返回。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        //删除启用实时调试的注册表项，禁用系统的实时调试模式
        private void DeleteDebugRegistryKey()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                key = key.OpenSubKey("Microsoft", true);
                key = key.OpenSubKey("Windows NT", true);
                key = key.OpenSubKey("CurrentVersion", true);
                key = key.OpenSubKey("AeDebug", true);
                key.DeleteValue("Debugger", false);

                key = Registry.LocalMachine;
                key = key.OpenSubKey("SOFTWARE", true);
                key = key.OpenSubKey("Microsoft", true);
                key = key.OpenSubKey(".NETFramework", true);
                key.DeleteValue("DbgManagedDebugger", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tmrRestart_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (dt.Hour == 4)
            {
                if (!_Restart)
                {
                    ProcessManage.GetInstance.DoKill();
                    ProcessManage.GetInstance.DoKill("AVTRSrv");
                    _Restart = true;
                }
            }
            else
            {
                _Restart = false;
            }
        }

        private void chkRestart_CheckedChanged(object sender, EventArgs e)
        {
            tmrRestart.Enabled = chkRestart.Checked;
            AppSettings.CurrentSetting.SaveConfig("AutoReboot", chkRestart.Checked ? "True" : "False");
        }
    }
}
