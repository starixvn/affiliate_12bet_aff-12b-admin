using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public string AffName { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
