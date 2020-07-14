using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using System.Collections.Generic;

namespace Fanex.Affiliate.ThirdPartyProduct.Repository
{
    public interface ITimeTrackerRepository
    {
        IEnumerable<TimeTracker> GetDataList(TimeTrackerParam param);
    }
}
