using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.Home;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Home
{
    public class HomeServiceTest
    {
        [Fact]
        public void GetOnlineList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<OnlineListInfo>(It.IsAny<UserOnlineCriteria>())).Returns(
                new List<OnlineListInfo> {
                    new OnlineListInfo {
                        LoggedIn = new DateTime(2019, 11, 15),
                        LastRequest = new DateTime(2019, 11, 15)
                    }
                }).Callback<UserOnlineCriteria>(c => c.TotalItem = 1);

            // Action
            var service = new HomeService(dynamicRepository.Object);
            var result = service.GetOnlineList(1, 20);
            var item = result.MainData.FirstOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("11/15/2019 00:00:00", item.LoggedInText);
            Assert.Equal("11/15/2019 00:00:00", item.LastRequestText);
        }

        [Fact]
        public void GetGeneralInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<GeneralInfo>(It.IsAny<BaseCriteria>())).Returns(new GeneralInfo());

            // Action
            var service = new HomeService(dynamicRepository.Object);
            var result = service.GetGeneralInfo();

            // Assert
            Assert.NotNull(result);
        }
    }
}
