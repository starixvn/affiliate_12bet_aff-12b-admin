namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;
    using Enum;
    using Utilities;

    public class GetBannerListCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_Admin_Banner_Sel";

        public string Category { get; set; }

        public string Language { get; set; }

        public string Size { get; set; }

        public string Currency { get; set; }

        public string AffType { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public byte? DeviceType { get; set; }

        public AffiliateLinkType TargetId { get; set; }

        public int Nrows { get; set; }

        public string SubCategory { get; set; }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
