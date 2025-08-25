using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class LandingPagePreview
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PreviewHtml { get; set; }
        public string PreviewCss { get; set; }
        public string PreviewJs { get; set; }
        public string PreviewUrl { get; set; }
        public string MobilePreviewUrl { get; set; }
        public string TabletPreviewUrl { get; set; }
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; }
        public List<string> ValidationWarnings { get; set; }
        public PreviewMetadata Metadata { get; set; }
    }

    public class PreviewMetadata
    {
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string CanonicalUrl { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string OgImage { get; set; }
        public string TwitterCard { get; set; }
        public string TwitterTitle { get; set; }
        public string TwitterDescription { get; set; }
        public string TwitterImage { get; set; }
        public int EstimatedLoadTime { get; set; } // in milliseconds
        public int PageSize { get; set; } // in bytes
        public int ImageCount { get; set; }
        public int ScriptCount { get; set; }
        public int StylesheetCount { get; set; }
    }
}
