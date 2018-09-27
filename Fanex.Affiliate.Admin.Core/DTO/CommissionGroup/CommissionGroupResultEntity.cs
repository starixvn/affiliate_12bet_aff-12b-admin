using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.CommissionGroup
{
    public class CommissionGroupResultEntity
    {
        public IEnumerable<CommGroup> CommGroupList { get; set; }

        public IEnumerable<TierSetting> TierSettingList { get; set; }

        public IEnumerable<DeductionSetting> DeductionSettingList { get; set; }
    }
}