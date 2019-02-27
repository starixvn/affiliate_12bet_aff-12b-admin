namespace Fanex.Affiliate.Admin.Core.Utilities
{
    using System.IO;
    using Fanex.MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using RazorEngine;
    using RazorEngine.Templating;

    public class MailProcessor : IMailProcessor
    {
        public string BuildTemplateContent(string path, object model)
        {
            var templateContent = File.ReadAllText(path);
            return Engine.Razor.RunCompile(templateContent, path, model: model);
        }

        public void Send(string content, string mailTo, string subject, string mailBcc = "", string mailCc = "")
        {
            var mailItem = new MailRecord
            {
                MailTo = mailTo,
                Subject = subject,
                Title = subject,
                Body = content,
                MailBcc = mailBcc,
                MailCc = mailCc
            };

            using (var client = new MailDeliver())
            {
                client.SendMail(mailItem);
            }
        }
    }
}