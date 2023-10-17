using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class TierSetting
    {
        public int GroupId { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "The Tier Id is not exists!")]
        public int TierId { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "The Min Amount is not valid!")]
        public decimal MinAmount { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "The Max Amount is not valid!")]
        public decimal MaxAmount { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "The Min Qm is not valid!")]
        public int MinQm { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "The Max Qm is not valid!")]
        public int MaxQm { get; set; }

        public decimal Commission { get; set; }
    }
}
