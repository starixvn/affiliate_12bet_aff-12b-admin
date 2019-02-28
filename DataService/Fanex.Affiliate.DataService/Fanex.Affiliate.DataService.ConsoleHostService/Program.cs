using System;
using System.Globalization;
using System.Threading;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();

            RunOpusService();

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