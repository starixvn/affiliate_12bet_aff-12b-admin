using System;
using DNAide.Web;

public partial class ChangeAdminPass : IBCPage
{
    protected int pendingAffNo = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("ChangeAdminPass.htm");
        TemplateHandler.Assign(ref strContent, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strContent, "username", AppSettings.Session.UserName);
        TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strContent, "oldpass", Resources.Lang.OldPass);
        TemplateHandler.Assign(ref strContent, "password", Resources.Lang.PassNew);
        TemplateHandler.Assign(ref strContent, "confirmpass", Resources.Lang.ConfirmPass);
        TemplateHandler.Assign(ref strContent, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strContent, "cancel", Resources.Lang.Reset);
        TemplateHandler.Assign(ref strContent, "oldrequire", Resources.Lang.OldRequire);
        TemplateHandler.Assign(ref strContent, "passrequire", Resources.Lang.PassRequire);
        TemplateHandler.Assign(ref strContent, "passcheck", Resources.Lang.PassCheck);
        TemplateHandler.Assign(ref strContent, "passconfirm", Resources.Lang.PassConfirm);

        TemplateHandler.Assign(ref strContent, "change-my-pwd", Resources.Lang.ChangeMyPwd);

        this.HtmlContent = strContent;
    }
}
