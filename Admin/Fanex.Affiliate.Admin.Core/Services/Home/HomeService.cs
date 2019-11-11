using Fanex.Affiliate.Admin.Core.Repository.Home;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public class HomeService: IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            return _homeRepository.GetOnlineList(pageIndex, pageSize);
        }
    }
}
