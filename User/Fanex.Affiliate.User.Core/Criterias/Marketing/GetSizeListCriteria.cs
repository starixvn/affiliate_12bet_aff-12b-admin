namespace Fanex.Affiliate.User.Core.Criterias
{
    using Enum;
    using Utilities;

    public class GetSizeListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_Admin_MediaSize_Sel";

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public byte DeviceType { get; set; }

        public string AffType { get; set; }

        public AffiliateLinkType TargetId { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
