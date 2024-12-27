using Fanex.Affiliate.Admin.Core.DTO.UserPanel.Statistics;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class ReferrersStatistics : ClickStatistics
    {
        public string SiteUrl { get; set; }
    }
}
