using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Report
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class PlayerCpaReportController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IPlayerGgrReportService _playerGgrReportService;

        public PlayerCpaReportController(
            ISessionService sessionService,
            ISystemService systemService,
            IPlayerGgrReportService playerGgrReportService) : base(sessionService)
        {
            _systemService = systemService;
            _playerGgrReportService = playerGgrReportService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Report/PlayerCpaReport.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetData(PlayerGgrReportParam param)
        {
            return GenerateJsonNetResult(ModelState.IsValid ? _playerGgrReportService.GetPlayerGgrReportData(param) : new EntityResult<PlayerGgrReport> { MainData = new List<PlayerGgrReport>() });
        }
    }
}
