using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.FeedbackReport;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.FeedbackReport;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.FeedbackReport;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class FeedbackReportControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var feedbackReportService = new Mock<IFeedbackReportService>();
            feedbackReportService.Setup(f => f.GetRateList()).Returns(new List<RateInfo> { new RateInfo { RateId = 123 } });
            var controller = CreateFeedbackReportController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/FeebackReport.cshtml", viewName);
        }

        [Fact]
        public void GetFeedbackReportList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new FeedbackReportParam();
            FeedbackReportParam passedParam = null;
            var feedbackReportService = new Mock<IFeedbackReportService>();
            feedbackReportService.Setup(t => t.GetFeedbackReportList(param)).Returns(new EntityResult<FeedbackReportInfo> { MainData = new List<FeedbackReportInfo> { new FeedbackReportInfo() }, TotalItem = 1 }).Callback<FeedbackReportParam>(p => passedParam = p);
            var controller = CreateFeedbackReportController(feedbackReportService: feedbackReportService.Object);

            // Action
            var result = controller.GetFeedbackReportList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<FeedbackReportInfo>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetFeedbackReportList_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            // Arrange
            var controller = CreateFeedbackReportController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetFeedbackReportList(new FeedbackReportParam()) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        private FeedbackReportController CreateFeedbackReportController(
            ISessionService sessionService = null,
            IFeedbackReportService feedbackReportService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeFeedbackReportService = feedbackReportService ?? Mock.Of<IFeedbackReportService>();

            return new FeedbackReportController(fakeSessionService, fakeFeedbackReportService);
        }
    }
}
