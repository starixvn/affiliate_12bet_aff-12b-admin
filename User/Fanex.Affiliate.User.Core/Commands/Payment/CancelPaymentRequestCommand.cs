namespace Fanex.Affiliate.User.Core.Commands.Payment
{
    public class CancelPaymentRequestCommand : BaseNonQueryCommand
    {
        public long TransId { get; set; }

        public string Status { get; set; }

        public int AffId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_User_PaymentRequest_Upd";

        public override bool IsValid() => AffId > 0 && TransId > 0;
    }
}
