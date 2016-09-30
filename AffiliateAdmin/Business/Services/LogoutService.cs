using System.Web;
using System.Web.Security;

namespace AffiliateAdmin.Business.Services
{
    public class LogoutService
    {
        public void Logout(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            DataHandler.Login.Logout(context);
            FormsAuthentication.SignOut();
            context.Session.Clear();
            context.Session.Abandon();

            FormsAuthentication.RedirectToLoginPage();
        }
    }
}