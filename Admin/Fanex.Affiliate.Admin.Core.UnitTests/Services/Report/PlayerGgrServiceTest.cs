using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Data.Repository;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Report
{
    public class PlayerGgrServiceTest
    {
        [Fact]
        public void GetPlayerGgrReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PlayerGgrReportParam { ProductIds = "123", FromDate = new DateTime(2021, 09, 01), ToDate = new DateTime(2021, 09, 29) };
            var dynamicRepository = new Mock<IDynamicRepository>();

            dynamicRepository.Setup(r => r.FetchMultiple<JObject, JObject>(It.IsAny<PlayerGgrReportCriteria>(), It.IsAny<string>())).Returns((new List<JObject> { new JObject() }, new List<JObject> { new JObject() }));
            var service = CreatePlayerGgrReportService(dynamicRepository.Object);

            // Action
            var result = service.GetPlayerGgrReportData(param);

            // Assert
            dynamicRepository.Verify(d => d.FetchMultiple<JObject, JObject>(It.Is<PlayerGgrReportCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetPlayerGgrReportData_WhenCallInvalidParam_ReturnEmpty()
        {
            // Arrange
            var service = CreatePlayerGgrReportService();

            // Action
            var result = service.GetPlayerGgrReportData(new PlayerGgrReportParam { FromDate = new DateTime(2020, 09, 01), ToDate = new DateTime(2021, 09, 29) });

            // Assert
            Assert.Empty(result.MainData);
        }

        private static PlayerGgrReportService CreatePlayerGgrReportService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new PlayerGgrReportService(fakedynamicRepository);
        }
    }
}
