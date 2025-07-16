using Fanex.Affiliate.User.Core.Enum;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class LinkToParam
    {
        public AffiliateLinkType LinkType { get; set; } = AffiliateLinkType.AffiliateLink;

        public string TargetCountryCode { get; set; }

        public int SelectedDomainId { get; set; }
    }
}
