using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class AffiliatePagingParam : PagingParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int AffId { get; set; }
    }
}
