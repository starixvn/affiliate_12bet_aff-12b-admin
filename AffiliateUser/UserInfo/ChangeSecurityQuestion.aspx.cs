using System;
using AffiliateUser;

public partial class ChangeSecurityQuestion : RootPageLogIn
{
    protected string strSecureQuestion = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AppSettings.Session.HasSecurityQuestion)
        {
            ltrChangeSecureQuestion.Text = Resources.Lang.UpdateSecurityQuestion;
            strSecureQuestion = DNAide.Web.TemplateHandler.LoadTemplate("UserInfo/SecureQuestion_" + AppSettings.Session.Language + ".htm");
        }
        else
        {
            Response.Redirect(Common.RootURL());
        }

    }
}
