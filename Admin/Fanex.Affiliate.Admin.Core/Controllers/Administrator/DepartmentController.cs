using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Administrator;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers.Administrator
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(
            ISessionService sessionService,
            IDepartmentService departmentService) : base(sessionService)
        {
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminList, Url = Url.Content("~/Portal/Administrator") },
                new SubMenu { Name = Lang.DeptList, Url = Url.Content("~/Portal/Department"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/Administrator/Department.cshtml", subMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDepartmentList()
        {
            return new JsonNetResult
            {
                Data = _departmentService.GetDepartmentList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDepartmentDetail(int selectedDepartmentId)
        {
            return new JsonNetResult
            {
                Data = _departmentService.GetDepartmentDetail(selectedDepartmentId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult InsertDepartment(InsertUpdateDepartmentParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _departmentService.InsertDepartment(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Please input Department's name."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult UpdateDepartment(InsertUpdateDepartmentParam param)
        {
            var isValidDepartmentId = param.DepartmentId >= Validation.MinId;
            if (ModelState.IsValid && isValidDepartmentId)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _departmentService.UpdateDepartment(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidDepartmentId ? "Department Id is invalid." : "Please input Department's name."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult DeleteDepartment(DeleteDepartmentParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _departmentService.DeleteDepartment(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Department id is incorrect."
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetRolePolicy(int departmentId)
        {
            return new JsonNetResult
            {
                Data = _departmentService.GetRolePolicy(departmentId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult UpdateRolePolicy(UpdatePolicyParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = new UpdatePolicyParam
                {
                    GroupPolicies = Server.HtmlEncode(param.GroupPolicies.Trim())
                };
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _departmentService.UpdateRolePolicy(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The role policy is invalid."
                }
            };
        }

        private InsertUpdateDepartmentParam EncodeParam(InsertUpdateDepartmentParam param)
        {
            return new InsertUpdateDepartmentParam
            {
                DepartmentId = param.DepartmentId,
                DepartmentName = Server.HtmlEncode(param.DepartmentName),
                Description = Server.HtmlEncode(param.Description)
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
