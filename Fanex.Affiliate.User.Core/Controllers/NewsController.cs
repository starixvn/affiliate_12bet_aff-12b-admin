using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CustomAuthorize]
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;

        public NewsController(
            ISessionService sessionService,
            ILanguageService languageService,
            INewsService newsService) : base(sessionService, languageService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult MessageHistory()
        {
            var param = new MessageHistoryParam
            {
                IsInternal = SessionService.IsInternal,
                Important = null,
                Language = SessionService.CurrentLanguage
            };

            var messageList = _newsService.GetMessageList(param);
            var viewModel = new MessageHistoryViewModel
            {
                MessageList = messageList,
                TotalItems = param.Nrows
            };

            ViewBag.StartRowNumber = 1;

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMessageHistory(MessageHistoryParam param)
        {
            param.IsInternal = SessionService.IsInternal;
            param.Language = SessionService.CurrentLanguage;

            var messageList = _newsService.GetMessageList(param);
            var viewModel = new MessageHistoryViewModel
            {
                MessageList = messageList,
                TotalItems = param.Nrows
            };

            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;

            return PartialView("~/Views/News/_MessageContent.cshtml", viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [AjaxOnly]
        public ActionResult LatestHotNews()
        {
            var userSession = SessionService.UserSession;
            var isInternal = (userSession != null) && SessionService.IsInternal;

            var hotNews = _newsService.GetLatestHotNews(isInternal);
            var preferredLanguage = userSession?.PreferredLanguage ?? SystemLanguage.DefaultLanguageCode;
            var hotNewsList = hotNews.HotNewsList;

            var viewModel = new LatestHotNewViewModel
            {
                IsImportant = hotNews.IsImportant,
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated && SessionService.UserSession != null
            };

            if (hotNewsList.Any())
            {
                viewModel.SelectedHotNews = hotNewsList.ContainsKey(preferredLanguage) ?
                                    hotNewsList[preferredLanguage] :
                                    hotNewsList.First().Value;
            }

            return PartialView("~/Views/News/_LatestHotNews.cshtml", viewModel);
        }
    }
}
