using System.Web.Mvc;
using System.Web.Security;
using ONEbook.UM.Services;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class NotifyController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public NotifyController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UnderMaintenance()
        {
            var underMaintenanceTimeFrom = string.Empty;
            var underMaintenanceTimeTo = string.Empty;
            var maintenanceInfo = _maintenanceService.GetUnderMaintenanceTime();

            if (!maintenanceInfo.IsUnderMaintenanceTime)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            if (maintenanceInfo.IsUnderMaintenanceTime)
            {
                underMaintenanceTimeFrom = maintenanceInfo.From;
                underMaintenanceTimeTo = maintenanceInfo.To;
            }

            ViewBag.UmTimeFrom = underMaintenanceTimeFrom;
            ViewBag.UmTimeTo = underMaintenanceTimeTo;

            return View();
        }
    }
}
