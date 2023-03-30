using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var computers = new List<string>();

            var network = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet);

            if (network == null)
            {
                Console.WriteLine("No network interface found.");
                return;
            }

            var ipProperties = network.GetIPProperties();

            if (ipProperties == null)
            {
                Console.WriteLine("No IP properties found.");
                return;
            }

            var subnet = ipProperties.UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                ?.IPv4Mask;

            if (subnet == null)
            {
                Console.WriteLine("No subnet mask found.");
                return;
            }

            var ipAddress = ipProperties.UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                ?.Address;

            if (ipAddress == null)
            {
                Console.WriteLine("No IP address found.");
                return;
            }

            var networkAddress = ipAddress.GetNetworkAddress(subnet);

            if (networkAddress == null)
            {
                Console.WriteLine("No network address found.");
                return;
            }

            for (var i = 1; i <= 254; i++)
            {
                var ip = new IPAddress(networkAddress.GetAddressBytes()
                    .Select((b, j) => j < 3 ? b : (byte)i).ToArray());

                var ping = new Ping();
                var reply = ping.Send(ip);

                if (reply.Status == IPStatus.Success)
                {
                    computers.Add(ip.ToString());
                }
            }

            foreach (var computer in computers)
            {
                Console.WriteLine(computer);
            }
        }
    }
}
