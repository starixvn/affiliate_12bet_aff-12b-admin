using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Commission
{
    public class CommissionResultEntity
    {
        public IEnumerable<CommGroup> CommGroupList { get; set; }

        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> DeductionSettingList { get; set; }
    }
}