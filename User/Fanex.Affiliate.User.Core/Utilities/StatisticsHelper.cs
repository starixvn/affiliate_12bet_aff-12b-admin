using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class StatisticsHelper
    {
        public static IEnumerable<LandingPage> GetFilteredLandingPageList(IAppSetting appSetting, IEnumerable<LandingPage> landingPages, string commGroupName)
        {
            if (appSetting.MarketingForCommGroups.Any(c => c.MatchWith(commGroupName)))
            {
                return landingPages;
            }

            return landingPages.Where(l => !appSetting.MarketingPageCodeRegexPatterns.Any(m => RegexHelper.IsMatch(l.PageCode, $"^{m}$", RegexOptions.IgnoreCase)));
        }

        public static IEnumerable<Domain> GetCustomAvailableDomainList(IAppSetting appSetting, IEnumerable<MappingDomainInfo> availDomains, int affId)
        {
            if (availDomains?.Any() != true)
            {
                return Enumerable.Empty<Domain>();
            }

            return availDomains.Select((domain, index) => MapToDomain(domain, appSetting, affId + index));
        }

        private static Domain MapToDomain(MappingDomainInfo domain, IAppSetting appSetting, int customId)
        {
            var isMobileDevice = appSetting.MarketingAvailDomainMobilePatterns
                .Any(pattern => RegexHelper.IsMatch(domain.RedirectDomain ?? string.Empty, $"^{pattern}$", RegexOptions.IgnoreCase));

            var domainDescription = isMobileDevice
                ? $"{domain.DomainName} ({Lang.MobileDevice})"
                : domain.DomainName;

            return new Domain
            {
                DomainId = domain.DomainId == 0 ? customId : domain.DomainId,
                DomainName = domain.DomainName,
                DomainDescription = domainDescription,
                IsSsl = domain.IsSsl,
                CreateDate = domain.CreateDate
            };
        }
    }
}
