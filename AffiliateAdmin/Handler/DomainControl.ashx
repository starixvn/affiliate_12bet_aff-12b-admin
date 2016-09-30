<%@ WebHandler Language="C#" Class="DomainControl" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class DomainControl : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new DomainControlService().DomainControl(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}