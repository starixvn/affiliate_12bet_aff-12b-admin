namespace Fanex.Affiliate.User.Core.Criterias
{
    using System;
    using Data.Repository;
    using MetalModels.UserPanel;

    public class GetSubAffDetailsCriteria : CriteriaBase
    {
        public GetSubAffDetailsCriteria(SubAffDetailParam param)
        {
            AffId = param.AffId;
            MonthDate = param.MonthDate;
        }

        public int AffId { get; set; }

        public DateTime MonthDate { get; set; }

        public override string GetSettingKey() => "Aff_User_Overview_SubAffiliate_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
