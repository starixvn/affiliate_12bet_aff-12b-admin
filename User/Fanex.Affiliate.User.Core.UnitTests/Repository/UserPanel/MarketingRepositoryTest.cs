using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Repository.UserPanel
{
    public class MarketingRepositoryTest : RepositoryTestBase<MarketingRepository>
    {
        private const int AffiliateLink = 1;
        private const int MediaGallery = 2;

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetTargetProductList_WhenCall_QueryTargetCountry(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCategory_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<TargetProduct>().ReturnsForAnyArgs(new List<TargetProduct> { new TargetProduct() });

            var result = repo.GetTargetProductList(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSizeList_WhenCall_QuerySizeType(bool isInternal)
        {
            var param = new SizeParam { };
            const string uSpKey = "Aff_Admin_MediaSize_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<SizeType>(param).Returns(new List<SizeType> { new SizeType { } });

            var result = repo.GetSizeList(param, isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetDomainList_WhenCall_QueryDomain(bool isInternal)
        {
            const string uSpKey = "Aff_User_Domain_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<Domain>(Arg.Compat.Is<object>(o => CheckPropertyValue(o, "AffId", "123"))).Returns(new List<Domain> { new Domain() });

            var result = repo.GetDomainListByTargetCountry("123", isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetBannerList_WhenCall_QueryBanner(bool isInternal)
        {
            var param = new MarketingBannerParam();
            param.IsInternal = isInternal;
            const string uSpKey = "Aff_Admin_Banner_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<Banner>(param).Returns(new List<Banner> { new Banner() });

            var result = repo.GetBannerList(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetLandingPageList_WhenCall_QueryLandingPage(bool isInternal)
        {
            const string SpKey = "Aff_User_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = SpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(SpKey, dbMode);
            db.Query<LandingPage>().Returns(new List<LandingPage> { new LandingPage() });

            var result = repo.GetLandingPageListByTargetCountry(string.Empty, isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void CountNumberOfCopies_WhenCall_ReturnCorrectData()
        {
            const string SpKey = "Aff_User_BannerStatistics_Ins";
            var param = new CopiedBannerCounterParam();
            var repo = CreateRepo();
            var db = Builder.Build(SpKey);
            db.ExecuteNonQuery(Arg.Compat.Is<CopiedBannerCounterParam>(p => CheckPropertyValue(p, "ErrCode", 0)));

            var result = repo.CountNumberOfCopies(param);

            Assert.Equal(SpKey, db.SettingKey);
            Assert.True(result.IsOk);
        }
    }
}
