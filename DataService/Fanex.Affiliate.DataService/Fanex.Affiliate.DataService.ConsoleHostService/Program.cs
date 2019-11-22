using System;
using System.Globalization;
using System.Threading;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Affiliate.DataService.CheckSum.Utilities;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Data;
using Fanex.Data.SqlServer;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbSettingProviderManager
             .StartNewSession()
             .WithSqlServer("~/App_Data")
             .Run();

            //RunOpusService();
            //var _ebetConfigHelper = new EbetConfigHelper();
            //var _timeTracker = new TimeTracker();
            //var now = DateTime.Now;
            //var nowByHour = new DateTime(now.Year, now.Month, now.Day, 5, 0, 0, DateTimeKind.Local);
            //var configList = _ebetConfigHelper.GetConfigList();
            //foreach (var config in configList)
            //{
            //    var currency = Convert.ToInt32(config.CurrencyID);
            //    var saveTimeTrackerParam = new InsertTimeTrackerParam
            //    {
            //        ProductId = Products.EbetProductId,
            //        CallTime = nowByHour,
            //        Currency = currency
            //    };
            //    _timeTracker.SaveTime(saveTimeTrackerParam);
            //}
            RunJobs();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void RunJobs()
        {
            var schedulerFactory = new JobSchedulerFactory();
            schedulerFactory.SetUp();
            Console.WriteLine("Done!!!");
        }

        private static void RunOpusService()
        {
            Console.WriteLine("Start");
            try
            {
                var appSetting = new AppSettings();
                var opusService = new OpusService();

                var startTime = DateTime.ParseExact(appSetting.GetValue("OpusStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                var endTime = new DateTime(2019, 1, 31, 12, 0, 0);
                for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddMinutes(15))
                {
                    var opusParam = new OpusParam
                    {
                        StartTime = start,
                        EndTime = start.AddMinutes(15)
                    };
                    opusService.Import(opusParam);

                    Console.WriteLine($"Done - Start:{opusParam.StartTime.ToString()} - End:{opusParam.EndTime.ToString()}");
                    Thread.Sleep(16000);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine(exception.Source);
            }
            Console.WriteLine("Finish");
        }
    }
}