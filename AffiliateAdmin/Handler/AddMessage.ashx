<%@ WebHandler Language="C#" Class="AddMessage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AddMessage : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new AddMessageService().AddMessage(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}