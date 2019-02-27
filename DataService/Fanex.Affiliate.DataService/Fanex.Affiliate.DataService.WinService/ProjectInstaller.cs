using System.ComponentModel;

namespace Fanex.Affiliate.DataService.WinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceInstaller1.ServiceName = Constants.ServiceName;
            serviceInstaller1.DisplayName = Constants.ServiceName;
            serviceInstaller1.Description = Constants.ServiceDescription;
        }
    }
}