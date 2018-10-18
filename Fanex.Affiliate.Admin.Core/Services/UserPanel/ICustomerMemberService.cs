using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public interface ICustomerMemberService
    {
        CustomerMemberViewModel GetCustomerMember(CustomerMemberParam param);
    }
}
