using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class ReportController : BaseController
    {
        public ReportController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.ReportDetails, Url = Url.Content("~/Report/MemberReport.aspx") },
                new SubMenu { Name = Lang.AffiliateReport, Url = Url.Content("~/Report/AffiliateReport.aspx") }
            };

            return View(subMenu);
        }
    }
}
