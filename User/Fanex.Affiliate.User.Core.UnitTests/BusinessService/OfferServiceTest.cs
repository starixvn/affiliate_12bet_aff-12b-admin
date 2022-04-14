using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Moq;
using System;
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
            var offerRepository = new Mock<IOfferRepository>();
            offerRepository.Setup(p => p.GetOfferById(offerId)).Returns(offer);
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
            Offer offer = new Offer {
                FileName = "abc.html",
                EffectiveFromDate = DateTime.Now,
                EffectiveToDate = DateTime.Now,
                Language = Constant.DefaultLanguage,
                PageId = 1,
                UploadDate = DateTime.Now
            };
            var offerRepository = new Mock<IOfferRepository>();
            offerRepository.Setup(p => p.GetOfferById(offerId)).Returns(offer);
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));
            var service = CreateOfferService(offerRepository.Object, httpClientUtility.Object);

            // Action
            var result = await service.GetOfferById(offerId);

            // Assert
            Assert.Equal(content, result);
        }

        private static OfferService CreateOfferService(
            IOfferRepository offerRepository = null,
            IHttpClientUtility httpClientUtility = null)
        {
            var fakeOfferRepository = offerRepository ?? Mock.Of<IOfferRepository>();
            var fakeHttpClientUtility = httpClientUtility ?? Mock.Of<IHttpClientUtility>();

            return new OfferService(fakeHttpClientUtility, fakeOfferRepository);
        }
    }
}
