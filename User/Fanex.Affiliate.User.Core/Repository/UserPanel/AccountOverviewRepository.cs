using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountOverviewRepository : IAccountOverviewRepository
    {
        public IEnumerable<PlayerDetail> GetPlayerDetailList(int affId, DateTime monthDate)
        {
            const string uSpKey = "Aff_User_Overview_PlayersDetail_Sel";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                return objectDb.DB.Query<PlayerDetail>(new { affId, monthDate });
            }
        }

        public IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate)
        {
            const string uSpKey = "Aff_User_Overview_Detail_Sel";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                return objectDb.DB.Query<OverviewDetail>(new { affId, monthDate });
            }
        }

        public OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate)
        {
            const string uSpKey = "Aff_User_Overview_Summary_Sel";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                return objectDb.DB.QueryEntity<OverviewSummary>(new { affId, monthDate });
            }
        }
    }
}
