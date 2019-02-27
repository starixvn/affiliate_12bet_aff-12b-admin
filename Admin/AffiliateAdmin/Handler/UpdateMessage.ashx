<%@ WebHandler Language="C#" Class="UpdateMessage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Message;

public class UpdateMessage : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/plain";

        string data = context.Request.Params["params"];
        var service = new UpdateMessageService();
        var result = service.Update(data);

        context.Response.Write(result.Message);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}