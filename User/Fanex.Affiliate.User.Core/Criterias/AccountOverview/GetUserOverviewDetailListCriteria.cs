namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;
    using System;

    public class GetUserOverviewDetailListCriteria : CriteriaBase
    {
        public GetUserOverviewDetailListCriteria(int affId, DateTime monthDate)
        {
            AffId = affId;
            MonthDate = monthDate;
        }

        public int AffId { get; set; }

        public DateTime MonthDate { get; set; }

        public override string GetSettingKey() => "Aff_User_Overview_Detail_Sel";

        public override bool IsValid() => AffId > 0;
    }
}