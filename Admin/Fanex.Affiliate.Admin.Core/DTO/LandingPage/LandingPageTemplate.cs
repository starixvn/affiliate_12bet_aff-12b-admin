using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class LandingPageTemplate
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public string Deal { get; set; }
        public string Status { get; set; }
        public string ModuleConfiguration { get; set; } // JSON string
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string PublishedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Helper properties
        public List<ModuleConfiguration> Modules { get; set; }
        public string PreviewUrl { get; set; }
        public int ModuleCount { get; set; }
        public string StatusDisplayName { get; set; }
        public string TargetCountryDisplayName { get; set; }
        public string LanguageDisplayName { get; set; }
    }

    public class ModuleConfiguration
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleType { get; set; }
        public string ModuleTypeName { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleSettings { get; set; } // JSON string
        public int? MediaSourceId { get; set; }
        public string MediaSourceUrl { get; set; }
        public string MediaSourceName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsRequired { get; set; }
    }
}
