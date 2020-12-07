using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class StatementHistoryRepositoryTest : RepositoryTestBase<StatementHistoryRepository>
    {
        [Theory]
        [InlineData(true, (int)TransType.Payment, "running")]
        [InlineData(false, (int)TransType.Adjustment, "pending")]
        [InlineData(false, (int)TransType.Commission, "completed")]
        [InlineData(false, 123, "cancelled")]
        [InlineData(false, 123, "approved")]
        [InlineData(false, 123, "rejected")]
        [InlineData(false, 123, "current")]
        [InlineData(false, 123, "test")]
        [InlineData(false, 123, "")]
        public void GetStatementHistory_WhenCall_QueryStatementHistory(bool isInternal, int transType, string status)
        {
            const string uSpKey = "Aff_User_StatementHistory_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var param = new StatementHistoryParam
            {
                IsInternal = isInternal
            };
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<StatementHistory>(param).Returns(new List<StatementHistory> {
                new StatementHistory {
                    MonthDate = new DateTime(2020, 12, 07),
                    TransType = transType,
                    Amount = 0,
                    Status = status,
                    Description = "test"
                }
            });
            var repo = CreateRepo();

            var result = repo.GetStatementHistory(param);
            var item = result.FirstOrDefault();

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
            Assert.NotNull(item.DateString);
            Assert.NotNull(item.DisplayStatus);
            Assert.NotNull(item.TransTypeString);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetProductList_WhenCall_QueryProduct(bool isInternal)
        {
            const string uSpKey = "Aff_ProductList_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<ProductItem>().Returns(new List<ProductItem> { new ProductItem { } });
            var repo = CreateRepo();

            var result = repo.GetProductList(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDailyEarning_WhenCall_ReturnDailyEarningResult(bool isInternal)
        {
            const string uSpKey = "Aff_DailyEarning_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var param = new DailyEarningParam { IsInternal = isInternal };
            var db = Builder.Build(uSpKey, dbMode);
            var multipleQuery = db.QueryMultiple(param);
            multipleQuery.Read<DailyEarning>().Returns(new List<DailyEarning>
            {
                new DailyEarning { Bonus = 100, Deposit = 100, MemberWinLost = 100, MemberWinLostFake = 100, WinlostDate = DateTime.Now, Withdrawal = 100 }
            });
            multipleQuery.ReadFirstOrDefault<DailyEarning>().Returns(new DailyEarning { });
            var repo = CreateRepo();

            var result = repo.GetDailyEarning(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result.MainDataList);
            Assert.NotNull(result.Total);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void GetExtraUserInfo_WhenCall_ReturNotNullResult(bool isInternal, bool isReturnNullInfo)
        {
            const string uSpKey = "Aff_AffiliateUser_ExtraUserInfoSel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var extraUserInfo = isReturnNullInfo ? null : new ExtraUserInfo { GroupName = "abc", HasDailyDetail = false };
            var db = Builder.Build(uSpKey, dbMode);
            db.QueryEntity<ExtraUserInfo>().Returns(extraUserInfo);
            var repo = CreateRepo();

            var result = repo.GetExtraUserInfo(1, isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDailyEarningDetail_WhenCall_QueryDailyEarningDetail(bool isInternal)
        {
            const string uSpKey = "Aff_DailyEarning_Detail_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var param = new DailyEarningParam { IsInternal = isInternal };
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<DailyEarningDetail>(param).Returns(new List<DailyEarningDetail>
            {
                new DailyEarningDetail { CustId = 1, Deposit = 100, Turnover = 100, UserName = "", WinLost = 100, Withdrawal = 100 }
            });
            var repo = CreateRepo();

            var result = repo.GetDailyEarningDetail(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
