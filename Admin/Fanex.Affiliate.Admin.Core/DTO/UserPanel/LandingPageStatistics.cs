
namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class LandingPageStatistics
    {
        public string Page { get; set; }

        public string Language { get; set; }

        public string LanguageCode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int Fdm { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
