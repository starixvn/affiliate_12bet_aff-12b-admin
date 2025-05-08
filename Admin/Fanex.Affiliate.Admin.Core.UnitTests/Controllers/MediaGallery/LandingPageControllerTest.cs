using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.MediaGallery
{
    public class LandingPageControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateLandingPageListController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/LandingPageList");

            controller.Url = urlHelperMock.Object;
            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/MediaGallery/LandingPageList.cshtml", viewName);
        }

        [Fact]
        public void GetLandingPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(t => t.GetLandingPageList()).Returns(new List<LandingPage> { new LandingPage() });
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.GetLandingPageList() as JsonNetResult;
            var data = result.Data as List<LandingPage>;

            // Assert
            Assert.Single(data);
        }

        [Fact]
        public void GetLandingPageItem_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var pageId = 22;
            int passedParam = 0;
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(t => t.GetLandingPageById(pageId)).Returns(new LandingPage { PageId = 22 }).Callback<int>(p => passedParam = p);
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.GetLandingPageItem(pageId) as JsonNetResult;
            var data = result.Data as LandingPage;

            // Assert
            Assert.Equal(pageId, data.PageId);
            Assert.Equal(pageId, passedParam);
        }

        [Fact]
        public void InsertLandingPage_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageName = "123", PageCode = "abc" };
            InsertUpdateLandingPageParam passedParam = null;
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(s => s.HandleSaveLandingPage(It.IsAny<InsertUpdateLandingPageParam>(), true)).Returns(new ResultInfo()).Callback<InsertUpdateLandingPageParam, bool>((p, i) => passedParam = p);
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.InsertLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.PageId, passedParam.PageId);
        }

        [Fact]
        public void InsertLandingPage_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateLandingPageListController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.InsertLandingPage(new InsertUpdateLandingPageParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(string.Empty, resultInfo.Message);
        }

        [Fact]
        public void UpdateLandingPage_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageId = 123, PageName = "123", PageCode = "abc" };
            InsertUpdateLandingPageParam passedParam = null;
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(s => s.HandleSaveLandingPage(It.IsAny<InsertUpdateLandingPageParam>(), false)).Returns(new ResultInfo()).Callback<InsertUpdateLandingPageParam, bool>((p, i) => passedParam = p);
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.UpdateLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.PageId, passedParam.PageId);
        }

        [Fact]
        public void UpdateText_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageId = 123 };
            var controller = CreateLandingPageListController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(string.Empty, resultInfo.Message);
        }

        [Fact]
        public void DeleteLandingPage_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new DeleteLandingPageParam { PageIds = "123" };
            DeleteLandingPageParam passedParam = null;
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(s => s.DeleteLandingPage(param)).Returns(new ResultInfo()).Callback<DeleteLandingPageParam>(p => passedParam = p);
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.DeleteLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteLandingPage_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateLandingPageListController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteLandingPage(new DeleteLandingPageParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(string.Empty, resultInfo.Message);
        }

        [Fact]
        public void GetTargetCountryListByLandingPage_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var pageId = 123;
            var expectedList = new List<TargetCountry> { new TargetCountry() };
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(s => s.GetExcludedTargetCountryList(pageId)).Returns(expectedList);
            var controller = CreateLandingPageListController(landingPageService: landingPageService.Object);

            // Action
            var result = controller.GetTargetCountryListByLandingPage(pageId) as JsonNetResult;
            var data = result.Data as List<TargetCountry>;

            // Assert
            Assert.Single(data);
            Assert.Equal(expectedList, data);
        }

        [Fact]
        public void InsertLandingPage_WhenInvalidTargetCountry_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageName = "test", PageCode = "test", BlockTargetCountry = "invalid" };
            var systemService = new Mock<ISystemService>();
            systemService.Setup(s => s.GetTargetCountryList()).Returns(new List<TargetCountry>());
            var controller = CreateLandingPageListController(systemService: systemService.Object);

            // Action
            var result = controller.InsertLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The target country's info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void UpdateLandingPage_WhenInvalidTargetCountry_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageId = 123, PageName = "test", PageCode = "test", BlockTargetCountry = "invalid" };
            var systemService = new Mock<ISystemService>();
            systemService.Setup(s => s.GetTargetCountryList()).Returns(new List<TargetCountry>());
            var controller = CreateLandingPageListController(systemService: systemService.Object);

            // Action
            var result = controller.UpdateLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The target country's info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void EncodeParam_WhenCall_EncodeAllFields()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam
            {
                PageId = 123,
                PageName = "test page",
                PageCode = "test-code",
                BlockTargetCountry = "en,th"
            };
            var landingPageService = new Mock<ILandingPageService>();
            landingPageService.Setup(s => s.HandleSaveLandingPage(It.IsAny<InsertUpdateLandingPageParam>(), It.IsAny<bool>())).Returns(new ResultInfo());
            var systemService = new Mock<ISystemService>();
            systemService.Setup(s => s.GetTargetCountryList()).Returns(new List<TargetCountry>
            {
                new TargetCountry { TargetCountryCode = "en" },
                new TargetCountry { TargetCountryCode = "th" }
            });
            var controller = CreateLandingPageListController(
                landingPageService: landingPageService.Object,
                systemService: systemService.Object);

            // Action
            var result = controller.InsertLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            landingPageService.Verify(s => s.HandleSaveLandingPage(It.Is<InsertUpdateLandingPageParam>(p =>
                p.PageId == 123 &&
                p.PageName == "Encodedtest page" &&
                p.PageCode == "Encodedtest-code" &&
                p.BlockTargetCountry == "Encodeden,th"), true), Times.Once);
        }

        [Fact]
        public void HandleSaveLandingPage_WhenModelStateInvalid_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageName = "test", PageCode = "test" };
            var systemService = new Mock<ISystemService>();
            systemService.Setup(s => s.GetTargetCountryList()).Returns(new List<TargetCountry>
            {
                new TargetCountry { TargetCountryCode = "en" }
            });
            var controller = CreateLandingPageListController(systemService: systemService.Object);
            controller.ModelState.AddModelError("Error", "Test error");

            // Action
            var result = controller.InsertLandingPage(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Test error", resultInfo.Message);
        }

        private LandingPageListController CreateLandingPageListController(
        ISessionService sessionService = null,
        ILandingPageService landingPageService = null,
        ISystemService systemService = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLandingPageService = landingPageService ?? Mock.Of<ILandingPageService>();
            var fakeSystemService = systemService ?? Mock.Of<ISystemService>();
            var controller = new LandingPageListController(fakeSessionService, fakeLandingPageService, fakeSystemService);

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
