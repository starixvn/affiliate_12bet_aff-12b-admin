using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AffiliateReportViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }
    }
}
