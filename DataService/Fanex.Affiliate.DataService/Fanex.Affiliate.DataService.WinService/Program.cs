using Fanex.Data;
using Fanex.Data.SqlServer;

namespace Fanex.Affiliate.DataService.WinService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/App_Data")
                .Run();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}