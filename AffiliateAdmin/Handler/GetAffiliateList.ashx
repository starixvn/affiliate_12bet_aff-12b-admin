<%@ WebHandler Language="C#" Class="GetAffiliateList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetAffiliateList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetAffiliateListService().GetAffiliateList(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}