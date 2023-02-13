using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class DomainControlInfo
    {
        public int DomainControlId { get; set; }

        public int AffDomainId { get; set; }

        public string AffDomain { get; set; }

        public int RedirectDomainId { get; set; }

        public string RedirectDomain { get; set; }

        public string MarketId { get; set; }

        public string MarketName { get; set; }

        public bool Status { get; set; }

        public string Creator { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
