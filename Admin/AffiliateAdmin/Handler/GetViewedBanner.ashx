<%@ WebHandler Language="C#" Class="GetViewedBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetViewedBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetViewedBannerService().GetViewedBanner(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}