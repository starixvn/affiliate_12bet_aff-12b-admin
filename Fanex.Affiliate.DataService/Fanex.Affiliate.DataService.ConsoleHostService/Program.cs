using System;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static readonly IAppSettings _appSetting = new AppSettings();

        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();
            var appSetting = new AppSettings();

            RunJobs();

            Console.ReadLine();
        }

        private static void RunJobs()
        {
            var schedulerFactory = new JobSchedulerFactory();
            schedulerFactory.SetUp();
            Console.WriteLine("Done!!!");
        }
    }
}