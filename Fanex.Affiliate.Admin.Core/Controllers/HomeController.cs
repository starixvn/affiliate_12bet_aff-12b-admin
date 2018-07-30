using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly static SubMenu[] _subMenu = new SubMenu[]{
            new SubMenu{}
        };
        public HomeController(ISessionService sessionService) : base(sessionService)
        {

        }
        public ActionResult Index()
        {

            return View(_subMenu);
        }
    }
}
