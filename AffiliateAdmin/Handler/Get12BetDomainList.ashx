<%@ WebHandler Language="C#" Class="Get12BetDomainList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class Get12BetDomainList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new Get12BetDomainListService().Get12BetDomainList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}