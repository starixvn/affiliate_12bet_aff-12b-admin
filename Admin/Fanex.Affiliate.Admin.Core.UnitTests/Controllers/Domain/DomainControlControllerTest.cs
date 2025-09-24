using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.Services.Operation;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Domain
{
    public class DomainControlControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateDomainControlController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/DomainControl");

            controller.Url = urlHelperMock.Object;

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainControlViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/DomainControl.cshtml", viewName);
            Assert.Empty(model.TargetCountryList);
        }

        [Fact]
        public void GetDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var targetCountryCode = "en";
            string passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControl(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<DomainControlInfo> { new DomainControlInfo() }).Callback<string, int>((p, i) => passedParam = p);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlList(targetCountryCode, string.Empty) as JsonNetResult;
            var data = result.Data as List<DomainControlInfo>;

            // Assert
            Assert.Single(data);
            Assert.Equal(targetCountryCode, passedParam);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertDomainControlParam { AffDomainId = 1, RedirectDomainId = 2 };
            InsertDomainControlParam passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.InsertDomainControl(It.IsAny<InsertDomainControlParam>())).Returns(new ResultInfo()).Callback<InsertDomainControlParam>((p) => passedParam = p);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.InsertDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.AffDomainId, passedParam.AffDomainId);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateDomainControlController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.InsertDomainControl(new InsertDomainControlParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain Control's info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new UpdateDomainControlParam() { DomainControlId = 123, AffDomainId = 1, RedirectDomainId = 2 };
            UpdateDomainControlParam passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.UpdateDomainControl(It.IsAny<UpdateDomainControlParam>())).Returns(new ResultInfo()).Callback<UpdateDomainControlParam>(p => passedParam = p);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.UpdateDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.DomainControlId, passedParam.DomainControlId);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithInvalidDomainId_ReturnError()
        {
            // Arrange
            var param = new UpdateDomainControlParam { AffDomainId = 1, RedirectDomainId = 2 };
            var controller = CreateDomainControlController();
            controller.ModelState.AddModelError("DomainControlId", "");

            // Action
            var result = controller.UpdateDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain Control's info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new UpdateDomainControlParam { DomainControlId = 123, RedirectDomainId = 2 };
            var controller = CreateDomainControlController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.UpdateDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain Control's info is invalid!", resultInfo.Message);
        }

        [Fact]
        public void DeleteDomainControl_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteDomainControlParam { DomainControlId = 123 };
            DeleteDomainControlParam passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.DeleteDomainControl(It.IsAny<DeleteDomainControlParam>())).Returns(new ResultInfo()).Callback<DeleteDomainControlParam>(p => passedParam = p);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.DeleteDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void DeleteDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var controller = CreateDomainControlController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.DeleteDomainControl(new DeleteDomainControlParam()) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal("The Domain Control is incorrect!", resultInfo.Message);
        }

        [Fact]
        public void GetSubAffDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var expectedList = new List<string> { "go" };
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetSubAffDomainControlList()).Returns(expectedList);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetSubAffDomainControlList() as JsonNetResult;
            var data = result.Data as List<string>;

            // Assert
            Assert.Single(data);
            Assert.Equal(expectedList, data);
        }

        [Fact]
        public void GetDomainControlById_WhenValidId_ReturnCorrectData()
        {
            // Arrange
            var domainControlId = 123;
            var expectedDomainControl = new DomainControlInfo { DomainControlId = domainControlId };
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetDomainControlById(domainControlId)).Returns(expectedDomainControl);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlById(domainControlId) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(domainControlId, data.DomainControlId);
        }

        [Fact]
        public void GetDomainControlById_WhenInvalidTargetCountryCode_ReturnNull()
        {
            // Arrange
            var domainControlId = 123;
            var expectedDomainControl = new DomainControlInfo { DomainControlId = domainControlId, TargetCountryCode = "invalid" };
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetDomainControlById(domainControlId)).Returns(expectedDomainControl);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlById(domainControlId) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public void GetDomainControlById_WhenInvalidDomainControlId_ReturnNull()
        {
            // Arrange
            var domainControlId = 123;
            DomainControlInfo expectedDomainControl = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetDomainControlById(domainControlId)).Returns(expectedDomainControl);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlById(domainControlId) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.Null(data);
        }


        [Fact]
        public void GetDomainControlList_WhenIsGetAllTrue_ReturnAllData()
        {
            // Arrange
            var targetCountryCode = "en";
            var expectedList = new List<DomainControlInfo> { new DomainControlInfo() };
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetDomainControl(targetCountryCode, It.IsAny<int>())).Returns(expectedList);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlList(targetCountryCode, string.Empty, true) as JsonNetResult;
            var data = result.Data as List<DomainControlInfo>;

            // Assert
            Assert.Single(data);
            Assert.Equal(expectedList, data);
        }

        [Fact]
        public void GetDomainControlList_WhenSubDomainRegexProvided_ReturnFilteredData()
        {
            // Arrange
            var targetCountryCode = "en";
            var subDomainRegex = "test";
            var expectedList = new List<DomainControlInfo>
            {
                new DomainControlInfo { AffDomain = "test.domain.com" },
                new DomainControlInfo { AffDomain = "other.domain.com" }
            };
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.GetDomainControl(targetCountryCode, It.IsAny<int>())).Returns(expectedList);
            var controller = CreateDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetDomainControlList(targetCountryCode, subDomainRegex, false) as JsonNetResult;
            var data = result.Data as IEnumerable<DomainControlInfo>;

            // Assert
            Assert.Single(data);
            //Assert.Equal("test.domain.com", data[0].AffDomain);
        }

        private DomainControlController CreateDomainControlController(
        ISessionService sessionService = null,
        IDomainControlService domainControlService = null,
        ICommonDealService commonDealService = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeDomainControlService = domainControlService ?? Mock.Of<IDomainControlService>();
            var fakeICommonDealService = commonDealService ?? Mock.Of<ICommonDealService>();

            var controller = new DomainControlController(fakeSessionService, fakeDomainControlService, fakeICommonDealService);

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
