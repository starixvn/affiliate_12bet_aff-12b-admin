using System;
using AffiliateUser;
using Affiliate.MailServices;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(DNAide.Utils.Encryption.CfsDeCrypt("f04OETq4v+XIUGHxP6Hy4w=="));
        const string message = "test MNL";
        const string subject = "test MNL";
        const string toMail = "alex.nguyen@starixsoft.com";

        var mailService = new MailService();
        mailService.SendEmail(message, toMail, subject, AppSettings.BCCMail, AppSettings.CCMail);
    }
}