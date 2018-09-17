namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;

    [CustomAuthorize]
    public class StatementHistoryController : BaseController
    {
        private const int DefaultPageSize = 20;
        private readonly IAppSetting _appSetting;
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public StatementHistoryController(ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting,
            IStatementHistoryRepository statementHistoryRepository

            ) : base(sessionService, languageService)
        {
            _appSetting = appSetting;
            _statementHistoryRepository = statementHistoryRepository;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var param = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                PageSize = DefaultPageSize
            };

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = 1;
            ViewBag.AvailableFromDate = _appSetting.StatementAvailableFrom;
            return View("~/Views/UserPanel/StatementHistory/Index.cshtml", statementHistoryList);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(StatementHistoryParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;
            param.PageSize = param.PageSize ?? DefaultPageSize;

            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            ViewBag.AvailableFromDate = _appSetting.StatementAvailableFrom;
            return PartialView("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }


    }
}