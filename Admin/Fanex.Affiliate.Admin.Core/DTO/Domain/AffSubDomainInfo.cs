using System.Linq;

namespace Fanex.Affiliate.Admin.Core.DTO.Domain
{
    public class AffSubDomainInfo
    {
        public string AffDomain { get; set; }

        public string AffSubDomain => AffDomain?.ToLowerInvariant().Split('.').FirstOrDefault();
    }
}
