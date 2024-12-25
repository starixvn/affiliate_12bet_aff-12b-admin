using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Data.Repository;
using Moq;
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

            dynamicRepository.Setup(r => r.FetchMultiple<dynamic, dynamic>(It.IsAny<TransferReportCriteria>())).Returns((new List<PlayerGgrReport> { new PlayerGgrReport() }, new List<PlayerGgrReport> { new PlayerGgrReport() }));
            var service = CreatePlayerGgrReportService(dynamicRepository.Object);

            // Action
            var result = service.GetPlayerGgrReportData(param);

            // Assert
            Assert.Empty(result.MainData);
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
