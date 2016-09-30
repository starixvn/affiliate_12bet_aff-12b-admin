<%@ WebHandler Language="C#" Class="GetClickStatisticsList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetClickStatisticsListService().GetClickStatisticsList(context);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}