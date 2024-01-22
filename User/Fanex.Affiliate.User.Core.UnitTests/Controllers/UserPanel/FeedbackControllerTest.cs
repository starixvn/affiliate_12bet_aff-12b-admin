using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class FeedbackControllerTest
    {
        private readonly ISessionService _sessionService;
        private readonly ISystemService _languageService;
        private readonly IFeedbackService _feedbackService;

        public FeedbackControllerTest()
        {
            _sessionService = NSubtituteMockingHelper.FakeSessionService();
            _languageService = Substitute.For<ISystemService>();
            _feedbackService = Substitute.For<IFeedbackService>();
        }

        [Fact]
        public void Index_WhenCall_ReturnEmptyResult()
        {
            _sessionService.IsOpenedFeedback.ReturnsForAnyArgs(true);
            var controller = CreateController();

            var result = controller.Index();

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
            _feedbackService.GetRateList().ReturnsForAnyArgs(new List<RateInfo> { new RateInfo { RateId = 123, RateName = "None" } });

            var controller = CreateController();

            var result = controller.Index(isClickLogout) as ViewResult;

            Assert.Equal("~/Views/UserPanel/Feedback/Index.cshtml", result.ViewName);
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, false, true)]
        public void Insert_WhenCall_ReturnCorrectData(bool isClickLogout, bool isInvalidModel, bool expectedResult)
        {
            var param = new FeedbackParam { IsClickLogout = isClickLogout, RateId = expectedResult ? 1 : 0 };
            _sessionService.IsOpenedFeedback.ReturnsForAnyArgs(isClickLogout);
            _feedbackService.IsValidIdleTime(Arg.Any<DateTime?>()).ReturnsForAnyArgs(false);
            _feedbackService.IsValidAff(Arg.Any<int>()).ReturnsForAnyArgs(true);
            _feedbackService.Insert(Arg.Any<FeedbackParam>()).ReturnsForAnyArgs(new ResultInfo { IsOk = true });

            var controller = CreateController();
            if (isInvalidModel)
            {
                controller.ModelState.AddModelError("Error1", "Error");
            }

            NSubtituteMockingHelper.FakeController(controller);

            var result = controller.Insert(param) as JsonResult;
            var data = result.Data as ResultInfo;

            Assert.Equal(expectedResult, data.IsOk);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CloseFeedback_WhenCall_ReturnCorrectData(bool isClickLogout)
        {
            _sessionService.IsOpenedFeedback.ReturnsForAnyArgs(isClickLogout);
            _feedbackService.IsValidIdleTime(Arg.Any<DateTime?>()).ReturnsForAnyArgs(false);
            _feedbackService.IsValidAff(Arg.Any<int>()).ReturnsForAnyArgs(true);
            _feedbackService.Insert(Arg.Any<FeedbackParam>()).ReturnsForAnyArgs(new ResultInfo { IsOk = true });

            var controller = CreateController();
            NSubtituteMockingHelper.FakeController(controller);

            var result = controller.CloseFeedback(isClickLogout) as JsonResult;
            var data = result.Data as ResultInfo;

            Assert.Equal(isClickLogout, data.IsOk);
        }

        private FeedbackController CreateController()
            => new FeedbackController(_sessionService, _languageService, _feedbackService);
    }
}
