using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Input;

namespace WpfTestApp
{
    public class ComputerInfo
    {
        public string LastBootUpTime { get; set; }
        public string LocalDateTime { get; set; }
        public string UpTime { get; set; }
    }
    
    public class MainWindowViewModel
    {
        public ComputerInfo ComputerInfo { get; set; }
        public ICommand RefreshCommand { get; set; }

        public MainWindowViewModel()
        {
            ComputerInfo = new ComputerInfo();
           
            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
                scope.Connect();

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    ComputerInfo.LastBootUpTime = ManagementDateTimeConverter.ToDateTime(queryObj["LastBootUpTime"].ToString()).ToString();
                    ComputerInfo.LocalDateTime = ManagementDateTimeConverter.ToDateTime(queryObj["LocalDateTime"].ToString()).ToString();

                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            RefreshCommand = new RefreshCommand(
                execute: () =>
                {
                   
                        ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
                        scope.Connect();

                        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

                        ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            ComputerInfo.LastBootUpTime = ManagementDateTimeConverter.ToDateTime(queryObj["LastBootUpTime"].ToString()).ToString();
                            ComputerInfo.LocalDateTime = ManagementDateTimeConverter.ToDateTime(queryObj["LocalDateTime"].ToString()).ToString();
                        }
                },
                canExecute: () =>
                {
                    return true; // Пока всегда разрешаем выполнение команды RefreshCommand
                });
        }
    }
}
