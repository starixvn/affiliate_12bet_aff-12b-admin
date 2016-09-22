<%@ WebHandler Language="C#" Class="UpdatePolicy" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdatePolicy : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdatePolicyService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}