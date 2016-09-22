<%@ WebHandler Language="C#" Class="UpText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Text;

public class UpText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/html";
        var service = new UpdateTextService();
        var result = service.Update(context);

        if (string.IsNullOrEmpty(result.Message))
        {
            context.Response.Write("<script type=\"text/javascript\">parent.RefreshTextList();</script>");
        }
        else
        {
            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + result.Message + "');</script>");
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}