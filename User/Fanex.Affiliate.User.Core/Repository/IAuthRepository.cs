using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.MetalModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IAuthRepository
    {
        AuthorizedUserInfo GetUserInfo(string userName, bool isInternal);

        LoginResultInfo UpdateLoginResult(LoginResultParam loginResultParam);

        int GetOnUserId(OnUserIdParam onUserIdParam);

        InternalLoginInfo GetInternalLoginInfo(int onUserId);

        IEnumerable<InternalPermission> GetInternalLoginPermission(int internalLoginUserId);

        bool Recheckin(int onUserId, bool isInternal);
    }
}