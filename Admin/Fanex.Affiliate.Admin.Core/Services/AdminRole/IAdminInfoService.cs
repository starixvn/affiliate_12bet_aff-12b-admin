using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;

namespace Fanex.Affiliate.Admin.Core.Services.AdminRole
{
    public interface IAdminInfoService
    {
        AdminInfo GetUserInfo(int userId);

        ResultInfo CreateUser(AdminInfoParam param);

        ResultInfo UpdateUser(AdminInfoParam param);
    }
}
