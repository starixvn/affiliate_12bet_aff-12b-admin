using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommissionGroupSetting : CommGroup
    {
        [Required]
        public IEnumerable<TierSetting> TierSettingList { get; set; }

        [Required]
        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        [Required]
        public DeductionSetting OperationalFee { get; set; }

        [Required]
        public DeductionSetting BankingFee { get; set; }
    }
}
