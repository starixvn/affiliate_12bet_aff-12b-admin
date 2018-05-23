using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class MarketingViewModel
    {
        public SearchFormViewModel SearchForm { get; set; }

        public LinkFormViewModel LinkForm { get; set; }

        public IDictionary<string, string> TextLinkList { get; set; }

        public IEnumerable<MarketingItem> MarketingDataList { get; set; }
    }
}
