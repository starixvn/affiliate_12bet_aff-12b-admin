namespace Fanex.Affiliate.Admin.Core.DTO.ExchangeRate
{
    public class ExchangeRateInfo
    {
        public int ExchangeId { get; set; }

        public string Currency { get; set; }

        public string ActualRate { get; set; }

        public string BuyingRate { get; set; }
    }
}
