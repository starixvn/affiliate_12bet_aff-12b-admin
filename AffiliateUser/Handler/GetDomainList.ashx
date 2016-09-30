<%@ WebHandler Language="C#" Class="GetDomainList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class GetDomainList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        new GetDomainListService().GetDomainList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}