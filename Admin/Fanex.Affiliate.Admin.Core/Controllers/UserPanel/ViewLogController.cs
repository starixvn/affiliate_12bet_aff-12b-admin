using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class ViewLogController : BaseController
    {
        private readonly IViewLogRepository _viewLogRepository;
        public ViewLogController(
            ISessionService sessionService,
            IViewLogRepository viewLogRepository) : base(sessionService)
        {
            _viewLogRepository = viewLogRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetViewLogList(ViewLogParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _viewLogRepository.GetViewLogList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
