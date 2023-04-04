namespace Fanex.Affiliate.Admin.Core.Utilities
{
    using Fanex.MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using RazorEngine;
    using RazorEngine.Templating;
    using System.IO;

    public class MailProcessor : IMailProcessor
    {
        public string BuildTemplateContent(string path, string pathBackup, object model)
        {
            try
            {
                var templateContent = File.ReadAllText(path);
                return Engine.Razor.RunCompile(templateContent, path, model: model);
            } catch
            {
                var templateContentBackup = File.ReadAllText(pathBackup);
                return Engine.Razor.RunCompile(templateContentBackup, pathBackup, model: model);
            }
        }

        public void Send(string content, string mailTo, string subject, string mailBcc = "", string mailCc = "")
        {
            var mailItem = new MailRecord
            {
                MailTo = mailTo,
                Subject = subject,
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