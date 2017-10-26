using DNAide.Web;

namespace Affiliate.MailServices
{
    public class RecoverPasswordMailService : MailService
    {
        public string FirstName { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }

        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public void SendRecoverPasswordMail()
        {
            string strMail = TemplateHandler.LoadTemplate("Email/RecoverPass/RecoverPass_" + PreferredLanguage + Common.EmailSuffix(MailOption) + ".htm").Replace("\r\n", "");
            TemplateHandler.Assign(ref strMail, "firstname", FirstName);
            TemplateHandler.Assign(ref strMail, "username", UserName);
            TemplateHandler.Assign(ref strMail, "password", NewPassword);
            TemplateHandler.Assign(ref strMail, "url", Url);
            TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
            TemplateHandler.Assign(ref strMail, "rooturl", Common.CurrentRootAddress());
            TemplateHandler.Assign(ref strMail, "lang", PreferredLanguage);
            string strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");

            SendEmail(strMail, Email, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
    }
}