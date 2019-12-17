using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IAccountOverviewRepository
    {
        OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate);

        IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate);

        IEnumerable<PlayerDetail> GetPlayerDetailList(int affId, DateTime monthDate);
    }
}
