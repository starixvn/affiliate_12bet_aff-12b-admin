using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using Newtonsoft.Json;
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
            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry() });
            var controller = CreateDomainControlController(systemService: systemService.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/Index.cshtml", viewName);
            Assert.Single(model.TargetCountryList);
            Assert.NotNull(JsonConvert.SerializeObject(model.TargetCountryList));
        }


        [Theory]
        [InlineData("")]
        [InlineData("en-US")]
        public void GetDomainControlList_WhenCallWithValidTargetCountry_ReturnCorrectData(string targetCountryCode)
        {
            // Arrange
            string passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControlList(It.IsAny<string>())).Returns(new List<DomainControlInfo> { new DomainControlInfo() }).Callback<string>(p => passedParam = p);

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.GetDomainControlList(targetCountryCode) as JsonNetResult;
            var data = result.Data as List<DomainControlInfo>;

            // Assert
            Assert.Single(data);
            Assert.Equal("en-US", passedParam);
        }

        [Fact]
        public void GetDomainControlList_WhenCallWithInvalidTargetCountry_ReturnCorrectData()
        {
            // Arrange
            string passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControlList(It.IsAny<string>())).Returns(new List<DomainControlInfo> { new DomainControlInfo() }).Callback<string>(p => passedParam = p);

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.GetDomainControlList("abc") as JsonNetResult;
            var data = result.Data as List<DomainControlInfo>;

            // Assert
            Assert.Single(data);
            Assert.Equal("", passedParam);
        }

        [Fact]
        public void GetDomainControlById_WhenCallWithValidDomainControlId_ReturnCorrectData()
        {
            // Arrange
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControlById(It.IsAny<int>())).Returns(new DomainControlInfo { TargetCountryCode = "en-US"});

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.GetDomainControlById(123) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.NotNull(data);
            Assert.NotNull(JsonConvert.SerializeObject(data));
        }

        [Fact]
        public void GetDomainControlById_WhenCallWithInvalidDomainControlId_ReturnNull()
        {
            // Arrange
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControlById(It.IsAny<int>())).Returns(null as DomainControlInfo);

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.GetDomainControlById(123) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public void GetDomainControlById_WhenCallWithInvalidTargetCountry_ReturnNull()
        {
            // Arrange
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(t => t.GetDomainControlById(It.IsAny<int>())).Returns(new DomainControlInfo { TargetCountryCode = "abc" });

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.GetDomainControlById(123) as JsonNetResult;
            var data = result.Data as DomainControlInfo;

            // Assert
            Assert.Null(data);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertDomainControlParam { AffDomainId = 1, RedirectDomainId = 2, TargetCountryCode = Constant.TargetCountryCode, Status = true };
            InsertDomainControlParam passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.InsertDomainControl(It.IsAny<InsertDomainControlParam>())).Returns(new ResultInfo()).Callback<InsertDomainControlParam>(p => passedParam = p);

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = Constant.TargetCountryCode } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

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
            var param = new UpdateDomainControlParam { DomainControlId = 123, AffDomainId = 1, RedirectDomainId = 2, TargetCountryCode = Constant.TargetCountryCode };
            UpdateDomainControlParam passedParam = null;
            var domainControlService = new Mock<IDomainControlService>();
            domainControlService.Setup(s => s.UpdateDomainControl(It.IsAny<UpdateDomainControlParam>())).Returns(new ResultInfo()).Callback<UpdateDomainControlParam>(p => passedParam = p);

            var systemService = new Mock<ISystemService>();
            systemService.Setup(l => l.GetTargetCountryListByUser()).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = Constant.TargetCountryCode } });
            var controller = CreateDomainControlController(null, domainControlService.Object, systemService.Object);

            // Action
            var result = controller.UpdateDomainControl(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
            Assert.Equal(param.DomainControlId, passedParam.DomainControlId);
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

        private DomainControlController CreateDomainControlController(
        ISessionService sessionService = null,
        IDomainControlService domainControlService = null,
        ISystemService systemService = null
        )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeDomainControlService = domainControlService ?? Mock.Of<IDomainControlService>();
            var fakeSystemService = systemService ?? Mock.Of<ISystemService>();

            var controller = new DomainControlController(fakeSessionService, fakeDomainControlService, fakeSystemService);

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
