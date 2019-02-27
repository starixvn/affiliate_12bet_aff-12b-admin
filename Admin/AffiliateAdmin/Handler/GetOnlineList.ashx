<%@ WebHandler Language="C#" Class="GetOnlineList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetOnlineList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetOnlineListService().GetOnlineList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}