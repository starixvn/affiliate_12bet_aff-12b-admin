using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ViewLogController : BaseController
    {
        private readonly IViewLogRepository _viewLogRepository;
        public ViewLogController(
            ISessionService sessionService,
            IViewLogRepository viewLogRepository) : base(sessionService)
        {
            _viewLogRepository = viewLogRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam affiliateLinkParam)
        {
            return View("~/Areas/Portal/Views/UserPanel/ViewLog/Index.cshtml", affiliateLinkParam);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetViewLogList(ViewLogParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _viewLogRepository.GetViewLogList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
