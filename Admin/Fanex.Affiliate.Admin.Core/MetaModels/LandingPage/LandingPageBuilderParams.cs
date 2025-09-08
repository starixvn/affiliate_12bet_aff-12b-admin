using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    #region Landing Page Management Parameters

    public class LandingPageListParam : AffiliatePagingParam
    {
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string SearchTerm { get; set; }
    }

    public class PublishLandingPageParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Publish status is required")]
        public bool IsPublished { get; set; }
    }

    #endregion

    #region Template Management Parameters

    public class SaveLandingPageTemplateParam : AffiliateBasicInfoParam
    {
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Page name is required")]
        [StringLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Page name must not exceed 200 characters")]
        public string PageName { get; set; }
        
        [Required(ErrorMessage = "Page code is required")]
        [StringLength(50, ErrorMessage = "Page code must not exceed 50 characters")]
        public string PageCode { get; set; }
        
        [Required(ErrorMessage = "Target country is required")]
        public string TargetCountry { get; set; }
        
        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }
        
        public string Deal { get; set; }
        public string Status { get; set; }
        public string ModuleConfiguration { get; set; } // JSON string
    }

    public class AddModuleToPageParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Module ID is required")]
        public int ModuleId { get; set; }
        
        public int ModuleOrder { get; set; }
        public string ModuleSettings { get; set; } // JSON string
        public int? MediaSourceId { get; set; }
    }

    public class RemoveModuleFromPageParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Module ID is required")]
        public int ModuleId { get; set; }
    }

    public class ReorderModulesParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Module order data is required")]
        public string ModuleOrderData { get; set; } // JSON string with module IDs and their new orders
    }

    public class UpdateModuleConfigurationParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        [Required(ErrorMessage = "Module ID is required")]
        public int ModuleId { get; set; }
        
        public int ModuleOrder { get; set; }
        public string ModuleSettings { get; set; } // JSON string
        public int? MediaSourceId { get; set; }
    }

    public class GetLivePreviewParam
    {
        [Required(ErrorMessage = "Page ID is required")]
        public int PageId { get; set; }
        
        public string PreviewType { get; set; } // desktop, mobile, tablet
        public bool IncludeMetadata { get; set; }
    }

    #endregion

    #region Asset Management Parameters

    public class UploadAssetsParam : UserInfoParam
    {
        public int MediaId { get; set; }

        [Required(ErrorMessage = "Module Id is required")]
        public int ModuleId { get; set; }
        
        [Required(ErrorMessage = "Target Country is required")]
        public string TargetCountryCode { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string MarketId { get; set; }

        public string Files { get; set; }

        public List<string> FilePaths { get; set; } = new List<string>();

        public List<string> MediaNames { get; set; } = new List<string>();

        public List<string> Dimensions { get; set; } = new List<string>();

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputFileNames { get; set; } = string.Empty;

        public string FunctionName { get; set; }
    }

    public class DeleteAssetsParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Asset IDs are required")]
        public string MediaIds { get; set; }
        
        public bool PermanentDelete { get; set; }
        public string DeleteReason { get; set; }
    }

    public class OptimizeAssetParam
    {
        [Required(ErrorMessage = "Asset ID is required")]
        public int MediaId { get; set; }
        
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public int? Quality { get; set; }
        public string OutputFormat { get; set; }
    }

    #endregion

    #region Registration Form Parameters

    public class RegistrationFormListParam : AffiliatePagingParam
    {
        public string TargetCountry { get; set; }
        public string Language { get; set; }
        public bool? IsActive { get; set; }
        public string SearchTerm { get; set; }
    }

    public class CreateRegistrationFormParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Form name is required")]
        [StringLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Form name must not exceed 200 characters")]
        public string FormName { get; set; }
        
        public string FormDescription { get; set; }
        
        [Required(ErrorMessage = "Target country is required")]
        public string TargetCountry { get; set; }
        
        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }
        
        [Required(ErrorMessage = "Form fields are required")]
        public string FormFields { get; set; } // JSON string
        
        public string FormSettings { get; set; } // JSON string
    }

    public class UpdateRegistrationFormParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Form ID is required")]
        public int FormId { get; set; }
        
        [Required(ErrorMessage = "Form name is required")]
        [StringLength(LandingPageConstants.ValidationLimits.StandardTextLength, ErrorMessage = "Form name must not exceed 200 characters")]
        public string FormName { get; set; }
        
        public string FormDescription { get; set; }
        
        [Required(ErrorMessage = "Target country is required")]
        public string TargetCountry { get; set; }
        
        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }
        
        [Required(ErrorMessage = "Form fields are required")]
        public string FormFields { get; set; } // JSON string
        
        public string FormSettings { get; set; } // JSON string
    }

    public class DeleteRegistrationFormParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Form IDs are required")]
        public string FormIds { get; set; }
        
        public bool PermanentDelete { get; set; }
        public string DeleteReason { get; set; }
    }

    public class UploadFormAssetsParam : AffiliateBasicInfoParam
    {
        [Required(ErrorMessage = "Form ID is required")]
        public int FormId { get; set; }
        
        [Required(ErrorMessage = "Asset type is required")]
        public string AssetType { get; set; }
        
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
    }

    #endregion
}
