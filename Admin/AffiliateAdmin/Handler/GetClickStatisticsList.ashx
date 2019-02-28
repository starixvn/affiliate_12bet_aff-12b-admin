<%@ WebHandler Language="C#" Class="GetClickStatisticsList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
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