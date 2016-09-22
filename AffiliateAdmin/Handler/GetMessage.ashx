<%@ WebHandler Language="C#" Class="GetMessage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetMessage : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetMessageService().GetMessage(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}