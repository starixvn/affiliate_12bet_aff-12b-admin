using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IAdminInfoService
    {
        AdminInforResultEntity GetUserInfo(int userId);

        ResultInfo CreateUser(AdminInfoParam param);

        ResultInfo UpdateUser(AdminInfoParam param);

        IEnumerable<RoleInfo> GetRoleList();
    }
}
