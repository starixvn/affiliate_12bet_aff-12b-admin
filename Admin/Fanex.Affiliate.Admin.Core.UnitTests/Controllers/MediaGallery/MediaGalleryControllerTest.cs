using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository;
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
            var param = new BannerParamFromHome { IsFromHome = true };
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });
            systemServive.Setup(a => a.GetAffTypeList()).Returns(new List<AffType> { new AffType { AffTypeId = 2, AffTypeName = "Asian" } });

            SizeParam passedSizeParam = null;
            var bannerListService = new Mock<IBannerService>();
            bannerListService.Setup(c => c.GetTargetProductList()).Returns(new List<TargetProduct> { new TargetProduct { TargetProductName = "Category" } });
            bannerListService.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } }).Callback<SizeParam>(p => passedSizeParam = p);
            var controller = CreateMediaGalleryController(systemServive: systemServive.Object, bannerListService: bannerListService.Object);

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/MediaGallery");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index(param) as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as BannerListViewModel;

            // Assert
            Assert.Equal("", viewName);
            Assert.Single(model.TargetProductList);
            Assert.Single(model.LanguageList);
            Assert.Single(model.SizeList);
            Assert.Single(model.AffTypeList);
            Assert.Equal(param, model.Param);
        }

        [Fact]
        public void GetSizeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var model = new ImageSizeViewModel { Language = "English" };
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });

            var bannerListService = new Mock<IBannerService>();
            bannerListService.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } });
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object, systemServive: systemServive.Object);

            // Action
            var result = controller.GetSizeList(model) as JsonNetResult;

            // Assert
            Assert.Single(result.Data as IEnumerable<MediaGalleryFilterItem>);
        }

        [Fact]
        public void GetBannerList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var model = new GetBannerViewModel();
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });

            var bannerListService = new Mock<IBannerService>();
            bannerListService.Setup(t => t.GetBannerList(It.IsAny<BannerParam>())).Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 });
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object, systemServive: systemServive.Object);

            // Action
            var result = controller.GetBannerList(model) as JsonNetResult;
            var entityResult = result.Data as EntityResult<BannerInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
        }

        [Fact]
        public void GetBannerDetail_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedBannerId = 123;
            var passedParam = 0;
            var bannerListService = new Mock<IBannerService>();
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
            var bannerListService = new Mock<IBannerService>();
            bannerListService.Setup(s => s.InsertBanner(It.IsAny<HttpPostedFileBase[]>(), It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
            var controller = CreateMediaGalleryController(bannerListService: bannerListService.Object);

            // Action
            var result = controller.InsertBanner(new MockFile[1], new InsertUpdateMultipleBannersParam()) as JsonNetResult;
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
            var result = controller.InsertBanner(new MockFile[1], new InsertUpdateMultipleBannersParam()) as JsonNetResult;
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
            var result = controller.InsertBanner(null, new InsertUpdateMultipleBannersParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must browse banner to upload.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { BannerId = 123, InputFileNames = "abc.gif" };
            var bannerListService = new Mock<IBannerService>();
            bannerListService.Setup(s => s.UpdateBanner(null, It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
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
            var param = new InsertUpdateMultipleBannersParam { BannerId = 123, InputFileNames = "abc.gif" };
            var controller = CreateMediaGalleryController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must either enter a file name for banner or browse other banner.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInValidBannerId_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "abc.gif" };
            var controller = CreateMediaGalleryController();

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Banner Id is invalid.", resultInfo.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInValidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { BannerId = 123 };
            var controller = CreateMediaGalleryController();

            // Action
            var result = controller.UpdateBanner(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must either enter a file name for banner or browse other banner.", resultInfo.Message);
        }

        [Fact]
        public void DeleteBanners_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteBannerParam { BannerIdList = "123" };
            DeleteBannerParam passedParam = null;
            var bannerListService = new Mock<IBannerService>();
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
            Assert.Equal("Please select Banner(s) to delete.", resultInfo.Message);
        }

        private MediaGalleryController CreateMediaGalleryController(
        ISessionService sessionService = null,
        ISystemService systemServive = null,
        IBannerService bannerListService = null,
        IAppSetting appSetting = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeCommonService = systemServive ?? Mock.Of<ISystemService>();
            var fakeBannerListService = bannerListService ?? Mock.Of<IBannerService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new MediaGalleryController(fakeSessionService, fakeCommonService, fakeBannerListService, fakeAppSetting);

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
