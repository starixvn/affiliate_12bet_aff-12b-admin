using Fanex.MailDeliver.MailClient.API;
using Fanex.MailDeliver.Model;

namespace Affiliate.MailServices
{
    public class MailService
    {
        /// <summary>
        /// Send Email message.
        /// </summary>
        /// <param name="message">Message body.</param>
        /// <param name="mailTo">Mail To someone.</param>
        /// <param name="subject">Subject of Email.</param>
        /// <param name="mailBcc">Mail Bcc someone.</param>
        /// <param name="mailCc">Mail CC someone.</param>
        public void SendEmail(string message, string mailTo, string subject, string mailBcc = "", string mailCc = "")
        {
            var mailItem = new MailRecord
            {
                MailTo = mailTo,
                Subject = subject,
                Title = subject,
                ////IsBodyHtml ready
                Body = message,
                MailBcc = mailBcc,
                MailCc = mailCc,
            };

            using (var client = new MailDeliver())
            {
                client.SendMail(mailItem);
            }
        }
    }
}