namespace Fanex.Affiliate.User.Core.Utilities
{
    using System.IO;
    using RazorEngine;
    using RazorEngine.Templating;

    public class EmailBuilder : IEmailBuilder
    {
        public string BuildTemplateContent(string path, object model)
        {
            var templateContent = File.ReadAllText(path);

            return Engine.Razor.RunCompile(templateContent, path, null, model);
        }
    }
}