namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class PageManagement
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public string Deal { get; set; }
        public byte StatusId { get; set; }
        public string CampaignName { get; set; }
        public string CreatedDate { get; set; }
        public string LastModified { get; set; }
    }
}
