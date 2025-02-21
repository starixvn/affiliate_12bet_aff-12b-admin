using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class ViewLogServiceTest
    {
        [Fact]
        public void GetLogTypeList_WhenCall_CallCorrectMethodOfdynamicRepository()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<LogType>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<LogType>());

            // Action
            var service = new ViewLogService(dynamicRepository.Object);
            var result = service.GetLogTypeList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<LogType>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
        }

        [Fact]
        public void GetViewLogList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<ViewLog>(It.IsAny<ViewLogCriteria>(), It.IsAny<string>())).Returns(new List<ViewLog>
            {
                new ViewLog { Date = new DateTime(2019, 12, 27) }
            });

            // Action
            var service = new ViewLogService(dynamicRepository.Object);
            var result = service.GetViewLogList(new ViewLogParam());
            var item = result.MainData.FirstOrDefault();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<ViewLog>(It.Is<ViewLogCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
            Assert.Equal("12/27/2019 00:00:00", item.DateText);
        }
    }
}
