namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetStatisticsDetailBySiteCriteria : BaseStatisticsCriteria
    {
        private const string SpKey = "Aff_ClickStatisticsDetailBySite";

        public string Domain { get; set; }

        public string SportCode { get; set; }

        public string LanguageCode { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int NRows { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
