<%@ WebHandler Language="C#" Class="AddBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Banner;

public class AddBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/html";

        var service = new AddBannerService();
        var resultInfo = service.AddBanner(context);
        if (resultInfo.IsSuccessful)
        {
            context.Response.Write("<script type=\"text/javascript\">alert('" + resultInfo.Message + "');parent.Cancelfunction();</script>");
        }
        else
        {
            context.Response.Write("<script type=\"text/javascript\">parent.Common.alertError(\"" + resultInfo.Message + "\");</script>");
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