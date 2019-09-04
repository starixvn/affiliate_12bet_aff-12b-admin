using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate
{
    public class ExchangeRateParam
    {
        public int ExchangeId { get; set; }

        [RegularExpression(@"^\d+\.?\d*$")]
        public decimal Rate { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        [Range(0, 1)]
        public byte RateType { get; set; }

        public bool IsValid()
        {
            return Rate != 0 && Rate > Money.MinValue
                    && Rate < Money.MaxValue;
        }
    }
}
