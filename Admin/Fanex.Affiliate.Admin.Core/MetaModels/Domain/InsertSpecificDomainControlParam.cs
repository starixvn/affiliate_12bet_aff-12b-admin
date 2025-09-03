using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertSpecificDomainControlParam : UserInfoParam
    {
        [Required(ErrorMessage = "Affiliate(s) is required!")]
        [RegularExpression("(?i)^(12betaff\\d{3,7})(,(12betaff\\d{3,7}))*$", ErrorMessage = "Affiliate(s) is an invalid format or affiliate!")]
        public string AffUsername { get; set; }

        [Required]
        public string AffDomain { get; set; }

        public bool AffDomainIsSSL { get; set; } = true;

        [Required]
        public string RedirectDomain { get; set; }

        public bool RedirectDomainIsSSL { get; set; } = true;

        public bool Status { get; set; } = true;

        public bool IsMobile { get; set; }
    }
}
