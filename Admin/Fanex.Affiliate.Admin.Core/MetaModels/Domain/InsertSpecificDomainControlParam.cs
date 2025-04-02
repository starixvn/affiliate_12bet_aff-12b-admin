using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertSpecificDomainControlParam : UserInfoParam
    {
        [Required(ErrorMessage = "Affiliate(s) is required!")]
        [RegularExpression("^(12betaff\\d{3,7})(,(12betaff\\d{3,7}))*$", ErrorMessage = "Affiliate(s) is an invalid format or affiliate!")]
        public string AffNames { get; set; }

        [Required]
        public string AffDomain { get; set; }

        public bool AffDomainSSL { get; set; }

        [Required]
        public string RedirectDomain { get; set; }

        public bool RedirectDomainSSL { get; set; }

        public bool IsUsed { get; set; }
    }
}
