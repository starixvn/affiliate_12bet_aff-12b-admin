using System.Globalization;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for AffHandler
/// </summary>
public class AffHandler
{
    public static HttpContext InitializeCulture(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.Private);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppSettings.Session.Language);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(AppSettings.Session.Language);

        Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
        Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        return context;
    }
}
