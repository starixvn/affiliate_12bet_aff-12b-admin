using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.NewsViewLog;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.NewViewLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.AffAdjustment)]
    public class ViewLogController: ReadOnlySessionBaseController
    {
        private readonly INewViewLogService _newViewLogService;

        public ViewLogController(
            ISessionService sessionService,
            INewViewLogService newViewLogService) : base(sessionService)
        {
            _newViewLogService = newViewLogService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Affiliate/NewViewLog.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNewViewLogList(NewViewLogParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                var model = _newViewLogService.GetNewViewLogList(param);

                return new JsonNetResult
                {
                    Data = model
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<NewViewLogInfo>
                {
                    MainData = new List<NewViewLogInfo>(),
                    TotalItem = 0
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNewViewLogDetailList(NewViewLogParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                var model = _newViewLogService.GetNewViewLogDetailList(param);

                return new JsonNetResult
                {
                    Data = model
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<NewViewLogDetailInfo>
                {
                    MainData = new List<NewViewLogDetailInfo>(),
                    TotalItem = 0
                }
            };
        }
    }
}
