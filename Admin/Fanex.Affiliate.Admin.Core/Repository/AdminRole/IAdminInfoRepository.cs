using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.AdminRole
{
    public interface IAdminInfoRepository
    {
        AdminInforResultEntity GetUserInfo(int userId);

        ResultInfo CreateUser(AdminInfoParam param);

        ResultInfo UpdateUser(AdminInfoParam param);

        IEnumerable<UserPermissionInfo> GetPermissionListByRole(int roleId);
    }
}
