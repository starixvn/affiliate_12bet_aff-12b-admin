namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;
    using System;

    public class GetAffiliateCommissionCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime WinLostDate { get; set; }

        public int BaseCurrency { get; set; }

        public override string GetSettingKey() => "Aff_User_Rpt_Commission";

        public override bool IsValid() => AffId > 0;
    }
}
