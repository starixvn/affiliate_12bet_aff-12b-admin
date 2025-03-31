using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertSpecificDomainControlParam : UserInfoParam
    {
        [Required]
        public string AffName { get; set; }

        [Required]
        public string AffDomain { get; set; }

        public bool AffDomainSSL { get; set; }

        [Required]
        public string RedirectDomain { get; set; }

        public bool RedirectDomainSSL { get; set; }

        public bool IsUsed { get; set; }
    }
}
