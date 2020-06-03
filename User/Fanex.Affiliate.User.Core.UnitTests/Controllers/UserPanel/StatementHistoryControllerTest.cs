using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class StatementHistoryControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ILanguageService LanguageService;
        private readonly IAppSetting AppSetting;
        private readonly IStatementHistoryRepository Repo;

        public StatementHistoryControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            AppSetting = Substitute.For<IAppSetting>();
            Repo = Substitute.For<IStatementHistoryRepository>();
        }

        [Fact]
        public void Index_WhenCallWithNoParm_ReturnCorrectViewWithDefaultValue()
        {
            var controller = CreateController();
            var defaultParam = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                PageSize = 20
            };
            Repo.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()).Returns(new List<StatementHistory>());
            var result = controller.Index() as ViewResult;

            Repo.Received().GetStatementHistory(Arg.Compat.Is<StatementHistoryParam>(param =>
                param.AffId == defaultParam.AffId &&
                param.IsInternal == defaultParam.IsInternal &&
                param.PageSize == defaultParam.PageSize));
            Assert.Equal("~/Views/UserPanel/StatementHistory/Index.cshtml", result.ViewName);
        }

        [Fact]
        public void Index_WhenCallWithParam_ReturnPartialView()
        {
            var controller = CreateController();
            var param = new StatementHistoryParam
            {
                PageIndex = 1
            };
            Repo.When(a => a.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()))
                .Do( a =>
                {
                    param.NRows = 10;
                    param.PageSize = 10;
                });
            var result = controller.Index(param) as PartialViewResult;

            var startRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            Assert.Equal("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", result.ViewName);
            Assert.Equal(param.NRows, result.ViewBag.TotalItem);
            Assert.Equal(startRowNumber, result.ViewBag.StartRowNumber);
        }

        private StatementHistoryController CreateController()
            => new StatementHistoryController(SessionService, LanguageService, AppSetting, Repo);
    }
}
