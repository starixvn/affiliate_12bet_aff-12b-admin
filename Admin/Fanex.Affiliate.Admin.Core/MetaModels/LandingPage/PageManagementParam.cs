using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class PageManagementParam : PagingParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TargetCountryCodes { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LanguageCodes { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DealIds { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string StatusIds { get; set; }
        public string LandingPageName { get; set; }
        public string CampaignName { get; set; }
    }
}
