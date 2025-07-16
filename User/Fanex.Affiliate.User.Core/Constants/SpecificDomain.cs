using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SpecificDomain
    {
        public static readonly IReadOnlyList<Domain> SupportedDomainsForAffNoCommissionCPA = new List<Domain> {
            new Domain
            {
                DomainId = 1,
                DomainName = "goto.5689fun.com",
                DomainDescription = "goto.5689fun.com",
                IsSsl = true
            },
            new Domain
            {
                DomainId = 2,
                DomainName = "goto.168sure.com",
                DomainDescription = "goto.168sure.com",
                IsSsl = true
            },
            new Domain
            {
                DomainId = 3,
                DomainName = "goto.oli368.com",
                DomainDescription = "goto.oli368.com",
                IsSsl = true
            },
        };
    }
}
