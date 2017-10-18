using System;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();
            IDailyMemberCheckSumRunner dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
            IDataImportingRunner dataImportingRunner = new DataImportingRunner();
            UMBusiness umBusiness = new UMBusiness();

            var _scheduler = new JobScheduler();

            _scheduler.Start(dailyMemberCheckSumRunner, dataImportingRunner, umBusiness);
            Console.WriteLine("Done!!!");
        }
    }
}