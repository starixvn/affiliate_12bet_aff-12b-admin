using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class StatementHistoryService : IStatementHistoryService
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public StatementHistoryService(IStatementHistoryRepository statementHistoryRepository)
        {
            _statementHistoryRepository = statementHistoryRepository;
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam param)
        {
            return _statementHistoryRepository.GetDailyEarning(param);
        }

        public DailyEarningDetailResult GetDailyEarningDetail(DailyEarningParam param)
        {
            var rawData = _statementHistoryRepository.GetDailyEarningDetail(param);

            var total = new DailyEarningDetail();

            foreach (var item in rawData)
            {
                total.Deposit += item.Deposit;
                total.Withdrawal += item.Withdrawal;
                total.Turnover += item.Turnover;
                total.WinLost += item.WinLost;
                total.Bonus += item.Bonus;
            }

            return new DailyEarningDetailResult
            {
                MainData = rawData,
                Total = total
            };
        }
    }
}
