<%@ WebHandler Language="C#" Class="GetViewedText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetViewedText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetViewedTextService().GetViewedText(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}