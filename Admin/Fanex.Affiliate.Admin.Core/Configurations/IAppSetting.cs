
namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public interface IAppSetting
    {
        string GetValue(string key);

        string InternalEuropeanAffiliateUrl { get; }

        string InternalAsianAffiliateUrl { get; }

        string EuropeanAffSiteUrl { get; }

        string AsianAffSiteUrl { get; }

        string Environment { get; }

        bool IsProduction { get; }

        bool IsDev { get; }

        string CcMail { get; }

        string BccMail { get; }

        string MailContact { get; }

        string JsVersion { get; }

        string SentryDsn { get; }

        string LoggingCategory { get; }

        string RabbitMQConnectionString { get; }

        string BannerRootUrl { get; }

        string[] BannerFolders { get; }

        int ImageSize { get; }

        string ServerId { get; }
    }
}
