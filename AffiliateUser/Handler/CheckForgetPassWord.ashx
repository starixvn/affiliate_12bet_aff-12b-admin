<%@ WebHandler Language="C#" Class="CheckForgetPassWord" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class CheckForgetPassWord : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new CheckForgetPasswordService().CheckForgetPassword(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}