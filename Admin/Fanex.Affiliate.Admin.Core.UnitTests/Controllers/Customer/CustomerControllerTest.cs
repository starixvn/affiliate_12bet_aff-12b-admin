using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO;
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
            var languageService = new Mock<ILanguageService>();
            languageService.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language() });

            var controller = CreateCustomerController(languageService: languageService.Object);

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
            var languageService = new Mock<ILanguageService>();
            languageService.Setup(l => l.GetLanguageList()).Returns(new List<Language> { new Language() });

            var controller = CreateCustomerController(languageService: languageService.Object);

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
            ILanguageService languageService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeLanguageService = languageService ?? Mock.Of<ILanguageService>();

            return new CustomerController(fakeSessionService, fakeLanguageService);
        }
    }
}
