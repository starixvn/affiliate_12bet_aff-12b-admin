<%@ WebHandler Language="C#" Class="GetSubCategory" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetSubCategory : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetCommGroupService().GetCommGroup(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}