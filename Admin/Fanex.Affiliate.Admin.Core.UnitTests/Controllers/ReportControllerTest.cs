using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class ReportControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(s => s.GetProductList()).Returns(new List<Product> { new Product()});
            var languageService = new Mock<ILanguageService>();
            languageService.Setup(s => s.GetLanguageList()).Returns(new List<Language> { new Language() });
            var controller = CreateReportController(productRepository: productRepository.Object, languageService: languageService.Object);

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
            var param = new AffiliateReportMainParam();
            AffiliateReportMainParam passedParam = null;
            var affiliateReportService = new Mock<IAffiliateReportService>();
            affiliateReportService.Setup(s => s.GetMainReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportMainParam>(p => passedParam = p);
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
            var param = new AffiliateReportMainParam();
            AffiliateReportMainParam passedParam = null;
            var affiliateReportService = new Mock<IAffiliateReportService>();
            affiliateReportService.Setup(s => s.GetDetailReportData(param)).Returns(new EntityResult<AffiliateReport> { MainData = new List<AffiliateReport> { new AffiliateReport() }, TotalItem = 1 }).Callback<AffiliateReportMainParam>(p => passedParam = p);
            var controller = CreateReportController(affiliateReportService: affiliateReportService.Object);

            // Action
            var result = controller.GetDetailData(param) as JsonNetResult;
            var data = result.Data as EntityResult<AffiliateReport>;

            // Assert
            Assert.Single(data.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetAffiliateList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = "abc";
            var passedParam = string.Empty;
            var affiliateReportService = new Mock<IAffiliateReportService>();
            affiliateReportService.Setup(s => s.SearchAffiliateList(param)).Returns(new List<string> { "abc" }).Callback<string>(p => passedParam = p);
            var controller = CreateReportController(affiliateReportService: affiliateReportService.Object);

            // Action
            var result = controller.GetAffiliateList(param) as JsonResult;
            var data = result.Data as List<string>;

            // Assert
            Assert.Single(data);
            Assert.Equal(param, passedParam);
        }

        private ReportController CreateReportController(
            ISessionService sessionService = null,
            IProductRepository productRepository = null,
            ILanguageService languageService = null,
            IAffiliateReportService affiliateReportService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeProductRepository = productRepository ?? Mock.Of<IProductRepository>();
            var fakeLanguageService = languageService ?? Mock.Of<ILanguageService>();
            var fakeAffiliateReportService = affiliateReportService ?? Mock.Of<IAffiliateReportService>();

            return new ReportController(fakeSessionService, fakeProductRepository, fakeLanguageService, fakeAffiliateReportService);
        }
    }
}
