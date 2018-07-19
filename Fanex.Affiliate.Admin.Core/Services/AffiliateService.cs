using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.Enum;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Core;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepository _affRepository;

        private readonly ISessionService _sessionService;

        public AffiliateService(ISessionService sessionService, IAffiliateRepository affRepository)
        {
            _sessionService = sessionService;
            _affRepository = affRepository;
        }

        public AffLeftPanelInfo GetInfo(string username)
        {
            var leftPanelInfo = _affRepository.GetInfo(username);
            var generalInfo = leftPanelInfo.GeneralInfo;

            if (generalInfo != null)
            {
                if (!string.IsNullOrEmpty(generalInfo.ContactApps))
                {
                    generalInfo.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(generalInfo.ContactApps);
                }

                AssignSession(generalInfo);
            }

            return leftPanelInfo;
        }

        private void AssignSession(AffInfo affInfo)
        {
            _sessionService.SelectedAffId = affInfo.AffId;
            _sessionService.SelectedAffUserName = affInfo.UserName;
            _sessionService.SelectedAffType = (AffType)affInfo.AffType;
        }
    }
}