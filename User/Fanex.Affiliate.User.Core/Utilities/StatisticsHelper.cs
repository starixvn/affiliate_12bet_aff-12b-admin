using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class StatisticsHelper
    {
        public static IEnumerable<Domain> GetCustomAvailableDomainList(IEnumerable<MappingDomainInfo> availDomains, int affId)
        {
            if (availDomains?.Any() != true)
            {
                return Enumerable.Empty<Domain>();
            }

            var customDomains = availDomains.Select((domain, index) => MapToDomain(domain, affId + index));

            return customDomains;
        }

        private static Domain MapToDomain(MappingDomainInfo domain, int customId)
        {
            var domainDescription = domain.IsMobile
                ? $"{domain.DomainName} ({Lang.MobileDevice})"
                : domain.DomainName;

            return new Domain
            {
                DomainId = domain.DomainId == 0 ? customId : domain.DomainId,
                DomainName = domain.DomainName,
                DomainDescription = domainDescription,
                IsSsl = domain.IsSsl,
                IsMobile = domain.IsMobile,
                CreateDate = domain.CreateDate
            };
        }
    }
}
