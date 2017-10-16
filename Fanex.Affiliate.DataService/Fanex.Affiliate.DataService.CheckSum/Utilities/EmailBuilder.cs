namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using ViewModel;

    public class EmailBuilder : IEmailBuilder
    {
        private readonly IFileSystem _fileSystem;
        private readonly IHtmlRender _htmlRender;

        public EmailBuilder() : this(null, null)
        {
        }

        public EmailBuilder(IFileSystem fileSystem, IHtmlRender htmlRender)
        {
            _fileSystem = fileSystem ?? new FileSystem();
            _htmlRender = htmlRender ?? new HtmlRender();
        }

        public string GenerateDailyMemberContent(DailyMemberViewModel data, string emailTemplatePath)
        {
            var filePath = $"{emailTemplatePath}/DailyMemberEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(filePath);
            return _htmlRender.Generate(templateContent, "DailyMemberEmail", data);
        }

        public string GenerateDailyMemberIncomeAccessContent(DailyMemberIncomeAccessViewModel data, string emailTemplatePath)
        {
            var filePath = $"{emailTemplatePath}/DailyMemberIncomeAccessEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(filePath);

            return _htmlRender.Generate(templateContent, "DailyMemberIncomeAccessEmail", data);
        }
    }
}