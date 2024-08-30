using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerLeftPanelService : ManagePermissionService, ICustomerLeftPanelService
    {
        private readonly IEmailService _emailService;
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;
        private readonly ISystemService _systemService;

        public CustomerLeftPanelService(
            IEmailService emailService,
            IAffiliateService affiliateService,
            IAuthService authService,
            ISessionService sessionService,
            ICustomerLeftPanelRepository customerLeftPanelRepository,
            ISystemService systemService) : base(authService, sessionService, affiliateService)
        {
            _emailService = emailService;
            _customerLeftPanelRepository = customerLeftPanelRepository;
            _systemService = systemService;
        }

        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName, string accOnId, string sessionId)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);

            if (affiliateInfo == null || !AuthService.IsUserHasPermissionOnAff(UserId, affiliateInfo.AffId))
            {
                return null;
            }

            affiliateInfo.PreferredLanguageName = GetPreferredLanguageName(affiliateInfo.PreferredLanguage);
            affiliateInfo.AffLink = GenerateUrl(affUserName, affiliateInfo.AffType, accOnId, sessionId).ToString();
            if (!string.IsNullOrEmpty(affiliateInfo.ContactApps))
            {
                affiliateInfo.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<CensoredContactApp>>(affiliateInfo.ContactApps).OrderBy(p => p.Order);
            }

            return affiliateInfo;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);
            IEnumerable<OnlineList> onlineList = new List<OnlineList>();

            if (affiliateInfo != null && AuthService.IsUserHasPermissionOnAff(UserId, affiliateInfo.AffId))
            {
                onlineList = _customerLeftPanelRepository.GetOnlineList(affUserName);
            }
            return onlineList;
        }

        public ResultInfo UpdateAffType(UpdateAffTypeParam param)
        {
            return HasHostMultiPermissions(param.AffId, Function.EditAffiliate)
                ? _customerLeftPanelRepository.UpdateAffType(param)
                : NotHavePermissionOnAffResult;
        }

        public ResultInfo UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            return AuthService.IsUserHasPermissionOnAff(UserId, param.AffId)
                ? _customerLeftPanelRepository.UpdateClosedStatus(param)
                : NotHavePermissionOnAffResult;
        }

        public ResultInfo UpdateLanguage(UpdateLanguageParam param)
        {
            if (_systemService.GetInChangeMarkets().Any(m => string.Equals(m.LanguageCode, param.PreferredLanguage, StringComparison.OrdinalIgnoreCase)))
            {
                return HasHostMultiPermissions(param.AffId, Function.LanguageAmendment)
                    ? _customerLeftPanelRepository.UpdateLanguage(param)
                    : NotHavePermissionOnAffResult;
            }

            return ResultInfo.GenerateErr("The selected language is invalid!");
        }

        public ResultInfo UpdateStatus(UpdateStatusParam param, AffInformationEmail emailParam)
        {
            if (!HasHostMultiPermissions(param.AffId, Function.AffAccountStatus))
            {
                return NotHavePermissionOnAffResult;
            }

            var result = _customerLeftPanelRepository.UpdateStatus(param);

            if (result.IsSuccessful && string.Equals(param.Status, AffStatus.Disapproved, StringComparison.InvariantCultureIgnoreCase))
            {
                var affInfo = AffiliateService.GetAffiliateBasicInfo(param.AffId);

                SendDisapprovedEmail(emailParam, affInfo);
            }

            return result;
        }

        private void SendDisapprovedEmail(AffInformationEmail emailParam, AffiliateBasicInfo affInfo)
        {
            emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
            emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguageCode;
            emailParam.FirstName = affInfo.FirstName;
            emailParam.MailTo = affInfo.Email;
            emailParam.IsHtml = !affInfo.MailOption;

            _emailService.SendDisapprovedEmail(emailParam);
        }

        private static Uri GenerateUrl(string custName, int affType, string accOnId, string sessionId)
        {
            var rndStr = Encryption.RandomString(6);
            var accKey = Generator.GenerateSHA512String(sessionId + rndStr);
            var url = SiteDomainHelper.GetUserInternalSiteUrl(affType);
            url += "?chalengeStr=" + rndStr;
            url += "&accKey=" + accKey;
            url += "&custName=" + custName;
            url += "&accOnId=" + accOnId;
            return new Uri(url, UriKind.Absolute);
        }

        private string GetPreferredLanguageName(string preferredLanguage)
        {
            var languageList = _systemService.GetInChangeMarkets();
            var language = languageList.FirstOrDefault(l => l.LanguageCode == preferredLanguage);

            return language?.LanguageName ?? preferredLanguage;
        }
    }
}
