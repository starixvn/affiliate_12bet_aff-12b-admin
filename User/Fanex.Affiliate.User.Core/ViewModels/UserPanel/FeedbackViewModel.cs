
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    public class FeedbackViewModel
    {
        public string UserName { get; set; }

        public bool IsClickLogout { get; set; }

        public IEnumerable<RateInfo> RateList { get; set; }
    }
}
