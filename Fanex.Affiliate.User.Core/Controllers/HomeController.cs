using System.Web.Mvc;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public HomeController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            var result = new JResult
            {

            };

            return Json(result);
        }
    }
}
