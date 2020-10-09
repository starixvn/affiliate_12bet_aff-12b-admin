using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class StatementHistoryController : ReadOnlySessionBaseController
    {
        private readonly IStatementHistoryService _statementHistoryService;

        public StatementHistoryController(
            ISessionService sessionService,
            IStatementHistoryService statementHistoryService) : base(sessionService)
        {
            _statementHistoryService = statementHistoryService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetStatementHistoryList(AffiliatePagingByDateParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _statementHistoryService.GetStatementHistoryList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDailyEarning(DailyEarningParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _statementHistoryService.GetDailyEarning(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDailyEarningDetail(DailyEarningParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _statementHistoryService.GetDailyEarningDetail(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
