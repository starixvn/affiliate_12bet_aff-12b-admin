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
    public class AffiliateReportServiceTest
    {
        [Fact]
        public void GetMainReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportParam { ProductIds = "123", FromDate = new DateTime(2019, 10, 24), ToDate = new DateTime(2019, 11, 24) };

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.FetchMultiple<AffiliateReport, AffiliateReport>(It.IsAny<AffiliateReportCriteria>(), It.IsAny<string>())).Returns((new List<AffiliateReport> { new AffiliateReport() }, new List<AffiliateReport> { new AffiliateReport() }));
            var service = CreateAffiliateReportService(dynamicRepository.Object);

            // Action
            var result = service.GetMainReportData(param);

            // Assert
            dynamicRepository.Verify(d => d.FetchMultiple<AffiliateReport, AffiliateReport>(It.Is<AffiliateReportCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetMainReportData_WhenCallInvalidParam_ReturnEmpty()
        {
            // Arrange
            var service = CreateAffiliateReportService();

            // Action
            var result = service.GetMainReportData(new AffiliateReportParam { FromDate = new DateTime(2019, 11, 24), ToDate = new DateTime(2019, 10, 24) });

            // Assert
            Assert.Null(result.MainData);
        }

        [Fact]
        public void GetDetailReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportParam();

            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.FetchMultiple<AffiliateReport, AffiliateReport>(It.IsAny<AffiliateReportDetailCriteria>(), It.IsAny<string>())).Returns((new List<AffiliateReport> { new AffiliateReport() }, new List<AffiliateReport> { new AffiliateReport() }));
            var service = CreateAffiliateReportService(dynamicRepository.Object);

            // Action
            var result = service.GetDetailReportData(param);

            // Assert
            dynamicRepository.Verify(d => d.FetchMultiple<AffiliateReport, AffiliateReport>(It.Is<AffiliateReportDetailCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetReportCutOffDate_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<DateTime>(It.IsAny<ReportCutOffDateCriteria>(), It.IsAny<string>())).Returns(new DateTime(2023, 11, 01));
            var service = CreateAffiliateReportService(dynamicRepository.Object);

            // Action
            var result = service.GetReportCutOffDate();

            // Assert
            dynamicRepository.Verify(d => d.Get<DateTime>(It.Is<ReportCutOffDateCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Equal("01/11/2023", result.ToString("dd/MM/yyyy"));
        }

        private static AffiliateReportService CreateAffiliateReportService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new AffiliateReportService(fakedynamicRepository);
        }
    }
}
