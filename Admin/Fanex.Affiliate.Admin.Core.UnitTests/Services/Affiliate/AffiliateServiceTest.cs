using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class AffiliateServiceTest
    {
        [Fact]
        public void GetAffiliateBasicInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<AffiliateBasicInfo>(It.IsAny<AffBasicInfoCriteria>(), It.IsAny<string>())).Returns(new AffiliateBasicInfo());

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.GetAffiliateBasicInfo(123, "aff");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ApproveAffiliate_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<ApproveAffCommand>(), It.IsAny<string>())).Callback<INonQueryCommand>(a => (a as ApproveAffCommand).AffCode = "123456");

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var (result, affCode) = service.ApproveAffiliate(new ApproveAffiliateParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.NotNull(affCode);
        }

        [Fact]
        public void GetAffiliateList_WhenCall_ReturnCorrectAfilliateInfo()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<AffiliateInfo>(It.IsAny<AffiliateCriteria>(), It.IsAny<string>()))
                .Returns(new List<AffiliateInfo> { new AffiliateInfo { ApprovedBy = "abc", RegistrationDate = new DateTime(2020, 01, 01), ApprovalDate = new DateTime(2020, 04, 15) } });

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.GetAffiliateList(new AffiliateInfoParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("01/01/2020 00:00:00", item.RegistrationDateText);
            Assert.Equal("04/15/2020 00:00:00", item.ApprovalDateText);
            Assert.Equal("abc", item.ApprovedByText);
        }

        [Fact]
        public void GetAffiliateList_WhenCall_ReturnAffiliateHasNull()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<AffiliateInfo>(It.IsAny<AffiliateCriteria>(), It.IsAny<string>()))
                .Returns(new List<AffiliateInfo> { new AffiliateInfo { RegistrationDate = new DateTime(2020, 01, 01) } });

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.GetAffiliateList(new AffiliateInfoParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("01/01/2020 00:00:00", item.RegistrationDateText);
            Assert.Equal("N/A", item.ApprovalDateText);
            Assert.Equal("N/A", item.ApprovedByText);
        }

        [Fact]
        public void ToggleFavorite_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<ToggleFavoriteCommand>(), It.IsAny<string>()));

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.ToggleFavorite(new FavoriteParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ChangePassword_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Execute(It.IsAny<ChangeAffPasswordCommand>(), It.IsAny<string>()));

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.ChangePassword(new PasswordParam { Password = "123abc", ConfirmPassword = "123abc" });

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
