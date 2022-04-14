using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class OfferAffServiceTest
    {
        [Fact]
        public void GetOfferAffList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SelectOfferAffParam { AffName = "", FileName = "" };
            PagingParam passedParam = null;
            var OfferAffRepository = new Mock<IOfferAffRepository>();
            OfferAffRepository.Setup(s => s.GetOfferAffList(param)).Returns(new EntityResult<OfferAff> { MainData = new List<OfferAff> { new OfferAff {
                FileName = "abc.html",
                AffId = 123,
                EffectiveFromDate = new DateTime(2022, 02, 01),
                EffectiveToDate = new DateTime(2022, 03, 01),
                UserName = "UserName",
                PageId = 234
            } }, TotalItem = 1 }).Callback<PagingParam>(p => passedParam = p);
            var service = CreateOfferAffService(OfferAffRepository.Object);

            // Action
            var result = service.GetOfferAffList(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithAffNamesHasSpecialChars_ReturnError()
        {
            // Arrange
            var controller = CreateOfferAffService();

            // Action
            var result = controller.InsertOfferAff(new InsertOfferAffParam { AffNameList = "%&*", PageId = 123 });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Affiliate(s) must be letters and digits, separated by [Comma]", result.Message);
        }

        [Fact]
        public void InsertOfferAff_WhenCallWithAffNamesIsInvalidLength_ReturnError()
        {
            // Arrange
            var controller = CreateOfferAffService();
            StringBuilder affNames = new StringBuilder("affname,affname,affname");
            for(int i = 0; i < 170; i++)
            {
                affNames.Append(",affname,affname,affname");
            }

            // Action
            var result = controller.InsertOfferAff(new InsertOfferAffParam { AffNameList = affNames.ToString() });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The function can only create assignment for up to 500 affiliates per request.", result.Message);
        }

        [Fact]
        public void InsertOfferAff_WhenCall_AddSuccessfully()
        {
            // Arrange
            var OfferAffRepository = new Mock<IOfferAffRepository>();
            OfferAffRepository.Setup(s => s.InsertOfferAff(It.IsAny<InsertOfferAffParam>())).Returns(new ResultInfo());
            var controller = CreateOfferAffService(OfferAffRepository.Object);

            // Action
            var result = controller.InsertOfferAff(new InsertOfferAffParam { AffNameList = "AffName" });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteOfferAffs_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            var param = new DeleteOfferAffParam();
            var OfferAffRepository = new Mock<IOfferAffRepository>();
            OfferAffRepository.Setup(s => s.DeleteOfferAffs(param)).Returns(new ResultInfo());
            var service = CreateOfferAffService(OfferAffRepository.Object);

            // Action
            var result = service.DeleteOfferAffs(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private static OfferAffService CreateOfferAffService(
            IOfferAffRepository OfferAffRepository = null)
        {
            var fakeOfferAffRepository = OfferAffRepository ?? Mock.Of<IOfferAffRepository>();

            return new OfferAffService(fakeOfferAffRepository);
        }
    }
}
