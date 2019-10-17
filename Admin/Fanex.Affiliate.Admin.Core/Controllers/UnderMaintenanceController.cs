using ONEbook.UM.Services.Impl;
using System.Web.Mvc;
using System.Web.Security;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class UnderMaintenanceController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var _maintenanceService = new MaintenanceService();

            if (_maintenanceService.IsUnderMaintenance())
            {
                var underMaintenanceTime = _maintenanceService.GetUnderMaintenanceTime();

                return View(model: underMaintenanceTime);
            }

            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}
