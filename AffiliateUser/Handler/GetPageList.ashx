<%@ WebHandler Language="C#" Class="GetPageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetPageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetPageListService().GetPageList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}