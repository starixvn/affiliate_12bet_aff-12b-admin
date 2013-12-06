using System;

public partial class Controls_Footer : RootControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.FtLinkHome.InnerText = GetText("Home");
        //this.FtLinkSignUp.InnerText = GetText("SignUp");
        //this.FtLinkAboutUs.InnerText = GetText("aboutus");
        //this.FtLinkContact.InnerText = GetText("contact");
        //this.FtLinkFaq.InnerText = GetText("faqs");
        hlkProgram.Text = Resources.Lang.ProgDetails;
        hlkFAQ.Text = Resources.Lang.Faqs;
        hlkCondition.Text = Resources.Lang.TermCondition;
        //hlkContactUs.Text = Resources.Lang.ContactUs;
    }
}
