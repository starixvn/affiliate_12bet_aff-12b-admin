using Fanex.Affiliate.Admin.Core.Repository.Home;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using System;

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

        public decimal GetPSPToday()
        {
            return _homeRepository.GetPSPToday();
        }

        public DepositInfo GetDepositInfo()
        {
            return _homeRepository.GetDepositInfo();
        }

        public GeneralInfo GetGeneralInfo()
        {
            return _homeRepository.GetGeneralInfo();
        }
    }
}
