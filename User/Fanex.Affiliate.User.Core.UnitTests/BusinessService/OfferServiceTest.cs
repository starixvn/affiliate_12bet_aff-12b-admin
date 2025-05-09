using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Criterias;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Data.Repository;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{

    public class OfferServiceTest
    {
        [Fact]
        public async Task GetOfferById_WhenCall_ReturnEmptyString()
        {
            // Arrange
            var offerId = 123;
            Offer offer = null;
            var offerRepository = new Mock<IDynamicRepository>();
            offerRepository.Setup(p => p.Get<Offer>(It.IsAny<GetOfferByIdCriteria>(), string.Empty)).Returns(offer);
            var service = CreateOfferService(offerRepository.Object);

            // Action
            var result = await service.GetOfferById(offerId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetOfferById_WhenCall_ReturnContent()
        {
            // Arrange
            var offerId = 123;
            var content = "test";
            Offer offer = new Offer
            {
                FileName = "abc.html",
                EffectiveFromDate = DateTimeHelper.Gmt8Now,
                EffectiveToDate = DateTimeHelper.Gmt8Now,
                Language = Constant.DefaultLanguage,
                PageId = 1,
                UploadDate = DateTimeHelper.Gmt8Now
            };
            var offerRepository = new Mock<IDynamicRepository>();
            offerRepository.Setup(p => p.Get<Offer>(It.IsAny<GetOfferByIdCriteria>(), string.Empty)).Returns(offer);
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));
            var service = CreateOfferService(offerRepository.Object, httpClientUtility.Object);

            // Action
            var result = await service.GetOfferById(offerId);

            // Assert
            Assert.Equal(content, result);
        }

        private static OfferService CreateOfferService(
            IDynamicRepository offerRepository = null,
            IHttpClientUtility httpClientUtility = null)
        {
            var fakeOfferRepository = offerRepository ?? Mock.Of<IDynamicRepository>();
            var fakeHttpClientUtility = httpClientUtility ?? Mock.Of<IHttpClientUtility>();

            return new OfferService(fakeOfferRepository, fakeHttpClientUtility);
        }
    }
}
