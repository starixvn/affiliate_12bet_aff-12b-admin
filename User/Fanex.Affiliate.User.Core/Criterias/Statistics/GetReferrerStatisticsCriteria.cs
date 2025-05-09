namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetReferrerStatisticsCriteria : BaseStatisticsCriteria
    {
        private const string SpKey = "Aff_ClickStatisticsSelBySite";

        public int TotalClicks { get; set; }

        public int UniqueClicks { get; set; }

        public int TotalSignUps { get; set; }

        public int TotalFdms { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
