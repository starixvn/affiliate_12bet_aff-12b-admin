using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class StatisticsRepositoryTest : RepositoryTestBase<StatisticsRepository>
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetReferrerStatistics_WhenCall_QueryReferrerStatistics(bool isInternal)
        {
            const string uSpkey = "Aff_ClickStatisticsSelBySite";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpkey + dbMode;
            var param = new ReferrerStatisticsParam
            {
                AffId = 1,
                DeviceType = 1,
                FromDate = DateTime.Now,
                IsInternal = isInternal,
                ToDate = DateTime.Now,
                TotalClicks = 10,
                TotalFdms = 2,
                TotalSignUps = 4,
                UniqueClicks = 3
            };
            var db = Builder.Build(uSpkey, dbMode);
            db.Query<ReferrerStatistics>(param).Returns(new List<ReferrerStatistics>
            {
                new ReferrerStatistics { SiteUrls = "", Fdm = 1, SignUps = 1, TotalClick = 1, UniqueClick = 1, CountryCode = "EN", Country = "England"}
            });
            var repo = CreateRepo();

            var result = repo.GetReferrerStatistics(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void GetStatisticDetailBySite_WhenCall_ReturnListReferreStatitstics(bool isInternal, bool isRecordExist)
        {
            const string uSpkey = "Aff_ClickStatisticsDetailBySite";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpkey + dbMode;
            var param = new StatisticsDetailBySiteParam
            {
                AffId = 1,
                DeviceType = 1,
                Domain = "",
                FromDate = DateTime.Now,
                IsInternal = isInternal,
                LanguageCode = "",
                NRows = 1,
                PageIndex = 1,
                PageSize = 1,
                SportCode = "",
                CountryCode = "",
                ToDate = DateTime.Now,
                TotalPages = 1,
            };
            var rowValue = isRecordExist ? null : DBNull.Value;
            var db = Builder.Build(uSpkey, dbMode);
            var reader = db.ExecuteReader(param);
            reader.Read().Returns(true, false);
            reader[Arg.Compat.Any<string>()].Returns(rowValue);
            var repo = CreateRepo();

            var result = repo.GetStatisticsDetailBySite(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetLandingPageStatistics_WhenCall_QueryLandingPage(bool isInternal)
        {
            const string uSpkey = "Aff_ClickStatisticsSelByPage";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpkey + dbMode;
            var param = new LandingPageStatisticsParam
            {
                SportCode = "",
                LanguageCode = "",
                TotalFdms = 1,
                TotalSignUps = 1,
                IsInternal = isInternal
            };
            var db = Builder.Build(uSpkey, dbMode);
            db.Query<LandingPageStatistics>(param).Returns(new List<LandingPageStatistics>
            {
                new LandingPageStatistics { Fdm = 1, LanguageCode = "", Language = "", SignUps = 1, Page = "", TotalClick = 1, UniqueClick = 1, CountryCode = "EN", Country = "England" }
            });
            var repo = CreateRepo();

            var result = repo.GetLandingPageStatistics(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetCountryList_WhenCall_QueryCountry(bool isInternal)
        {
            const string uSpkey = "Aff_Country_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpkey + dbMode;
            var db = Builder.Build(uSpkey, dbMode);
            db.Query<Country>().Returns(new List<Country>
            {
                new Country { CountryCode = "EN", CountryName = "England" }
            });
            var repo = CreateRepo();

            var result = repo.GetCountryList(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
