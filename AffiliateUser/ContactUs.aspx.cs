using System;

public partial class ContactUS : RootPage
{
    public string AffID = "", Name = "", Email = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       Controls_ContactUs.WidthTable = "570px";
       Controls_ContactUs.DisplayTitle1 = "";
       Controls_ContactUs.DisplayTitle2 = "none";
       Controls_ContactUs.Size= "23";
       Controls_ContactUs.WidthTextArea = "433px";
       AppSettings.Session.IsFromHome = false;
    }
}