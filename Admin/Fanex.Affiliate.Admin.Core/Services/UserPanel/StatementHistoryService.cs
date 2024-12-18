using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.StatementHistory;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class StatementHistoryService : BaseDynamicService, IStatementHistoryService
    {
        public StatementHistoryService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<SubAffiliate> GetSubAffiliate(SubAffiliateParam param)
        {
            var criteria = new SubAffiliateCriteria
            {
                AffId = param.AffId,
                MonthDate = param.MonthDate,
                CurrencyId = param.CurrencyId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            var (dataList, itemTotal) = DynamicRepository.FetchMultiple<SubAffiliate, SubAffiliate>(criteria);

            return new EntityResult<SubAffiliate>
            {
                MainData = dataList,
                Total = itemTotal.FirstOrDefault(),
                TotalItem = criteria.TotalItem,
            };
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam param)
        {
            const string SpKey = "Aff_DailyEarning_Sel";

            var criteria = GetDailyEarningCriteria(SpKey, param);

            var (dataList, itemTotal) = DynamicRepository.FetchMultiple<DailyEarning, DailyEarning>(criteria);

            return new DailyEarningResult
            {
                MainData = dataList,
                Total = itemTotal.FirstOrDefault()
            };
        }

        public DailyEarningDetailResult GetDailyEarningDetail(DailyEarningParam param)
        {
            const string SpKey = "Aff_DailyEarning_Detail_Sel";

            var criteria = GetDailyEarningCriteria(SpKey, param);

            var rawData = FetchTData<DailyEarningDetail>(criteria);

            var total = new DailyEarningDetail();

            foreach (var item in rawData)
            {
                total.Turnover += item.Turnover;
                total.Ggr += item.Ggr;
                total.Deposit += item.Deposit;
                total.Withdrawal += item.Withdrawal;
                total.Bonus += item.Bonus;
                total.Rebate += item.Rebate;
            }

            return new DailyEarningDetailResult
            {
                MainData = rawData,
                Total = total
            };
        }

        public EntityResult<StatementHistory> GetStatementHistoryList(AffiliatePagingByDateParam param)
        {
            var vms = new List<StatementHistory>();
            var critera = new StatementHistoryCriteria
            {
                AffId = param.AffId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
            };
            var result = FetchEntityResult<StatementHistory>(critera);

            foreach (var item in result.MainData)
            {
                if (item.TransType == TransType.MonthlyEarning || item.TransType == TransType.SubAffiliate)
                {
                    if (CheckIfExistMonthlyTotal(item.MonthDate, vms))
                    {
                        continue;
                    }
                    vms.Add(CreateMonthlyTotal(item.MonthDate, result.MainData));
                }
                else
                {
                    vms.Add(item);
                }
            }

            return new EntityResult<StatementHistory>
            {
                MainData = vms,
                TotalItem = result.TotalItem
            };
        }

        private static StatementHistory CreateMonthlyTotal(DateTime monthDate, IEnumerable<StatementHistory> statementHistoryList)
        {
            var children = statementHistoryList.Where(s => s.MonthDate == monthDate)
                .Where(s => s.TransType == TransType.SubAffiliate || s.TransType == TransType.MonthlyEarning);

            var monthlyCom = children.FirstOrDefault(s => s.TransType == TransType.MonthlyEarning);

            return GetMonthlyTotal(monthDate, monthlyCom, children);
        }

        private static StatementHistory GetMonthlyTotal(DateTime monthDate, StatementHistory monthlyCom, IEnumerable<StatementHistory> children)
        {
            return new StatementHistory
            {
                Creator = monthlyCom?.Creator ?? children.FirstOrDefault()?.Creator,
                Amount = children.Sum(s => s.Amount),
                MonthDate = monthDate,
                Children = children,
                Status = monthlyCom?.Status ?? children.FirstOrDefault()?.Status,
                TransType = TransType.MonthlyTotal,
                CalculatedType = monthlyCom?.CalculatedType ?? children.FirstOrDefault()?.CalculatedType
            };
        }

        private static bool CheckIfExistMonthlyTotal(DateTime monthDate, List<StatementHistory> vms)
        {
            return vms.Exists(m => m.MonthDate == monthDate && m.TransType == TransType.MonthlyTotal);
        }

        private static DailyEarningCriteria GetDailyEarningCriteria(string spKey, DailyEarningParam param)
        {
            return new DailyEarningCriteria(spKey)
            {
                AffId = param.AffId,
                ProductId = param.ProductId,
                MonthDate = param.MonthDate,
                BaseCurrencyId = param.BaseCurrencyId,
                CurrencyIds = param.CurrencyIds,
            };
        }
    }
}