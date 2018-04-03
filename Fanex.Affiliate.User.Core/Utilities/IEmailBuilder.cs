namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IEmailBuilder
    {
        string BuildTemplateContent(string path, object model);
    }
}