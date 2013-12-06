using System;
using DNAide.Web;

public partial class SignUpSuccess : RootPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("RegistrationSuccess.htm");
        TemplateHandler.Assign(ref strContent, "openacc", Resources.Lang.UnderReview);
        TemplateHandler.Assign(ref strContent, "thankscontent", Resources.Lang.RegisSuccess);
        TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
        ltrSuccess.Text = strContent;
    }
}
