using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class StatementHistoryService : IStatementHistoryService
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;
        private readonly ISessionService _sessionService;

        public StatementHistoryService(IStatementHistoryRepository statementHistoryRepository, ISessionService sessionService)
        {
            _statementHistoryRepository = statementHistoryRepository;
            _sessionService = sessionService;
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
                total.WinLost += item.WinLost;
                total.Turnover += item.Turnover;
                total.Deposit += item.Deposit;
                total.Withdrawal += item.Withdrawal;
                total.Bonus += item.Bonus;
                total.BankingFee += item.BankingFee;
            }

            return new DailyEarningDetailResult
            {
                MainData = rawData,
                Total = total
            };
        }

        public EntityResult<StatementHistory> GetStatementHistoryList(AffiliatePagingByDateParam param)
        {
            var data = _statementHistoryRepository.GetStatementHistoryList(param);
            data.MainData = FilterDataHaveClaimToQuery(data.MainData);
            return data;
        }

        private IEnumerable<StatementHistory> FilterDataHaveClaimToQuery(IEnumerable<StatementHistory> mainData)
        {
            var claimPrincipal = _sessionService.UserClaim;
            var result = mainData;
            if (!claimPrincipal.HasClaim(Function.AffAdjustment))
            {
                result = result.Where(r => r.TransType != Code.Adjustment);
            }

            if (!claimPrincipal.HasClaim(Function.PaymentRequest))
            {
                result = result.Where(r => r.TransType != Code.PaymentRequest);
            }

            return result;
        }
    }
}
