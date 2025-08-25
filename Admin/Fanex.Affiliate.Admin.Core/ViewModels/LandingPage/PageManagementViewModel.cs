namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;
    using DTO.Operation;
    using Fanex.Affiliate.Admin.Core.DTO.LandingPage;

    public class PageManagementViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<LandingPageStatus> StatusList { get; set; }

        public IEnumerable<NormalDeal> DealList { get; set; }

        public string LandingPageUrl { get; set; }
    }
}
