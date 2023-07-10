
namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentUpdateLogParam : AffiliateBasicInfoParam
    {
        private const string SP_EXECUTE_UPDATE = "Aff_Admin_AffiliatePaymentRequest_Upd";

        public string OldData { get; set; }

        public string NewData { get; set; }

        public string SPName { get; } = SP_EXECUTE_UPDATE;
    }
}
