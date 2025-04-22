using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class DomainInfo
    {
        public int DomainId { get; set; }

        public string DomainName { get; set; }

        public bool IsSSL { get; set; }

        public string Creator { get; set; }

        public string BlockTargetCountry { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
