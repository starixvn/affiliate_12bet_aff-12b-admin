namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Data.Repository;

    public class GetPlayerDetailListCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime MonthDate { get; set; }

        public int SortById { get; set; }

        public bool IsSortAsc { get; set; }

        public int NRows { get; set; }

        public override string GetSettingKey() => "Aff_User_Overview_PlayersDetail_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
