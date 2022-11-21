
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class DeleteDomainParam : UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainId { get; set; }

        [SpParam(Ignored = true)]
        public bool IsAffDomain { get; set; }
    }
}
