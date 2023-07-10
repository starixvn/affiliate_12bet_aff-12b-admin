using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            domainService.Setup(d => d.GetMarketList(It.IsAny<MarketParam>())).Returns(new List<Market> { new Market { MarketId = "en-US", MarketName = "English" } });
            var controller = new MemberDomainsController(session, domainService.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/Index.cshtml", viewName);
            Assert.Single(model.MarketList);
            Assert.False(model.IsAffDomain);
        }
    }
}
