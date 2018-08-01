using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CommissionReportService : ICommissionReportService
    {
        private readonly ICommissionReportRepository _commissionReportRepository;
        public CommissionReportService(ICommissionReportRepository commissionReportRepository)
        {
            _commissionReportRepository = commissionReportRepository;
        }
        private static string GetCalculatedNote(int? type)
        {
            if (!type.HasValue || type == 2)
            {
                return string.Empty;
            }
            return type.Value == 0 ? $"({Lang.Temporary})" : $"({Lang.Uncalculated})";
        }
        public CommissionReportViewModel GenerateCommissionReportData(int affId, DateTime winlostdate)
        {
            var rawData = _commissionReportRepository.GetAffiliateCommission(affId, winlostdate);

            var byMonth =
                    (from n in rawData
                     group n by n.MonthDate into g
                     orderby g.Key
                     select new AffiliateCommisionByMonth
                     {
                         Month = g.Key.Month,
                         DiplayMonth = g.Key.ToString("MMM-yyyy") + GetCalculatedNote(g.FirstOrDefault()?.CalculatedType),
                         Data = g.FirstOrDefault(),
                         ProductData = g.Where(i => i.ProductId > 0).ToList()

                     }).ToList();

            var total = new AffiliateCommission();
            foreach (var item in byMonth)
            {
                total.TurnOver += item.Data.TurnOver;
                total.WinLoss += item.Data.WinLoss;
                total.PlatformFee += item.Data.PlatformFee;
                total.OperationalFee += item.Data.OperationalFee;
                total.BankingFee += item.Data.BankingFee;
                total.Bonus += item.Data.Bonus;
                total.Commission += item.Data.Commission;
            }

            return new CommissionReportViewModel
            {
                Data = byMonth,
                Total = total
            };
        }
    }
}
