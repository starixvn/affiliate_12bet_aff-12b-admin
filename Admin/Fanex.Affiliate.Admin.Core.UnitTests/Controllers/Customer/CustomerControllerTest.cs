using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Customer
{
    public class CustomerControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language() });

            var controller = CreateCustomerController(commonSettingService: commonSettingService.Object);

            // Action
            var viewResult = controller.Index("12BETAFF226") as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as CustomerViewModel;

            // Assert
            Assert.Equal("", viewName);
            Assert.Equal("12BETAFF226", model.AffName);
        }

        [Fact]
        public void Index_WhenCallWithSessionAffName_ReturnView()
        {
            // Arrange
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.AffName).Returns("12BETAFF226");
            var commonSettingService = new Mock<ICommonSettingService>();
            commonSettingService.Setup(l => l.GetInChangeMarkets()).Returns(new List<Language> { new Language() });

            var controller = CreateCustomerController(commonSettingService: commonSettingService.Object);

            // Action
            var viewResult = controller.Index("") as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as CustomerViewModel;

            // Assert
            Assert.Equal("", viewName);
            Assert.Equal("12BETAFF226", model.AffName);
        }

        private CustomerController CreateCustomerController(
            ISessionService sessionService = null,
            ICommonSettingService commonSettingService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakecommonSettingService = commonSettingService ?? Mock.Of<ICommonSettingService>();

            return new CustomerController(fakeSessionService, fakecommonSettingService);
        }
    }
}
