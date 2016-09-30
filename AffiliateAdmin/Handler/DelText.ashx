<%@ WebHandler Language="C#" Class="DelText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Text;

public class DelText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/plain";

        var service = new DeleteTextService();
        var result = service.Delete(context);

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