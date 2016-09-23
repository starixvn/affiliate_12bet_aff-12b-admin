<%@ WebHandler Language="C#" Class="UpBanner" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services.Banner;

public class UpBanner : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer == null)
        {
            return;
        }

        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/html";
        var service = new UploadBannerService();
        var resultInfo = service.UploadBanner(context);
        if (resultInfo.IsSuccessful)
        {
            context.Response.Write("<script type=\"text/javascript\">alert('" + resultInfo.Message + "');parent.Cancelfunction();</script>");
        }
        else
        {
            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + resultInfo.Message + "\");</script>");
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