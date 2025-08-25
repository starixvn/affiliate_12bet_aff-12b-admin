namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public abstract class BaseLandingPageModuleCommand : BaseNonQueryCommand
    {
        public string ModuleName { get; set; }

        public int ModuleType { get; set; }

        public string Remark { get; set; }

        public int NavigateTo { get; set; }

        public bool IsMultipleUpload { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }
    }
}
