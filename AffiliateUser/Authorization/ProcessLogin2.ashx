<%@ WebHandler Language="C#" Class="ProcessLogin2" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services.Authorization;

public class ProcessLogin2 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new InternalLoginService().Login(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}