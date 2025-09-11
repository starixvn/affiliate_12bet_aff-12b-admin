using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Campaign;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.Operation;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface ILandingPageService
    {
        IEnumerable<CampaignInfo> GetCampaignInfoList();

        IEnumerable<ModuleInfo> GetModuleList();

        IEnumerable<ModuleInfo> GetModuleListExcludeRegistrationForm();

        IEnumerable<StatusInfo> GetLandingPageStatusList();

        IEnumerable<NormalDeal> GetNormalDealList();

        IEnumerable<Language> GetLanguageList();

        IEnumerable<Language> GetAllLanguagesInSystem();

        IEnumerable<TargetCountry> GetTargetCountryList();
    }
}
