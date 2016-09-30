<%@ WebHandler Language="C#" Class="SignUp" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class SignUp : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new SignUpService().SignUp(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}