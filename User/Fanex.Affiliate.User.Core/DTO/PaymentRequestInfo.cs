namespace Fanex.Affiliate.User.Core.DTO
{
    public class PaymentRequestInfo
    {
        public long TransId { get; set; }

        public int AffId { get; set; }

        public decimal Amount { get; set; }

        public string IpAddress { get; set; }

        public string Language { get; set; }
    }
}
