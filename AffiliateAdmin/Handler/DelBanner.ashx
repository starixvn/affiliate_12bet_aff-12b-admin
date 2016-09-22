<%@ WebHandler Language="C#" Class="DelBanner" %>

using System.Web;
using AffiliateAdmin.Business.Services;
using System.Web.SessionState;

public class DelBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer != null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/plain";
        var service = new DeleteBannerService();
        service.DeleteBanner(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}