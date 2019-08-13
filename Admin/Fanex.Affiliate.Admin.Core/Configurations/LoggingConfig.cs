using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Logging;
using Fanex.Logging.RabbitMQ;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;

namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public static class LoggingConfig
    {
        public static void RegisterSentry()
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();
            var env = appSetting.GetValue("Environment");
            var dsn = appSetting.GetValue("SentryDsn");
            var category = appSetting.GetValue("LoggingCategory");
            var rabbitMQConnectionString = appSetting.GetValue("RabbitMQConnectionString");

            if (!string.IsNullOrEmpty(dsn) && !string.IsNullOrEmpty(rabbitMQConnectionString))
            {
                LogManager.SetDefaultLogCategory(category)
                    .Use(new List<ILogEngine>
                    {
                        new RabbitMQLogging(rabbitMQConnectionString),
                        new SentryLogging(new SentryEngineOptions
                        {
                            Dsn = new Dsn(dsn),
                            Environment = env
                        })
                    });
            }
        }
    }
}
