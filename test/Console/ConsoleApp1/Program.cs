using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using Microsoft.Management.Infrastructure;
using System.Management;
using Microsoft.Management;
using Microsoft.Management.Infrastructure.Options;
using System.Security;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Username = "test";
            options.Password = "1234";
            ObjectGetOptions opt = new ObjectGetOptions();


            //ManagementScope scope = new ManagementScope("\\\\localhost\\root\\CIMV2");
            ManagementScope scope = new ManagementScope("\\\\IDEAPAD330S\\root\\CIMV2", options);
            scope.Connect();

            // ВЫВОД КАКОЙ-ЛИБО ХАРАКТЕРИСТИКИ ОБ (УДАЛЕННОМ) УСТРОЙСТВЕ

            /*ManagementClass objClass = null;
            int PID = 0;
            try
            {
                objClass = new ManagementClass(scope, new ManagementPath("\\\\localhost\\root\\CIMV2:Win32_Process"),opt);
                ManagementBaseObject inParams = objClass.GetMethodParameters("Create");
                inParams["CommandLine"] = "calc.exe";
                ManagementBaseObject outParams = objClass.InvokeMethod("Create", inParams, null);
                PID = Convert.ToInt32(outParams["processId"]);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}: {1}", e.HResult, e.Message);
            }
            finally
            {
                if (objClass != null)
                {
                    objClass.Dispose();
                }
            }
            Console.WriteLine("PID: {0}", PID);*/

            // ЗАКРЫТИЕ ПРОЦЕССА НА (УДАЛЕННОМ) УСТРОЙСТВЕ
            /*
            try
            {
                
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name='chrome.exe'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                foreach (ManagementObject obj in searcher.Get())
                {
                    obj.InvokeMethod("Terminate", null);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("{0}: {1}", e.HResult, e.Message);
            }
            */

            // ПОЛУЧЕНИЯ СПИСКА ВСЕХ УСТРОЙСТВ В ЛОКАЛЬНОЙ СЕТИ
            
            
            Console.ReadLine();
        }

       

        public class WimInfo
        {
            public string Name;
            //....


        }
    }
}
