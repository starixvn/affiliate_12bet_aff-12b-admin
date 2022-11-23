using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class UpdateDomainParam : InsertDomainParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainId { get; set; }
    }
}
