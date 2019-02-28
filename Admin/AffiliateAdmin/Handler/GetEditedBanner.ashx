<%@ WebHandler Language="C#" Class="GetEditedBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetEditedBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetEditedBannerService().GetEditedBanner(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}