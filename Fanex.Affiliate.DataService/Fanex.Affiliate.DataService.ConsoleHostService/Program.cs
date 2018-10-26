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
            RunEbetService();
            Console.ReadLine();
        }

        private static void RunBetsoftService()
        {
            var betsoftService = new BetsoftService();
            var dateTime = new DateTime(2018, 9, 30, 22, 0, 0);
            var param = new BetsoftParam
            {
                ApiUrl = _appSetting.BetsoftApiUrl,
                BankId = _appSetting.BetsoftBankId,
                Password = _appSetting.BetsoftPassword,
                StartDate = dateTime.AddMinutes(-60).ToString(_appSetting.BetsoftFormatDateTime),
                EndDate = dateTime.ToString(_appSetting.BetsoftFormatDateTime)
            };

            var dataString = betsoftService.GetData(param);
            Console.WriteLine(DateTime.Now);
            var dataList = betsoftService.ConvertToDataList(dataString);
            betsoftService.Import(dataList);
            Console.WriteLine(DateTime.Now);

            //foreach (var item in dataList)
            //{
            //    Console.WriteLine($"UserName: {item.UserName}, Amount: {item.Amount}, Win: {item.Win}, WinLossDate: {item.WinLossDate}");
            //}

            Console.WriteLine(dataString);
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