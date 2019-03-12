using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class AdjustmentController : BaseController
    {
        private readonly IAdjustmentRepository _adjustmentRepository;
        private readonly IAffiliateRepository _affiliateRepository;

        public AdjustmentController(
            ISessionService sessionService,
            IAdjustmentRepository adjustmentRepository,
            IAffiliateRepository affiliateRepository) : base(sessionService)
        {
            _adjustmentRepository = adjustmentRepository;
            _affiliateRepository = affiliateRepository;
        }

        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            if(string.Empty.Equals(param.AffName))
            {
                var affInfor = _affiliateRepository.GetAffiliateBasicInfo(param.AffId);
                param.AffName = affInfor.UserName;
            }

            return View("~/Areas/Portal/Views/UserPanel/Adjustment/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetBalanceBroughtForward(int affId)
        {
            return new JsonNetResult
            {
                Data = _adjustmentRepository.GetBalanceBroughtForward(affId == 0 ? SessionService.AffId : affId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertAdjustment(AdjustmentParam param)
        {
            if(ModelState.IsValid)
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

            return new JsonNetResult
            {
                Data = new {
                    IsSuccessful = false,
                    Message = Lang.AmountIsInvalid
                }
            };
        }
    }
}
