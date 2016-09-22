<%@ WebHandler Language="C#" Class="GetEmailContent" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetEmailContent : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetEmailContentService().GetEmailContent(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}