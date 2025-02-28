namespace Fanex.Affiliate.User.Core.BusinessService
{
    using DTO.UserPanel;
    using DTO.UserPanel.AccountOverview;
    using MetalModels.UserPanel;
    using System;
    using System.Collections.Generic;

    public interface IAccountOverviewService
    {
        OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate);

        IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate);

        IEnumerable<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param);

        IEnumerable<SubAffDetail> GetSubAffDetails(SubAffDetailParam param);

        LastLoginInfo GetLastLoginInfo(int affId);
    }
}
