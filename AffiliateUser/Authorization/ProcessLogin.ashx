<%@ WebHandler Language="C#" Class="ProcessLogin" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services.Authorization;

public class ProcessLogin : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        AuthHelper.ValidateXsrfToken(context);
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