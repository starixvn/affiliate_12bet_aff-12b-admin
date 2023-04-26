using System;
using System.Configuration;

namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public class AppSetting : IAppSetting
    {
        public string InternalAsianAffiliateUrl => GetValue("internalAsianAffiliateURL");

        public string InternalEuropeanAffiliateUrl => GetValue("internalEuropeanAffiliateURL");

        public string Environment => GetValue("Environment");

        public bool IsProduction => Environment.Equals("PRODUCTION", StringComparison.OrdinalIgnoreCase);

        public string CcMail => GetValue("ccMail");

        public string BccMail => GetValue("bccMail");

        public string EuropeanAffSiteUrl => GetValue("EuropeanAffiliateURL");

        public string AsianAffSiteUrl => GetValue("AsianAffiliateURL");

        public string MailContact => GetValue("MailContact");

        public string JsVersion => GetValue("JsVersion");

        public bool IsDev => Environment.Equals("DEV", StringComparison.OrdinalIgnoreCase);

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];

        public string SentryDsn => GetValue("Dsn");

        public string LoggingCategory => GetValue("LoggingCategory");

        public string RabbitMQConnectionString => GetValue("RabbitMQConnectionString");

        public string BannerRootUrl => GetValue("BannerUrl");

        public string EmailBackgroundUrl => GetValue("EmailBackgroundUrl");

        public string[] BannerFolders => GetValue("BannerFolders").Split(';');

        public int ImageSize => Convert.ToInt32(GetValue("imagesize"));

        public string ServerId => GetValue("ServerID");
    }
}
