namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    using System.Collections.Generic;
    using DTO;
    using DTO.Common;

    public class FormManagementViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<TargetCountry> TargetCountryList { get; set; }

        public string BannerRootUrl { get; set; }
    }
}
