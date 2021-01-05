using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerLeftPanelService : ICustomerLeftPanelService
    {
        private readonly IEmailService _emailService;
        private readonly IAffiliateService _affiliateService;
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;

        public CustomerLeftPanelService(
            IEmailService emailService,
            IAffiliateService affiliateService,
            IAuthService authService,
            ISessionService sessionService,
            ICustomerLeftPanelRepository customerLeftPanelRepository)
        {
            _emailService = emailService;
            _affiliateService = affiliateService;
            _customerLeftPanelRepository = customerLeftPanelRepository;
            _sessionService = sessionService;
            _authService = authService;
        }

        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName, string accOnId, string sessionId)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);
            var userId = _sessionService.UserId;

            if (affiliateInfo == null || !_authService.IsUserHasPermissionOnAff(userId, affiliateInfo.AffId))
            {
                return null;
            }

            affiliateInfo.AffLink = GenerateUrl(affUserName, affiliateInfo.AffType, accOnId, sessionId).ToString();

            return affiliateInfo;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);
            var userId = _sessionService.UserId;
            IEnumerable<OnlineList> onlineList = new List<OnlineList>();

            if (affiliateInfo != null && _authService.IsUserHasPermissionOnAff(userId, affiliateInfo.AffId))
            {
                onlineList = _customerLeftPanelRepository.GetOnlineList(affUserName);
            }
            return onlineList;
        }

        public ResultInfo UpdateAffType(UpdateAffTypeParam param)
        {
            var userId = _sessionService.UserId;
            return _authService.IsUserHasPermissionOnAff(userId, param.AffId)
                ? _customerLeftPanelRepository.UpdateAffType(param)
                : Result.NotHavePermissionOnAff;
        }

        public ResultInfo UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            var userId = _sessionService.UserId;
            return _authService.IsUserHasPermissionOnAff(userId, param.AffId)
                ? _customerLeftPanelRepository.UpdateClosedStatus(param)
                : Result.NotHavePermissionOnAff;
        }

        public ResultInfo UpdateLanguage(UpdateLanguageParam param)
        {
            var userId = _sessionService.UserId;
            return _authService.IsUserHasPermissionOnAff(userId, param.AffId)
                ? _customerLeftPanelRepository.UpdateLanguage(param)
                : Result.NotHavePermissionOnAff;
        }

        public ResultInfo UpdateStatus(UpdateStatusParam param, AffInformationEmail emailParam)
        {
            var userId = _sessionService.UserId;
            if (!_authService.IsUserHasPermissionOnAff(userId, param.AffId))
            {
                return Result.NotHavePermissionOnAff;
            }

            var result = _customerLeftPanelRepository.UpdateStatus(param);

            if (result.IsSuccessful && string.Equals(param.Status, AffStatus.Disapproved, StringComparison.InvariantCultureIgnoreCase))
            {
                var affInfo = _affiliateService.GetAffiliateBasicInfo(param.AffId);

                SendDisapprovedEmail(emailParam, affInfo);
            }

            return result;
        }

        private void SendDisapprovedEmail(AffInformationEmail emailParam, AffiliateBasicInfo affInfo)
        {
            emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
            emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguage;
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
    }
}
