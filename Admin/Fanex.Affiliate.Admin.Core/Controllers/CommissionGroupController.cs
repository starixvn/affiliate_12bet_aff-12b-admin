using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.CommissionGroup)]
    public class CommissionGroupController : BaseController
    {
        private const string PrivateGroupName = "[None]";

        private readonly ICommissionGroupService _commissionGroupService;

        private readonly IProductRepository _productRepository;

        private readonly IAffiliateService _affiliateService;

        public CommissionGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            IProductRepository productRepository,
            IAffiliateService affiliateService) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _productRepository = productRepository;
            _affiliateService = affiliateService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _productRepository.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList()
            };

            return View("~/Areas/Portal/Views/CommissionGroup/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroupList(GetCommissionGroupParam param)
        {
            ModelState.Remove("GroupId");
            if (ModelState.IsValid)
            {
                var viewModel = new GetCommissionGroupViewModel
                {
                    CommissionSettingList = _commissionGroupService.GetCommissionGroupList(param),
                    TotalRows = param.Nrows
                };

                return new JsonNetResult
                {
                    Data = viewModel
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(GetCommissionGroupParam param)
        {
            ModelState.Remove("PageIndex");
            ModelState.Remove("PageSize");
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _commissionGroupService.GetCommissionGroupList(param).FirstOrDefault()
                };
            }

            return new JsonNetResult();
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (ModelState.IsValid)
            {
                var createParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.UserHostAddress,
                    GroupName = commGroup.GroupName?.Trim(),
                    Description = commGroup.Description?.Trim(),
                    QmTurnover = commGroup.QmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.CreateCommissionGroup(createParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var commGroupInfo = _commissionGroupService.GetCommissionGroupList(new GetCommissionGroupParam { GroupId = commGroup.GroupId }).FirstOrDefault();
            var result = ValidateUpdateCommGroupParam(commGroup, commGroupInfo.NoAffs);

            if (ModelState.IsValid && result.IsSuccessful)
            {
                var updateParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = commGroup.GroupId,
                    GroupName = commGroup.GroupName?.Trim(),
                    Ip = Request.UserHostAddress,
                    Description = commGroup.Description?.Trim(),
                    QmTurnover = commGroup.NextQmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.UpdateCommissionGroup(updateParam)
                };
            }

            result.Code = Code.GeneralErrorCode;
            result.Message = result.Message + string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            return new JsonNetResult
            {
                Data = result
            };
        }

        public static ResultInfo ValidateUpdateCommGroupParam(CommissionGroupSetting param, int noAffs)
        {
            var result = new ResultInfo();

            if(param.GroupId < Validation.MinId)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = "GroupId not exists!";
            }
            if (param.RowIdx < Validation.MinId)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = result.Message + "<br /> RowIdx not exists!";
            }

            if (param.NoAffs != noAffs)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = result.Message + "<br /> Affiliate number in the group is incorrect!";
            }

            return result;
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _commissionGroupService.DeleteCommissionGroup(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [HttpGet]
        public ActionResult AssignGroup(int affId)
        {
            var viewModel = new AssignNormalGroupViewModel();

            if (affId >= Validation.MinId)
            {
                var param = new GetGroupListParam { AffId = affId };
                var commGroupList = _commissionGroupService.GetNormalCommGroupList(param);

                viewModel = new AssignNormalGroupViewModel
                {
                    CommGroupList = commGroupList,
                    SelectedGroup = param.GroupId,
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
                };
            }

            return View("~/Areas/Portal/Views/CommissionGroup/AssignGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignGroup(AssignGroupParam assignParam, AffiliateBasicInfo affInfo)
        {
            if (ModelState.IsValid)
            {
                assignParam.UserId = SessionService.UserId;
                assignParam.Ip = Request.UserHostAddress;
                assignParam.AffId = affInfo.AffId;

                var emailParam = new AffInformationEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this)
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.AssignGroup(assignParam, affInfo, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup(int affId)
        {
            var viewModel = new AssignPrivateGroupViewModel();

            if (affId >= Validation.MinId)
            {
                viewModel = new AssignPrivateGroupViewModel
                {
                    ProductList = _productRepository.GetProductList(),
                    DeductionList = _commissionGroupService.GetDeductionList(),
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
                };
            }

            return View("~/Areas/Portal/Views/CommissionGroup/AssignPrivateGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignPrivateGroup(CommissionGroupSetting commGroup, AffiliateBasicInfo affInfo)
        {
            ModelState.Remove("commGroup.GroupName");
            if (ModelState.IsValid)
            {
                var assignParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = affInfo.AffId,
                    GroupName = affInfo.UserName,
                    Description = PrivateGroupName,
                    QmTurnover = commGroup.QmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee,
                    Ip = Request.UserHostAddress
                };

                var emailParam = new AffInformationEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this)
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.AssignPrivateGroup(assignParam, affInfo, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }
    }
}