using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class CommissionReportServiceTest
    {
        private ICommissionReportRepository Repo;

        public CommissionReportServiceTest()
        {
            Repo = Substitute.For<ICommissionReportRepository>();
        }

        [Fact]
        public void GenerateCommissionReportData_WhenRepoReturnDataNotNull_NotThrowException()
        {
            var service = new CommissionReportService(Repo);
            Repo.GetAffiliateCommission(0, DateTime.Now).ReturnsForAnyArgs(GetAffiliateCommission());

            var result = service.GenerateCommissionReportData(1, DateTime.Now);

            Assert.NotNull(result);
        }

        private static IEnumerable<AffiliateCommission> GetAffiliateCommission()
            => new List<AffiliateCommission>
            {
                new AffiliateCommission
                {
                    MonthDate = new DateTime(2020, 01,01),
                    BankingFee = 100,
                    Bonus = 100,
                    CalculatedType = 0,
                    Commission = 100,
                    CommissionRate = 0.222m,
                    OperationalFee = 100,
                    PlatformFee = 100,
                    ProductId = 0,
                    ProductName = "test",
                    TurnOver = 1000,
                    WinLoss = 300
                },
                new AffiliateCommission
                {
                    MonthDate = new DateTime(2020, 01,01),
                    BankingFee = 100,
                    Bonus = 100,
                    CalculatedType = 0,
                    Commission = 100,
                    CommissionRate = 0.222m,
                    OperationalFee = 100,
                    PlatformFee = 100,
                    ProductId = 2,
                    ProductName = "test",
                    TurnOver = 1000,
                    WinLoss = 300
                },
                new AffiliateCommission
                {
                    MonthDate = new DateTime(2020, 01,01),
                    BankingFee = 100,
                    Bonus = 100,
                    CalculatedType = 0,
                    Commission = 100,
                    CommissionRate = 0.222m,
                    OperationalFee = 100,
                    PlatformFee = 100,
                    ProductId = 0,
                    ProductName = "test",
                    TurnOver = 1000,
                    WinLoss = 300
                },
                new AffiliateCommission
                {
                    MonthDate = new DateTime(2020, 02,01),
                    BankingFee = 100,
                    Bonus = 100,
                    CalculatedType = 1,
                    Commission = 100,
                    CommissionRate = 0.222m,
                    OperationalFee = 100,
                    PlatformFee = 100,
                    ProductId = 0,
                    ProductName = "test",
                    TurnOver = 1000,
                    WinLoss = 300
                }
            };
    }
}
