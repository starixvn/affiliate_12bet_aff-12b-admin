namespace Fanex.Affiliate.User.Core.Commands.Payment
{
    using Data;

    public class CreatePaymentRequestCommand : BaseNonQueryCommand
    {
        public long TransId { get; set; }

        public int AffId { get; set; }

        public decimal Amount { get; set; }

        [SpParam(SqlParamName = "IP")]
        public string IpAddress { get; set; }

        public string Language { get; set; }

        public override string GetSettingKey() => "Aff_User_PaymentRequest_Ins";

        public override bool IsValid() => AffId > 0;
    }
}
