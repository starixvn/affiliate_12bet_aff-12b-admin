using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public HomeController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            var subMenu = new[] {
                new SubMenu
                {
                    Name = Lang.Home,
                    Url=  Url.Content("~/Home/Home.aspx")
                }
         };
            return View(subMenu);
        }
    }
}