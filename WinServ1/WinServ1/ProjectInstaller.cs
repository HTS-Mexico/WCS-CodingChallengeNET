using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
    private ServiceInstaller serviceInstaller;
    private ServiceProcessInstaller processInstaller;

    public ProjectInstaller()
    {
        processInstaller = new ServiceProcessInstaller();
        serviceInstaller = new ServiceInstaller();

        processInstaller.Account = ServiceAccount.LocalSystem;

        serviceInstaller.StartType = ServiceStartMode.Automatic;
        serviceInstaller.ServiceName = "WinServ1";

        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }
}
