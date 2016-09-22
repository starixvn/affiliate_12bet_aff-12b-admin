<%@ WebHandler Language="C#" Class="CommGroupAssign" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class CommGroupAssign : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new CommGroupAssignService().CommGroupAssign(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}