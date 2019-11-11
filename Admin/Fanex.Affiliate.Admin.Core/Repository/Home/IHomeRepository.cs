using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Home;

namespace Fanex.Affiliate.Admin.Core.Repository.Home
{
    public interface IHomeRepository
    {
        EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize);
    }
}
