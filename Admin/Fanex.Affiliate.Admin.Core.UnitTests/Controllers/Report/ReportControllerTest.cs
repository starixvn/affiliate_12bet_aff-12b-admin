namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Report
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Core.Controllers.Report;
    using Core.Extensions;
    using Core.Services.Common;
    using Core.Services.Operation;
    using Core.Services.Report;
    using DTO;
    using DTO.Report;
    using MetaModels.Report;
    using Moq;
    using TestHelper;
    using ViewModels.Report;
    using Xunit;

    public class ReportControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var systemService = new Mock<ISystemService>();
            systemService.Setup(s => s.GetLanguageList()).Returns(new List<Language> { new Language() });
            systemService.Setup(s => s.GetProductList()).Returns(new List<Product> { new Product() });
            systemService.Setup(s => s.GetCurrencies()).Returns(new List<Currency> { new Currency { ExchangeId = 123, Code = "ABC" } });
            var controller = CreateReportController(systemService: systemService.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var model = viewResult.Model as AffiliateReportViewModel;

            // Assert
            Assert.Equal("", viewResult.ViewName);
            Assert.Single(model.ProductList);
            Assert.Single(model.LanguageList);
        }

        [Fact]
        public void GetMainData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportParam();
            AffiliateReportParam passedParam = null;
            var affiliateReportService = new Mock<IAffiliateReportService>();
            affiliateReportService.Setup(s => s.GetMainReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportParam>(p => passedParam = p);
            var controller = CreateReportController(affiliateReportService: affiliateReportService.Object);

            // Action
            var result = controller.GetMainData(param) as JsonNetResult;
            var data = result.Data as EntityResult<AffiliateReport>;

            // Assert
            Assert.Single(data.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetDetailData_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliateReportParam();
            AffiliateReportParam passedParam = null;
            var affiliateReportService = new Mock<IAffiliateReportService>();
            affiliateReportService.Setup(s => s.GetDetailReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportParam>(p => passedParam = p);
            var controller = CreateReportController(affiliateReportService: affiliateReportService.Object);

            // Action
            var result = controller.GetDetailData(param) as JsonNetResult;
            var data = result.Data as EntityResult<AffiliateReport>;

            // Assert
            Assert.Single(data.MainData);
            Assert.Equal(param, passedParam);
        }


        private ReportController CreateReportController(
            ISessionService sessionService = null,
            ISystemService systemService = null,
            IAffiliateReportService affiliateReportService = null,
            ICommonDealService commissionGroupRepository = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakesystemService = systemService ?? Mock.Of<ISystemService>();
            var fakeAffiliateReportService = affiliateReportService ?? Mock.Of<IAffiliateReportService>();
            var fakeCommissionGroupRepository = commissionGroupRepository ?? Mock.Of<ICommonDealService>();

            return new ReportController(fakeSessionService, fakesystemService, fakeAffiliateReportService, fakeCommissionGroupRepository);
        }
    }
}
