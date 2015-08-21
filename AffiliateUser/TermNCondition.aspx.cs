using System;

public partial class TermNCondition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _locale = (string.IsNullOrEmpty(AppSettings.Session.Language)) ? "en-US" : AppSettings.Session.Language;
        if (!string.IsNullOrEmpty(AppSettings.Session.Language))
        {
            this.Culture = _locale;
            this.UICulture = _locale;
        }
        else
        {
            AppSettings.Session.Language = _locale;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_locale);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(_locale);
        }

    }
}
