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
using System.Collections.Generic;

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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(GetCommissionGroupParam param)
        {
            if (param.GroupId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _commissionGroupService.GetCommissionGroupList(param).FirstOrDefault()
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "GroupId is required!"
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var result = ValidateGroupName(commGroup.GroupName);
            var validatePlatformFeeList = ValidatePlatformFeeList(commGroup.PlatformFeeList);

            if (ModelState.IsValid && result.IsSuccessful && validatePlatformFeeList.IsSuccessful)
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

            result.Code = Code.GeneralErrorCode;
            result.Message = result.Message + validatePlatformFeeList.Message + string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            return new JsonNetResult
            {
                Data = result
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var commGroupInfo = _commissionGroupService.GetCommissionGroupList(new GetCommissionGroupParam { GroupId = commGroup.GroupId }).FirstOrDefault();
            var result = ValidateUpdateCommGroupParam(commGroup, commGroupInfo.NoAffs);
            var validatePlatformFeeList = ValidatePlatformFeeList(commGroup.PlatformFeeList);

            if (ModelState.IsValid && result.IsSuccessful && validatePlatformFeeList.IsSuccessful)
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
            result.Message = result.Message + validatePlatformFeeList.Message + string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            return new JsonNetResult
            {
                Data = result
            };
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
            var result = ValidateGroupId(assignParam.GroupId);
            if (ModelState.IsValid && result.IsSuccessful)
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

            result.Code = Code.GeneralErrorCode;
            result.Message = result.Message + string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            return new JsonNetResult
            {
                Data = result
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
            var result = ValidatePlatformFeeList(commGroup.PlatformFeeList);
            if (ModelState.IsValid && result.IsSuccessful)
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

            result.Code = Code.GeneralErrorCode;
            result.Message = result.Message + string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            return new JsonNetResult
            {
                Data = result
            };
        }

        public static ResultInfo ValidateUpdateCommGroupParam(CommissionGroupSetting param, int noAffs)
        {
            var result = ValidateGroupId(param.GroupId);
            var validateGroupName = ValidateGroupName(param.GroupName);

            if (!validateGroupName.IsSuccessful)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = result.Message + validateGroupName.Message;
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

        public static ResultInfo ValidateGroupId(int groupId)
        {
            var result = new ResultInfo();
            if (groupId < Validation.MinId)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = result.Message + "<br /> GroupId not exists!";
            }

            return result;
        }

        public static ResultInfo ValidateGroupName(string groupName)
        {
            var result = new ResultInfo();

            if (string.IsNullOrEmpty(groupName))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = "GroupName cannot be empty. Please check!";
            }
            if (groupName.Length > 100)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = "GroupName should shorter than 100 characters!";
            }

            return result;
        }

        private ResultInfo ValidatePlatformFeeList(IEnumerable<DeductionSetting> platformFeeList)
        {
            var result = new ResultInfo();

            if (platformFeeList == null || !platformFeeList.Any())
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + "<br />", "Platform Fee must have at least 1 platform!");
                return result;
            }

            if (platformFeeList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + "<br />", "At least 1 product must be selected per group!");
                return result;
            }

            var productIdOfDB = _productRepository.GetProductList().Select(product => product.ProductId.ToString()).ToArray();
            var productIdOfPlatform = string.Join(",", platformFeeList.Select(platformFee => platformFee.ProductIds.Replace(" ","")).ToArray())
                                        .Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);

            var validateProductIds = ValidateProductIds(productIdOfDB, productIdOfPlatform);

            if (!validateProductIds.IsSuccessful)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message, validateProductIds.Message);
            }

            return result;
        }

        private static ResultInfo ValidateProductIds(string[] productIdOfDB, string[] productIdOfPlatform)
        {
            var result = new ResultInfo();

            if (productIdOfDB.Length != productIdOfPlatform.Length)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + "<br />", "You must select all products!");
                return result;
            }

            var distinctProductIdOfPlatform = productIdOfPlatform.Distinct().ToArray();

            if (productIdOfPlatform.Length != distinctProductIdOfPlatform.Length)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + "<br />", "The Products selected in a group, which must not exist in others!");
            }

            if (distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + "<br />", "The Product selected in a group is incorrect!");
            }
            return result;
        }
    }
}