<%@ WebHandler Language="C#" Class="DelText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class DelText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new DelTextService().DelText(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}