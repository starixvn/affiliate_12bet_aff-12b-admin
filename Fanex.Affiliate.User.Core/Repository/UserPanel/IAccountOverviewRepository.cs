namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;

    public interface IAccountOverviewRepository
    {
        AccountOverviewStatistics GetStatistics(int userId, DateTime month);
    }
}