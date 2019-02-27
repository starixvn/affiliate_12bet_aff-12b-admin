<%@ WebHandler Language="C#" Class="AddNewText" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Text;

public class AddNewText : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/html";
        var service = new AddTextService();
        var result = service.AddNewText(context);

        if (result.IsSuccessful)
        {
            context.Response.Write("<script type=\"text/javascript\">alert('" + Resources.Lang.AddTextSuccessful + "');parent.Cancelfunction();</script>");
        }
        else
        {
            context.Response.Write("<script type=\"text/javascript\">parent.Common.alertError('" + result.Message + "');</script>");
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