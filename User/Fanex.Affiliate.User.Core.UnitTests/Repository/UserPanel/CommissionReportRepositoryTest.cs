using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class CommissionReportRepositoryTest : RepositoryTestBase<CommissionReportRepository>
    {
        [Fact]
        public void GetAffiliateCommission_WhenCall_QueryAffiliateCommissions()
        {
            const string uSpKey = "Aff_User_Rpt_Commission";
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.Query<AffiliateCommission>().ReturnsForAnyArgs(new List<AffiliateCommission>
            {
                new AffiliateCommission
                {
                    BankingFee = 100, Bonus = 100, CalculatedType =1, Commission = 100, CommissionRate =100,
                    MonthDate = DateTime.Now, OperationalFee = 100, PlatformFee = 100, ProductId = 1, ProductName = "saba", TurnOver = 100, WinLoss = 100
                }
            });

            var result = repo.GetAffiliateCommission(1, DateTime.Now);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
