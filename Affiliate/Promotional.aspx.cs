using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;

public partial class UserInfo_Promotional : RootPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _locale = (string.IsNullOrEmpty(AppSettings.Session.Language)) ? "en-US" : AppSettings.Session.Language;
        string tplSuc = TemplateHandler.LoadTemplate("Promotional_" + _locale + ".htm");
        TemplateHandler.Assign(ref tplSuc, "culture", _locale);
        this.lblContent.Text = tplSuc;
    }
}
