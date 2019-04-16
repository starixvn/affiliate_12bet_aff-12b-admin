using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class AdjustmentParam : AffiliateBasicInfoParam
    {
        [RegularExpression(@"^\-?\d+\.?\d*$")]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
