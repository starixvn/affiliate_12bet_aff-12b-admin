namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Linq;
    using Constants;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using MetalModels.UserPanel;
    using Resources;
    using Utilities;
    using ViewModels.UserPanel;

    public class CommissionReportService : BaseDynamicService, ICommissionReportService
    {
        private const int PercentageUnit = 100;

        public CommissionReportService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        private static string GetCalculatedNote(int? type)
        {
            if (!type.HasValue || type == CommissionType.Calculated)
            {
                return string.Empty;
            }
            return type.Value == 0 ? $" ({Lang.Temporary})" : $" ({Lang.Uncalculated})";
        }

        public CommissionReportDataViewModel GenerateCommissionReportData(CommissionReportParam param)
        {
            var rawData = FetchTData<AffiliateCommission>(param.MapTo<GetAffiliateCommissionCriteria>());

            var byMonth = rawData.GroupBy(n => n.MonthDate)
                        .OrderByDescending(g => g.Key)
                        .Select(g => new AffiliateCommisionByMonth
                        {
                            Month = g.Key.Month,
                            DiplayMonth = g.Key.ToString("MMM-yyyy") + GetCalculatedNote(g.FirstOrDefault()?.CalculatedType),
                            Data = g.FirstOrDefault(i => i.ProductId == 0),
                            ProductData = g.Where(i => i.ProductId > 0).ToList()
                        }).ToList();

            var total = new AffiliateCommission();
            foreach (var item in byMonth)
            {
                item.Data.CommissionRate *= PercentageUnit;
                item.Data.SubCommRate *= PercentageUnit;

                total.TurnOver += item.Data.TurnOver;
                total.GGR += item.Data.GGR;
                total.PlatformFee += item.Data.PlatformFee;
                total.DepositFee += item.Data.DepositFee;
                total.WithdrawalFee += item.Data.WithdrawalFee;
                total.Bonus += item.Data.Bonus;
                total.Rebate += item.Data.Rebate;
                total.Commission += item.Data.Commission;
                total.P2pCommission += item.Data.P2pCommission;
                total.SubAffCommission += item.Data.SubAffCommission;
            }

            return new CommissionReportDataViewModel
            {
                Data = byMonth,
                Total = total
            };
        }
    }
}
