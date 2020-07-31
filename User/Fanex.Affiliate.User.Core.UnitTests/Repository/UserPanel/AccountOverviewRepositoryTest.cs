using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class AccountOverviewRepositoryTest : RepositoryTestBase<AccountOverviewRepository>
    {
        [Fact]
        public void GetLastLoginInfo_WhenCall_ReturnLastLoginInfo()
        {
            const string uSpKey = "Aff_User_LoginInfo_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.QueryEntity<LastLoginInfo>(null).ReturnsForAnyArgs(new LastLoginInfo
            {
                LastLoginIP = "",
                LastLoginDate = DateTime.Now,
            });

            var result = repo.GetLastLoginInfo(1);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPlayerDetailList_WhenCall_ReturnListPlayerDetail()
        {
            const string uSpKey = "Aff_User_Overview_PlayersDetail_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.Query<PlayerDetail>(null).ReturnsForAnyArgs(new List<PlayerDetail>
            {
                new PlayerDetail
                {
                    BankingFee = 100, Bonus = 100, Deposit = 100, RegisteredDate= DateTime.Now, Turnover = 100, Username = "name", Winloss = 100, Withdrawal = 100
                }
            });

            var result =  repo.GetPlayerDetailList(new MetalModels.UserPanel.PlayerDetailParam
            {
                AffId = 1,
                IsSortAsc = true,
                MonthDate = DateTime.Now,
                NRows = 10,
                PageIndex = 1,
                PageSize = 10,
                SortById = 1,
            });

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetUserOverviewDetailList_WhenCall_QueryOverviewDetail()
        {
            const string uSpKey = "Aff_User_Overview_Detail_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.Query<OverviewDetail>(null).ReturnsForAnyArgs(new List<OverviewDetail>
            {
                new OverviewDetail
                {
                    ActiveMembers = 3, Deposit = 100, DepositedMembers = 2, QualifiedActiveMembers = 2, Signups = 1, Turnover = 100,
                    Winloss = 100, WinlostDate = DateTime.Now, Withdrawal = 100
                }
            });

            var result = repo.GetUserOverviewDetailList(1, DateTime.Now);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetUserOverviewSummary_WhenCall_ReturnResult()
        {
            const string uSpKey = "Aff_User_Overview_Summary_Sel";
            var repo = CreateRepo();
            var db = Builder.Build(null, null);
            db.QueryEntity<OverviewSummary>(null).ReturnsForAnyArgs(new OverviewSummary
            {
                ActivePlayers = 2,
                BalanceBroughtForward = 100,
                Commission = 10,
                Deposit = 100,
                DepositedMembers = 2,
                QualifiedActivePlayers = 1,
                Signups = 2,
                Turnover = 100
            });

            var result = repo.GetUserOverviewSummary(1, DateTime.Now);

            Assert.Equal(uSpKey, db.SettingKey);
            Assert.NotNull(result);
        }
    }
}
