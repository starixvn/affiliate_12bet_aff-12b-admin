namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Enum;

    public class LandingPageParam
    {
        public int AffId { get; set; }

        public bool IsMobile { get; set; }

        public string TargetCountryCode  { get; set; }

        public bool IsInternal { get; set; }

        public AffiliateLinkType LinkType { get; set; } = AffiliateLinkType.AffiliateLink;
    }
}
