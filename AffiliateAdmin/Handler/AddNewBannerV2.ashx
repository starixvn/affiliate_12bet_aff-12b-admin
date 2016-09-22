<%@ WebHandler Language="C#" Class="AddNewBannerV2" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AddNewBannerV2 : IHttpHandler, IRequiresSessionState
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