using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Commission
{
    public class CommissionSetting : CommGroup
    {
        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        public IEnumerable<DeductionSetting> RemainingFeeList { get; set; }
    }
}
