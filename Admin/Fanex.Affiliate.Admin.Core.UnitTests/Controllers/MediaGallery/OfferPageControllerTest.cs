using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.MediaGallery
{
    public class OfferPageControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://aff-12bet.nexdev.net/12BetUserAsianNew");
            var controller = CreateOfferPageController(systemServive: systemServive.Object, appSetting: appSetting.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/MediaGallery/Common.cshtml", viewName);
        }

        [Fact]
        public void GetOfferPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam();

            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(t => t.GetOfferPageList(param)).Returns(new EntityResult<OfferPage> { MainData = new List<OfferPage> { new OfferPage() }, TotalItem = 1 });
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            var result = controller.GetOfferPageList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<OfferPage>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
        }

        [Fact]
        public void GetOfferPageById_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedOfferPageId = 123;
            var passedParam = 0;
            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(t => t.GetOfferPageById(selectedOfferPageId)).Returns(new OfferPage { PageId = 123 }).Callback<int>(p => passedParam = p);
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            var result = controller.GetOfferPageDetailById(selectedOfferPageId) as JsonNetResult;
            var OfferPage = result.Data as OfferPage;

            // Assert
            Assert.Equal(selectedOfferPageId, OfferPage.PageId);
            Assert.Equal(selectedOfferPageId, passedParam);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(s => s.InsertOfferPage(It.IsAny<HttpPostedFileBase>(), It.IsAny<InsertHtmlFileParam>())).Returns(new ResultInfo());
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.InsertOfferPage(new MockFile(memoryStream), new InsertHtmlFileParam()) as JsonNetResult;
                var resultInfo = result.Data as ResultInfo;

                // Assert
                Assert.True(resultInfo.IsSuccessful);
            }
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var expectedMessage = "You must browse file before upload";
            var controller = CreateOfferPageController();
            controller.ModelState.AddModelError("Error01", expectedMessage);

            // Action
            var result = controller.InsertOfferPage(new MockFile(), new InsertHtmlFileParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectedMessage, resultInfo.Message);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var controller = CreateOfferPageController();

            // Action
            var result = controller.InsertOfferPage(null, new InsertHtmlFileParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must browse file before upload", resultInfo.Message);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new UpdateOfferPageParam { PageId = 123, FileName = "abc.html" };
            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(s => s.UpdateOfferPage(null, It.IsAny<UpdateOfferPageParam>())).Returns(new ResultInfo());
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            var result = controller.UpdateOfferPage(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithHasFile_UpdateSuccessful()
        {
            // Arrange
            var param = new UpdateOfferPageParam { PageId = 123, FileName = "abc.html" };
            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(s => s.UpdateOfferPage(It.IsAny<HttpPostedFileBase>(), It.IsAny<UpdateOfferPageParam>())).Returns(new ResultInfo());
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateOfferPage(new MockFile(memoryStream), param) as JsonNetResult;
                var resultInfo = result.Data as ResultInfo;

                // Assert
                Assert.True(resultInfo.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithInValidParam_ReturnError()
        {
            // Arrange
            var expectedMessage = "You must browse file before upload";
            var param = new UpdateOfferPageParam { PageId = 123, FileName = "abc.html" };
            var controller = CreateOfferPageController();
            controller.ModelState.AddModelError("Error01", expectedMessage);

            // Action
            var result = controller.UpdateOfferPage(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectedMessage, resultInfo.Message);
        }

        [Fact]
        public void DeleteOfferPages_WhenCall_DeleteSuccessful()
        {
            // Arrange
            var id = "123";
            DeleteOfferPageParam passedParam = null;
            var OfferPageService = new Mock<IOfferPageService>();
            OfferPageService.Setup(s => s.DeleteOfferPages(It.IsAny<DeleteOfferPageParam>())).Returns(new ResultInfo()).Callback<DeleteOfferPageParam>(p => passedParam = p);
            var controller = CreateOfferPageController(OfferPageService: OfferPageService.Object);

            // Action
            var result = controller.DeleteOfferPages(id) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(id, passedParam.PageIds);
        }

        private OfferPageController CreateOfferPageController(
            ISessionService sessionService = null,
            ISystemService systemServive = null,
            IOfferPageService OfferPageService = null,
            IAppSetting appSetting = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakesystemServive = systemServive ?? Mock.Of<ISystemService>();
            var fakeOfferPageService = OfferPageService ?? Mock.Of<IOfferPageService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new OfferPageController(fakeOfferPageService, fakeSessionService, fakesystemServive, fakeAppSetting);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => s);

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}
