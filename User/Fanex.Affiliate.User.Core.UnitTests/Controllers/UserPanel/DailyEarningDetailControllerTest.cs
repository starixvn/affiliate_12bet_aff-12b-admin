using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class DailyEarningDetailControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ILanguageService LanguageService;
        private readonly IStatementHistoryRepository Repo;

        public DailyEarningDetailControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            Repo = FakeStatementHistoryRespository();

        }

        [Fact]
        public void Index_WhenParamIsNeedReturnOnlyContent_ReturnPartialView()
        {
            var param = new DailyEarningParam
            {
                IsOnlyContent = true
            };
            var controller = new DailyEarningDetailController(SessionService, LanguageService, Repo);
            var viewResult = controller.Index(param) as ViewResult;
            var model = viewResult.Model as DailyEarningDetailViewModel;

            Assert.Contains("StatementHistoryPartial/_DailyEarningDetailContent.cshtml", viewResult.ViewName);
            Assert.Null(model.ProductList);
        }

        [Fact]
        public void Index_WhenParamIsNeedReturnOnlyContent_ReturnFullView()
        {
            var param = new DailyEarningParam
            {
                IsOnlyContent = false,
                MonthDate = new DateTime(2020, 11, 23),
                ProductId = 123
            };
            var controller = new DailyEarningDetailController(SessionService, LanguageService, Repo);
            var viewResult = controller.Index(param) as ViewResult;
            var model = viewResult.Model as DailyEarningDetailViewModel;

            Assert.Contains("StatementHistory/DailyEarningDetail.cshtml", viewResult.ViewName);
            Assert.NotNull(model.ProductList);
        }

        private static IStatementHistoryRepository FakeStatementHistoryRespository()
        {
            var repo = Substitute.For<IStatementHistoryRepository>();
            repo.GetDailyEarningDetail(Arg.Compat.Any<DailyEarningParam>()).Returns(new List<DailyEarningDetail>());
            repo.GetProductList(Arg.Compat.Any<bool>()).Returns(new List<ProductItem>());
            repo.GetExtraUserInfo(Arg.Compat.Any<int>(), Arg.Compat.Any<bool>()).Returns(new ExtraUserInfo());
            return repo;
        }
    }
}
