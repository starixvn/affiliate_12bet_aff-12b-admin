namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Data.Repository;

    public class GetSubAffDetailsCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime MonthDate { get; set; }

        public override string GetSettingKey() => "Aff_User_Overview_SubAffiliate_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
