using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class AffLeftPanelInfo
    {
        public AffInfo GeneralInfo { get; set; }

        public IEnumerable<OnlineInfo> OnlineInfoList { get; set; }
    }
}
