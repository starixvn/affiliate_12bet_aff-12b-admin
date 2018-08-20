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

        public bool IsLocal => Environment.Equals("LOCAL", StringComparison.OrdinalIgnoreCase);

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
