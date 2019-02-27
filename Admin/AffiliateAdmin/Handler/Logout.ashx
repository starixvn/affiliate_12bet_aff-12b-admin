<%@ WebHandler Language="C#" Class="Logout" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class Logout : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new LogoutService().Logout(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
