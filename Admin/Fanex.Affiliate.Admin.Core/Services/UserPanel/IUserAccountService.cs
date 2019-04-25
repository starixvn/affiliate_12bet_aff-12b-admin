using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public interface IUserAccountService
    {
        UserAccount GetUserAccountInfo(int affId);

        ResultInfo UpdateUserAccount(UserAccountParam param);
    }
}
