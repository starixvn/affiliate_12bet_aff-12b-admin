using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class AssignGroupParam : AffiliateBasicInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "GroupId not exists!")]
        public int GroupId { get; set; }
    }
}
