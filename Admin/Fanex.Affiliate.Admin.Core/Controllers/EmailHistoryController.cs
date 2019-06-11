using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.EmailHistory;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class EmailHistoryController : BaseController
    {
        private readonly IEmailHistoryService _emailHistoryService;

        public EmailHistoryController(
            ISessionService sessionService,
            IEmailHistoryService emailHistoryService) : base(sessionService)
        {
            _emailHistoryService = emailHistoryService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/EmailHistory/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetEmailHistoryList(EmailHistoryParam param)
        {
            return new JsonNetResult
            {
                Data = _emailHistoryService.GetEmailHistoryList(param)
            };
        }
    }
}
