using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Payment
{
    public class PaymentApproveParam : UserInfoParam
    {
        public string RefId { get; set; }

        public string AffUserName { get; set; }

        [SpParam(SqlParamName = "Player_IBCCustID")]
        public string PlayerIbcCustId { get; set; }

        [SpParam(SqlParamName = "Player_Currency")]
        public string PlayerCurrency { get; set; }

        [SpParam(SqlParamName = "Ballance_CustID")]
        public string BallanceCustId { get; set; }

        public string AffPayout { get; set; }

        [SpParam(Ignored = true)]
        public string SpKey { get; set; }
    }
}
