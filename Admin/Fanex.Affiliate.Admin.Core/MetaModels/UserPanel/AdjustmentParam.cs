using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class AdjustmentParam : AffiliateBasicInfoParam
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public bool IsValid(decimal absBalanceBf)
        {
            return Amount != 0 && Amount - absBalanceBf > Money.MinValue
                    && Amount + absBalanceBf < Money.MaxValue;
        }
    }
}

