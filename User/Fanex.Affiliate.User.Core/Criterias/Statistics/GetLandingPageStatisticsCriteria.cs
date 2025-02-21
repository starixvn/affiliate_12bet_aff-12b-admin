namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetLandingPageStatisticsCriteria : BaseStatisticsCriteria
    {
        private const string SpKey = "Aff_ClickStatisticsSelByPage";

        public string SportCode { get; set; }

        public string LanguageCode { get; set; }

        public int TotalSignUps { get; set; }

        public int TotalFdms { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
