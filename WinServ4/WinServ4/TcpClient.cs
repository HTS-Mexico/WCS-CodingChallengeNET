using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WinServ4
{
    public class TcpClient
    {
        private System.Net.Sockets.TcpClient client;

        public TcpClient()
        {
            try
            {
                string serverIp = GetIP();
                int serverPort = GetPort();

                this.client = new System.Net.Sockets.TcpClient(serverIp, serverPort);

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
                receiveThread.Start();

            }
            catch (SocketException ex)
            {
                LogEvent(ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            NetworkStream clientStream = this.client.GetStream();
            byte[] messageBytes = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(messageBytes, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string receivedMessage = Encoding.ASCII.GetString(messageBytes, 0, bytesRead);
                WriteMessage(receivedMessage + "-" + GetServiceMessage());
            }

            this.client.Close();
        }


        private static void LogEvent(string message)
        {
            WriteMessage(message);
        }

        static int GetPort()
        {
            string filePath = "{path}";

            FileReader iniReader = new FileReader(filePath);

            string section = "WinServ1";
            string key = "port";

            var port = int.Parse(iniReader.GetValue(section, key));

            return port;
        }

        static string GetIP()
        {
            string filePath = "{path}";

            FileReader iniReader = new FileReader(filePath);

            string section = "WinServ1";
            string key = "IP";

            var ip = iniReader.GetValue(section, key);

            return ip;
        }

        static string GetServiceMessage()
        {
            string filePath = "{path}";

            FileReader iniReader = new FileReader(filePath);

            string section = "WinServ4";
            string key = "message";

            var message = iniReader.GetValue(section, key);

            return message;
        }

        static void WriteMessage(string message)
        {
            string filePath = "{path}";

            if (File.ReadLines(filePath).Count() > 50)
            {
                File.WriteAllText(filePath, string.Empty);
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(message);
            }
        }


    }
}
