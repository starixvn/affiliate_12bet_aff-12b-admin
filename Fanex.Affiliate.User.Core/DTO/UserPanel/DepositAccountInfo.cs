using Fanex.Data;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class DepositAccountInfo
    {
        [SpParam(Ignored = true)]
        public int Success { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DepositUserName { get; set; }

        public int DepositUserId { get; set; }

        public string Password { get; set; }

        public string OddsType { get; set; }

        public int AgentId { get; set; }

        public int AffiliateId { get; set; }

        public string BonusCode { get; set; }

        public string Country { get; set; }

        [SpParam(Ignored = true)]
        public bool IsValid => Success == 0;
    }
}
