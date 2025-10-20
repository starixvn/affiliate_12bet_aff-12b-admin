using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class SpecificDomainControlInfo
    {
        public int SpecificDomainControlId { get; set; }

        public string AffUsername { get; set; }

        public string AffDomain { get; set; }

        public bool AffDomainIsSSL { get; set; }

        public string RedirectDomain { get; set; }

        public bool RedirectDomainIsSSL { get; set; }

        public bool Status { get; set; }

        public string Creator { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Editor { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsMobile { get; set; }
    }
}
