using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.MediaGallery
{
    public class NewsLetterControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language { LanguageCode = "en", LanguageName = "English" } });
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://aff-12bet.nexdev.net/12BetUserAsianNew");
            var controller = CreateNewsLetterController(systemServive: systemServive.Object, appSetting: appSetting.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/MediaGallery/Common.cshtml", viewName);
        }

        [Fact]
        public void GetNewsLetterList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam();

            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(t => t.GetNewsLetters(param)).Returns(new EntityResult<NewsLetter> { MainData = new List<NewsLetter> { new NewsLetter() }, TotalItem = 1 });
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            var result = controller.GetNewsLetterList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<NewsLetterViewModel>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
        }

        [Fact]
        public void GetNewsLetterById_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedNewsLetterId = 123;
            var passedParam = 0;
            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(t => t.GetNewsLetterById(selectedNewsLetterId)).Returns(new NewsLetter { NewsLetterId = 123 }).Callback<int>(p => passedParam = p);
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            var result = controller.GetNewsLetterDetailById(selectedNewsLetterId) as JsonNetResult;
            var newsLetter = result.Data as NewsLetterViewModel;

            // Assert
            Assert.Equal(selectedNewsLetterId, newsLetter.NewsLetterId);
            Assert.Equal(selectedNewsLetterId, passedParam);
        }

        [Fact]
        public void InsertNewsLetter_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(s => s.AddNewsLetter(It.IsAny<HttpPostedFileBase>(), It.IsAny<InsertHtmlFileParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.InsertNewsLetter(new MockFile(memoryStream), new NewsLetterViewModel()) as JsonNetResult;
                var resultInfo = result.Data as ResultInfo;

                // Assert
                Assert.True(resultInfo.IsSuccessful);
            }
        }

        [Fact]
        public void InsertNewsLetter_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var expectedMessage = "You must browse file before upload";
            var controller = CreateNewsLetterController();
            controller.ModelState.AddModelError("Error01", expectedMessage);

            // Action
            var result = controller.InsertNewsLetter(new MockFile(), new NewsLetterViewModel()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectedMessage, resultInfo.Message);
        }

        [Fact]
        public void InsertNewsLetter_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterController();

            // Action
            var result = controller.InsertNewsLetter(null, new NewsLetterViewModel()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("You must browse file before upload", resultInfo.Message);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new NewsLetterViewModel { NewsLetterId = 123, FileName = "abc.html" };
            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(s => s.UpdateNewsLetter(null, It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            var result = controller.UpdateNewsLetter(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithHasFile_UpdateSuccessful()
        {
            // Arrange
            var param = new NewsLetterViewModel { NewsLetterId = 123, FileName = "abc.html" };
            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(s => s.UpdateNewsLetter(It.IsAny<HttpPostedFileBase>(), It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateNewsLetter(new MockFile(memoryStream), param) as JsonNetResult;
                var resultInfo = result.Data as ResultInfo;

                // Assert
                Assert.True(resultInfo.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithInValidParam_ReturnError()
        {
            // Arrange
            var expectedMessage = "You must browse file before upload";
            var param = new NewsLetterViewModel { NewsLetterId = 123, FileName = "abc.html" };
            var controller = CreateNewsLetterController();
            controller.ModelState.AddModelError("Error01", expectedMessage);

            // Action
            var result = controller.UpdateNewsLetter(null, param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectedMessage, resultInfo.Message);
        }

        [Fact]
        public void DeleteNewsLetters_WhenCall_DeleteSuccessful()
        {
            // Arrange
            var id = "123";
            DeleteNewsLetterParam passedParam = null;
            var newsLetterService = new Mock<INewsLetterService>();
            newsLetterService.Setup(s => s.DeleteNewsLetters(It.IsAny<DeleteNewsLetterParam>())).Returns(new ResultInfo()).Callback<DeleteNewsLetterParam>(p => passedParam = p);
            var controller = CreateNewsLetterController(newsLetterService: newsLetterService.Object);

            // Action
            var result = controller.DeleteNewsLetters(id) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(id, passedParam.NewsLetterIds);
        }

        private NewsLetterController CreateNewsLetterController(
            ISessionService sessionService = null,
            ISystemService systemServive = null,
            INewsLetterService newsLetterService = null,
            IAppSetting appSetting = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakesystemServive = systemServive ?? Mock.Of<ISystemService>();
            var fakeNewsLetterService = newsLetterService ?? Mock.Of<INewsLetterService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new NewsLetterController(fakeNewsLetterService, fakeSessionService, fakesystemServive, fakeAppSetting);

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
