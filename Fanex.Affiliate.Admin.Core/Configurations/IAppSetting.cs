namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public interface IAppSetting
    {
        string GetValue(string key);

        string InternalEuropeanAffiliateUrl { get; }

        string InternalAsianAffiliateUrl { get; }

        string EuropeanAffSiteUrl { get; }

        string AsianAffSiteUrl { get; }

        bool IsTesting { get; }

        string Environment { get; }

        bool IsProduction { get; }

        string CcMail { get; }

        string BccMail { get; }

        string MailContact { get; }

        string JsVersion { get; }
    }
}
