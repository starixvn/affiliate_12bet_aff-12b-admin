using Fanex.Affiliate.Admin.Core.Commands.LandingPage;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class ModuleManagementService : BaseDynamicService, IModuleManagementService
    {
        private const string RestrictedCharacterPattern = "[<>\"'&]";
        private const int MaxModuleNameLength = LandingPageConstants.ValidationLimits.StandardTextLength;
        private const int MaxRemarkLength = LandingPageConstants.ValidationLimits.StandardTextLength;

        public ModuleManagementService(IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<LandingPageModule> GetModuleList()
        {
            return FetchTData<LandingPageModule>(new GetModuleCriteria());
        }

        public LandingPageModule GetModuleById(int moduleId)
        {
            return GetTData<LandingPageModule>(new LandingPageModuleCriteria { ModuleId = moduleId });
        }

        public IEnumerable<LandingPageModule> GetModulesByType(int moduleType)
        {
            return FetchTData<LandingPageModule>(new LandingPageModuleCriteria { ModuleType = moduleType });
        }

        public ResultInfo InsertModule(InsertUpdateModuleParam param)
        {
            var validationResult = ValidateModuleData(param);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            var command = new InsertLandingPageModuleCommand();
            AssignModuleCommand(command, param);
            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateModule(InsertUpdateModuleParam param)
        {
            if (param.ModuleId <= 0)
            {
                return ResultInfo.GenerateErr("Invalid module ID");
            }

            var validationResult = ValidateModuleData(param);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            var command = new UpdateLandingPageModuleCommand { ModuleId = param.ModuleId };
            AssignModuleCommand(command, param);
            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo DeleteModule(DeleteModuleParam param)
        {
            if (string.IsNullOrWhiteSpace(param.ModuleIds))
            {
                return ResultInfo.GenerateErr("No modules selected for deletion");
            }

            var command = new DeleteLandingPageModuleCommand
            {
                ModuleIds = param.ModuleIds,
                UserId = param.UserId,
                Ip = param.Ip
            };
            return ExecuteReturnResultInfo(command);
        }

        public IEnumerable<ModuleTypeInfo> GetModuleTypes()
        {
            return new List<ModuleTypeInfo>
            {
            };
        }

        public IEnumerable<NavigationTargetInfo> GetNavigationTargets()
        {
            return new List<NavigationTargetInfo>
            {
                new NavigationTargetInfo 
                { 
                    Id = 0, 
                    Name = "None", 
                    Description = "No navigation action",
                    Url = "",
                    IsExternal = false,
                    RequiresAuthentication = false
                },
                new NavigationTargetInfo 
                { 
                    Id = 9, 
                    Name = "Registration Form", 
                    Description = "Navigate to user registration form",
                    Url = "/register",
                    IsExternal = false,
                    RequiresAuthentication = false
                },
                new NavigationTargetInfo 
                { 
                    Id = 10, 
                    Name = "Login Form", 
                    Description = "Navigate to user login form",
                    Url = "/login",
                    IsExternal = false,
                    RequiresAuthentication = false
                },
                new NavigationTargetInfo 
                { 
                    Id = 11, 
                    Name = "Contact Page", 
                    Description = "Navigate to contact information page",
                    Url = "/contact",
                    IsExternal = false,
                    RequiresAuthentication = false
                }
            };
        }

        public bool ValidateModuleUniqueness(string moduleName, int moduleId = 0)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                return false;
            }

            var criteria = new ValidateModuleUniquenessCriteria 
            { 
                ModuleName = moduleName.Trim(), 
                ExcludeModuleId = moduleId 
            };
            
            var count = DynamicRepository.Get<int>(criteria);
            return count == 0;
        }

        private ResultInfo ValidateModuleData(InsertUpdateModuleParam param)
        {
            // Validate module name
            if (string.IsNullOrWhiteSpace(param.ModuleName))
            {
                return ResultInfo.GenerateErr("Module name is required");
            }

            if (param.ModuleName.Length > MaxModuleNameLength)
            {
                return ResultInfo.GenerateErr($"Module name must not exceed {MaxModuleNameLength} characters");
            }

            if (RegexHelper.IsMatch(param.ModuleName, RestrictedCharacterPattern))
            {
                return ResultInfo.GenerateErr("Module name contains restricted characters: < > \" ' &");
            }

            // Validate remark
            if (!string.IsNullOrEmpty(param.Remark) && param.Remark.Length > MaxRemarkLength)
            {
                return ResultInfo.GenerateErr($"Remark must not exceed {MaxRemarkLength} characters");
            }

            if (!string.IsNullOrEmpty(param.Remark) && RegexHelper.IsMatch(param.Remark, RestrictedCharacterPattern))
            {
                return ResultInfo.GenerateErr("Remark contains restricted characters: < > \" ' &");
            }

            // Validate module type
            if (param.ModuleType < 1 || param.ModuleType > 4)
            {
                return ResultInfo.GenerateErr("Invalid module type. Must be between 1 and 4");
            }

            // Validate navigation target
            if (param.NavigateTo < 0)
            {
                return ResultInfo.GenerateErr("Invalid navigation target");
            }

            // Validate uniqueness if required
            if (param.IsUnique && !ValidateModuleUniqueness(param.ModuleName, param.ModuleId))
            {
                return ResultInfo.GenerateErr("A module with this name already exists");
            }

            return ResultInfo.Success;
        }

        private static void AssignModuleCommand(BaseLandingPageModuleCommand command, InsertUpdateModuleParam param)
        {
            command.ModuleName = param.ModuleName?.Trim();
            command.ModuleType = param.ModuleType;
            command.Remark = param.Remark?.Trim();
            command.NavigateTo = param.NavigateTo;
            command.IsMultipleUpload = param.IsMultipleUpload;
            command.IsRequired = param.IsRequired;
            command.IsUnique = param.IsUnique;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}
