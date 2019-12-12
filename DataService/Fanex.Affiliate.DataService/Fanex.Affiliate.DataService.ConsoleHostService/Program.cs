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
using System.Configuration;
using Fanex.Logging;
using System.Collections.Generic;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;

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

            InitLogging();
            RunJobs().GetAwaiter().GetResult();
            LogService.Info("Done");

            Console.ReadLine();
        }

        private static async Task RunJobs()
        {
            var schedulerFactory = new JobSchedulerFactory();
            await schedulerFactory.SetUpAsync();
            LogService.Info("StartService!!!");
            await schedulerFactory.PauseAsync();
            await schedulerFactory.ResumeAsync();
        }

        private static void InitLogging()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            var dsn = ConfigurationManager.AppSettings["SentryDsn"];
            var category = ConfigurationManager.AppSettings["LoggingCategory"];

            if (!string.IsNullOrEmpty(dsn))
            {
                LogManager.SetDefaultLogCategory(category)
                    .Use(new List<ILogEngine>
                    {
                        new SentryLogging(new SentryEngineOptions
                        {
                            Dsn = new Dsn(dsn),
                            Environment = env
                        }),
                        new ConsoleLoggerEngine()
                    });
            }
        }


    }
}