using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MassAdjustment;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.AffAdjustment)]
    public class AdjustmentController : ReadOnlySessionBaseController
    {
        private readonly IMassAdjustmentService _massAdjustmentService;

        public AdjustmentController(
            ISessionService sessionService,
            IMassAdjustmentService massAdjustmentService) : base(sessionService)
        {
            _massAdjustmentService = massAdjustmentService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Affiliate/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AffAdjustment, IsHost = true)]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var param = new MassAdjustmentParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.UserHostAddress
                };
                var data = _massAdjustmentService.UpdateMassAdjusment(file, param);
                return new JsonNetResult
                {
                    Data = data
                };
            }

            return new JsonNetResult
            {
                Data = new AdjustmentResultInfo
                {
                    Result = new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = "You must browse file to upload!"
                    }
                }
            };
        }
    }
}
