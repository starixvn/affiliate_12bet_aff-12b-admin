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
    public class TransferServiceTest
    {
        [Fact]
        public void GetTransferReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new TransferReportParam { TransId = "123", FromDate = new DateTime(2021, 10, 01), ToDate = new DateTime(2021, 10, 21) };
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<TransferReport>(It.IsAny<TransferReportCriteria>(), It.IsAny<string>())).Returns(new List<TransferReport> { new TransferReport() });
            var service = CreateTransferReportService(dynamicRepository.Object);

            // Action
            var result = service.GetTransferReportData(param);

            // Assert
            Assert.Single(result.MainData);
            dynamicRepository.Verify(d => d.Fetch<TransferReport>(It.Is<TransferReportCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
        }

        [Fact]
        public void GetTransferReportData_WhenCallInvalidParam_ReturnEmpty()
        {
            // Arrange
            var service = CreateTransferReportService();

            // Action
            var result = service.GetTransferReportData(new TransferReportParam { FromDate = new DateTime(2021, 11, 11), ToDate = new DateTime(2021, 10, 21) });

            // Assert
            Assert.Empty(result.MainData);
        }

        private static TransferReportService CreateTransferReportService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new TransferReportService(fakedynamicRepository);
        }
    }
}
