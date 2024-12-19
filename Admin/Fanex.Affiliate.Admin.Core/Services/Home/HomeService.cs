using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public HomeService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }
        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            var criteria = new UserOnlineCriteria
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            var userOnlineList = _dynamicRepository.Fetch<OnlineListInfo>(criteria);

            return new EntityResult<OnlineListInfo>
            {
                TotalItem = criteria.TotalItem,
                MainData = userOnlineList,
            };
        }

        public GeneralInfo GetGeneralInfo()
        {
            const string SpKey = "Aff_Admin_GeneralInfo_Sel";

            return _dynamicRepository.Get<GeneralInfo>(new BaseCriteria(SpKey));
        }
    }
}
