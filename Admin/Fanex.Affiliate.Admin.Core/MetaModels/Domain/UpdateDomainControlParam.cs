using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class UpdateDomainControlParam : InsertDomainControlParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "The Domain Control is invalid!")]
        public int DomainControlId { get; set; }
    }
}
