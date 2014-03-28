using System;
using System.Web;
using System.Web.UI;
using System.Text;

public partial class Master2 : System.Web.UI.MasterPage
{
    protected StringBuilder strScript = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCssLink1 = Common.RootURL() + "/App_Themes/Aff12bet.ico?v=" + AppSettings.JsVersion;
        string strCssLink2 = Common.RootURL() + "/App_Themes/Aff12bet.png?v=" + AppSettings.JsVersion;
        string link1 = "<link href=\"" + strCssLink1 + "\" rel=\"shortcut icon\"/>";
        string link2 = "<![if ! IE]><link href=\"" + strCssLink2 + "\" rel=\"shortcut icon\"/><![endif]>";
        Page.Header.Controls.AddAt(2, new LiteralControl(link1));
        Page.Header.Controls.AddAt(3, new LiteralControl(link2));
        strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/prototype.js\"></script>");
        strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/Common.js?vers=" + AppSettings.JsVersion + "\"></script>");
        strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/login.js?vers=" + AppSettings.JsVersion + "\"></script>");
        strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/Scroller.js?vers=" + AppSettings.JsVersion + "\"></script>");
        strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/swfobject.js?vers=" + AppSettings.JsVersion + "\"></script>");
    }
}
