using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.FeedbackReport)]
    public class FeedbackReportController : ReadOnlySessionBaseController
    {

        public FeedbackReportController(
            ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Affiliate/Index.cshtml");
        }

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[UpdateUnauthorizedProperty]
        //public ActionResult GetEmailHistoryList(EmailHistoryParam param)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return new JsonNetResult
        //        {
        //            Data = _emailHistoryService.GetEmailHistoryList(param)
        //        };
        //    }

        //    return new JsonNetResult();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[FunctionAuthorize(FunctionName = Function.EmailHistory, IsHost = true)]
        //public ActionResult GetEmailHistoryDetail(int selectedEmailId)
        //{
        //    if (selectedEmailId >= Validation.MinId)
        //    {
        //        return new JsonNetResult
        //        {
        //            Data = _emailHistoryService.GetEmailHistoryDetail(selectedEmailId)
        //        };
        //    }

        //    return new JsonNetResult();
        //}
    }
}
