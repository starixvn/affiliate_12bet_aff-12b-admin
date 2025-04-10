using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class DeleteSpecificDomainControlParam : UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int SpecificDomainControlId { get; set; }
    }
}
