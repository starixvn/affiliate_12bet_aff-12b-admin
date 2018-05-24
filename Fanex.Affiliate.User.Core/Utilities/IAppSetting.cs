using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IAppSetting
    {
        string GetValue(string key);

        string Environment { get; }

        bool IsLocal { get; }

        bool IsProduction { get; }

        IEnumerable<string> AllowedAffiliateTypes { get; }

        int ServerId { get; }

        string JsVersion { get; }

        string Promotion12BetLink { get; }

        bool IsEuroSite { get; }

        string TextLinkTemplate { get; }

        string BannerDomain { get; }

        string MediaScriptUrl { get; }
    }
}
