using Fanex.Affiliate.Admin.Core.DTO.UserPanel.Statistics;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class LandingPageStatistics : ClickStatistics
    {
        public string Page { get; set; }

        public string LanguageCode { get; set; }
    }
}
