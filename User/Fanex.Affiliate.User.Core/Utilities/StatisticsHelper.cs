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
        public static IEnumerable<LandingPage> GetFilteredLandingPageList(IAppSetting appSetting, IEnumerable<LandingPage> landingPages, bool isSelectedMobileDomain, bool isUsingInStatistics)
        {
            if (isUsingInStatistics)
            {
                return landingPages;
            }    

            if (isSelectedMobileDomain)
            {
                return landingPages.Where(l => appSetting.MarketingPageCodeRegexPatterns.Any(m => RegexHelper.IsMatch(l.PageCode, $"^{m}$", RegexOptions.IgnoreCase)));
            }

            return landingPages.Where(l => !appSetting.MarketingPageCodeRegexPatterns.Any(m => RegexHelper.IsMatch(l.PageCode, $"^{m}$", RegexOptions.IgnoreCase)));
        }

        public static IEnumerable<Domain> GetCustomAvailableDomainList(IAppSetting appSetting, IEnumerable<MappingDomainInfo> availDomains, int affId, string commGroup)
        {
            if (availDomains?.Any() != true)
            {
                return Enumerable.Empty<Domain>();
            }

            var customDomains = availDomains.Select((domain, index) => MapToDomain(domain, appSetting, affId + index));

            if (!IsAffOfNoCommissionGroup(appSetting, commGroup))
            {
                return customDomains.Where(d => !d.IsUsingForMobile);
            }

            return customDomains;
        }

        public static bool IsAffOfNoCommissionCPAGroup(IAppSetting appSetting, string commGroupName)
        {
            return appSetting.MarketingForCPACommGroups.Any(c => c.MatchWith(commGroupName));
        }

        private static Domain MapToDomain(MappingDomainInfo domain, IAppSetting appSetting, int customId)
        {
            var isDomainUsingForMobile = IsDomainUsingForMobile(domain, appSetting);

            var domainDescription = isDomainUsingForMobile
                ? $"{domain.DomainName} ({Lang.MobileDevice})"
                : domain.DomainName;

            return new Domain
            {
                DomainId = domain.DomainId == 0 ? customId : domain.DomainId,
                DomainName = domain.DomainName,
                DomainDescription = domainDescription,
                IsSsl = domain.IsSsl,
                IsUsingForMobile = isDomainUsingForMobile,
                CreateDate = domain.CreateDate
            };
        }

        private static bool IsDomainUsingForMobile(MappingDomainInfo domain, IAppSetting appSetting)
        {
            return appSetting.MarketingAvailDomainMobilePatterns
                .Any(pattern => RegexHelper.IsMatch(domain.RedirectDomain ?? string.Empty, $"^{pattern}$", RegexOptions.IgnoreCase));
        }

        private static bool IsAffOfNoCommissionGroup(IAppSetting appSetting, string commGroupName)
        {
            return appSetting.MarketingForCommGroups.Any(c => c.MatchWith(commGroupName));
        }
    }
}
