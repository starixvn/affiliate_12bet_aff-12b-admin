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
                LogService.Error($"Connect db error {ex.Message}", ex.InnerException);
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
                LogService.Error("Error when innit logger", ex);
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
                LogService.Info("Start 12bet AFF service");
                _schedulerFactory.SetUpAsync();
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogService.Info("Stop 12bet AFF service");
                _schedulerFactory.ShutdownAsync();
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            this._schedulerFactory.PauseAsync();
        }

        protected override void OnContinue()
        {
            base.OnContinue();

            this._schedulerFactory.ResumeAsync();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            this._schedulerFactory.ShutdownAsync();
        }
    }
}