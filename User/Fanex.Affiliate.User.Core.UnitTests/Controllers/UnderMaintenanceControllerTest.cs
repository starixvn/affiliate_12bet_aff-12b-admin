using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using NSubstitute;
using ONEbook.UM.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class UnderMaintenanceControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ISystemService LanguageService;
        private readonly IUnderMaintenanceService MaintenanceService;

        public UnderMaintenanceControllerTest()
        {
            SessionService = NSubstituteMockingHelper.FakeSessionService();
            LanguageService = NSubstituteMockingHelper.FakeLanguageService();
            MaintenanceService = Substitute.For<IUnderMaintenanceService>();
        }

        [Fact]
        public void Index_WhenNotUnderMaintenanceTime_Redirect()
        {
            var controller = CreateController();
            MaintenanceService.GetUnderMaintenanceTime().Returns(new UnderMaintenanceTime { IsUnderMaintenanceTime = false });

            var result = controller.Index();

            Assert.NotNull(result as RedirectResult);
        }

        [Fact]
        public void Index_WhenIsUnderMaintenanceTime_ReturnView()
        {
            var controller = CreateController();
            var fromTime = new DateTime(2020, 01, 01);
            var toTime = new DateTime(2020, 02, 02);
            MaintenanceService.GetUnderMaintenanceTime()
                .Returns(new UnderMaintenanceTime
                {
                    From = fromTime.ToString(),
                    To = toTime.ToString(),
                    IsUnderMaintenanceTime = true
                });

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(fromTime.ToString(), result.ViewBag.UmTimeFrom);
            Assert.Equal(toTime.ToString(), result.ViewBag.UmTimeTo);
        }

        [Theory]
        [InlineData("en-US")]
        [InlineData("vi-VN")]
        public void Excute_WhenRequestChangeLang_SetForNewLang(string languageCode)
        {
            var controller = CreateController();
            NSubstituteMockingHelper.FakeController(controller);
            var routeData = CreateRouteData();
            controller.ControllerContext.RouteData = routeData;
            var requestContext = new RequestContext(controller.HttpContext, routeData);

            SessionService.CurrentLanguage.Returns("vi-VN");
            LanguageService.GetFavoriteLanguage().Returns(languageCode);

            try
            {
                (controller as IController).Execute(requestContext);
            }
            catch { }

            LanguageService.Received().SetCulture(languageCode);
        }

        private static RouteData CreateRouteData()
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "UnderMaintenance");
            routeData.Values.Add("action", "index");
            return routeData;
        }

        private UnderMaintenanceController CreateController()
            => new UnderMaintenanceController(SessionService, LanguageService, MaintenanceService);
    }
}
