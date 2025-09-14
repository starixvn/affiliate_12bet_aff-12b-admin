namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class PageManagement
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string TargetCountryCode { get; set; }
        public string TargetCountryName { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string DealIds { get; set; }
        public int NoOfDeals { get; set; }
        public byte StatusId { get; set; }
        public string CampaignName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
