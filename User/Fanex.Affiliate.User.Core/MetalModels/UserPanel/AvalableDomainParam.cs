namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Enum;

    public class AvailableDomainParam
    {
        public int AffId { get; set; }

        public string AffUsername { get; set; }

        public string TargetCountryCode { get; set; }

        public AffiliateLinkType LinkType { get; set; } = AffiliateLinkType.AffiliateLink;

        public bool IsInternal { get; set; }
    }
}