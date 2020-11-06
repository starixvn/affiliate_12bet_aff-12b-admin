using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.EmailHistory;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [Authorize]
    public class EmailHistoryController : ReadOnlySessionBaseController
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
            return View("~/Areas/Portal/Views/Affiliate/EmailHistory.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetEmailHistoryList(EmailHistoryParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _emailHistoryService.GetEmailHistoryList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetEmailHistoryDetail(int selectedEmailId)
        {
            if (selectedEmailId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _emailHistoryService.GetEmailHistoryDetail(selectedEmailId)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        public ActionResult DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _emailHistoryService.DeleteEmailHistory(param)
                };
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }
    }
}
