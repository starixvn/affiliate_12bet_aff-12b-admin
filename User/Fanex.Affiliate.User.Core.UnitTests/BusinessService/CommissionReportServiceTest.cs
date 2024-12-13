namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System;
    using System.Collections.Generic;
    using Core.Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using MetalModels.UserPanel;
    using NSubstitute;
    using Xunit;

    public class CommissionReportServiceTest
    {
        private ICommissionReportRepository Repo { get; }

        public CommissionReportServiceTest()
        {
            Repo = Substitute.For<ICommissionReportRepository>();
        }

        [Fact]
        public void GenerateCommissionReportData_WhenRepoReturnDataNotNull_NotThrowException()
        {
            var service = new CommissionReportService(Repo);
            var param = new CommissionReportParam { AffId = 1, BaseCurrency = 20, WinLostDate = DateTime.Now };
            Repo.GetAffiliateCommission(param).ReturnsForAnyArgs(GetAffiliateCommission());

            var result = service.GenerateCommissionReportData(param);

            Assert.NotNull(result);
        }

        private static IEnumerable<AffiliateCommission> GetAffiliateCommission()
            => new List<AffiliateCommission>
            {
            new AffiliateCommission
            {
                MonthDate = new DateTime(2020, 01,01),
                Bonus = 100,
                CalculatedType = 0,
                Commission = 100,
                CommissionRate = 0.222m,
                PlatformFee = 100,
                ProductId = 0,
                ProductName = "test",
                TurnOver = 1000,
                EarningBy = EarningByType.Turnover,
                IsMultiGroupProduct = true,
                GGR = 300
            },
            new AffiliateCommission
            {
                MonthDate = new DateTime(2020, 01,01),
                Bonus = 100,
                CalculatedType = 0,
                Commission = 100,
                CommissionRate = 0.222m,
                PlatformFee = 100,
                ProductId = 2,
                ProductName = "test",
                TurnOver = 1000,
                EarningBy = EarningByType.Turnover,
                IsMultiGroupProduct = false,
                GGR = 300
            },
            new AffiliateCommission
            {
                MonthDate = new DateTime(2020, 01,01),
                Bonus = 100,
                CalculatedType = 1,
                Commission = 100,
                CommissionRate = 0.222m,
                PlatformFee = 100,
                ProductId = 0,
                ProductName = "test",
                TurnOver = 1000,
                EarningBy = EarningByType.Winloss,
                IsMultiGroupProduct = true,
                GGR = 300
            },
            new AffiliateCommission
            {
                MonthDate = new DateTime(2020, 02,01),
                Bonus = 100,
                CalculatedType = 2,
                Commission = 100,
                CommissionRate = 0.222m,
                PlatformFee = 100,
                ProductId = 0,
                ProductName = "test",
                TurnOver = 1000,
                EarningBy = EarningByType.Winloss,
                IsMultiGroupProduct = false,
                GGR = 300
            }
            };
    }
}
