using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAuthRepository
    {
        IList<UserPermission> GetPermissionList(int userId);
        bool ReCheckin(int onUserId);

        ResultInfo Login(LoginParam param);

        int LoginOnUser(LoginParam param, string sessionId, string serverId);

        void Logout(int onUserId);
    }
}
