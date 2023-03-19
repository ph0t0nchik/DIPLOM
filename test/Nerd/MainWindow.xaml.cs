using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;

namespace Nerd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            GetBattery();
            GetOSInfo();
            GetProcessorInfo();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GetTime();
           
        }

        private void GetTime()
        {
            DateTime date;
            date = DateTime.Now;
            LblClock.Text = date.ToLongDateString() + "  " + date.ToLongTimeString();
        }
        #region // Battery Void
        private void GetBattery()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Battery");
            var providers = wmi.GetInstances();

            foreach(var provider in providers)
            {
                int batteryStatus = Convert.ToInt16(provider["BatteryStatus"]);
                if (batteryStatus == 0)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Other";
                if (batteryStatus == 1)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Unknown";
                if (batteryStatus == 2)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Fully Charged";
                if (batteryStatus == 3)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Low";
                if (batteryStatus == 4)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Critical";
                if (batteryStatus == 5)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Charging";
                if (batteryStatus == 6)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Charging and High";
                if (batteryStatus == 7)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Charging and Low";
                if (batteryStatus == 8)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Charging and Critical";
                if (batteryStatus == 9)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Undefined";
                if (batteryStatus == 10)
                    LblBatteryStatus.Text = "Batteru Satus: " + " " + "Partially Charged";
            }
        }
        #endregion

        #region  // Operating System
        private void GetOSInfo()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_ComputerSystem");
            var providers = wmi.GetInstances();

            foreach(var provider in providers)
            {
                string systemSku = provider["SystemSKUNumber"].ToString();
                LblSystemSku.Text = "System Sku: " + " " + systemSku.ToString();
            }
        }
        #endregion

        #region // Processor Info
        private void GetProcessorInfo()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Processor");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                string procName = provider["Name"].ToString();
                int procSpeed = Convert.ToInt32(provider["CurrentClockSpeed"]);
                string procStatus = provider["Status"].ToString();
                Boolean powerManagementSuppoted = Convert.ToBoolean(provider["PowerManagementSupported"]);

                LblProcName.Text = "Processor Name: " + " " + procName.ToString();
                LblProcStatus.Text = "Processor Status" + " " + procStatus.ToString();
                LblProcClockSpeed.Text = "Current Clock Speed: " + " " + procSpeed.ToString() + "MHz";
                LblProcPowerMan.Text = powerManagementSuppoted.ToString();
            }
        }
        #endregion
    }
}
