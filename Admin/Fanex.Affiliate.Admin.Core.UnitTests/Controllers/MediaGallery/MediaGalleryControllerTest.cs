using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.MediaGallery
{
    public class MediaGalleryControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var param = new BannerParamFromHome { ImageType = "Image", IsFromHome = true };
            var commonMediaGalleryService = new Mock<ICommonMediaGalleryService>();
            commonMediaGalleryService.Setup(c => c.GetCategoryList(1)).Returns(new List<Category> { new Category { CategoryId = 123, CategoryName = "Category", ParentCategoryId = 123 } });
            commonMediaGalleryService.Setup(c => c.GetCurrencyList()).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 234, Name = "Currency" } });
            commonMediaGalleryService.Setup(c => c.GetImageTypeList()).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 345, Name = "Image" } });
            var languageService = new Mock<ILanguageService>();
            languageService.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });
            var affTypeService = new Mock<IAffTypeService>();
            affTypeService.Setup(a => a.GetAffTypeList()).Returns(new List<AffTypeInfo> { new AffTypeInfo { AffTypeId = 2, AffTypeName = "Asian" } });

            SizeParam passedSizeParam = null;
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } }).Callback<SizeParam>(p => passedSizeParam = p);
            var controller = CreateMediaGalleryController(languageService: languageService.Object, commonMediaGalleryService: commonMediaGalleryService.Object, bannerListService: bannerListService.Object, affTypeService: affTypeService.Object);

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/MediaGallery");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index(param) as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as BannerListViewModel;

            // Assert
            Assert.Equal("", viewName);
            Assert.Single(model.CategoryList);
            Assert.Single(model.LanguageList);
            Assert.Single(model.CurrencyList);
            Assert.Single(model.ImageTypeList);
            Assert.Single(model.SizeList);
            Assert.Single(model.AffTypeList);
            Assert.Equal(param, model.Param);
            Assert.Equal(param.ImageType, passedSizeParam.ImageType);
        }

        [Fact]
        public void GetSubCategoryCasinoList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonMediaGalleryService = new Mock<ICommonMediaGalleryService>();
            commonMediaGalleryService.Setup(t => t.GetCategoryList(1)).Returns(new List<Category> { new Category { CategoryId = 123, CategoryName = "Casino Bonus", ParentCategoryId = 51 } });
            var controller = CreateMediaGalleryController(commonMediaGalleryService: commonMediaGalleryService.Object);

            // Action
            var result = controller.GetSubCategoryCasinoList() as JsonNetResult;

            // Assert
            Assert.Single(result.Data as IEnumerable<Category>);
        }

        [Fact]
        public void GetSubCategorySportsbookList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commonMediaGalleryService = new Mock<ICommonMediaGalleryService>();
            commonMediaGalleryService.Setup(t => t.GetCategoryList(1)).Returns(new List<Category> { new Category { CategoryId = 123, CategoryName = "SB Bonus", ParentCategoryId = 1 } });
            var controller = CreateMediaGalleryController(commonMediaGalleryService: commonMediaGalleryService.Object);

            // Action
            var result = controller.GetSubCategorySportsbookList() as JsonNetResult;

            // Assert
            Assert.Single(result.Data as IEnumerable<Category>);
        }

        [Fact]
        public void GetBannerRootUrl_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(t => t.BannerRootUrl).Returns("Test/Banner");
            var controller = CreateMediaGalleryController(appSetting: appSetting.Object);

            // Action
            var result = controller.GetBannerRootUrl() as JsonNetResult;

            // Assert
            Assert.Equal("Test/Banner", result.Data as string);
        }

        [Fact]
        public void GetSizeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SizeParam();
            SizeParam passedSizeParam = null;
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(t => t.GetSizeList(param)).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } }).Callback<SizeParam>(p => passedSizeParam = p);
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.GetSizeList(param) as JsonNetResult;

            // Assert
            Assert.Single(result.Data as IEnumerable<MediaGalleryFilterItem>);
            Assert.Equal(param, passedSizeParam);
        }

        [Fact]
        public void GetBannerList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new BannerParam();
            BannerParam passedParam = null;
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(t => t.GetBannerList(param)).Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 }).Callback<BannerParam>(p => passedParam = p);
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.GetBannerList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<BannerInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetBannerDetail_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedBannerId = 123;
            var passedParam = 0;
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(s => s.GetBannerDetail(selectedBannerId)).Returns(new BannerInfo { BannerId = 123 }).Callback<int>(p => passedParam = p);
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.GetBannerDetail(selectedBannerId) as JsonNetResult;
            var bannerInfo = result.Data as BannerInfo;

            // Assert
            Assert.Equal(selectedBannerId, bannerInfo.BannerId);
            Assert.Equal(selectedBannerId, passedParam);
        }

        [Fact]
        public void InsertBanner_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(s => s.InsertBanner(It.IsAny<HttpPostedFileBase>(), It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo ());
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.InsertBanner(new MockFile(), new InsertUpdateBannerParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateMediaGalleryController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.InsertBanner(new MockFile(), new InsertUpdateBannerParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must browse banner to upload.", resultInfo.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var controller = CreateMediaGalleryController();

            // Action
            var result = controller.InsertBanner(null, new InsertUpdateBannerParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must browse banner to upload.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { BannerId = 123, InputFileName = "abc.gif" };
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(s => s.UpdateBanner(null, It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo());
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInValidParam_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { BannerId = 123, InputFileName = "abc.gif" };
            var controller = CreateMediaGalleryController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must enter a file name for banner.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInValidBannerId_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { InputFileName = "abc.gif" };
            var controller = CreateMediaGalleryController();

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("BannerId is invalid.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInValidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { BannerId = 123 };
            var controller = CreateMediaGalleryController();

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must enter a file name for banner.", resultInfo.Message);
        }

        [Fact]
        public void DeleteBanners_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteBannerParam { BannerIdList = "123" };
            DeleteBannerParam passedParam = null;
            var bannerListService = new Mock<IBannerListService>();
            bannerListService.Setup(s => s.DeleteBanners(It.IsAny<DeleteBannerParam>())).Returns(new ResultInfo()).Callback<DeleteBannerParam>(p => passedParam = p);
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.DeleteBanners(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.BannerIdList, passedParam.BannerIdList);
        }

        [Fact]
        public void DeleteBanners_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateMediaGalleryController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteBanners(new DeleteBannerParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Please select Banners to delete.", resultInfo.Message);
        }

        private MediaGalleryController CreateMediaGalleryController(
        ISessionService sessionService = null,
        ILanguageService languageService = null,
        ICommonMediaGalleryService commonMediaGalleryService = null,
        IBannerListService bannerListService = null,
        IAffTypeService affTypeService = null,
        IAppSetting appSetting = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ILanguageService>();
            var fakeCommonMediaGalleryService = commonMediaGalleryService ?? Mock.Of<ICommonMediaGalleryService>();
            var fakeBannerListService = bannerListService ?? Mock.Of<IBannerListService>();
            var fakeAffTypeService = affTypeService ?? Mock.Of<IAffTypeService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new MediaGalleryController(fakeSessionService, fakeLanguageService, fakeCommonMediaGalleryService, fakeBannerListService, fakeAffTypeService, fakeAppSetting);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}
