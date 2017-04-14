using System;
using System.Web;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string lang = (Request.QueryString["lang"] ?? string.Empty);
        switch (lang.ToLower())
        {
            case "ch":
                AppSettings.Session.Flag = "ct";
                lang = "zh-TW";
                break;
            case "cs":
                AppSettings.Session.Flag = "cs";
                lang = "zh-CN";
                break;
            case "es":
                AppSettings.Session.Flag = "es";
                lang = "es-ES";
                break;
            case "vi":
                AppSettings.Session.Flag = "vi";
                lang = "vi-VN";
                break;
            case "id":
                AppSettings.Session.Flag = "id";
                lang = "id-ID";
                break;
            case "th":
                AppSettings.Session.Flag = "th";
                lang = "th-TH";
                break;
            case "ko":
                AppSettings.Session.Flag = "th";
                lang = "ko-KR";
                break;
            case "ja":
                AppSettings.Session.Flag = "ja";
                lang = "ja-JP";
                break;
            case "hi":
                AppSettings.Session.Flag = "hi";
                lang = "hi-IN";
                break;
            default:
                AppSettings.Session.Flag = "e";
                lang = "en-US";
                break;
        }
        AppSettings.Session.Language = lang;
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
        Response.Redirect(Common.RootURL());
    }
}
