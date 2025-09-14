using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.LandingPage
{
    public class PageBuilderViewModel : PageManagementViewModel
    {
        public IEnumerable<ModuleInfo> ModuleList { get; set; }

        public PageBuilderDetail PageBuilderDetail { get; set; }
    }



    public class ManageLandingPageViewModel
    {
        public IEnumerable<TargetCountry> TargetCountries { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<LandingPageTemplate> LandingPageList { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class CreateLandingPageViewModel
    {
        public int PageId { get; set; }
        public IEnumerable<TargetCountry> TargetCountries { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<LandingPageModule> AvailableModules { get; set; }
        public IEnumerable<ModuleTypeInfo> ModuleTypes { get; set; }
        public IEnumerable<NavigationTargetInfo> NavigationTargets { get; set; }
        public LandingPageTemplate ExistingPageData { get; set; }
    }

    public class ManageRegistrationFormViewModel
    {
        public IEnumerable<TargetCountry> TargetCountries { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<RegistrationFormTemplate> FormTemplateList { get; set; }
        public IEnumerable<FormFieldType> FormFieldTypes { get; set; }
        public int TotalForms { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
