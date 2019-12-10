using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerLeftPanelService : ICustomerLeftPanelService
    {
        private readonly IEmailService _emailService;
        private readonly IAffiliateService _affiliateService;
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;

        public CustomerLeftPanelService(
            IEmailService emailService,
            IAffiliateService affiliateService,
            ICustomerLeftPanelRepository customerLeftPanelRepository)
        {
            _emailService = emailService;
            _affiliateService = affiliateService;
            _customerLeftPanelRepository = customerLeftPanelRepository;
        }
        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName, string accId, string accOnId, string sessionId)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);
            if (affiliateInfo != null)
            {
                affiliateInfo.AffLink = GenerateUrl(affUserName, affiliateInfo.AffType, accId, accOnId, sessionId).ToString();
            }
            return affiliateInfo;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            return _customerLeftPanelRepository.GetOnlineList(affUserName);
        }

        public ResultInfo UpdateAffType(UpdateAffTypeParam param)
        {
            return _customerLeftPanelRepository.UpdateAffType(param);
        }

        public ResultInfo UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            return _customerLeftPanelRepository.UpdateClosedStatus(param);
        }

        public ResultInfo UpdateLanguage(UpdateLanguageParam param)
        {
            return _customerLeftPanelRepository.UpdateLanguage(param);
        }

        public ResultInfo UpdateStatus(UpdateStatusParam param, AffInformationEmail emailParam)
        {
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

        private static Uri GenerateUrl(string custName, int affType, string accId, string accOnId, string sessionId)
        {
            var rndStr = Encryption.RandomString(6);
            var accKey = Generator.GenerateSHA512String(sessionId + rndStr);
            var url = SiteDomainHelper.GetUserInternalSiteUrl(affType);
            url += "?chalengeStr=" + rndStr;
            url += "&accKey=" + accKey;
            url += "&custName=" + custName;
            url += "&accId=" + accId;
            url += "&accOnId=" + accOnId;
            return new Uri(url, UriKind.Absolute);
        }
    }
}
