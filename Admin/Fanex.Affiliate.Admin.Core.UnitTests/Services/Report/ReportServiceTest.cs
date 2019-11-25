using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository.Report;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Report
{
    public class ReportServiceTest
    {
        [Fact]
        public void GetMainReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportMainParam { ProductIds = "123", FromDate = new DateTime(2019, 10, 24), ToDate = new DateTime(2019, 11, 24) };
            AffiliateReportMainParam passedParam = null;
            var repository = new Mock<IAffiliateReportRepository>();
            repository.Setup(re => re.GetMainReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportMainParam>(p => passedParam = p);
            var service = CreateAffiliateReportService(repository.Object);

            // Action
            var result = service.GetMainReportData(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetMainReportData_WhenCallInvalidParam_ReturnEmpty()
        {
            // Arrange
            var service = CreateAffiliateReportService();

            // Action
            var result = service.GetMainReportData(new AffiliateReportMainParam { FromDate = new DateTime(2019, 11, 24), ToDate = new DateTime(2019, 10, 24) });

            // Assert
            Assert.Null(result.MainData);
        }

        [Fact]
        public void GetDetailReportData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportMainParam();
            AffiliateReportMainParam passedParam = null;
            var repository = new Mock<IAffiliateReportRepository>();
            repository.Setup(re => re.GetDetailReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportMainParam>(p => passedParam = p);
            var service = CreateAffiliateReportService(repository.Object);

            // Action
            var result = service.GetDetailReportData(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void SearchAffiliateList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = "abc";
            var passedParam = string.Empty;
            var repository = new Mock<IAffiliateReportRepository>();
            repository.Setup(re => re.SearchAffiliateList(param)).Returns(new List<string> { "abc" }).Callback<string>(p => passedParam = p);
            var service = CreateAffiliateReportService(repository.Object);

            // Action
            var result = service.SearchAffiliateList(param);

            // Assert
            Assert.Single(result);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void SearchAffiliateList_WhenCall_ReturnEmpty()
        {
            // Arrange
            var service = CreateAffiliateReportService();

            // Action
            var result = service.SearchAffiliateList(string.Empty);

            // Assert
            Assert.Empty(result);
        }

        private static AffiliateReportService CreateAffiliateReportService(IAffiliateReportRepository affiliateReportRepository = null)
        {
            var fakeAffiliateReportRepository = affiliateReportRepository ?? Mock.Of<IAffiliateReportRepository>();

            return new AffiliateReportService(fakeAffiliateReportRepository);
        }
    }
}
