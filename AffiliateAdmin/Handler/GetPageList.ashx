<%@ WebHandler Language="C#" Class="GetPageList" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetPageList : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
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