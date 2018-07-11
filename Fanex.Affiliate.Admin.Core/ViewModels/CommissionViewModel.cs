using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Commission;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class CommissionViewModel
    {
        public IEnumerable<CommissionSetting> CommissionSettingList { get; set; }
    }
}
