using Fanex.Data;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class DepositCustomerInfo
    {
        [SpParam(Ignored = true)]
        public int Success { get; set; }

        public string DepositUserName { get; set; }

        public int DepositUserId { get; set; }

        public string Password { get; set; }

        public string OddsType { get; set; }

        public int AgentId { get; set; }

        public int AffiliateId { get; set; }

        public string Country { get; set; }

        [SpParam(Ignored = true)]
        public bool IsValid => Success == 0;
    }
}
