namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class ModuleTypeInfo
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string ModuleType { get; set; }

        public string Description { get; set; }

        public string NavigateTo { get; set; }

        public bool IsMultipleUpload { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }
    }
}
