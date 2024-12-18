using Fanex.Affiliate.Admin.Core.DTO.UserPanel.Statistics;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class LandingPageStatistics : ClickStatistics
    {
        [JsonIgnore]
        public string SportCode { get; set; }

        public string Page => SportCode;

        public string LanguageCode { get; set; }
    }
}
