using DNAide.Web;

namespace Affiliate.MailServices
{
    public class ContactUsMailService : MailService
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string AffId { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public void SendContactUsMail()
        {
            string body = TemplateHandler.LoadTemplate("MailContact.htm");

            TemplateHandler.Assign(ref body, "name", Name);
            TemplateHandler.Assign(ref body, "email", Email);
            TemplateHandler.Assign(ref body, "AffID", AffId);
            TemplateHandler.Assign(ref body, "comment", Content);

            SendEmail(body, AppSettings.ToMail, Subject, AppSettings.BCCMail, AppSettings.CCMail);
        }
    }
}