using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class LandingPage
    {
        public int PageId { get; set; }

        public string PageName { get; set; }

        public string PageCode { get; set; }

        [JsonProperty(PropertyName = "ExcludedTargetCountries")]
        public string ExcludedTargetCountry { get; set; }
    }
}
