using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class AccountOverviewViewModel
    {
        public AccountOverviewStatistics Statistics { get; set; }

        public IEnumerable<CommissionSetting> CommissionSettingList { get; set; }

        public DateTime SelectedMonth { get; set; }
    }
}
