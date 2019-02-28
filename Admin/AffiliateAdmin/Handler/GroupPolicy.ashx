<%@ WebHandler Language="C#" Class="GroupPolicy" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GroupPolicy : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GroupPolicyService().GroupPolicy(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}