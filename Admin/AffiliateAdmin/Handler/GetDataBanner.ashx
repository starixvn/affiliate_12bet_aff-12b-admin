<%@ WebHandler Language="C#" Class="GetDataBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetDataBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetDataBannerService().GetDataBanner(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}