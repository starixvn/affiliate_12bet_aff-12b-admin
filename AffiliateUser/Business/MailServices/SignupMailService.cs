using System;
using DNAide.Web;

namespace Affiliate.MailServices
{
    public class SignupMailService : MailService
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public void SendRegisterMail(string preferLang, string customerEmail)
        {
            if (preferLang.ToUpper() == string.Empty)
                preferLang = "en-US";

            var strFileName = "Email/SignupMail/SignupMail_" + preferLang + Common.EmailSuffix(false) + ".htm";

            string strRegisMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
            TemplateHandler.Assign(ref strRegisMail, "fname", FirstName);
            TemplateHandler.Assign(ref strRegisMail, "lname", LastName);
            TemplateHandler.Assign(ref strRegisMail, "mailadd", AppSettings.MailContact);
            TemplateHandler.Assign(ref strRegisMail, "rooturl", Common.GetFullDomain());
            TemplateHandler.Assign(ref strRegisMail, "lang", preferLang);
            string strRegisSubject = TemplateHandler.ReplaceBlock(ref strRegisMail, "subject", "");

            SendEmail(strRegisMail, customerEmail, strRegisSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }

        public void SendCsdMail(string rdWebsite, string strSiteUrl, string strMoreUrl, string strNoSiteDesc)
        {
            string strCsdMail = TemplateHandler.LoadTemplate("CsdMail.htm").Replace("\r\n", "");
            TemplateHandler.Assign(ref strCsdMail, "fname", FirstName);
            TemplateHandler.Assign(ref strCsdMail, "lname", LastName);
            TemplateHandler.Assign(ref strCsdMail, "username", UserName);
            TemplateHandler.Assign(ref strCsdMail, "mailadd", AppSettings.MailContact);

            if (rdWebsite.Equals("0", StringComparison.OrdinalIgnoreCase))
            {
                TemplateHandler.Assign(ref strCsdMail, "siteurl", strSiteUrl);
                TemplateHandler.RemoveBlock(ref strCsdMail, "nositeurl");
                if (strMoreUrl == "")
                    TemplateHandler.ReplaceBlock(ref strCsdMail, "moreurl", "");
                else
                    TemplateHandler.Assign(ref strCsdMail, "moreurl", strMoreUrl.Replace("\\n", ","));
            }
            else
            {
                TemplateHandler.RemoveBlock(ref strCsdMail, "siteurl");
                TemplateHandler.Assign(ref strCsdMail, "nositedesc", strNoSiteDesc);
            }

            var strCsdSubject = TemplateHandler.ReplaceBlock(ref strCsdMail, "subject", "");

            SendEmail(strCsdMail, AppSettings.ToMail, strCsdSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
    }
}