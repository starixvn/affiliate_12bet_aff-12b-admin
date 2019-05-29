using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
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

        public bool IsEuroSite => AllowedAffiliateTypes.Any(t => t == AffiliateType.European.ToString("D"));

        public string TextLinkTemplate => GetValue("TextLinkTemplate");

        public string BannerDomain => GetValue("BannerDomain");

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string MediaScriptUrl => GetValue("MediaScriptUrl");

        public string MailContact => GetValue("MailContact");

        public string MailCc => GetValue("MailCc");

        public string MailBcc => GetValue("MailBcc");

        public DateTime StatementAvailableFrom => Convert.ToDateTime(GetValue("StatementAvailableFrom"));

        public string RsaPublicKey => GetValue("RsaPublicKey");

        public string RsaPrivateKey => GetValue("RsaPrivateKey");

        public int LoginTimeout => Convert.ToInt32(GetValue("LoginTimeout"));

        public string SymmetricPassword => GetValue("SymmetricPassword");

        public string SymmetricSalt => GetValue("SymmetricSalt");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}