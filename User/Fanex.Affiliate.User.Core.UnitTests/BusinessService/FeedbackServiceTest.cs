using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class FeedbackServiceTest
    {
        private readonly IAppSetting _appSetting;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackServiceTest()
        {
            _appSetting = Substitute.For<IAppSetting>();
            _feedbackRepository = Substitute.For<IFeedbackRepository>();
        }

        [Fact]
        public void GetRateList_WhenCall_CallRepo()
        {
            var service = CreateService();

            service.GetRateList();

            _feedbackRepository.Received().GetRateList();
        }

        [Fact]
        public void Insert_WhenCall_CallRepo()
        {
            var param = new FeedbackParam();
            var service = CreateService();

            service.Insert(param);

            _feedbackRepository.Received().Insert(param);
        }

        [Fact]
        public void IsValidAff_WhenCall_CallRepo()
        {
            const int affId = 123;
            var service = CreateService();

            service.IsValidAff(affId);

            _feedbackRepository.Received().IsShowFeedback(affId);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void IsValidIdleTime_WhenCall_ReturnCorrectValue(bool isNull, bool expectedResult)
        {
            _appSetting.ValidIdleTime.ReturnsForAnyArgs(10);
            var lastestRequestTime = isNull ? null : (DateTime?)new DateTime(2022, 08, 30);
            var service = CreateService();

            var result = service.IsValidIdleTime(lastestRequestTime);

            Assert.Equal(result, expectedResult);
        }

        private FeedbackService CreateService()
            => new FeedbackService(_appSetting, _feedbackRepository);
    }
}
