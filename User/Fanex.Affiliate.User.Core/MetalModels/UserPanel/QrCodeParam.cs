namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    using Enum;

    public class QrCodeParam
    {
        public string SelectedLanguage { get; set; }

        public string SelectedPage { get; set; }

        public string SelectedChannel { get; set; }

        public int? SelectedDomainId { get; set; }

        public string SelectedTargetCountry { get; set; }

        public AffiliateLinkType SelectedLinkType { get; set; }
    }
}