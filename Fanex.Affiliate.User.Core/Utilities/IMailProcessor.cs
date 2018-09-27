namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IMailProcessor
    {
        string BuildTemplateContent(string path, object model);

        void Send(string content, string mailTo, string subject, string mailBcc = "", string mailCc = "");
    }
}