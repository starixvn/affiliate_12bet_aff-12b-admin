using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerLeftPanelService : ICustomerLeftPanelService
    {
        private readonly ICustomerLeftPanelRepository _customerLeftPanelRepository;

        public CustomerLeftPanelService(ICustomerLeftPanelRepository customerLeftPanelRepository)
        {
            _customerLeftPanelRepository = customerLeftPanelRepository;
        }
        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName, string accId, string accOnId, string sessionId)
        {
            var affiliateInfo = _customerLeftPanelRepository.GetAffiliateInfoOnLeftPanel(affUserName);
            if (affiliateInfo != null)
            {
                affiliateInfo.AffLink = GenerateUrl(affUserName, affiliateInfo.AffType, accId, accOnId, sessionId);
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

        public ResultInfo UpdateStatus(UpdateStatusParam param)
        {
            return _customerLeftPanelRepository.UpdateStatus(param);
        }

        private static string GenerateUrl(string custName, int affType, string accId, string accOnId, string sessionId)
        {
            var rndStr = Encryption.RandomString(6);
            var accKey = GenerateSHA512String(sessionId + rndStr);
            var _url = affType == AffType.European ? ConfigurationManager.AppSettings["internalEuropeanAffiliateURL"] : ConfigurationManager.AppSettings["internalAsianAffiliateURL"];
            _url += "?chalengeStr=" + rndStr;
            _url += "&accKey=" + accKey;
            _url += "&custName=" + custName;
            _url += "&accId=" + accId;
            _url += "&accOnId=" + accOnId;
            return _url;
        }

        private static string GenerateSHA512String(string inputString)
        {
            var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha512.ComputeHash(bytes);

            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
