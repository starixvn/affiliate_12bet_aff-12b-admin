using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
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
    public class MarketingRepositoryTest : RepositoryTestBase<MarketingRepository>
    {
        private const int AffiliateLink = 1;
        private const int MediaGallery = 2;

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]    
        public void GetCategoryList_WhenCall_QueryCategory(bool isMediaPage, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCategory_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var typeId = isMediaPage ? AffiliateLink : MediaGallery;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<CategoryType>().ReturnsForAnyArgs(new List<CategoryType> { new CategoryType() });

            var result = repo.GetCategoryList(isMediaPage, isInternal);

            Assert.Equal(key, db.SettingKey);
            db.Received().Query<CategoryType>(Arg.Compat.Is<object>(a => CheckPropertyValue(a, "TypeId", typeId)));
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetCurrencyList_WhenCall_QueryCurrencyType(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCurrency_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<CurrencyType>().ReturnsForAnyArgs(new List<CurrencyType> { new CurrencyType() });

            var result = repo.GetCurrencyList(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetBannerTypeList_WhenCall_QueryBannerType(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaType_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<BannerType>().Returns(new List<BannerType> { new BannerType() });

            var result = repo.GetBannerTypeList(isInternal);

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
            db.Query<Domain>(Arg.Compat.Is<object>(o => CheckPropertyValue(o, "AffId", 1))).Returns(new List<Domain> { new Domain() });

            var result = repo.GetDomainList(1, isInternal);

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
            db.Query<MarketingItem>(param).Returns(new List<MarketingItem> { new MarketingItem() });

            var result = repo.GetBannerList(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetTextList_WhenCall_QueryTextList(bool isInternal)
        {
            var param = new MarketingTextParam();
            param.IsInternal = isInternal;
            const string uSpKey = "Aff_Admin_Text_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = uSpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(uSpKey, dbMode);
            db.Query<MarketingItem>(param).Returns(new List<MarketingItem> { new MarketingItem() });

            var result = repo.GetTextList(param);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetLandingPageList_WhenCall_QueryLandingPage(bool isInternal)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var key = SpKey + dbMode;
            var repo = CreateRepo();
            var db = Builder.Build(SpKey, dbMode);
            db.Query<LandingPage>().Returns(new List<LandingPage> { new LandingPage() });

            var result = repo.GetLandingPageList(isInternal);

            Assert.Equal(key, db.SettingKey);
            Assert.NotEmpty(result);
        }
    }
}
