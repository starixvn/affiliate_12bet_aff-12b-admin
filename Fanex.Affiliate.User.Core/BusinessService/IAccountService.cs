using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IAccountService
    {
        ResultInfo CheckPlayerAccountIsActived(int affId);
    }
}
