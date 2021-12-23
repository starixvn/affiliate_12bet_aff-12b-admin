using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class OffersForMeControllerTest
    {
        private readonly IOfferService offerService;
        private readonly ISessionService sessionService;

        public OffersForMeControllerTest()
        {
            offerService = Substitute.For<IOfferService>();
            sessionService = Substitute.For<ISessionService>();
        }

        [Fact]
        public async Task Index_WhenCall_ReturnCorrectView()
        {
            sessionService.UserSession.Returns(new UserSession { OfferPageId = 123 });
            offerService.GetOfferById(Arg.Any<int>()).Returns(Task.FromResult("abc.html"));

            var controller = new OffersForMeController(offerService, sessionService, null);
            var result = await controller.Index() as ViewResult;
            Assert.Equal("abc.html", result.ViewBag.Content);
            Assert.Equal("~/Views/UserPanel/OffersForMe/Index.cshtml", result.ViewName);
        }

        [Fact]
        public async Task Preview_WhenCall_ReturnCorrectView()
        {
            const int offerId = 123;
            offerService.GetOfferById(offerId).Returns(Task.FromResult("abc.html"));

            var controller = new OffersForMeController(offerService, null, null);
            var result = await controller.Preview(offerId) as ViewResult;
            Assert.Equal("abc.html", result.ViewBag.Content);
            Assert.Equal("~/Views/UserPanel/OffersForMe/Preview.cshtml", result.ViewName);
        }
    }
}
