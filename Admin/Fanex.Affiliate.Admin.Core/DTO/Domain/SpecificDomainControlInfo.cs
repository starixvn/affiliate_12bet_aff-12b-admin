using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class SpecificDomainControlInfo
    {
        public int DomainControlId { get; set; }

        public string AffName { get; set; }

        public string AffDomain { get; set; }

        public bool AffDomainSSL { get; set; }

        public string RedirectDomain { get; set; }

        public bool RedirectDomainSSL { get; set; }

        public bool IsUsed { get; set; }

        public string Creator { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Editor { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
