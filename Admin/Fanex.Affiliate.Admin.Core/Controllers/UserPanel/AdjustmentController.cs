using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class AdjustmentController : BaseController
    {
        private readonly IAdjustmentRepository _adjustmentRepository;

        public AdjustmentController(
            ISessionService sessionService,
            IAdjustmentRepository adjustmentRepository) : base(sessionService)
        {
            _adjustmentRepository = adjustmentRepository;
        }

        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            return View("~/Areas/Portal/Views/UserPanel/Adjustment/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetBalanceBroughtForward(int affId)
        {
            return new JsonNetResult
            {
                Data = _adjustmentRepository.GetBalanceBroughtForward(affId == 0 ? SessionService.AffId : affId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult InsertNote(AdjustmentParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            var model = _adjustmentRepository.InsertAdjustment(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
