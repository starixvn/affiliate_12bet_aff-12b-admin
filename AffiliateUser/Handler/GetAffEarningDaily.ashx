<%@ WebHandler Language="C#" Class="GetSubCategory" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetSubCategory : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetAffiliateEarningDailyService().GetAffiliateEarningDaily(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}