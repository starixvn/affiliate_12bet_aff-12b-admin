namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public interface IAppSetting
    {
        string GetValue(string key);

        string InternalEuropeanAffiliateUrl { get; }

        string InternalAsianAffiliateUrl { get; }
    }
}
