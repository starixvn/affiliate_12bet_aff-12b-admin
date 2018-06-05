namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Extensions;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

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
        public ActionResult DailyEarning(DateTime? monthDate)
        {
            var param = new DailyEarningParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                SportType = SportType.Sportsbook,
                MonthDate = monthDate.HasValue ? monthDate.Value : DateTime.Now.AddDays(-1)
            };

            var viewModel = GetDailyEarningData(param, true);
            ViewBag.MonthDate = param.MonthDate;

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDailyEarning(DateTime monthDate, byte sportType)
        {
            var param = new DailyEarningParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                SportType = sportType,
                MonthDate = monthDate
            };

            var viewModel = GetDailyEarningData(param, false);

            return PartialView("~/Views/UserPanel/StatementHistoryPartial/_DailyEarningContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarningDetail()
        {
            return View();
        }

        private DailyEarningViewModel GetDailyEarningData(DailyEarningParam param, bool isInitial)
        {
            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            return new DailyEarningViewModel
            {
                SportTypeList = isInitial ? _statementHistoryRepository.GetSportTypeList(param.AffId, param.IsInternal) : null,
                Data = _statementHistoryRepository.GetDailyEarning(param),
                SelectedSportType = param.SportType,
                IsKoreanAgentCommGroup = extraUserInfo.GroupName.MatchWith(CommGroupName.KoreanAgentComm),
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };
        }
    }
}