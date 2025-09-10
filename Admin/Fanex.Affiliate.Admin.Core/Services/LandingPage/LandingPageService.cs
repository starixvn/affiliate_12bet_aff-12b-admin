using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Campaign;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO.Operation;
using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Operation;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class LandingPageService : BaseDynamicService, ILandingPageService
    {
        private const int RegistrationFormModuleId = 9;
        private readonly ISystemService _systemService;
        private readonly ICommonDealService _commonDealService;

        public LandingPageService(
            IDynamicRepository dynamicRepository,
            ISystemService systemService,
            ICommonDealService commonDealService) : base(dynamicRepository)
        {
            _systemService = systemService;
            _commonDealService = commonDealService;
        }

        public IEnumerable<CampaignInfo> GetCampaignInfoList()
        {
            return FetchTData<CampaignInfo>(new GetCampaignInfoCriteria());
        }
        
        public IEnumerable<LandingPageModule> GetModuleList()
        {
            return FetchTData<LandingPageModule>(new GetModuleCriteria());
        }

        public IEnumerable<LandingPageModule> GetModuleListExcludeRegistrationForm()
        {
            return GetModuleList().Where(m => m.ModuleId != RegistrationFormModuleId);
        }

        public IEnumerable<StatusInfo> GetLandingPageStatusList()
        {
            return FetchTData<StatusInfo>(new BaseCriteria("Aff_LandingPage_Status_Sel"));
        }

        public IEnumerable<Language> GetLanguageList()
        {
            return _systemService.GetLanguageList();
        }

        public IEnumerable<NormalDeal> GetNormalDealList()
        {
            return _commonDealService.GetNormalDealList(new GetNormalDealParam());
        }

        public IEnumerable<TargetCountry> GetTargetCountryList()
        {
            return _systemService.GetTargetCountryList();
        }
    }
}
