using System;
using System.Linq;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CommissionReportService : ICommissionReportService
    {
        private const int DecimalDigit = 2;
        private const int PercentageUnit = 100;

        private readonly ICommissionReportRepository _commissionReportRepository;
        public CommissionReportService(ICommissionReportRepository commissionReportRepository)
        {
            _commissionReportRepository = commissionReportRepository;
        }
        private static string GetCalculatedNote(int? type)
        {
            if (!type.HasValue || type == CommissionType.Calculated)
            {
                return string.Empty;
            }
            return type.Value == 0 ? $" ({Lang.Temporary})" : $" ({Lang.Uncalculated})";
        }
        public CommissionReportDataViewModel GenerateCommissionReportData(int affId, DateTime winlostDate)
        {
            var rawData = _commissionReportRepository.GetAffiliateCommission(affId, winlostDate);

            var byMonth =
                    (from n in rawData
                     group n by n.MonthDate into g
                     orderby g.Key descending
                     select new AffiliateCommisionByMonth
                     {
                         Month = g.Key.Month,
                         DiplayMonth = g.Key.ToString("MMM-yyyy") + GetCalculatedNote(g.FirstOrDefault()?.CalculatedType),
                         Data = g.FirstOrDefault(i => i.ProductId == 0),
                         ProductData = g.Where(i => i.ProductId > 0).ToList()

                     }).ToList();

            var total = new AffiliateCommission();
            foreach (var item in byMonth)
            {
                item.Data.CommissionRate = decimal.Round(item.Data.CommissionRate, DecimalDigit) * PercentageUnit;

                total.TurnOver += item.Data.TurnOver;
                total.WinLoss += item.Data.WinLoss;
                total.PlatformFee += item.Data.PlatformFee;
                total.OperationalFee += item.Data.OperationalFee;
                total.BankingFee += item.Data.BankingFee;
                total.Bonus += item.Data.Bonus;
                total.Commission += item.Data.Commission;
            }

            return new CommissionReportDataViewModel
            {
                Data = byMonth,
                Total = total
            };
        }
    }
}
