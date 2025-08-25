using System;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class LandingPageModule
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int ModuleType { get; set; }

        public string ModuleTypeName { get; set; }

        public string Remark { get; set; }

        public int NavigateTo { get; set; }

        public string NavigateToName { get; set; }

        public bool IsMultipleUpload { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
