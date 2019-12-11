using System;
using System.Globalization;
using System.Threading;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Affiliate.DataService.Importer.Opus.MetaModels;
using Fanex.Data;
using Fanex.Data.SqlServer;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            DbSettingProviderManager
             .StartNewSession()
             .WithSqlServer("~/App_Data")
             .Run();
            await RunJobs();
            Console.WriteLine("Done");
            
            Console.ReadLine();
        }

        private static async Task RunJobs()
        {
            LogProvider.Logger = new ConsoleLogger();
            var schedulerFactory = new JobSchedulerFactory();
            await schedulerFactory.SetUpAsync();
            Console.WriteLine("Done!!!");
            await schedulerFactory.PauseAsync();
            await schedulerFactory.ResumeAsync();
        }


    }
}