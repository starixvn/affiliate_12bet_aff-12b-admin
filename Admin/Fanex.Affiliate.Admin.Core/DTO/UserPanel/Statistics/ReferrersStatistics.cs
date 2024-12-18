using Fanex.Affiliate.Admin.Core.DTO.UserPanel.Statistics;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class ReferrersStatistics : ClickStatistics
    {
        [JsonIgnore]
        public string DomainReferrer { get; set; }

        public string SiteUrl => DomainReferrer;
    }
}
