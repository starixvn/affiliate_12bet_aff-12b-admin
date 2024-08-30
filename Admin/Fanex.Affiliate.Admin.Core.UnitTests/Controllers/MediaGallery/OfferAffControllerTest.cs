using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Controllers.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
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
    public class OfferAffControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var offerPageService = new Mock<IOfferPageService>();
            offerPageService.Setup(l => l.GetValidEndDateOfferPages()).Returns(new List<OfferPageBasicInfo> { new OfferPageBasicInfo() });
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://aff-12bet.nexdev.net/12BetUserAsianNew");
            var controller = CreateOfferAffController(offerPageService: offerPageService.Object, appSetting: appSetting.Object);

            // Action
            var viewResult = controller.Index("abc.html") as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as string;

            // Assert
            Assert.Equal("abc.html", model);
            Assert.Equal("~/Areas/Portal/Views/MediaGallery/OfferAff.cshtml", viewName);
        }

        [Fact]
        public void GetOfferAffList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SelectOfferAffParam();

            var OfferAffService = new Mock<IOfferAffService>();
            OfferAffService.Setup(t => t.GetOfferAffList(param)).Returns(new EntityResult<OfferAff> { MainData = new List<OfferAff> { new OfferAff() }, TotalItem = 1 });
            var controller = CreateOfferAffController(OfferAffService: OfferAffService.Object);

            // Action
            var result = controller.GetOfferAffList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<OfferAff>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var OfferAffService = new Mock<IOfferAffService>();
            OfferAffService.Setup(s => s.InsertOfferAff(It.IsAny<InsertOfferAffParam>())).Returns(new ResultInfo());
            var controller = CreateOfferAffController(OfferAffService: OfferAffService.Object);

            // Action
            var result = controller.InsertOfferAff(new InsertOfferAffParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var expectedMessage = "Please enter Affiliate(s)!";
            var controller = CreateOfferAffController();
            controller.ModelState.AddModelError("Error01", expectedMessage);

            // Action
            var result = controller.InsertOfferAff(new InsertOfferAffParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectedMessage, resultInfo.Message);
        }

        [Fact]
        public void DeleteOfferAffs_WhenCall_DeleteSuccessful()
        {
            // Arrange
            var id = "123";
            DeleteOfferAffParam passedParam = null;
            var OfferAffService = new Mock<IOfferAffService>();
            OfferAffService.Setup(s => s.DeleteOfferAffs(It.IsAny<DeleteOfferAffParam>())).Returns(new ResultInfo()).Callback<DeleteOfferAffParam>(p => passedParam = p);
            var controller = CreateOfferAffController(OfferAffService: OfferAffService.Object);

            // Action
            var result = controller.DeleteOfferAffs(id) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(id, passedParam.AffIds);
        }

        private OfferAffController CreateOfferAffController(
            IOfferAffService OfferAffService = null,
            ISessionService sessionService = null,
            IOfferPageService offerPageService = null,
            IAppSetting appSetting = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeOfferPageService = offerPageService ?? Mock.Of<IOfferPageService>();
            var fakeOfferAffService = OfferAffService ?? Mock.Of<IOfferAffService>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new OfferAffController(fakeOfferAffService, fakeSessionService, fakeOfferPageService, fakeAppSetting);

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
