<%@ WebHandler Language="C#" Class="RequestPayout" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class RequestPayout : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new RequestPayoutService().RequestPayout(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}