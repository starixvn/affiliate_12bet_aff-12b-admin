using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class AppSetting : IAppSetting
    {
        public string Environment => GetValue("Environment");

        public bool IsDev => Environment.MatchWith("Dev");

        public bool IsLocal => Environment.MatchWith("Local");

        public bool IsProduction => Environment.MatchWith("Production");

        public int ServerId => Convert.ToInt32(GetValue("ServerId"));

        public IEnumerable<string> AllowedAffiliateTypes => GetValue("AllowedAffType").Split(',');

        public string JsVersion => GetValue("JsVersion");

        public string Promotion12BetLink => GetValue("Promotion12BetLink");

        public bool IsEuroSite => AllowedAffiliateTypes.Any(t => t == AffiliateType.European.ToString("D"));

        public string TextLinkTemplate => GetValue("TextLinkTemplate");

        public string BannerDomain => GetValue("BannerDomain");

        public string EmailBackgroundUrl => GetValue("EmailBackgroundUrl");

        public string MediaScriptUrl => GetValue("MediaScriptUrl");

        public string MailContact => GetValue("MailContact");

        public string MailCc => GetValue("MailCc");

        public string MailBcc => GetValue("MailBcc");

        public DateTime StatementAvailableFrom => Convert.ToDateTime(GetValue("StatementAvailableFrom"));

        public string RsaPublicKey => GetValue("RsaPublicKey");

        public string RsaPrivateKey => GetValue("RsaPrivateKey");

        public int LoginTimeout => Convert.ToInt32(GetValue("LoginTimeout"));

        public int ValidIdleTime => Convert.ToInt32(GetValue("ValidIdleTime"));

        public string SymmetricPassword => GetValue("SymmetricPassword");

        public string SymmetricSalt => GetValue("SymmetricSalt");

        public string QrCodeIconPathTemplate => GetValue("QrCodeIconPathTemplate");

        public string ResourceDomain => GetValue("ResourceDomain");

        public string CcMail => GetValue("CcMail");

        public string BccMail => GetValue("BccMail");

        public string AppMailAddress => GetValue("app_mail_address");

        public string AppMailId => GetValue("app_mail_id");

        public IEnumerable<string> MarketingForCommGroups => GetValue("MarketingForCommGroups").Split(',');

        public IEnumerable<string> MarketingLandingPages => GetValue("MarketingLandingPages").Split(',');

        public string LicenseeCustDbApiUrl => GetValue("LicenseeCustDbApiUrl");

        public bool IsUseProxyToGetLicenseeCustDbApi => Convert.ToBoolean(GetValue("IsUseProxyToGetLicenseeCustDbApi"));

        public string LicenseeCustDbApiProxyUrl => GetValue("LicenseeCustDbApiProxyUrl");

        public string LicenseeCustDbApiAuthenKey => GetValue("LicenseeCustDbApiAuthenKey");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}