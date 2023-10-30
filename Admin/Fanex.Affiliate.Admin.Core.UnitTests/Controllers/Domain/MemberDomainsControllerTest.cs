using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Domain
{
    public class MemberDomainsControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var session = MockingHelper.GetSessionService();
            var domainService = new Mock<IDomainService>();
            domainService.Setup(d => d.GetTargetCountryList(It.IsAny<TargetCountryParam>())).Returns(new List<TargetCountry> { new TargetCountry { TargetCountryCode = "en-US", TargetCountryName = "English" } });
            var controller = new MemberDomainsController(session, domainService.Object, null);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/Index.cshtml", viewName);
            Assert.Single(model.TargetCountryList);
            Assert.False(model.IsAffDomain);
        }
    }
}
