using System;
using System.IO;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.MetaModels.Quartz;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;
using Newtonsoft.Json.Linq;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static readonly IAppSettings _appSetting = new AppSettings();

        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();
            var appSetting = new AppSettings();

            //RunJobs();
            //var fakeData = "<historyinfo pageNum=\"1\" pageSize=\"50\" totalPage=\"49\" currentRows=\"50\" totalRows=\"2429\"><history operationCode=\"2461279486\" userId=\"rocbenz123\" changeTime=\"06 - 11 - 2018 14:20:01\" changeType=\"Result\" gameName=\"God Of Fortune\" fround=\"0\" bet=\"4.00000000\" ret=\"0.00000000\" changes=\"-4.00\" endBalance=\"0.00\" operator=\"PSI\" jcon=\"0\" jwin=\"0.0\" platform=\"3\" gametype=\"0\" ver=\"1\"/></historyinfo>";
            //var error = "<error>Request too often, please try again in 15 seconds</error>";
            //var xDocument = XDocument.Parse(fakeData);
            //Console.Write(xDocument.Root);

            var dataSimulator = new DataSimulator();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"App_Data\\QuartzData\\QuartzData.json");

            var jsonData = dataSimulator.GetLocalData(filePath).Result;
            var jsonObject = JObject.Parse(jsonData);
            var quartzSummary = QuartzParser.FromJsonObject(jsonObject);
            var param = new QuartzParam
            {
                RequestTime = DateTime.Now,
                PageNumber = 1
            };

            var quartzService = new QuartzService();
            var xml = quartzService.ConvertToXml(quartzSummary, param);

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