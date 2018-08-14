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
            ViewBag.AvailableFromDate = param.AvailableFromDate;
            return View("~/Views/UserPanel/StatementHistory/Index.cshtml", statementHistoryList);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(StatementHistoryParam param)
        {
            param.AffId = _sessionService.UserSession.AffId;
            param.IsInternal = _sessionService.IsInternal;
            param.AvailableFromDate = _appSetting.StatementAvailableFrom;
            param.PageSize = param.PageSize ?? DefaultPageSize;

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            ViewBag.AvailableFromDate = param.AvailableFromDate;

            return PartialView("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }

       
    }
}