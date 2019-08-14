using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;
using Fanex.Logging.RabbitMQ;

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

            if (!string.IsNullOrEmpty(backEndDsn) && !string.IsNullOrEmpty(frontEndDsn) && !string.IsNullOrEmpty(rabbitMQConnectionString))
            {
                LogManager.SetDefaultLogCategory(category)
                    .Use(
                        isRegisterSentry ? new List <ILogEngine>
                        {
                            new RabbitMQLogging(rabbitMQConnectionString),
                            new SentryLogging(new SentryEngineOptions
                            {
                                Dsn = new Dsn(backEndDsn),
                                FrontEndDsn = new Dsn(frontEndDsn),
                                Environment = env
                            })
                        } : new List<ILogEngine>
                        {
                            new RabbitMQLogging(rabbitMQConnectionString)
                        }
                    );
            }
        }
    }
}