using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Domain
{
    public class DomainControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateDomainController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/Domain");

            controller.Url = urlHelperMock.Object;

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/Index.cshtml", viewName);
            Assert.True(model.IsAffDomain);
        }

        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var isAffDomain = true;
            bool passedParam = false;
            var domainService = new Mock<IDomainService>();
            domainService.Setup(t => t.GetDomainList(isAffDomain)).Returns(new List<DomainInfo> { new DomainInfo() }).Callback<bool>(p => passedParam = p);
            var controller = CreateDomainController(domainService: domainService.Object);

            // Action
            var result = controller.GetDomainList(isAffDomain) as JsonNetResult;
            var data = result.Data as List<DomainInfo>;

            // Assert
            Assert.Single(data);
            Assert.Equal(isAffDomain, passedParam);
        }

        [Fact]
        public void InsertDomain_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertDomainParam { DomainName = "abc.com" };
            InsertDomainParam passedParam = null;
            var domainService = new Mock<IDomainService>();
            domainService.Setup(s => s.InsertDomain(It.IsAny<InsertDomainParam>())).Returns(new ResultInfo()).Callback<InsertDomainParam>(p => passedParam = p);
            var controller = CreateDomainController(domainService: domainService.Object);

            // Action
            var result = controller.InsertDomain(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void InsertDomain_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateDomainController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.InsertDomain(new InsertDomainParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("Please input Domain's name.", resultInfo.Message);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new UpdateDomainParam { DomainId = 123, DomainName = "abc.com" };
            UpdateDomainParam passedParam = null;
            var domainService = new Mock<IDomainService>();
            domainService.Setup(s => s.UpdateDomain(It.IsAny<UpdateDomainParam>())).Returns(new ResultInfo()).Callback<UpdateDomainParam>(p => passedParam = p);
            var controller = CreateDomainController(domainService: domainService.Object);

            // Action
            var result = controller.UpdateDomain(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.DomainId, passedParam.DomainId);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithInvalidDomainId_ReturnError()
        {
            // Arrange
            var param = new UpdateDomainParam { DomainId = -1, DomainName = "abc" };
            var controller = CreateDomainController();
            controller.ModelState.AddModelError("DomainId", "");

            // Action
            var result = controller.UpdateDomain(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain' info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new UpdateDomainParam { DomainId = 123, DomainName = "abc.com" };
            var controller = CreateDomainController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateDomain(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain' info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteDomainParam { DomainId = 123 };
            DeleteDomainParam passedParam = null;
            var domainService = new Mock<IDomainService>();
            domainService.Setup(s => s.DeleteDomain(It.IsAny<DeleteDomainParam>())).Returns(new ResultInfo()).Callback<DeleteDomainParam>(p => passedParam = p);
            var controller = CreateDomainController(domainService: domainService.Object);

            // Action
            var result = controller.DeleteDomain(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateDomainController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteDomain(new DeleteDomainParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain is incorrect!", resultInfo.Message);
        }

        private DomainController CreateDomainController(
        ISessionService sessionService = null,
        IDomainService domainService = null,
        ISystemService systemService = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeDomainService = domainService ?? Mock.Of<IDomainService>();
            var fakeISystemService = systemService ?? Mock.Of<ISystemService>();

            var controller = new DomainController(fakeSessionService, fakeDomainService, fakeISystemService);

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
