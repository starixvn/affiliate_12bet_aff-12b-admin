using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public string AffName { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
