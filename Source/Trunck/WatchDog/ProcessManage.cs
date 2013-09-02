using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace WatchDog
{
    public class ProcessManage
    {
        private int waitSecond = int.Parse(AppSettings.CurrentSetting.GetConfigContent("WaitSecond"));
        public string Processes_Name = AppSettings.CurrentSetting.GetConfigContent("ProcessName");
        public string UserAndPassword = AppSettings.CurrentSetting.GetConfigContent("UserAndPassword");
        public string Active_Status = "正常";

        private DateTime lastTime = DateTime.Now;
        private Process process = new Process();

        private static ProcessManage m_ProcessManage = new ProcessManage();
        public static ProcessManage GetInstance
        {
            get
            {
                return m_ProcessManage;
            }
        }

        private bool IsFirstTime = true;
        public string GetProcessStatus()
        {
            Process[] ps = Process.GetProcessesByName(Processes_Name);
            if (ps.Count() > 0)
            {
                if (ps[0].Responding)
                {
                    IsFirstTime = true;
                    return Active_Status;
                }
                else
                {
                    if (IsFirstTime)
                    {
                        lastTime = DateTime.Now;
                        IsFirstTime = false;
                    }

                    // 判断进程是否长时间无响应
                    if (DateTime.Now < lastTime.AddSeconds(waitSecond))
                    {
                        return Active_Status + "，但暂时无响应";
                    }
                    else
                    {
                        return "长时间无响应";
                    }
                }
            }
            else
            {
                return "未启动";
            }
        }

        public void DoKill()
        {
            DoKill(Processes_Name);
        }

        public void DoKill(string processName)
        {
            Process[] ps = Process.GetProcessesByName(processName);
            if (ps.Count() > 0)
            {
                process.StartInfo.FileName = "taskkill.exe";
                //cmd.StartInfo.Arguments = string.Format("/F /fi \"STATUS eq NOT RESPONDING\" /im {0}.exe", PROCESSES_NAME);
                process.StartInfo.Arguments = string.Format("/F /im {0}.exe", processName);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
        }


        public void DoStart()
        {
            Process[] ps = Process.GetProcessesByName(Processes_Name);
            if (ps.Count() == 0)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = Processes_Name + ".exe";
                info.Arguments = UserAndPassword;
                Process.Start(info);
            }
        }

        public void DoRestart()
        {
            // 执行终止程序
            DoKill();

            // 执行启动程序
            System.Threading.Thread.Sleep(1 * 1000);
            DoStart();

            lastTime = DateTime.Now;

            //// 5秒后，若未启动，则再次执行启动程序
            //System.Threading.Thread.Sleep(5 * 1000);
            //DoStart();
        }

    }
}
