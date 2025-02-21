using Fanex.Affiliate.Admin.Core.Criterias.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public class HomeService : BaseDynamicService, IHomeService
    {

        public HomeService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            var criteria = new UserOnlineCriteria
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return FetchEntityResult<OnlineListInfo>(criteria);
        }

        public GeneralInfo GetGeneralInfo()
        {
            const string SpKey = "Aff_Admin_GeneralInfo_Sel";

            return GetTData<GeneralInfo>(SpKey);
        }
    }
}
