using System;
using System.IO;
using System.ServiceProcess;
using Fanex.Affiliate.DataService.Job;
using Fanex.Logging;
using System.Configuration;
using System.Collections.Generic;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;
using Fanex.Data;
using Fanex.Data.SqlServer;
using Fanex.Affiliate.DataService.Job.Log;
using Fanex.Affiliate.DataService.Job.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.WinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IJobSchedulerFactory _schedulerFactory;

        public Service1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            InitLogging();
            InitDbConnect();
            _schedulerFactory = new JobSchedulerFactory();

        }

        private static void InitDbConnect()
        {
            try
            {
                DbSettingProviderManager
                 .StartNewSession()
                 .WithSqlServer("~/App_Data")
                 .Run();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }

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
                        })

                    });
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.WriteInfo("Start 12bet AFF service");
                _schedulerFactory.SetUpAsync();
            }
            catch (Exception exception)
            {
                LogHelper.WriteError(exception);
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogHelper.WriteInfo("Stop 12bet AFF service");
                _schedulerFactory.ShutdownAsync();
            }
            catch (Exception exception)
            {
                LogHelper.WriteError(exception);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            _schedulerFactory.PauseAsync();
        }

        protected override void OnContinue()
        {
            base.OnContinue();

            _schedulerFactory.ResumeAsync();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            _schedulerFactory.ShutdownAsync();
        }
    }
}