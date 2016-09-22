<%@ WebHandler Language="C#" Class="UpdateMessage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdateMessage : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateMessageService().Update(context);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}