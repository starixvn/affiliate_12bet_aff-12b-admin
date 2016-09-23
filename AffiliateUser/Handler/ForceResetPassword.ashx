<%@ WebHandler Language="C#" Class="ForceResetPassword" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class ForceResetPassword : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new ForceResetPasswordService().ForceResetPassword(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}