using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel.Statistics
{
    public class ClickStatistics
    {
        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int Fdm { get; set; }

        [JsonIgnore]
        public string URLReferrer { get; set; }

        public string WebsiteUrl => URLReferrer;
    }
}
