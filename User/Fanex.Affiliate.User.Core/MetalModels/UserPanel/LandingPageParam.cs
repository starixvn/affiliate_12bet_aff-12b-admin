namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Enum;

    public class LandingPageParam
    {
        public string CommGroupName { get; set; }

        public string TargetCountryCode  { get; set; }

        public bool IsInternal { get; set; }

        public AffiliateLinkType LinkType { get; set; } = AffiliateLinkType.AffiliateLink;
    }
}
