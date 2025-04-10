using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Domain
{
    public class SpecificDomainControlControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateSpecificDomainControlController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/SpecificDomainControl");

            controller.Url = urlHelperMock.Object;

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/SpecificDomainControl.cshtml", viewName);
            Assert.NotNull(model.SubMenuList);
            Assert.Equal(5, model.SubMenuList.Count());
        }

        [Fact]
        public void GetSpecificDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SpecificDomainControlParam();
            var domainControlService = new Mock<ISpecificDomainControlService>();
            domainControlService.Setup(t => t.GetSpecificDomainControl(It.IsAny<SpecificDomainControlParam>()))
                .Returns(new EntityResult<SpecificDomainControlInfo> { MainData = new List<SpecificDomainControlInfo> { new SpecificDomainControlInfo() } , TotalItem = 1});
            
            var controller = CreateSpecificDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetSpecificDomainControlList(param);

            // Assert
            domainControlService.Verify(t => t.GetSpecificDomainControl(param), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificDomainControlById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var domainControlId = 1;
            var domainControlService = new Mock<ISpecificDomainControlService>();
            domainControlService.Setup(t => t.GetSpecificDomainControlById(It.IsAny<int>()))
                .Returns(new SpecificDomainControlInfo());
            
            var controller = CreateSpecificDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.GetSpecificDomainControlById(domainControlId);

            // Assert
            domainControlService.Verify(t => t.GetSpecificDomainControlById(domainControlId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void InsertSpecificDomainControl_WhenCallWithValidParam_InsertSuccessful()
        {
            // Arrange
            var param = new InsertSpecificDomainControlParam();
            var domainControlService = new Mock<ISpecificDomainControlService>();
            domainControlService.Setup(t => t.InsertSpecificDomainControl(It.IsAny<InsertSpecificDomainControlParam>()))
                .Returns(new ResultInfo());

            var controller = CreateSpecificDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.InsertSpecificDomainControl(param);

            // Assert
            domainControlService.Verify(t => t.InsertSpecificDomainControl(param), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void InsertSpecificDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new InsertSpecificDomainControlParam();
            var controller = CreateSpecificDomainControlController();
            controller.ModelState.AddModelError("Error", "Invalid parameter");

            // Action
            var result = controller.InsertSpecificDomainControl(param);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateSpecificDomainControl_WhenCallWithValidParam_UpdateSuccessful()
        {
            // Arrange
            var param = new UpdateSpecificDomainControlParam();
            var domainControlService = new Mock<ISpecificDomainControlService>();
            domainControlService.Setup(t => t.UpdateSpecificDomainControl(It.IsAny<UpdateSpecificDomainControlParam>()))
                .Returns(new ResultInfo());
            
            var controller = CreateSpecificDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.UpdateSpecificDomainControl(param);

            // Assert
            domainControlService.Verify(t => t.UpdateSpecificDomainControl(param), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateSpecificDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new UpdateSpecificDomainControlParam();
            var controller = CreateSpecificDomainControlController();
            controller.ModelState.AddModelError("Error", "Invalid parameter");

            // Action
            var result = controller.UpdateSpecificDomainControl(param);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteSpecificDomainControl_WhenCallWithValidParam_DeleteSuccessful()
        {
            // Arrange
            var param = new DeleteDomainControlParam();
            var domainControlService = new Mock<ISpecificDomainControlService>();
            domainControlService.Setup(t => t.DeleteSpecificDomainControl(It.IsAny<DeleteDomainControlParam>()))
                .Returns(new ResultInfo());

            var controller = CreateSpecificDomainControlController(domainControlService: domainControlService.Object);

            // Action
            var result = controller.DeleteSpecificDomainControl(param);

            // Assert
            domainControlService.Verify(t => t.DeleteSpecificDomainControl(param), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteSpecificDomainControl_WhenCallWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new DeleteDomainControlParam();
            var controller = CreateSpecificDomainControlController();
            controller.ModelState.AddModelError("Error", "Invalid parameter");

            // Action
            var result = controller.DeleteSpecificDomainControl(param);

            // Assert
            Assert.NotNull(result);
        }

        private SpecificDomainControlController CreateSpecificDomainControlController(
            ISessionService sessionService = null,
            ISpecificDomainControlService domainControlService = null)
        {
            var controller = new SpecificDomainControlController(
                sessionService ?? new Mock<ISessionService>().Object,
                domainControlService ?? new Mock<ISpecificDomainControlService>().Object);

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