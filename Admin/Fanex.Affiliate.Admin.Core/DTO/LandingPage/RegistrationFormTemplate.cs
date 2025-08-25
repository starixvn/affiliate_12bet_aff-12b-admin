using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class RegistrationFormTemplate
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public string FormFields { get; set; } // JSON string
        public string FormSettings { get; set; } // JSON string
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        // Helper properties
        public List<FormField> Fields { get; set; }
        public FormSettings Settings { get; set; }
        public string PreviewUrl { get; set; }
        public int FieldCount { get; set; }
        public string TargetCountryDisplayName { get; set; }
        public string LanguageDisplayName { get; set; }
    }

    public class FormField
    {
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldLabel { get; set; }
        public string Placeholder { get; set; }
        public bool IsRequired { get; set; }
        public int FieldOrder { get; set; }
        public string ValidationRules { get; set; }
        public string DefaultValue { get; set; }
        public List<string> Options { get; set; } // For dropdown, radio, checkbox
        public string CssClass { get; set; }
        public string Attributes { get; set; } // Additional HTML attributes
    }

    public class FormSettings
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public string SubmitButtonText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string RedirectUrl { get; set; }
        public bool EnableCaptcha { get; set; }
        public bool EnableEmailNotification { get; set; }
        public string NotificationEmail { get; set; }
        public string FormTheme { get; set; }
        public string CustomCss { get; set; }
        public string CustomJs { get; set; }
    }

    public class FormFieldType
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public bool RequiresOptions { get; set; }
        public bool SupportsValidation { get; set; }
        public string DefaultAttributes { get; set; }
    }

    public class FormPreview
    {
        public string FormHtml { get; set; }
        public string FormCss { get; set; }
        public string FormJs { get; set; }
        public string PreviewUrl { get; set; }
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
