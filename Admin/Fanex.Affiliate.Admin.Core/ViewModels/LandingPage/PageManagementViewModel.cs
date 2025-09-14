namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;
    using DTO.Operation;
    using DTO.Campaign;
    using DTO.LandingPage;

    public class PageManagementViewModel
    {
        public IEnumerable<CampaignInfo> CampaignList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<Language> AllLanguagesInSystem { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<StatusInfo> StatusList { get; set; }

        public IEnumerable<NormalDeal> DealList { get; set; }

        public string LandingPageUrl { get; set; }
    }
}
