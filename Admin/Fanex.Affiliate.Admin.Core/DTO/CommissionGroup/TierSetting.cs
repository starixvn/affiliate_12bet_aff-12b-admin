using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class TierSetting
    {
        public int GroupId { get; set; }

        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "TierId not exists!")]
        public int TierId { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "MinAmount not valid!")]
        public decimal MinAmount { get; set; }

        [Range(0, (double)Validation.InfinityAmount, ErrorMessage = "MaxAmount not valid!")]
        public decimal MaxAmount { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "MinQm not valid!")]
        public int MinQm { get; set; }

        [Range(0, Validation.InfinityQualifiedMember, ErrorMessage = "MaxQm not valid!")]
        public int MaxQm { get; set; }

        public decimal Commission { get; set; }
    }
}
