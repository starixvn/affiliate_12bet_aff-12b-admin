<%@ WebHandler Language="C#" Class="AddMessage" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Message;

public class AddMessage : IHttpHandler, IRequiresSessionState
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

        var service = new AddMessageService();
        var result = service.AddMessage(data);


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