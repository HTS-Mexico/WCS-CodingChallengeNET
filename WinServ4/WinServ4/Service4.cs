using System;
using System.ServiceProcess;

namespace WinServ4
{
    public partial class Service4 : ServiceBase
    {
        public Service4()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TcpClient server = new TcpClient();
            Console.WriteLine("TCP client started. Press Enter to stop the server.");
        }

        protected override void OnStop()
        {
        }
    }
}
