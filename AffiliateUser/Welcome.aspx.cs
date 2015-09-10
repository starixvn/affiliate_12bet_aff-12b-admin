using System;
using System.Collections;
using DNAide.Web;
using DNAide.Utils;
using DataHandler;

public partial class Welcome : RootPage
{
    protected string strSecureQuestion = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AppSettings.Session.IsAuthenticated)
            Response.Redirect("~/");

        //if (!AppSettings.Session.ForceChangePass)
        //    Response.Redirect("~/UserInfo/Account_Overview.aspx");

        ltrForceResetPasswordForm.Text = Resources.Lang.Welcome;
        ltrDearAff.Text = string.Format("Dear {0},", AppSettings.Session.UserName);

        strSecureQuestion = TemplateHandler.LoadTemplate("UserInfo/SecureQuestion_" + AppSettings.Session.Language + ".htm");
    }
}
