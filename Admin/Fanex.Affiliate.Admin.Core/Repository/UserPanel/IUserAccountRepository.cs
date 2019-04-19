using Fanex.Affiliate.Admin.Core.DTO.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IUserAccountRepository
    {
        UserAccount GetUserAccountInfo(int affId);
    }
}
