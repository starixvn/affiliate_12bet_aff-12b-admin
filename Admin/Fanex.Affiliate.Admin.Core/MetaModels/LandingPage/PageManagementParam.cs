namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class PageManagementParam : PagingParam
    {
        public string TargetCountryCodes { get; set; }
        public string LanguageCodes { get; set; }
        public string DealIds { get; set; }
        public string CampaignIds { get; set; }
        public string StatusIds { get; set; }
        public string LandingPageName { get; set; }
        public string CampaignName { get; set; }
    }
}
