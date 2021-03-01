using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Customer)]
    public class NewViewLogController : ReadOnlySessionBaseController
    {
        private readonly INewViewLogService _newViewLogService;
        public NewViewLogController(
            ISessionService sessionService,
            INewViewLogService newViewLogService) : base(sessionService)
        {
            _newViewLogService = newViewLogService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetViewLogList(ViewLogParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _newViewLogService.GetViewLogList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
