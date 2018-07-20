using System.Configuration;

namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public class AppSetting : IAppSetting
    {
        public string InternalAsianAffiliateUrl => GetValue("internalAsianAffiliateURL");

        public string InternalEuropeanAffiliateUrl => GetValue("internalEuropeanAffiliateURL");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
