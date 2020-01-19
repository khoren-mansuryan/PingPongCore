using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Pinger : IPinger
    {
        private bool pinging;
        private readonly IEnumerable<string> addresses;
        public Pinger(IEnumerable<string> addresses, string filePath = null)
        {
            this.addresses = addresses;
            if (filePath != null)
            {
                Logger.Path = filePath;
            }
        }

        public void StartPing()
        {
            pinging = true;
            while (pinging)
            {
                foreach (string address in addresses)
                {
                    bool success;
                    string ok;

                    success = PingICMP(address);
                    ok = success ? "OK" : "FAILED";
                    Logger.FileLogger.Information($"ICMP {DateTime.Now.ToString("dd/MM/yyyy hh:m:ss")} {address} {ok}");
                    success = PingTCP(address);
                    ok = success ? "OK" : "FAILED";
                    Logger.FileLogger.Information($"TCP {DateTime.Now.ToString("dd/MM/yyyy hh:m:ss")} {address} {ok}");
                    success = PingHttp(address);
                    ok = success ? "OK" : "FAILED";
                    Logger.FileLogger.Information($"HTTP {DateTime.Now.ToString("dd/MM/yyyy hh:m:ss")} {address} {ok}");
                    Task.Delay(2000).Wait();
                }
            }
        }
        private bool PingHttp(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.facebook.com/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode == HttpStatusCode.OK;

        }
        private bool PingTCP(string address)
        {
            TcpClient c = new TcpClient(AddressFamily.InterNetwork);
            c.SendTimeout = 1;
            c.ReceiveTimeout = 1;
            try
            {
                c.Connect(address, 80);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool PingICMP(string address)
        {
            var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send("stackoverflow.com");

            return result.Status == System.Net.NetworkInformation.IPStatus.Success;
        }

        public void StopPing()
        {
            this.pinging = false;
        }
    }
}
