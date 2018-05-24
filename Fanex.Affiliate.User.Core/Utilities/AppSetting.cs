using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Fanex.Affiliate.User.Core.Enum;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class AppSetting : IAppSetting
    {
        public string Environment => GetValue("Environment");

        public bool IsLocal => Environment.Equals("Local", StringComparison.OrdinalIgnoreCase);

        public bool IsProduction => Environment.Equals("Production", StringComparison.OrdinalIgnoreCase);

        public int ServerId => Convert.ToInt32(GetValue("ServerId"));

        public IEnumerable<string> AllowedAffiliateTypes => GetValue("AllowedAffType").Split(',');

        public string JsVersion => GetValue("JsVersion");

        public string Promotion12BetLink => GetValue("Promotion12BetLink");

        public bool IsEuroSite => AllowedAffiliateTypes.Any(t => Convert.ToByte(t) == (byte)AffiliateType.European);

        public string TextLinkTemplate => GetValue("TextLinkTemplate");

        public string BannerDomain => GetValue("BannerDomain");

        public string MediaScriptUrl => GetValue("MediaScriptUrl");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}