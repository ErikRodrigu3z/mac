using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace mac
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine(GetPublicIP());




            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            IPAddress hostIPAddress1 = (Dns.Resolve("google.com")).AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(hostIPAddress1, 80);
            Console.WriteLine("Endpoint.Address : " + endpoint.Address);
            Console.WriteLine("Endpoint.AddressFamily : " + endpoint.AddressFamily);
            Console.WriteLine("Endpoint.Port : " + endpoint.Port);
            Console.WriteLine("Endpoint.ToString() : " + endpoint.ToString());

            Console.WriteLine("Tú IP Local Es: " + localIP);
            Console.WriteLine(Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily.ToString().ToUpper().Equals("INTERNETWORK")).FirstOrDefault().ToString());
            Console.WriteLine(GetMACAddress2());
            Console.ReadLine();

        }

        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
        public static string GetMACAddress2() 
        {

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetIPProperties().ToString();
                }
            }
            return sMacAddress;
        }

        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }
    }
}
