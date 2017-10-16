using System;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();
            IDailyMemberCheckSumRunner dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
            IDataImportingRunner dataImportingRunner = new DataImportingRunner();
            var _scheduler = new JobScheduler();

            _scheduler.Start(dailyMemberCheckSumRunner, dataImportingRunner);
            Console.WriteLine("Done!!!");
        }
    }
}