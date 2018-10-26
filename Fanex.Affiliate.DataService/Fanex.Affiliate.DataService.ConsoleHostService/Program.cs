using System;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static readonly IAppSettings _appSetting = new AppSettings();

        private static void Main(string[] args)
        {
            RunBetsoftService();
            Console.ReadLine();
        }

        private static void RunBetsoftService()
        {
            var betsoftService = new BetsoftService();
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
            var param = new BetsoftParam
            {
                ApiUrl = _appSetting.BetsoftApiUrl,
                BankId = _appSetting.BetsoftBankId,
                Password = _appSetting.BetsoftPassword,
                StartDate = start,
                EndDate = now
            };

            var xmlDataList = betsoftService.GetMultipleRawDataBatch(param);

            Console.WriteLine($"Item Count: {xmlDataList.Count}");
            //foreach (var item in xmlDataList)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine("Done!!!");
        }

        private static void RunEbetService()
        {
            var appSetting = new AppSettings();
            var config = new EbetConfigHelper();
            var configList = config.GetConfigList();

            var ebetService = new EbetService();
            var now = DateTime.Now;
            var param = new EbetParam
            {
                StartTime = new DateTime(2018, 9, 10, 10, 10, 10).ToString(appSetting.EbetFormatDateTime),
                EndTime = new DateTime(2018, 10, 10, 10, 10, 10).ToString(appSetting.EbetFormatDateTime)
            };

            var jsonData = ebetService.GetJsonData(configList[0], param);

            Console.WriteLine(jsonData);
            Console.WriteLine("Done!!!");
        }

        private static void RunCasinoService()
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