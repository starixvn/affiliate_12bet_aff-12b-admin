using System;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IconICO.Attributes.Add("href", "App_Themes/Aff12bet.ico?v=" + AppSettings.JsVersion);
        IconPNG.Attributes.Add("href", "App_Themes/Aff12bet.png?v=" + AppSettings.JsVersion);
    }
}
