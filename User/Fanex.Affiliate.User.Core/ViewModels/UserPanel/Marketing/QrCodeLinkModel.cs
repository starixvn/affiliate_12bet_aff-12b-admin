
namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using DTO;

    public class QrCodeLinkModel
    {
        public string Link { get; set; }

        public string QrCode { get; set; }

        public Language Language { get; set; }

        public string DisplayName { get; set; }

        public bool IsShow { get; set; }
    }
}
