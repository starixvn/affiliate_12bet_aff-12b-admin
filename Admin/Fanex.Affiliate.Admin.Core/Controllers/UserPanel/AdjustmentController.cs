using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Resources;
using System;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class AdjustmentController : BaseController
    {
        private readonly IAdjustmentRepository _adjustmentRepository;
        private const int DefaultBalanceBfValue = 0;

        public AdjustmentController(
            ISessionService sessionService,
            IAdjustmentRepository adjustmentRepository) : base(sessionService)
        {
            _adjustmentRepository = adjustmentRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBalanceBroughtForward(int affId)
        {
            if(affId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _adjustmentRepository.GetBalanceBroughtForward(affId)
                };
            }

            return new JsonNetResult
            {
                Data = DefaultBalanceBfValue
            };

        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertAdjustment(AdjustmentParam param)
        {
            if (param.AffId >= Validation.MinId)
            {
                var absBalanceBf = Math.Abs(_adjustmentRepository.GetBalanceBroughtForward(param.AffId));

                if(ModelState.IsValid && param.IsValid(absBalanceBf))
                {
                    param.UserId = SessionService.UserId;
                    param.Ip = Request.UserHostAddress;

                    return Json(_adjustmentRepository.InsertAdjustment(param));
                }

                return new JsonNetResult
                {
                    Data = new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = Lang.AmountIsInvalid
                    }
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Affiliate not exists!"
                }
            };
        }
    }
}
