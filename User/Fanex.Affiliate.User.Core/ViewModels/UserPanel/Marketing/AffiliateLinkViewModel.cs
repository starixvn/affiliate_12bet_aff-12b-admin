using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class AffiliateLinkViewModel
    {
        public LinkFormViewModel LinkForm { get; set; }

        public IDictionary<string, QrCodeLinkModel> QrCodeList { get; set; }
    }
}
