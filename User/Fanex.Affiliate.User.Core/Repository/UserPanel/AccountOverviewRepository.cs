using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountOverviewRepository : IAccountOverviewRepository
    {
        public IEnumerable<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param)
        {
            const string uSpKey = "Aff_User_Overview_PlayersDetail_Sel";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                //return objectDb.DB.Query<PlayerDetail>(param);
            }

            const int nrows = 40;
            param.NRows = nrows;
            List<PlayerDetail> result = new List<PlayerDetail>();
            for(int i = 1; i <= nrows; i++)
            {
                result.Add(new PlayerDetail { RowNum = i });
            }

            if(param.PageSize > nrows)
            {
                return result;
            }
            else
            {
                List<PlayerDetail> subResult = new List<PlayerDetail>();
                int pageIndex = (param.PageSize * (param.PageIndex - 1)) + 1;
                int rows = param.PageSize * param.PageIndex;

                for (int i = pageIndex; i <= rows; i++)
                {
                    subResult.Add(result[i - 1]);
                }

                return subResult;
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
