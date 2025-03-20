namespace Fanex.Affiliate.User.Core.Utilities
{
    using Fanex.MailDeliver.MailClient;
    using Fanex.MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using RazorEngine;
    using RazorEngine.Templating;
    using System.IO;

    public class MailProcessor : IMailProcessor
    {
        private readonly IAppSetting _appSetting;
        public MailProcessor(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public string BuildTemplateContent(string path, string pathBackup, object model)
        {
            try
            {
                var templateContent = File.ReadAllText(path);
                return Engine.Razor.RunCompile(templateContent, path, model: model);
            }
            catch
            {
                var templateContentBackup = File.ReadAllText(pathBackup);
                return Engine.Razor.RunCompile(templateContentBackup, pathBackup, model: model);
            }
        }

        public void Send(string content, string mailTo, string subject, bool shouldSendMarketingSpecificEmail = false)
        {
            var isMarketingSpecificEmail = _appSetting.IsMarketingSpecificEmail(mailTo);

            if (isMarketingSpecificEmail && !shouldSendMarketingSpecificEmail)
            {
                LogHelper.WriteInfo($"Don't send the mail to {mailTo}: {subject}");
                return;
            }

            var mailItem = new MailRecord
            {
                MailTo = isMarketingSpecificEmail && !_appSetting.IsProduction ? _appSetting.MailCc : mailTo,
                Subject = subject,
                Body = content,
                MailBcc = _appSetting.MailBcc,
                MailCc = _appSetting.MailCc
            };


            AppSettings.AppMailId = _appSetting.AppMailId;
            AppSettings.AppMailAddress = _appSetting.AppMailAddress;

            using (var client = new MailDeliver())
            {
                client.SendMail(mailItem);
            }
        }
    }
}