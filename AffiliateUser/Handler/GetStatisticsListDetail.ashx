<%@ WebHandler Language="C#" Class="GetStatisticsListPaging" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetStatisticsListPaging : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetStatisticsListDetailService().GetStatisticsListDetail(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}