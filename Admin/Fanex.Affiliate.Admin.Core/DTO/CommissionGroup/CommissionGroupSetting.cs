using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommissionGroupSetting : CommGroup
    {
        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> PlatformFeeList { get; set; }

        public DeductionSetting OperationalFee { get; set; }

        public DeductionSetting BankingFee { get; set; }
    }
}
