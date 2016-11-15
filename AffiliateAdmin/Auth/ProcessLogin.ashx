<%@ WebHandler Language="C#" Class="ProcessLogin" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Auth;

public class ProcessLogin : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new LoginService().Login(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}