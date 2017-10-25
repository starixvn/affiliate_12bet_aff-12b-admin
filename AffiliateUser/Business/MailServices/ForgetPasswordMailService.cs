using DNAide.Web;
using System.Web.UI;

namespace Affiliate.MailServices
{
    public class ForgetPasswordMailService : MailService
    {
        public string PreferLanguage { get; set; }

        public bool MailOption { get; set; }

        public string FirstName { get; set; }

        public string UserName { get; set; }

        public int IdCust { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }

        public void SendForgetPassword()
        {
            var strFileName = "Email/ForgetPass/ForgetPass_" + PreferLanguage + Common.EmailSuffix(MailOption) + ".htm";

            var strMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
            TemplateHandler.Assign(ref strMail, "fname", FirstName);
            TemplateHandler.Assign(ref strMail, "username", UserName);
            TemplateHandler.Assign(ref strMail, "url", Common.CurrentRootAddress() + "/Recoverpw.aspx");
            TemplateHandler.Assign(ref strMail, "idcust", IdCust.ToString());
            TemplateHandler.Assign(ref strMail, "code", Code);
            TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
            TemplateHandler.Assign(ref strMail, "rooturl", Common.CurrentRootAddress());
            TemplateHandler.Assign(ref strMail, "lang", PreferLanguage);
            var strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");

            SendEmail(strMail, Email, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
    }
}