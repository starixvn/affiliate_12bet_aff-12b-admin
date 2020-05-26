using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class CommissionReportControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ILanguageService LanguageService;
        private readonly ICommissionReportService CommissionService;

        public CommissionReportControllerTest()
        {
            SessionService = NSubstitute.Substitute.For<ISessionService>();
            LanguageService = NSubstitute.Substitute.For<ILanguageService>();
            CommissionService = NSubstitute.Substitute.For<ICommissionReportService>();
        }


        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new CommissionReportController(SessionService, LanguageService, CommissionService);
            var result = controller.Index() as ViewResult;
            Assert.Contains("CommissionReport/Index.cshtml", result.ViewName);
        }
    }
}
