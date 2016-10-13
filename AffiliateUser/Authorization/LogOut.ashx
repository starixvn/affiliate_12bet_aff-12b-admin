<%@ WebHandler Language="C#" Class="LogOut" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services.Authorization;

public class LogOut : IHttpHandler, IRequiresSessionState
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