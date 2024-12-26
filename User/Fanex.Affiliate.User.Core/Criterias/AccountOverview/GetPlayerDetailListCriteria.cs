namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;

    public class GetPlayerDetailListCriteria : AffPagingCriteria
    {
        public DateTime MonthDate { get; set; }

        public int SortById { get; set; }

        public bool IsSortAsc { get; set; }

        public override string GetSettingKey() => "Aff_User_Overview_PlayersDetail_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
