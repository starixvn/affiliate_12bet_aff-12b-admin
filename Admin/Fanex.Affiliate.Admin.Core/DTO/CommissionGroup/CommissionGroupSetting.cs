using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommissionGroupSetting : CommGroup
    {
        [Required(ErrorMessage = "Tier Setting must have at least 1 tier!")]
        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<FdmSetting> FdmSettingList { get; set; }

        [Required]
        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        [Required]
        public DeductionSetting OperationalFee { get; set; }

        [Required]
        public DeductionSetting BankingFee { get; set; }

        [Required]
        public DeductionSetting ExchangeWinLossShare { get; set; }

        [Required]
        public DeductionSetting ExchangeCommissionShare { get; set; }
    }
}
