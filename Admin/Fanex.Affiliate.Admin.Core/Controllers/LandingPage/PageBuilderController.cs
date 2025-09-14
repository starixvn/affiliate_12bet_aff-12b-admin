using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using Fanex.Affiliate.Admin.Core.ViewModels.LandingPage;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Configurations;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
    [AllowLandingPageFrame]
    public class PageBuilderController : ReadOnlySessionBaseController
    {
        private readonly IAppSetting _appSetting;
        private readonly ILandingPageBuilderService _builderService;
        private readonly ILandingPageService _landingPageService;
        private readonly IPageBuilderService _pageBuilderService;

        public PageBuilderController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILandingPageBuilderService builderService,
            ILandingPageService landingPageService,
            IPageBuilderService pageBuilderService) : base(sessionService)
        {
            _appSetting = appSetting;
            _builderService = builderService;
            _landingPageService = landingPageService;
            _pageBuilderService = pageBuilderService;
        }

        #region Main Landing Page Builder Interface

        /// <summary>
        /// Main Landing Page Builder interface - shows all 4 functions
        /// </summary>
        [HttpGet]
        public ActionResult Index(int? pageId, bool isClone = false)
        {
            PageBuilderDetail pageBuilderDetail = null;
            if(pageId.HasValue)
            {
                pageBuilderDetail = _pageBuilderService.GetPageBuilderDetail(pageId.Value);
                if(isClone)
                {
                    pageBuilderDetail.PageId = 0;
                    pageBuilderDetail.PageName = pageBuilderDetail.PageName + " (Clone)";
                    pageBuilderDetail.PageCode = pageBuilderDetail.PageCode + " (Clone)";
                }
            }

            var model = new PageBuilderViewModel
            {
                LandingPageUrl = _appSetting.LandingPageUrl,
                CampaignList = _landingPageService.GetCampaignInfoList(),
                LanguageList = _landingPageService.GetAllLanguagesInSystem(),
                DealList = _landingPageService.GetNormalDealList(),
                TargetCountryList = _landingPageService.GetTargetCountryList(),
                StatusList = _landingPageService.GetLandingPageStatusList(),
                ModuleList = _landingPageService.GetModuleList(),
                PageBuilderDetail = pageBuilderDetail
            };

            return View("~/Areas/Portal/Views/LandingPage/PageBuilder.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMediaSources(MediaSourceParam param)
        {
             return GenerateJsonNetResult(_pageBuilderService.GetMediaSources(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult InsertPageBuilder(InsertUpdatePageBuilderParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodePageBuilderParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_pageBuilderService.InsertPageBuilder(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult UpdatePageBuilder(InsertUpdatePageBuilderParam param)
        {
            if (ModelState.IsValid && param.PageId > 0)
            {
                var encodeParam = EncodePageBuilderParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_pageBuilderService.UpdatePageBuilder(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        private InsertUpdatePageBuilderParam EncodePageBuilderParam(InsertUpdatePageBuilderParam param)
        {
            return new InsertUpdatePageBuilderParam
            {
                PageId = param.PageId,
                CampaignId = param.CampaignId,
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode),
                DealIds = Server.HtmlEncode(param.DealIds),
                StatusId = param.StatusId,
                TargetCountryCode = Server.HtmlEncode(param.TargetCountryCode),
                LanguageCode = Server.HtmlEncode(param.LanguageCode),
                ModuleComponentsSetting = EncodeModuleComponents(param.ModuleComponentsSetting),
                IsPreview = param.IsPreview
            };
        }

        private IList<ModuleComponentItem> EncodeModuleComponents(IList<ModuleComponentItem> componentStructure)
        {
            foreach (var item in componentStructure)
            {
                if (!string.IsNullOrEmpty(item.ExtraInfo))
                {
                    item.ExtraInfo = Server.HtmlEncode(item.ExtraInfo);
                }
            }
            
            return componentStructure;
        }

        #endregion

        #region 1. Manage Landing Page Functions

        /// <summary>
        /// Manage Landing Page - List existing landing pages
        /// </summary>
        [HttpGet]
        public ActionResult ManageLandingPage()
        {
            var model = new ManageLandingPageViewModel
            {
                TargetCountries = _landingPageService.GetTargetCountryList(),
                Languages = _landingPageService.GetLanguageList(),
                LandingPageList = _builderService.GetLandingPageList().MainData
            };

            return View("~/Areas/Portal/Views/MediaGallery/ManageLandingPage.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageList(LandingPageListParam param)
        {
            return GenerateJsonNetResult(_builderService.GetLandingPageList(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageById(int pageId)
        {
            if (pageId <= 0)
            {
                return GenerateErrResult("Invalid landing page ID.");
            }

            return GenerateJsonNetResult(_builderService.GetLandingPageById(pageId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult DeleteLandingPage(DeleteLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.DeleteLandingPage(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult PublishLandingPage(PublishLandingPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.PublishLandingPage(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GenerateLandingPagePreview(int pageId)
        {
            if (pageId <= 0)
            {
                return GenerateErrResult("Invalid landing page ID.");
            }

            return GenerateJsonNetResult(_builderService.GenerateLandingPagePreview(pageId));
        }

        #endregion

        #region 2. Create Landing Page Functions

        /// <summary>
        /// Create Landing Page - Drag & drop builder interface
        /// </summary>
        [HttpGet]
        public ActionResult CreateLandingPage(int? pageId)
        {
            var model = new CreateLandingPageViewModel
            {
                PageId = pageId ?? 0,
                TargetCountries = _landingPageService.GetTargetCountryList(),
                Languages = _landingPageService.GetLanguageList(),
                //AvailableModules = _landingPageService.GetModuleList(),
                //ModuleTypes = _moduleService.GetModuleTypes(),
                //NavigationTargets = _moduleService.GetNavigationTargets()
            };

            // If editing existing page, load its configuration
            if (pageId.HasValue && pageId > 0)
            {
                model.ExistingPageData = _builderService.GetLandingPageById(pageId.Value);
            }

            return View("~/Areas/Portal/Views/MediaGallery/CreateLandingPage.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult SaveLandingPageTemplate(SaveLandingPageTemplateParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeLandingPageParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_builderService.SaveLandingPageTemplate(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult AddModuleToPage(AddModuleToPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.AddModuleToPage(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult RemoveModuleFromPage(RemoveModuleFromPageParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.RemoveModuleFromPage(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult ReorderModules(ReorderModulesParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.ReorderModules(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult UpdateModuleConfiguration(UpdateModuleConfigurationParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeModuleConfigParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_builderService.UpdateModuleConfiguration(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLivePreview(GetLivePreviewParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_builderService.GetLivePreview(param));
            }
            return GenerateModelStateErrResult();
        }

        #endregion

        #region 3. Asset Uploader Functions
        /// <summary>
        /// Media Picker for Page Builder - Select media for modules
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMediaPickerList(MediaPickerParam param)
        {
            if (ModelState.IsValid)
            {
                var criteria = new ModuleMediaCriteria
                {
                    ModuleId = param.ModuleId,
                    SearchTerm = param.SearchTerm,
                    FileType = param.FileType,
                    PageIndex = param.PageIndex,
                    PageSize = param.PageSize,
                    IncludeDeleted = false
                };
            }
            return GenerateModelStateErrResult();
        }

        /// <summary>
        /// Assign media to module in page builder
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult AssignMediaToModule(AssignMediaToModuleParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.AssignMediaToModule(param));
            }
            return GenerateModelStateErrResult();
        }

        /// <summary>
        /// Remove media assignment from module
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        public ActionResult RemoveMediaFromModule(RemoveMediaFromModuleParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_builderService.RemoveMediaFromModule(param));
            }
            return GenerateModelStateErrResult();
        }

        /// <summary>
        /// Get assigned media for a specific module
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleAssignedMedia(int moduleId, int pageId)
        {
            if (moduleId <= 0 || pageId <= 0)
            {
                return GenerateErrResult("Invalid module or page ID.");
            }

            var result = _builderService.GetModuleAssignedMedia(moduleId, pageId);
            return GenerateJsonNetResult(result);
        }

        #endregion

        //#region 4. Manage Registration Form Functions

        ///// <summary>
        ///// Manage Registration Form - Form template management
        ///// </summary>
        //[HttpGet]
        //public ActionResult ManageRegistrationForm()
        //{
        //    var model = new ManageRegistrationFormViewModel
        //    {
        //        TargetCountries = _landingPageService.GetTargetCountryList(),
        //        Languages = _landingPageService.GetLanguageList(),
        //        FormTemplateList = _formService.GetRegistrationFormList().MainData,
        //        FormFieldTypes = _formService.GetFormFieldTypes()
        //    };

        //    return View("~/Areas/Portal/Views/MediaGallery/ManageRegistrationForm.cshtml", model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //public ActionResult GetRegistrationFormList(RegistrationFormListParam param)
        //{
        //    return GenerateJsonNetResult(_formService.GetRegistrationFormList(param));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //public ActionResult GetRegistrationFormById(int formId)
        //{
        //    if (formId <= 0)
        //    {
        //        return GenerateErrResult("Invalid form ID.");
        //    }

        //    return GenerateJsonNetResult(_formService.GetRegistrationFormById(formId));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        //public ActionResult CreateRegistrationForm(CreateRegistrationFormParam param)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var encodeParam = EncodeRegistrationFormParam(param);
        //        AssignUserInfoParam(encodeParam);
        //        return GenerateJsonNetResult(_formService.CreateRegistrationForm(encodeParam));
        //    }
        //    return GenerateModelStateErrResult();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        //public ActionResult UpdateRegistrationForm(UpdateRegistrationFormParam param)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var encodeParam = EncodeUpdateRegistrationFormParam(param);
        //        AssignUserInfoParam(encodeParam);
        //        return GenerateJsonNetResult(_formService.UpdateRegistrationForm(encodeParam));
        //    }
        //    return GenerateModelStateErrResult();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        //public ActionResult DeleteRegistrationForm(DeleteRegistrationFormParam param)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AssignUserInfoParam(param);
        //        return GenerateJsonNetResult(_formService.DeleteRegistrationForm(param));
        //    }
        //    return GenerateModelStateErrResult();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //[FunctionAuthorize(FunctionName = Function.LandingPage, IsHost = true)]
        //public ActionResult UploadFormAssets(HttpPostedFileBase[] files, UploadFormAssetsParam param)
        //{
        //    if (ModelState.IsValid && files != null && files.Length > 0)
        //    {
        //        var encodeParam = EncodeFormAssetsParam(param);
        //        AssignUserInfoParam(encodeParam);
        //        return GenerateJsonNetResult(_formService.UploadFormAssets(files, encodeParam));
        //    }
        //    return GenerateErrResult("Please select valid files to upload.");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryHeader]
        //public ActionResult GenerateFormPreview(int formId)
        //{
        //    if (formId <= 0)
        //    {
        //        return GenerateErrResult("Invalid form ID.");
        //    }

        //    return GenerateJsonNetResult(_formService.GenerateFormPreview(formId));
        //}

        //#endregion

        #region Helper Methods

        private SaveLandingPageTemplateParam EncodeLandingPageParam(SaveLandingPageTemplateParam param)
        {
            return new SaveLandingPageTemplateParam
            {
                PageId = param.PageId,
                PageName = Server.HtmlEncode(param.PageName),
                PageCode = Server.HtmlEncode(param.PageCode),
                TargetCountry = Server.HtmlEncode(param.TargetCountry),
                Language = Server.HtmlEncode(param.Language),
                Deal = Server.HtmlEncode(param.Deal),
                Status = param.Status,
                ModuleConfiguration = param.ModuleConfiguration // This will be JSON, handle separately
            };
        }

        private UpdateModuleConfigurationParam EncodeModuleConfigParam(UpdateModuleConfigurationParam param)
        {
            return new UpdateModuleConfigurationParam
            {
                PageId = param.PageId,
                ModuleId = param.ModuleId,
                ModuleOrder = param.ModuleOrder,
                ModuleSettings = param.ModuleSettings, // JSON data
                MediaSourceId = param.MediaSourceId
            };
        }

        private CreateRegistrationFormParam EncodeRegistrationFormParam(CreateRegistrationFormParam param)
        {
            return new CreateRegistrationFormParam
            {
                FormName = Server.HtmlEncode(param.FormName),
                FormDescription = Server.HtmlEncode(param.FormDescription),
                TargetCountry = Server.HtmlEncode(param.TargetCountry),
                Language = Server.HtmlEncode(param.Language),
                FormFields = param.FormFields, // JSON data
                FormSettings = param.FormSettings // JSON data
            };
        }

        private UpdateRegistrationFormParam EncodeUpdateRegistrationFormParam(UpdateRegistrationFormParam param)
        {
            return new UpdateRegistrationFormParam
            {
                FormId = param.FormId,
                FormName = Server.HtmlEncode(param.FormName),
                FormDescription = Server.HtmlEncode(param.FormDescription),
                TargetCountry = Server.HtmlEncode(param.TargetCountry),
                Language = Server.HtmlEncode(param.Language),
                FormFields = param.FormFields, // JSON data
                FormSettings = param.FormSettings // JSON data
            };
        }

        private UploadFormAssetsParam EncodeFormAssetsParam(UploadFormAssetsParam param)
        {
            return new UploadFormAssetsParam
            {
                FormId = param.FormId,
                AssetType = param.AssetType,
                AssetName = Server.HtmlEncode(param.AssetName),
                AssetDescription = Server.HtmlEncode(param.AssetDescription)
            };
        }



        #endregion
    }
}
