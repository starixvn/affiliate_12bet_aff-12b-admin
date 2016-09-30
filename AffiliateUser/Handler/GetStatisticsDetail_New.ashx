<%@ WebHandler Language="C#" Class="GetStatisticsDetail_New" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetStatisticsDetail_New : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetStatisticsDetailNewService().GetStatisticsDetailNew(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}