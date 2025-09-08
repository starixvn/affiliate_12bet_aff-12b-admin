namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;
    using Fanex.Affiliate.Admin.Core.DTO.LandingPage;

    public class FormManagementViewModel
    {
        public IEnumerable<LandingPageModule> FormNameList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public IEnumerable<StatusInfo> StatusList { get; set; }

        public string BannerRootUrl { get; set; }
    }
}
