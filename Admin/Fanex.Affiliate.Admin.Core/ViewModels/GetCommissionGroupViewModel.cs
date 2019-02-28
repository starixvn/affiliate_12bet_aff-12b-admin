using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class GetCommissionGroupViewModel
    {
        public IEnumerable<CommissionGroupSetting> CommissionSettingList { get; set; }

        public int TotalRows { get; set; }
    }
}
