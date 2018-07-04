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
        private const int DefaultPageSize = 20;

        [HttpGet]
        [AjaxOnly]
        public ActionResult StatementHistory()
        {
            var param = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                AvailableFromDate = AppSetting.StatementAvailableFrom,
                PageSize = DefaultPageSize
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
            param.PageSize = param.PageSize ?? DefaultPageSize;

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;

            return PartialView("~/Views/UserPanel/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarning(DailyEarningParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;
            param.SportType = param.SportType.HasValue ? param.SportType.Value : Product.Sportsbook;
            param.MonthDate = param.MonthDate.HasValue ? param.MonthDate.Value : DateTime.Now.AddDays(-1);

            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            var viewModel = new DailyEarningViewModel
            {
                ProductList = param.IsInitial ? _statementHistoryRepository.GetProductList(param.IsInternal) : null,
                Data = _statementHistoryRepository.GetDailyEarning(param),
                SelectedProduct = param.SportType.Value,
                IsKoreanAgentCommGroup = extraUserInfo.GroupName.MatchWith(CommGroupName.KoreanAgentComm),
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };
            ViewBag.MonthDate = param.MonthDate;

            return param.IsInitial ? View(viewModel) :
                                    View("~/Views/UserPanel/StatementHistoryPartial/_DailyEarningContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarningDetail(DailyEarningParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;

            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            var viewModel = new DailyEarningDetailViewModel
            {
                MainData = _statementHistoryRepository.GetDailyEarningDetail(param),
                ProductList = param.IsInitial ? _statementHistoryRepository.GetProductList(param.IsInternal) : null,
                SelectedDate = param.MonthDate.HasValue ? param.MonthDate.Value : DateTime.Now.Date,
                SelectedProduct = param.SportType.HasValue ? param.SportType.Value : Product.Sportsbook,
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };

            return param.IsInitial ? View(viewModel) :
                                    View("~/Views/UserPanel/StatementHistoryPartial/_DailyEarningDetailContent.cshtml", viewModel);
        }
    }
}