
namespace Fanex.Affiliate.Admin.Core.Criterias.PaymentRequest
{
    public class PaymentRequestCriteria : PagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_AffiliatePaymentRequest_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int SearchById { get; set; }

        public string Username { get; set; }

        public string Status { get; set; }

        public bool IsGetAll { get; set; }
    }
}
