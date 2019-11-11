using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public interface IHomeService
    {
        EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize);
    }
}
