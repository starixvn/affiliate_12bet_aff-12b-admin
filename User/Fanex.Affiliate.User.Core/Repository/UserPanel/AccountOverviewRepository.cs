using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountOverviewRepository : IAccountOverviewRepository
    {
        private readonly IObjectDBBuilder _builder;

        public AccountOverviewRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public LastLoginInfo GetLastLoginInfo(int affId)
        {
            const string uSpKey = "Aff_User_LoginInfo_Sel";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<LastLoginInfo>(new { affId });
            }
        }

        public IEnumerable<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param)
        {
            const string uSpKey = "Aff_User_Overview_PlayersDetail_Sel";

            using (var db = _builder.Build(uSpKey))
            {
                return db.Query<PlayerDetail>(param);
            }
        }

        public IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate)
        {
            const string uSpKey = "Aff_User_Overview_Detail_Sel";

            using (var db = _builder.Build(uSpKey))
            {
                return db.Query<OverviewDetail>(new { affId, monthDate });
            }
        }

        public OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate)
        {
            const string uSpKey = "Aff_User_Overview_Summary_Sel";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<OverviewSummary>(new { affId, monthDate });
            }
        }
    }
}
