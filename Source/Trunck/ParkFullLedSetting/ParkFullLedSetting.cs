using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.LED;

namespace ParkFullLedTest
{
    public partial class FrmParkFullLedSetting : Form
    {
        public FrmParkFullLedSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ParkFullLed led = GetLedFromInput();
                string filePath = Application.StartupPath + @"\ParkFullLed.xml";
                ParkFullLed.SaveToFile(led, filePath);
                MessageBox.Show("配置保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + @"\ParkFullLed.xml";
            if (System.IO.File.Exists(filePath))  //显示在地下空间满位显示屏上
            {
                ParkFullLed led = ParkFullLed.Create(filePath);
                ShowLed(led);
            }
        }

        private void ShowLed(ParkFullLed led)
        {
            this.txtComport.Text = led.COMPort.ToString();
            this.txtBaud.Text = led.Baud.ToString();
            this.txtWidth.Text = led.RowCharCount.ToString();
            this.txtRow.Text = led.Rows.ToString();
            this.txtGreetingID.Text = led.GreetingLedID.ToString();
            this.txtGreeting.Text = led.Greeting;
            this.txtVacantID.Text = led.VacantLedID.ToString();
            this.txtVacant.Text = led.VacantText;
        }

        private ParkFullLed GetLedFromInput()
        {
            ParkFullLed led = new ParkFullLed();
            led.COMPort = byte.Parse(txtComport.Text);
            led.Baud = int.Parse(txtBaud.Text);
            led.RowCharCount = byte.Parse(txtWidth.Text);
            led.Rows = byte.Parse(txtRow.Text);
            led.GreetingLedID = short.Parse(txtGreetingID.Text);
            led.Greeting = txtGreeting.Text;
            led.VacantLedID = short.Parse(txtVacantID.Text);
            led.VacantText = txtVacant.Text;
            return led;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                ParkFullLed led = GetLedFromInput();
                if (led.COMPort > 0)
                {
                    led.Open();
                    led.DisplayVacantInfo(15);
                    led.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
