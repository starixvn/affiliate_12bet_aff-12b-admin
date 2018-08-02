namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Extensions;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel;

    [CheckUMFilter]
    [CustomAuthorize]
    public class StatementHistoryController : Controller
    {
        private const int DefaultPageSize = 20;
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public StatementHistoryController(ISessionService sessionService,
            IAppSetting appSetting,
            IStatementHistoryRepository statementHistoryRepository

            )
        {
            _sessionService = sessionService;
            _appSetting = appSetting;
            _statementHistoryRepository = statementHistoryRepository;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var param = new StatementHistoryParam
            {
                AffId = _sessionService.UserSession.AffId,
                IsInternal = _sessionService.IsInternal,
                AvailableFromDate = _appSetting.StatementAvailableFrom,
                PageSize = DefaultPageSize
            };

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = 1;

            return View("~/Views/UserPanel/StatementHistory/Index.cshtml",statementHistoryList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStatementHistoryData(StatementHistoryParam param)
        {
            param.AffId = _sessionService.UserSession.AffId;
            param.IsInternal = _sessionService.IsInternal;
            param.AvailableFromDate = _appSetting.StatementAvailableFrom;
            param.PageSize = param.PageSize ?? DefaultPageSize;

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;

            return PartialView("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarning(DailyEarningParam param)
        {
            param.AffId = _sessionService.UserSession.AffId;
            param.IsInternal = _sessionService.IsInternal;
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
                                    View("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarningDetail(DailyEarningParam param)
        {
            param.AffId = _sessionService.UserSession.AffId;
            param.IsInternal = _sessionService.IsInternal;

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
                                    View("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningDetailContent.cshtml", viewModel);
        }
    }
}