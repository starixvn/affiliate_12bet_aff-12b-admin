namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Filters;
    using MetalModels.UserPanel;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult StatementHistory()
        {
            var param = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                AvailableFromDate = AppSetting.StatementAvailableFrom,
                PageSize = 20
            };

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = 1;

            return View(statementHistoryList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStatementHistoryData(StatementHistoryParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;
            param.AvailableFromDate = AppSetting.StatementAvailableFrom;

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;

            return PartialView("~/Views/UserPanel/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarning()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarningDetail()
        {
            return View();
        }
    }
}