using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.AffAdjustment)]
    public class AdjustmentController : ReadOnlySessionBaseController
    {
        private readonly IAdjustmentService _adjustmentService;
        private const int DefaultBalanceBfValue = 0;

        public AdjustmentController(
            ISessionService sessionService,
            IAdjustmentService adjustmentService) : base(sessionService)
        {
            _adjustmentService = adjustmentService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBalanceBroughtForward(int affId)
        {
            if (affId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _adjustmentService.GetBalanceBroughtForward(affId)
                };
            }

            return new JsonNetResult
            {
                Data = DefaultBalanceBfValue
            };

        }

        [FunctionAuthorize(FunctionName = Function.AffAdjustment, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertAdjustment(AdjustmentParam param)
        {
            if (param.AffId >= Validation.MinId)
            {
                var absBalanceBf = Math.Abs(_adjustmentService.GetBalanceBroughtForward(param.AffId));

                if (ModelState.IsValid && param.IsValid(absBalanceBf))
                {
                    var adjustmentParam = new AdjustmentParam
                    {
                        AffId = param.AffId,
                        UserId = SessionService.UserId,
                        Ip = Request.UserHostAddress,
                        Amount = param.Amount,
                        Description = Server.HtmlEncode(param.Description)
                    };

                    return Json(_adjustmentService.InsertAdjustment(adjustmentParam));
                }

                return new JsonNetResult
                {
                    Data = ResultInfo.GenerateErr(Lang.AmountIsInvalid)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("Affiliate not exists!")
            };
        }
    }
}
