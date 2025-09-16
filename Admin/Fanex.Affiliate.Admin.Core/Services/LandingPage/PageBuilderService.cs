using Fanex.Affiliate.Admin.Core.Commands.LandingPage;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Data.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class PageBuilderService : BaseDynamicService, IPageBuilderService
    {
        private readonly ILandingPageService _landingPageService;
        private static readonly Regex PageCodePattern = new Regex(@"^[a-zA-Z0-9\-_]+$", RegexOptions.Compiled);
        
        private const string ValidationMessage_PageNameRequired = "Page Name is required";
        private const string ValidationMessage_PageNameMaxLength = "Page Name must not exceed 200 characters";
        private const string ValidationMessage_PageCodeRequired = "Page Code is required";
        private const string ValidationMessage_PageCodeMaxLength = "Page Code must not exceed 200 characters";
        private const string ValidationMessage_PageCodeInvalid = "Page Code can only contain letters, numbers, hyphens and underscores";
        private const string ValidationMessage_CampaignRequired = "Campaign is required";
        private const string ValidationMessage_TargetCountryRequired = "Target Country is required";
        private const string ValidationMessage_LanguageRequired = "Language is required";
        private const string ValidationMessage_DealRequired = "At least one deal must be selected";
        private const string ValidationMessage_StatusRequired = "Status is required";
        private const string ValidationMessage_ModuleRequired = "Module (Order {0}) is required";
        private const string ValidationMessage_SourceRequired = "Source (Order {0}) is required";

        // Module ID constants for special validation logic
        private const int LogoSponsorshipModuleId = 2;
        private const int LogoSponsorshipRegisterModuleId = 3;

        // Module type constants
        private const int ContactUsModuleType = 2;
        private const int YoutubeModuleType = 4;
        private const int LoginButtonModuleType = 5;

        public PageBuilderService(IDynamicRepository dynamicRepository, ILandingPageService landingPageService) : base(dynamicRepository)
        {
            _landingPageService = landingPageService;
        }

        public ResultInfo InsertLandingPageDeal(InsertLandingPageDealParam param)
        {
            var command = param.MapTo<InsertLandingPageDealParam, InsertLandingPageDealCommand>();

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo InsertLandingPageCampaign(InsertLandingPageCampaignParam param)
        {
            var command = param.MapTo<InsertLandingPageCampaignParam, InsertLandingPageCampaignCommand>();

            return ExecuteReturnResultInfo(command);
        }

        public IEnumerable<MediaSourceInfo> GetMediaSources(MediaSourceParam param)
        {
            var criteria = param.MapTo<MediaSourceParam, GetMediaSourceCriteria>();

            return FetchTData<MediaSourceInfo>(criteria);
        }

        public PageBuilderDetail GetPageBuilderDetail(int pageId)
        {
            var criteria = new GetPageBuilderDetailCriteria { PageId = pageId };
            
            var result = DynamicRepository.FetchMultiple<PageBuilderDetail, ModuleComponentItem>(criteria);
            var pageDetail = result.Item1.FirstOrDefault();
            if(pageDetail != null)
            {
                pageDetail.ModuleComponentsSetting = result.Item2;
            }    

            return pageDetail;
        }

        public ResultInfo InsertPageBuilder(InsertUpdatePageBuilderParam param)
        {
            // Validate the parameters before inserting
            var validationResult = ValidatePageBuilderParam(param);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            var command = param.MapTo<InsertUpdatePageBuilderParam, InsertPageBuilderCommand>();
            command.UserId = param.UserId;
            command.ComponentStructure = JsonConvert.SerializeObject(param.ModuleComponentsSetting);

            var result = ExecuteReturnResultInfo(command);
            result.Message = result.IsSuccessful ? command.PageId.ToString() : result.Message;

            return result;
        }

        public ResultInfo UpdatePageBuilder(InsertUpdatePageBuilderParam param)
        {
            // Validate the parameters before updating
            var validationResult = ValidatePageBuilderParam(param);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            var command = param.MapTo<InsertUpdatePageBuilderParam, UpdatePageBuilderCommand>();
            command.UserId = param.UserId;
            command.ComponentStructure = JsonConvert.SerializeObject(param.ModuleComponentsSetting);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo ValidatePageBuilderParam(InsertUpdatePageBuilderParam param)
        {
            var errors = new List<string>();

            ValidateBasicFields(param, errors);
            ValidateModuleComponents(param, errors);

            return errors.Count == 0 
                ? ResultInfo.Success 
                : ResultInfo.GenerateErr(string.Join(Environment.NewLine, errors));
        }

        private static void ValidateBasicFields(InsertUpdatePageBuilderParam param, List<string> errors)
        {
            ValidatePageFields(param, errors);

            if (param.CampaignId == 0)
            {
                errors.Add(ValidationMessage_CampaignRequired);
            }

            if (string.IsNullOrWhiteSpace(param.TargetCountryCode))
            {
                errors.Add(ValidationMessage_TargetCountryRequired);
            }

            if (string.IsNullOrWhiteSpace(param.LanguageCode))
            {
                errors.Add(ValidationMessage_LanguageRequired);
            }

            if (string.IsNullOrWhiteSpace(param.DealIds))
            {
                errors.Add(ValidationMessage_DealRequired);
            }

            if (param.StatusId == 0)
            {
                errors.Add(ValidationMessage_StatusRequired);
            }
        }

        private static void ValidatePageFields(InsertUpdatePageBuilderParam param, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(param.PageName))
            {
                errors.Add(ValidationMessage_PageNameRequired);
            }
            else
            {
                if (param.PageName.Length > Validation.MaxLengthLandingPageName)
                {
                    errors.Add(ValidationMessage_PageNameMaxLength);
                }
            }

            if (string.IsNullOrWhiteSpace(param.PageCode))
            {
                errors.Add(ValidationMessage_PageCodeRequired);
            }
            else
            {
                if (!PageCodePattern.IsMatch(param.PageCode))
                {
                    errors.Add(ValidationMessage_PageCodeInvalid);
                }

                if (param.PageName.Length > Validation.MaxLengthLandingPageCode)
                {
                    errors.Add(ValidationMessage_PageCodeMaxLength);
                }
            }
        }

        private void ValidateModuleComponents(InsertUpdatePageBuilderParam param, List<string> errors)
        {
            if (param.ModuleComponentsSetting == null || !param.ModuleComponentsSetting.Any())
            {
                ValidateModuleRequirements(param, errors);
                return;
            }

            var moduleList = _landingPageService.GetModuleList().ToList();

            foreach (var component in param.ModuleComponentsSetting)
            {
                ValidateIndividualModuleComponent(component, moduleList, errors);
            }

            ValidateModuleRequirements(param, errors);
            ValidateModuleUniqueness(param, errors);
        }

        private static void ValidateIndividualModuleComponent(ModuleComponentItem component, List<ModuleInfo> moduleList, List<string> errors)
        {
            if (component.ModuleId <= 0)
            {
                errors.Add(string.Format(ValidationMessage_ModuleRequired, component.OrderId));
                return;
            }

            var moduleInfo = moduleList.Find(m => m.ModuleId == component.ModuleId);
            if (moduleInfo == null)
            {
                return;
            }

            ValidateModuleByType(component, moduleInfo, errors);
        }

        private static void ValidateModuleByType(ModuleComponentItem component, ModuleInfo moduleInfo, List<string> errors)
        {
            switch (moduleInfo.ModuleType)
            {
                case YoutubeModuleType:
                    ValidateYouTubeModule(component, errors);
                    break;
                case ContactUsModuleType:
                    ValidateContactUsModule(component, errors);
                    break;
                case LoginButtonModuleType:
                    ValidateLoginButtonModule(component, errors);
                    break;
                default:
                    ValidateRegularModule(component, errors);
                    break;
            }
        }

        private static void ValidateYouTubeModule(ModuleComponentItem component, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(component.ExtraInfo))
            {
                errors.Add($"YouTube URL (Order {component.OrderId}) is required");
            }
        }

        private static void ValidateContactUsModule(ModuleComponentItem component, List<string> errors)
        {
            if (component.MediaId == null || component.MediaId <= 0)
            {
                errors.Add(string.Format(ValidationMessage_SourceRequired, component.OrderId));
            }
            if (string.IsNullOrWhiteSpace(component.ExtraInfo))
            {
                errors.Add($"Contact link (Order {component.OrderId}) is required");
            }
        }

        private static void ValidateLoginButtonModule(ModuleComponentItem component, List<string> errors)
        {
            if (component.MediaId == null || component.MediaId <= 0)
            {
                errors.Add(string.Format(ValidationMessage_SourceRequired, component.OrderId));
            }
            if (string.IsNullOrWhiteSpace(component.ExtraInfo))
            {
                errors.Add($"Login Endpoint (Order {component.OrderId}) is required");
            }
        }

        private static void ValidateRegularModule(ModuleComponentItem component, List<string> errors)
        {
            // Regular modules need mediaId
            if (component.MediaId == null || component.MediaId <= 0)
            {
                errors.Add(string.Format(ValidationMessage_SourceRequired, component.OrderId));
            }
        }

        private void ValidateModuleRequirements(InsertUpdatePageBuilderParam param, List<string> errors)
        {
            var moduleList = _landingPageService.GetModuleList().ToList();
            var requiredModules = moduleList.Where(m => m.IsRequired).ToList();
            
            // Check if "Logo Sponsorship & Register" (moduleId 3) is selected
            var hasLogoSponsorshipRegister = param.ModuleComponentsSetting.Any(comp => comp.ModuleId == LogoSponsorshipRegisterModuleId);
            
            foreach (var requiredModule in requiredModules)
            {
                var hasRequiredModule = param.ModuleComponentsSetting.Any(comp => comp.ModuleId == requiredModule.ModuleId);
                
                // Special case: If "Logo Sponsorship & Register" (moduleId 3) is selected,
                // then "Logo $ Sponsorship" (moduleId 2) is not required
                if (requiredModule.ModuleId == LogoSponsorshipModuleId && hasLogoSponsorshipRegister)
                {
                    // Skip validation for "Logo $ Sponsorship" when "Logo Sponsorship & Register" is present
                    continue;
                }
                
                if (!hasRequiredModule)
                {
                    errors.Add($"{requiredModule.ModuleName ?? "Unknown Module"} is required and must be included");
                }
            }
        }

        private void ValidateModuleUniqueness(InsertUpdatePageBuilderParam param, List<string> errors)
        {
            var moduleList = _landingPageService.GetModuleList().ToList();
            var uniqueModules = moduleList.Where(m => m.IsUnique).ToList();
            
            foreach (var uniqueModule in uniqueModules)
            {
                var instances = param.ModuleComponentsSetting.Where(comp => comp.ModuleId == uniqueModule.ModuleId).ToList();
                if (instances.Count > 1)
                {
                    errors.Add($"{uniqueModule.ModuleName} can only be used once");
                }
            }
        }
    }
}
