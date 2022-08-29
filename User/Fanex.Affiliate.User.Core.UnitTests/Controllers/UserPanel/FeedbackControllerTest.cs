using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
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
    public class FeedbackControllerTest
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageService _languageService;
        private readonly IFeedbackService _feedbackService;

        public FeedbackControllerTest()
        {
            _sessionService = Substitute.For<ISessionService>();
            _languageService = Substitute.For<ILanguageService>();
            _feedbackService = Substitute.For<IFeedbackService>();
        }

        [Fact]
        public void Index_WhenCall_ReturnEmptyResult()
        {
            _sessionService.IsOpenedFeedback.ReturnsForAnyArgs(true);
            var service = CreateController();

            var result = service.Index();

            Assert.Equal("EmptyResult", result.GetType().Name);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Index_WhenCall_ReturnCorrectView(bool isClickLogout)
        {
            _sessionService.IsOpenedFeedback.ReturnsForAnyArgs(false);
            _feedbackService.IsValidIdleTime(Arg.Any<DateTime?>()).ReturnsForAnyArgs(false);
            _feedbackService.IsValidAff(Arg.Any<int>()).ReturnsForAnyArgs(true);
            _feedbackService.GetRateList().ReturnsForAnyArgs(new List<RateInfo> { new RateInfo { RateId = 123 } });

            var service = CreateController();

            var result = service.Index(isClickLogout) as ViewResult;

            Assert.Equal("~/Views/UserPanel/Feedback/Index.cshtml", result.ViewName);
        }

        private FeedbackController CreateController()
            => new FeedbackController(_sessionService, _languageService, _feedbackService);
    }
}
