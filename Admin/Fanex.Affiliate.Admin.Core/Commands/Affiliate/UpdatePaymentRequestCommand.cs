
namespace Fanex.Affiliate.Admin.Core.Commands.Affiliate
{
    public class UpdatePaymentRequestCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliatePaymentRequest_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => TransId > 0;

        public long TransId { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }
    }
}
