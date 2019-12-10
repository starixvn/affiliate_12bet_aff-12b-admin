using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate
{
    public class ExchangeRateParam: UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int ExchangeId { get; set; }

        public decimal Rate { get; set; }

        [Range(0, 1)]
        public byte RateType { get; set; }

        public bool IsValid()
        {
            return Rate > 0 && Rate < Money.MaxValue;
        }
    }
}
