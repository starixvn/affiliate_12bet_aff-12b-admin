using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Logging;
using Fanex.Logging.RabbitMQ;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;

namespace Fanex.Affiliate.User.Site.App_Start
{
    public static class LoggingConfig
    {
        public static void RegisterSentry()
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();
            var env = appSetting.GetValue("Environment");
            var backEndDsn = appSetting.GetValue("SentryBackEndDsn");
            var frontEndDsn = appSetting.GetValue("SentryFrontEndDsn");
            var category = appSetting.GetValue("LoggingCategory");
            var rabbitMQConnectionString = appSetting.GetValue("RabbitMQConnectionString");
            var isRegisterSentry = string.Equals(appSetting.GetValue("IsRegisterSentry"), "true", System.StringComparison.CurrentCultureIgnoreCase);

            var logEngineList = new List<ILogEngine>();
            if (!string.IsNullOrEmpty(rabbitMQConnectionString))
            {
                logEngineList.Add(new RabbitMQLogging(rabbitMQConnectionString));
            }

            if (isRegisterSentry)
            {
                logEngineList.Add(
                    new SentryLogging(new SentryEngineOptions
                    {
                        Dsn = new Dsn(backEndDsn),
                        FrontEndDsn = new Dsn(frontEndDsn),
                        Environment = env
                    }));
            }

            LogManager.SetDefaultLogCategory(category).Use(logEngineList);
        }
    }
}