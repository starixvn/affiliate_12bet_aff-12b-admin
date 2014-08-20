using System;
using Affiliate.MailServices;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        const string message = "test MNL";
        const string subject = "test MNL";
        const string toMail = "brian.dinh@mailinator.com";

        var mailService = new MailService();
        mailService.SendEmail(message, toMail, subject, AppSettings.BCCMail, AppSettings.CCMail);
    }
}