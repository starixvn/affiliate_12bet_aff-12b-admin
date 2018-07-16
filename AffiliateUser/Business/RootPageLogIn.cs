using System;
using System.Web;
using AffiliateUser.Business.Services.Authorization;

/// <summary>
/// Summary description for RootPageLogIn
/// </summary>
public class RootPageLogIn : RootPage
{
    protected override void OnInit(EventArgs e)
    {
        if (!AppSettings.Session.IsAuthenticated)
        {
            //Response.Redirect(ResolveUrl("~") + "?url=" + Server.UrlEncode(Request.Url.AbsolutePath));
            HttpContext.Current.Response.Redirect(Common.RootURL());
        }
        if (!IsSessionValid())
        {
            AuthHelper.ClearSession(HttpContext.Current);
            HttpContext.Current.Response.Redirect(Common.RootURL());
        }
        if (!AppSettings.Session.HasSecurityQuestion && !HttpContext.Current.Request.RawUrl.Contains("ChangeSecurityQuestion.aspx"))
        {
            HttpContext.Current.Response.Redirect("ChangeSecurityQuestion.aspx", false);
        }
        var cache = HttpContext.Current.Response.Cache;
        cache.SetNoStore();

        base.OnInit(e);
    }

    private bool IsSessionValid()
    {
        var sessionAuthToken = AppSettings.Session.AuthToken;
        var cookieAuthToken = Request.Cookies["AuthToken"];
        return sessionAuthToken != null && cookieAuthToken != null && sessionAuthToken.Equals(cookieAuthToken.Value);
    }

    protected override void InitializeCulture()
    {


        AffHandler.InitializeCulture(HttpContext.Current);
    }
}