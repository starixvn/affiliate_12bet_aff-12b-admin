using Fanex.Data;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class DomainInfo
    {
        public int DomainId { get; set; }

        public string DomainName { get; set; }

        public bool IsSSL { get; set; }

        public string Creator { get; set; }

        [SpParam(SqlParamName = "BlockTargetCountries")]
        public string ExcludedTargetCountries { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
