using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public class AppSetting : IAppSetting
    {
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string InternalAsianAffiliateUrl => GetValue("internalAsianAffiliateURL");

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string InternalEuropeanAffiliateUrl => GetValue("internalEuropeanAffiliateURL");

        public string Environment => GetValue("Environment");

        public bool IsProduction => Environment.Equals("PRODUCTION", StringComparison.OrdinalIgnoreCase);

        public string CcMail => GetValue("ccMail");

        public string BccMail => GetValue("bccMail");

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string EuropeanAffSiteUrl => GetValue("EuropeanAffiliateURL");

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string AsianAffSiteUrl => GetValue("AsianAffiliateURL");

        public string MailContact => GetValue("MailContact");

        public string JsVersion => GetValue("JsVersion");

        public bool IsLocal => Environment.Equals("LOCAL", StringComparison.OrdinalIgnoreCase);

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];

        public string SentryDsn => GetValue("Dsn");

        public string LoggingCategory => GetValue("LoggingCategory");

        public string RabbitMQConnectionString => GetValue("RabbitMQConnectionString");

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string BannerRootUrl => GetValue("BannerUrl");

        public string[] BannerFolders => GetValue("BannerFolders").Split(';');

        public int ImageSize => Convert.ToInt32(GetValue("imagesize"));

        public string ServerId => GetValue("ServerID");
    }
}
