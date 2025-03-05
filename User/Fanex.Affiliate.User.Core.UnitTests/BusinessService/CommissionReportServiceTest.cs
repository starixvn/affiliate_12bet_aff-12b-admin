namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Core.Constants;
    using Core.Utilities;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using MetalModels.UserPanel;
    using Moq;
    using System.Collections.Generic;
    using Xunit;

    public class CommissionReportServiceTest
    {
        private readonly Mock<IDynamicRepository> _repository;

        public CommissionReportServiceTest()
        {
            _repository = new Mock<IDynamicRepository> { CallBase = true };
        }

        [Fact]
        public void GenerateCommissionReportData_WhenRepoReturnDataNotNull_NotThrowException()
        {
            var param = new CommissionReportParam { AffId = 1, BaseCurrency = 20, WinLostDate = DateTimeHelper.Now };
            _repository.Setup(_ => _.Fetch<AffiliateCommission>(It.IsAny<GetAffiliateCommissionCriteria>(), string.Empty))
                .Returns(GetAffiliateCommission());

            var service = new CommissionReportService(_repository.Object);

            var result = service.GenerateCommissionReportData(param);

            Assert.NotNull(result);
        }

        private static IEnumerable<AffiliateCommission> GetAffiliateCommission()
            => new List<AffiliateCommission>
            {
            new AffiliateCommission
            {
                MonthDate = DateTimeHelper.CreateDateTime(2020, 01,01),
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
                MonthDate = DateTimeHelper.CreateDateTime(2020, 01,01),
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
                MonthDate = DateTimeHelper.CreateDateTime(2020, 01, 01),
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
                MonthDate = DateTimeHelper.CreateDateTime(2020, 02,01),
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
