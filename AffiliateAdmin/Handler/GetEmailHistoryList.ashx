<%@ WebHandler Language="C#" Class="GetEmailHistoryList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetEmailHistoryList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetEmailHistoryService().GetEmailHistoryList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}