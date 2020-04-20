using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class AccountOverviewViewModel
    {
        public LastLoginInfo LastLoginInfo { get; set; }

        public string SelectedSubMenu { get; set; }
    }
}
