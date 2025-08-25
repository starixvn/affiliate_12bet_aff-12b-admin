namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class PageManagementParam : PagingParam
    {
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public string Deal { get; set; }
        public string Status { get; set; }
        public string LandingPageName { get; set; }
    }
}
