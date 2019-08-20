using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;

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
                    Message = CommGroupErrorMessage.InvalidGroupId
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var platformFeeValidation = ValidatePlatformFeeList(commGroup.PlatformFeeList);

            if (groupNameValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
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
                    Message = groupNameValidation.Message + platformFeeValidation.Message
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            var commGroupValidation = ValidateUpdateCommGroupParam(commGroup);
            var platformFeeValidation = ValidatePlatformFeeList(commGroup.PlatformFeeList);

            if (commGroupValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
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

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = commGroupValidation.Message + platformFeeValidation.Message
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            return new JsonNetResult
            {
                Data = _commissionGroupService.DeleteCommissionGroup(param)
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
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            var validationResult = ValidateGroupId(assignParam.GroupId);
            if (validationResult.IsSuccessful)
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
                    Message = validationResult.Message
                }
            };
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup(int affId)
        {
            var viewModel = affId >= Validation.MinId ?
                new AssignPrivateGroupViewModel
                {
                    ProductList = _productRepository.GetProductList(),
                    DeductionList = _commissionGroupService.GetDeductionList(),
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
                } :
                new AssignPrivateGroupViewModel();

            return View("~/Areas/Portal/Views/CommissionGroup/AssignPrivateGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignPrivateGroup(CommissionGroupSetting commGroup, AffiliateBasicInfo affInfo)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            var platformFeeValidation = ValidatePlatformFeeList(commGroup.PlatformFeeList);
            if (platformFeeValidation.IsSuccessful)
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
                    Message = platformFeeValidation.Message
                }
            };
        }

        private ActionResult CreateErrorResult()
        {
            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        private static ResultInfo ValidateUpdateCommGroupParam(CommissionGroupSetting param)
        {
            var result = ValidateGroupId(param.GroupId);

            return result.IsSuccessful ? ValidateGroupName(param.GroupName) : result;
        }

        private static ResultInfo ValidateGroupId(int groupId)
        {
            var result = new ResultInfo();
            if (groupId < Validation.MinId)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = CommGroupErrorMessage.InvalidGroupId;
            }

            return result;
        }

        private static ResultInfo ValidateGroupName(string groupName)
        {
            var result = new ResultInfo();
            if (string.IsNullOrEmpty(groupName) || groupName.Length > 100)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = CommGroupErrorMessage.InvalidGroupName;
            }

            return result;
        }

        private ResultInfo ValidatePlatformFeeList(IEnumerable<DeductionSetting> platformFeeList)
        {
            var result = new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = CommGroupErrorMessage.InvalidPlatformFee
            };

            if (platformFeeList == null || !platformFeeList.Any() || platformFeeList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                return result;
            }

            var productIdOfDB = _productRepository.GetProductList().Select(p => p.ProductId.ToString());
            var productIdOfPlatform = string.Join(",", platformFeeList.Select(p => p.ProductIds.Replace(" ", "")))
                                        .Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            var productIdValidation = ValidateProductIds(productIdOfDB, productIdOfPlatform);
            if (!productIdValidation.IsSuccessful)
            {
                return result;
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateProductIds(IEnumerable<string> productIdOfDB, IEnumerable<string> productIdOfPlatform)
        {
            var result = new ResultInfo { Code = Code.GeneralErrorCode };

            var inputProductIdListCount = productIdOfPlatform.Count();
            if (productIdOfDB.Count() != inputProductIdListCount)
            {
                return result;
            }

            var distinctProductIdOfPlatform = productIdOfPlatform.Distinct();
            if (inputProductIdListCount != distinctProductIdOfPlatform.Count() ||
                distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                return result;
            }

            return new ResultInfo();
        }
    }
}