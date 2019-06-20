using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class AffiliatePagingParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int AffId { get; set; }

        [Range(Validation.MinPageIndex, Validation.MaxPageIndex)]
        public int PageIndex { get; set; }

        [Range(Validation.MinPageSize, Validation.MaxPageSize)]
        public int PageSize { get; set; }
    }
}
