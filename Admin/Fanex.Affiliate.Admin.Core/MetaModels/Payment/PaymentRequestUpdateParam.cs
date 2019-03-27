namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentRequestUpdateParam
    {
        public long TransId { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

    }
}
