using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.UserAccount
{
    public class PaymentMethodCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_PaymentMethod_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public decimal MinAmount { get; set; }
    }
}
