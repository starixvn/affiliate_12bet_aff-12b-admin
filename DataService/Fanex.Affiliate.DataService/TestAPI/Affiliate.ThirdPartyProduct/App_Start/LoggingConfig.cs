using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;

using SharpRaven;

using System.Collections.Generic;
using System.Configuration;

namespace Fanex.Affiliate.ThirdPartyProduct.Site
{
    public static class LoggingConfig
    {
        public static void RegisterSentry()
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
    }
}