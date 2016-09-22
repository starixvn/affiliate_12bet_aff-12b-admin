<%@ WebHandler Language="C#" Class="GetStatisticsListPaging" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetStatisticsListPaging : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetStatisticsListService().GetStatisticsList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}