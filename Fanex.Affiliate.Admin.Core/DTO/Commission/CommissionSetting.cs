using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Commission
{
    public class CommissionSetting : CommGroup
    {
        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> PlatformFee { get; set; }

        public DeductionSetting OperationalFee { get; set; }

        public DeductionSetting BankingFee { get; set; }
    }
}
