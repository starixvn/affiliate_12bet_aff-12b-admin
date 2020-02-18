using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class ViewLogController : BaseController
    {
        private readonly IViewLogService _viewLogService;
        public ViewLogController(
            ISessionService sessionService,
            IViewLogService viewLogService) : base(sessionService)
        {
            _viewLogService = viewLogService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetViewLogList(ViewLogParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _viewLogService.GetViewLogList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
