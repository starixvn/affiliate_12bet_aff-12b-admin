using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Customer;

namespace Fanex.Affiliate.Admin.Core.Repository.Customer
{
    public interface ICustomerLeftPanelRepository
    {
        AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName);

        IEnumerable<OnlineList> GetOnlineList(string affUserName);
    }
}
