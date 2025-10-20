using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services.Auth
{
    public interface IAuthService
    {
        IList<UserPermission> GetPermissionList(int userId);
        bool ReCheckin(int onUserId);

        ResultInfo Login(LoginParam param);

        int LoginOnUser(LoginParam param, string sessionId, string serverId);

        void Logout(int onUserId);

        ResultInfo CheckPermission(CheckPermissionParam param);

        IEnumerable<Claim> GetClaims(int userId, IList<UserPermission> permissions);
    }
}
