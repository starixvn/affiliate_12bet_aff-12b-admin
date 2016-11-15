using System;
using DNAide.Web;
using DNAide.Core;

public partial class ChangePass : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("ChangePass.htm");
        string isAdmin = CastAide.AsString(Request["isAdmin"], "0");
        TemplateHandler.Assign(ref strContent, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strContent, "resetpass", Resources.Lang.ResetPass);
        TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strContent, "password", Resources.Lang.Password);
        TemplateHandler.Assign(ref strContent, "confirmpass", Resources.Lang.ConfirmPass);
        TemplateHandler.Assign(ref strContent, "passrequire", Resources.Lang.PassRequire);
        TemplateHandler.Assign(ref strContent, "passconfirm", Resources.Lang.PassConfirm);
        TemplateHandler.Assign(ref strContent, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strContent, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strContent, "username", Request["username"]);
        TemplateHandler.Assign(ref strContent, "custid", Request["custid"]);
        TemplateHandler.Assign(ref strContent, "isAdmin", isAdmin);
        this.HtmlContent = strContent;
    }
}
