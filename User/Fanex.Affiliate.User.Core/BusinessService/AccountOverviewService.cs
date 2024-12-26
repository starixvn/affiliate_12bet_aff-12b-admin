namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System;
    using System.Collections.Generic;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using DTO.UserPanel.AccountOverview;
    using MetalModels.UserPanel;
    using Utilities;

    public class AccountOverviewService : BaseDynamicService, IAccountOverviewService
    {
        public AccountOverviewService(IDynamicRepository dynamicRepository):base(dynamicRepository)
        {}

        public LastLoginInfo GetLastLoginInfo(int affId)
        {
            return GetTData<LastLoginInfo>(new GetLastLoginInfoCriteria(affId));
        }

        public IEnumerable<PlayerDetail> GetPlayerDetailList(PlayerDetailParam param)
        {
            var criteria = param.MapTo<GetPlayerDetailListCriteria>();

            return FetchTDataWithPaging<PlayerDetail>(criteria, param);
        }

        public IEnumerable<OverviewDetail> GetUserOverviewDetailList(int affId, DateTime monthDate)
        {
            return FetchTData<OverviewDetail>(new GetUserOverviewDetailListCriteria(affId, monthDate));
        }

        public OverviewSummary GetUserOverviewSummary(int affId, DateTime monthDate)
        {
            return GetTData<OverviewSummary>(new GetUserOverviewSummaryCriteria(affId, monthDate));
        }

        public IEnumerable<SubAffDetail> GetSubAffDetails(SubAffDetailParam param)
        {
            return FetchTData<SubAffDetail>(param.MapTo<GetSubAffDetailsCriteria>());
        }
    }
}
