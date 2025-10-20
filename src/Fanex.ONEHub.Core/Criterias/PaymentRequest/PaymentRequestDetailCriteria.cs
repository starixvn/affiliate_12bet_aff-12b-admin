using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.PaymentRequest
{
    public class PaymentRequestDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_AffiliatePaymentRequest_Detail_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => TransId > 0;

        public long TransId { get; set; }
    }
}
