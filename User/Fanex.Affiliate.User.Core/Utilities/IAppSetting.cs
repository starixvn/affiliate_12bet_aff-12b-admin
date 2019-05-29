using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        string MediaScriptUrl { get; }

        string MailContact { get; }

        string MailCc { get; }

        string MailBcc { get; }

        string RsaPublicKey { get; }

        string RsaPrivateKey { get; }

        int LoginTimeout { get; }

        DateTime StatementAvailableFrom { get; }

        string SymmetricPassword { get; }

        string SymmetricSalt { get; }
    }
}
