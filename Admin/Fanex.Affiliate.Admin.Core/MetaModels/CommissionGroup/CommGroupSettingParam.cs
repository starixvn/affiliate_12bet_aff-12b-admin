using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class CommGroupSettingParam
    {
        public int GroupId { get; set; }

        public int GroupTypeId { get; set; }

        public string GroupName { get; set; }

        [Range(Validation.Zero, Validation.MaxPercentage, ErrorMessage = "Sub-Affiliate Commission Rate is incorrect!")]
        public decimal SubAffCommRate { get; set; }

        [Range(Validation.Zero, Money.AllowedMaxValue, ErrorMessage = "QpTurnover not valid!")]
        public decimal QmTurnover { get; set; }

        public bool IsActivePlayerSum { get; set; }

        [MaxLength(Validation.MaxLengthCommGroupDescription, ErrorMessage = "Description should shorter than 100 characters!")]
        public string Description { get; set; }

        [Required]
        public IEnumerable<TierSettingParam> TierSettingList { get; set; }

        [Required]
        public IEnumerable<TierDeductionSettingParam> PlatformFeeList { get; set; }

        public IEnumerable<TierDeductionSettingParam> KillRateFeeList { get; set; }

        [Required]
        public TierDeductionSettingParam DepositFee { get; set; }

        [Required]
        public TierDeductionSettingParam WithdrawalFee { get; set; }
    }
}
