using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public interface ICustomerLeftPanelService
    {
        AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName, string accOnId, string sessionId);

        IEnumerable<OnlineList> GetOnlineList(string affUserName);

        ResultInfo UpdateLanguage(UpdateLanguageParam param);

        ResultInfo UpdateAffType(UpdateAffTypeParam param);

        ResultInfo UpdateStatus(UpdateStatusParam param, AffInformationEmail emailParam);

        ResultInfo UpdateClosedStatus(UpdateClosedStatusParam param);
    }
}