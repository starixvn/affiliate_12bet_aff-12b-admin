using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class AssignGroupParam : AffiliateBasicInfoParam
    {
        public int GroupId { get; set; }
    }
}
