using System;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data;
using Fanex.Data.SqlServer;
using System.Threading.Tasks;
using System.Configuration;
using Fanex.Logging;
using System.Collections.Generic;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;
using Fanex.Affiliate.DataService.Job.Log;
using Fanex.Affiliate.DataService.Job.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal static class Program
    {
        private static void Main(string[] args)
        {

            DbSettingProviderManager
             .StartNewSession()
             .WithSqlServer("~/App_Data")
             .Run();

            InitLogging();
            RunJobs().GetAwaiter().GetResult();
            LogHelper.WriteInfo("Done");

            Console.ReadLine();
        }

        private static async Task RunJobs()
        {
            var schedulerFactory = new JobSchedulerFactory();
            await schedulerFactory.SetUpAsync();
            LogHelper.WriteInfo("StartService!!!");
            await schedulerFactory.PauseAsync();
            await schedulerFactory.ResumeAsync();
        }

        private static void InitLogging()
        {
            try
            {
                SetupFanexLogging();
                SetupJobLogging();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }

        }

        private static void SetupFanexLogging()
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


        private static void SetupJobLogging()
        {
            var defaultLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), ConfigurationManager.AppSettings["LogLevel"]);
            var trackers = ConfigurationManager.AppSettings["LogTrackerProduct"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var logManager = LogWrapperManager.StartNewLogTracker();
            logManager.DefaultLogLevel = defaultLogLevel;
            foreach (var tracker in trackers)
            {
                if (!string.IsNullOrWhiteSpace(tracker))
                {
                    var category = ServiceMapping.ServiceFullName[tracker];
                    logManager.AddLogConfig(category, LogLevel.Debug);
                }
            }
        }
    }
}