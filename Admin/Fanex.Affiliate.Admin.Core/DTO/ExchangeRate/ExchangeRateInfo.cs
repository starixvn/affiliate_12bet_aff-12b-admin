namespace Fanex.Affiliate.Admin.Core.DTO.ExchangeRate
{
    public class ExchangeRateInfo
    {
        public int ExchangeId { get; set; }

        public string Currency { get; set; }

        public double ActualRate { get; set; }

        public double BuyingRate { get; set; }
    }
}
