using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IAuthService
    {
        LoginResultViewModel GetLoginResult(UserLoginParam userParam, bool isInternal);

        int GetOnUserId(OnUserIdParam onUserIdParam);

        InternalLoginInfo GetInternalLoginInfo(int onUserId);

        IEnumerable<Claim> GetInternalLoginClaim(int internalLoginUserId);

        bool Recheckin(int onUserId, bool isInternal);

        void SignIn(string userName, bool createPersistentCookie);

        void SignOut();

        bool IsValidAffToShowFeedback(int affId, DateTime? lastestRequestTime = null);
    }
}