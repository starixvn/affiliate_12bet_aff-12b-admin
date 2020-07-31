using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Data;
using System.Collections.Generic;

namespace Fanex.Affiliate.ThirdPartyProduct.Repository
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        public IEnumerable<TimeTracker> GetDataList(TimeTrackerParam param)
        {
            const string SpKey = "Aff_3rdProduct_TimeTrackerSel";
            using (var objectDb = new ObjectDb(SpKey))
            {
                return objectDb.Query<TimeTracker>(param);
            }
        }
    }
}
