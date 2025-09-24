namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class ModuleInfo
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleType { get; set; }
        public bool IsMultipleUpload { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
    }
}
