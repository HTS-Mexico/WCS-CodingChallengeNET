using System;
using System.ServiceProcess;
using System.Net.Security;

namespace WinServ1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TcpServer server = new TcpServer();
            Console.WriteLine("TCP server started. Press Enter to stop the server.");
        }

        protected override void OnStop()
        {
        }

        protected override void OnContinue()
        {
        }

        protected override void OnPause()
        {
        }

        protected override void OnShutdown()
        {
        }

        static void HandleSecureClientComm(object sslStreamObj)
        {
            SslStream sslStream = (SslStream)sslStreamObj;
        }

    }
}
