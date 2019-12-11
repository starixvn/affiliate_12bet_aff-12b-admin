using Fanex.Affiliate.ThirdPartyProduct.Site.Services;

using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? new AuthenticationService();
        }
        public AccountController() : this(null)
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _authenticationService.Auth(model.User, model.Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(model.User, true);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Wrong username or password");
                return View(model);
            }

        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}