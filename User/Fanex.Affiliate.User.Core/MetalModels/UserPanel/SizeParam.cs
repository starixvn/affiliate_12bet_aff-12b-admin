namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Enum;

    public class SizeParam
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public byte DeviceType { get; set; }

        public string AffType { get; set; }

        public AffiliateLinkType TargetId { get; set; }
    }
}
