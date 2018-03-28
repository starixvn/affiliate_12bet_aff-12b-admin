using Fanex.Affiliate.User.Core.Extensions;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class AccountOverviewStatistics
    {
        public int NewSignup { get; set; }

        public int TotalActivePlayer { get; set; }

        public int? TotalQualifiedMember { get; set; }

        public int TotalQualifiedCasino { get; set; }

        public decimal TotalDeposit { get; set; }

        public decimal Turnover { get; set; }

        public int TotalPlayerUpToNow { get; set; }

        public decimal TurnoverUpToNow { get; set; }

        public decimal TotalDepositUpToNow { get; set; }

        public decimal BalanceBroughtForward { get; set; }

        public string TotalQualifiedActivePlayer
        {
            get
            {
                return !TotalQualifiedMember.HasValue ? "N/A" :
                    TotalQualifiedMember.Value.ToStringNumber() + "/" + TotalQualifiedCasino.ToStringNumber();
            }
        }
    }
}