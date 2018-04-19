using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IAuthRepository
    {
        AuthorizedUserInfo GetUserInfo(UserLoginParam userLoginParam);

        void UpdateLoginResult(LoginResultParam loginResultParam);

        int GetOnUserId(OnUserIdParam onUserIdParam);

        string GetSessionId(int onUserId);
    }
}