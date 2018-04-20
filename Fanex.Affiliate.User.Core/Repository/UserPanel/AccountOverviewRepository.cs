using System;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountOverviewRepository : IAccountOverviewRepository
    {
        public AccountOverviewStatistics GetStatistics(int userId, DateTime month)
        {
            const string uSpKey = "Aff_StatisticsSel";

            using (var dataBox = new ObjectDBBuilder(uSpKey))
            {
                var db = dataBox.DB;

                var parameterSet = new
                {
                    AffId = userId,
                    MonthDate = month,
                    TotalQualifiedMembers = default(int?),
                    TotalQualifiedCasino = 0,
                    TotalQualifiedMembersHR = 0,
                    TotalActivePlayer = 0,
                    TotalPlayerDepUp = 0,
                    TotalPlayer = 0,
                    NewSignUpDep = 0,
                    NewSignUp = 0,
                    BalBroughtforward = 0m,
                    TotalDeposit = 0m,
                    TotalDepositUp = 0m,
                    TotalTurnover = 0m,
                    TotalTurnoverUp = 0m,
                    TotalWinlostUp = 0m,
                    TotalWinlost = 0m,
                    TotalUniqueClicks = 0,
                    TotalClicks = 0,
                    TotalUniqueClicksBySite = 0,
                    TotalClicksBySite = 0,
                    TotalUniqueClicksByMobile = 0,
                    TotalClicksByMobile = 0
                };

                db.ExecuteNonQuery(parameterSet);

                return new AccountOverviewStatistics
                {
                    NewSignup = parameterSet.NewSignUp,
                    TotalActivePlayer = parameterSet.TotalActivePlayer,
                    TotalQualifiedMember = parameterSet.TotalQualifiedMembers,
                    TotalQualifiedCasino = parameterSet.TotalQualifiedCasino,
                    TotalDeposit = parameterSet.TotalDeposit,
                    Turnover = parameterSet.TotalTurnover,
                    TotalPlayerUpToNow = parameterSet.TotalPlayer,
                    TurnoverUpToNow = parameterSet.TotalTurnoverUp,
                    TotalDepositUpToNow = parameterSet.TotalDepositUp,
                    BalanceBroughtForward = parameterSet.BalBroughtforward
                };
            }
        }
    }
}