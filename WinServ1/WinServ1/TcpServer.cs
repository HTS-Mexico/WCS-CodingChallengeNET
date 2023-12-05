using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace WinServ1
{
    public class TcpServer
    {
        private TcpListener tcpListener;
        private Thread listenerThread;

        public TcpServer()
        {
            try
            {
                int port = GetPort();
                this.tcpListener = new TcpListener(IPAddress.Parse(GetIP()), port);
                this.listenerThread = new Thread(new ThreadStart(ListenForClients));
                this.listenerThread.Start();
            }
            catch (SocketException ex)
            {
                LogEvent(ex.Message);
            }
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                TcpClient client = this.tcpListener.AcceptTcpClient();

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();

            while (true)
            {
                string message = "WinServ1";
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                clientStream.Write(messageBytes, 0, messageBytes.Length);

                Thread.Sleep(1000);
            }
        }

        public void StopServer()
        {
            this.tcpListener.Stop();
        }

        static void LogEvent(string message)
        {
            string sourceName = "ServWin1"; 
            string logName = "Application"; 

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, logName);
            }

            // Create a new event log entry
            using (EventLog eventLog = new EventLog(logName))
            {
                eventLog.Source = sourceName;

                // Add an entry to the event log
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        static int GetPort()
        {
            string filePath = "{path}";

            FileReader iniReader = new FileReader(filePath);

            string section = "WinServ1";
            string key = "port";

            var port =  int.Parse(iniReader.GetValue(section, key));

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
    }
}
