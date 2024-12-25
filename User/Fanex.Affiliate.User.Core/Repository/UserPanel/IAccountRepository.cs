using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IAccountRepository
    {
        UserInfo GetUserInfoById(int affId, bool isInternal);
    }
}