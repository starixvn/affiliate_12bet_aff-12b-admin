using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.LandingPage;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.DisabledFunction
{
    [FunctionAuthorize(FunctionName = Function.DisabledFunction)]
    public class ModuleManagementController : ReadOnlySessionBaseController
    {
        private readonly IModuleManagementService _moduleService;

        public ModuleManagementController(
            ISessionService sessionService,
            IModuleManagementService moduleService,
            ISystemService systemService) : base(sessionService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/LandingPage/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleList()
        {
            return GenerateJsonNetResult(_moduleService.GetModuleList());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleById(int moduleId)
        {
            if (moduleId <= 0)
            {
                return GenerateErrResult("Invalid module ID.");
            }

            return GenerateJsonNetResult(_moduleService.GetModuleById(moduleId));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DisabledFunction, IsHost = true)]
        public ActionResult InsertModule(InsertUpdateModuleParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeModuleParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_moduleService.InsertModule(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DisabledFunction, IsHost = true)]
        public ActionResult UpdateModule(InsertUpdateModuleParam param)
        {
            if (ModelState.IsValid && param.ModuleId > 0)
            {
                var encodeParam = EncodeModuleParam(param);
                AssignUserInfoParam(encodeParam);
                return GenerateJsonNetResult(_moduleService.UpdateModule(encodeParam));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.DisabledFunction, IsHost = true)]
        public ActionResult DeleteModule(DeleteModuleParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);
                return GenerateJsonNetResult(_moduleService.DeleteModule(param));
            }
            return GenerateModelStateErrResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModulesByType(int moduleType)
        {
            if (moduleType < 1 || moduleType > 4)
            {
                return GenerateErrResult("Invalid module type.");
            }

            return GenerateJsonNetResult(_moduleService.GetModulesByType(moduleType));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetModuleTypes()
        {
            return GenerateJsonNetResult(_moduleService.GetModuleTypes());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNavigationTargets()
        {
            return GenerateJsonNetResult(_moduleService.GetNavigationTargets());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ValidateModuleUniqueness(string moduleName, int moduleId = 0)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                return GenerateErrResult("Module name is required.");
            }

            var isUnique = _moduleService.ValidateModuleUniqueness(moduleName, moduleId);
            return GenerateJsonNetResult(new { IsUnique = isUnique });
        }

        private InsertUpdateModuleParam EncodeModuleParam(InsertUpdateModuleParam param)
        {
            return new InsertUpdateModuleParam
            {
                ModuleId = param.ModuleId,
                ModuleName = Server.HtmlEncode(param.ModuleName),
                ModuleType = param.ModuleType,
                Remark = Server.HtmlEncode(param.Remark),
                NavigateTo = param.NavigateTo,
                IsMultipleUpload = param.IsMultipleUpload,
                IsRequired = param.IsRequired,
                IsUnique = param.IsUnique
            };
        }
    }
}
