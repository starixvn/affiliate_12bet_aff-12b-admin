namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System;
    using System.Collections.Generic;
    using Criterias;
    using Extensions.Mappers;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.AccountOverview;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Data.Repository;

    public class AccountOverviewRepository : IAccountOverviewRepository
    {
        private readonly IDynamicRepository _repository;

        public AccountOverviewRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public LastLoginInfo GetLastLoginInfo(int affId)
        {
            return _repository.Get<LastLoginInfo>(new GetLastLoginInfoCriteria(affId));
        }

        public IEnumerable<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param)
        {
            var criteria = new GetPlayerDetailListCriteria();
            criteria.Map(param);

            return _repository.Fetch<PlayerDetail>(criteria);
        }

        public IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate)
        {
            return _repository.Fetch<OverviewDetail>(new GetUserOverviewDetailListCriteria(affId, monthDate));
        }

        public OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate)
        {
            return _repository.Get<OverviewSummary>(new GetUserOverviewSummaryCriteria(affId, monthDate));
        }

        public IEnumerable<SubAffDetail> GetSubAffDetails(SubAffDetailParam param)
        {
            return _repository.Fetch<SubAffDetail>(new GetSubAffDetailsCriteria(param));
        }
    }
}
