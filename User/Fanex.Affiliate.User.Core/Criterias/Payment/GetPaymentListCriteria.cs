namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetPaymentListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_PaymentRequest_Sel";

        public int AffId { get; set; }

        public string PreferredPayment { get; set; }

        public decimal BalBroughtForward { get; set; }

        public decimal MinAmount { get; set; }

        public int Year { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
