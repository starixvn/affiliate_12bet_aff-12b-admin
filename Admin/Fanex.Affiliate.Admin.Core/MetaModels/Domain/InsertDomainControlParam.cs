
using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class InsertDomainControlParam : UserInfoParam
    {
        [Required]
        public string TargetCountryCode { get; set; }

        [Range(Validation.MinId, Validation.MaxId)]
        public int AffDomainId { get; set; }

        [Range(Validation.MinId, Validation.MaxId)]
        public int RedirectDomainId { get; set; }

        public bool IsMobile { get; set; }

        public bool IsExcluded { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DealIds { get; set; }

        public bool IsUsed { get; set; } = true;
    }
}
