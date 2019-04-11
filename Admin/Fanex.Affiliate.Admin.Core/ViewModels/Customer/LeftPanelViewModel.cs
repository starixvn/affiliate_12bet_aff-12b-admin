using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Customer
{
    public class LeftPanelViewModel
    {
        public string AffName { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
