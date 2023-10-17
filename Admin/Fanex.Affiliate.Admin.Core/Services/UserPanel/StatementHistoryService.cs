using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
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
            var customStatementList = new List<StatementHistory>();
            var data = _statementHistoryRepository.GetStatementHistoryList(param);
            var mainData = FilterDataHaveClaimToQuery(data.MainData);

            foreach (var item in mainData)
            {
                if (item.TransType == TransType.MonthlyEarning || item.TransType == TransType.NewFdmCommissionBonus)
                {
                    if (CheckIfExistMonthlyTotal(item.Date, customStatementList))
                    {
                        continue;
                    }
                    customStatementList.Add(CreateMonthlyTotal(item.Date, mainData));
                }
                else
                {
                    customStatementList.Add(item);
                }
            }

            return new EntityResult<StatementHistory>
            {
                MainData = customStatementList,
                TotalItem = mainData.Count()
            };
        }

        private IEnumerable<StatementHistory> FilterDataHaveClaimToQuery(IEnumerable<StatementHistory> mainData)
        {
            var claimPrincipal = _sessionService.UserClaim;
            var result = mainData;
            if (!claimPrincipal.HasClaim(Function.AffAdjustment))
            {
                result = result.Where(r => r.TransType != TransType.Adjustment);
            }

            if (!claimPrincipal.HasClaim(Function.PaymentRequest))
            {
                result = result.Where(r => r.TransType != TransType.PaymentRequest);
            }

            return result;
        }

        private static StatementHistory CreateMonthlyTotal(DateTime monthDate, IEnumerable<StatementHistory> statementHistoryList)
        {
            var children = statementHistoryList.Where(s => s.Date == monthDate)
                .Where(s => s.TransType == TransType.MonthlyEarning || s.TransType == TransType.NewFdmCommissionBonus);

            var monthlyCom = children.FirstOrDefault(s => s.TransType == TransType.MonthlyEarning);

            var monthlyTotal = new StatementHistory
            {
                Creator = monthlyCom?.Creator ?? children.FirstOrDefault().Creator,
                Amount = children.Sum(s => s.Amount),
                Date = monthDate,
                Children = children,
                Status = monthlyCom?.Status ?? children.FirstOrDefault().Status,
                TransType = TransType.MonthlyTotal,
                Description = monthlyCom?.Description ?? children.FirstOrDefault().Description
            };
            return monthlyTotal;
        }

        private static bool CheckIfExistMonthlyTotal(DateTime monthDate, List<StatementHistory> customStatementList)
        {
            return customStatementList.Any(m => m.Date == monthDate && m.TransType == TransType.MonthlyTotal);
        }
    }
}
