using Fanex.Affiliate.Admin.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.UserAccount;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.ThirdPartyLicensee;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class MarketingAccountService : BaseDynamicService, IMarketingAccountService
    {
        private readonly IThirdPartyLicenseeService _thirdPartyLicenseeService;

        public MarketingAccountService(IDynamicRepository dynamicRepository, IThirdPartyLicenseeService thirdPartyLicenseeService) : base(dynamicRepository)
        {
            _thirdPartyLicenseeService = thirdPartyLicenseeService;
        }

        public async Task<ResultInfo> CreateMarketingAccountAsync(CreateMarketingAccountModel model)
        {
            var command = model.CreateCommand();
            var result = await ExecuteReturnResultInfoAsync(command);
            var affInfo = new AffiliateBasicInfo
            {
                AffId = model.AffId,
                AffCode = model.AffCode,
                ParentAffName = model.ParentAffName,
                GroupId = model.GroupId,
                AffName = model.UserName,
                AffType = model.AffType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Status = model.Status,
                MailOption = model.MailOption ?? false,
                PreferredLanguage = model.PreferredLanguage,
                PlayerUsername = command.Username,
                PlayerCustId = model.PlayerCustId,
                IsUsingPrivateCommGroup = model.IsPrivate
            };
            await _thirdPartyLicenseeService.SendAffiliateInfo(affInfo);

            return result;
        }

        public MarketingAccount GetMarketingAccountInfo(int affId)
        {
            var criteria = new UserAccountCriteria { AffId = affId };

            return GetTData<MarketingAccount>(criteria);
        }

    }
}
