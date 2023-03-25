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
            ManagementScope scope = new ManagementScope("\\\\IDEAPAD330S\\root\\CIMV2", options);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject manObj in searcher.Get())
            {
                Console.WriteLine(manObj["Name"].ToString());
                Console.WriteLine(Convert.ToInt32(manObj["VoltageCaps"]));
            }


            Console.ReadLine();
        }
    }
}
