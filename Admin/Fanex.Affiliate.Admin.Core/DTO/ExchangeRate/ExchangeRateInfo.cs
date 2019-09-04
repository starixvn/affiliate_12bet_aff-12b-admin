namespace Fanex.Affiliate.Admin.Core.DTO.ExchangeRate
{
    public class ExchangeRateInfo
    {
        public int ExchangeId { get; set; }

        public string Currency { get; set; }

        public decimal ActualRate { get; set; }

        public decimal BuyingRate { get; set; }
    }
}
