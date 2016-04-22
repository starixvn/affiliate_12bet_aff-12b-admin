using System;

public partial class Root : RootPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AppSettings.Session.IsAuthenticated) 
            Response.Redirect("UserInfo/Account_Overview.aspx");

        Controls_ContactUs.WidthTable = "350px";
        Controls_ContactUs.DisplayTitle1 = "none";
        Controls_ContactUs.DisplayTitle2 = "";
        Controls_ContactUs.Size = "13";
        Controls_ContactUs.WidthTextArea = "270px";
        AppSettings.Session.IsFromHome = true;
    }

    public override void CheckUnderMaintenance()
    {
        if (Common.IsUnderMaintenance())
        {
            Response.Redirect("~/UnderMaintenance.aspx");
        }
    }
}
