using System;
using DNAide.Web;

public partial class SignIn : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string temp = TemplateHandler.LoadTemplate("Login.htm");
        TemplateHandler.Assign(ref temp, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref temp, "Login", Resources.Lang.Login);
        TemplateHandler.Assign(ref temp, "Username", Resources.Lang.Username);
        TemplateHandler.Assign(ref temp, "Password", Resources.Lang.Password);
        TemplateHandler.Assign(ref temp, "ForgetPassword", Resources.Lang.ForgetPassword);
        TemplateHandler.Assign(ref temp, "Help", Resources.Lang.Help);
        TemplateHandler.Assign(ref temp, "Logon", Resources.Lang.LogOn);
        TemplateHandler.Assign(ref temp, "now", DateTime.Now.Year.ToString());
        TemplateHandler.Assign(ref temp, "relativeRootUrl", ResolveClientUrl("~/"));

        this.HtmlContent = temp;
    }

    public override void CheckUnderMaintenance()
    {
        if (Common.IsUnderMaintenance())
        {
            Response.Redirect("~/UnderMaintenance.aspx");
        }
    }
}
