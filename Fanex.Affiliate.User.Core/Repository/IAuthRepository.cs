using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IAuthRepository
    {
        AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam);

        int GetOnUserId(OnUserIdParam onUserIdParam);
    }
}