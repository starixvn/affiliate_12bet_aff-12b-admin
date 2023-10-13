using System;
using System.Text.Json.Serialization;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class DomainInfo
    {
        public int DomainId { get; set; }

        public string DomainName { get; set; }

        public bool IsSSL { get; set; }

        public string Creator { get; set; }

        public string BlockTargetCountries { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
