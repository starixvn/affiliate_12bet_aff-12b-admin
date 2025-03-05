using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics;
using NSubstitute;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class MarketingLinkStatisticsControllerTest
    {
        private readonly ISessionService SessionService = NSubstituteMockingHelper.FakeSessionService();
        private readonly ISystemService LanguageService = NSubstituteMockingHelper.FakeLanguageService();
        private readonly IStatisticsService StatisticsService = Substitute.For<IStatisticsService>();

        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new MarketingLinkStatisticsController(SessionService, LanguageService, StatisticsService);
            NSubstituteMockingHelper.FakeController(controller);

            var result = controller.Index() as ViewResult;
            var model = result.Model as MarketingPageStatisticsViewModel;

            Assert.NotNull(model);
            Assert.Contains("~/Views/UserPanel/Statistics/MarketingPageStatistics/Index.cshtml", result.ViewName);
        }

        [Fact]
        public void Index_Post_WhenCall_ReturnCorrectView()
        {
            StatisticsService.GetMarketingPageStatistics(null).ReturnsForAnyArgs(new List<MarketingPageStatistics> { new MarketingPageStatistics { ChannelName = "ABC" } });
            var controller = new MarketingLinkStatisticsController(SessionService, LanguageService, StatisticsService);
            NSubstituteMockingHelper.FakeController(controller);

            var result = controller.Index(new MarketingPageStatisticsViewModel()) as PartialViewResult;

            Assert.Contains("~/Views/UserPanel/Statistics/StatisticsPartial/_MarketingPageStatisticsContent.cshtml", result.ViewName);
        }
    }
}
