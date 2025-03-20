using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Auth;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.CommGroup
{
    public class CommGroupValidation : ICommGroupValidation
    {
        private const string brElementHtml = "<br />";

        private readonly IAuthService _authService;
        private readonly IAffiliateService _affiliateService;
        private readonly ISystemService _systemService;
        private readonly ICommGroupCommonService _commGroupCommonService;

        public CommGroupValidation(
            IAuthService authService,
            IAffiliateService affiliateService,
            ISystemService systemService,
            ICommGroupCommonService commGroupCommonService)
        {
            _authService = authService;
            _affiliateService = affiliateService;
            _systemService = systemService;
            _commGroupCommonService = commGroupCommonService;
        }

        public bool HasPermissionAssignOnAff(int userId, int affId)
        {
            var affInfo = _affiliateService.GetAffiliateBasicInfo(affId);

            if (string.IsNullOrEmpty(affInfo.AffCode))
            {
                var permissionList = _authService.GetPermissionList(userId);

                return permissionList.Any(i => i.FunctionName.MatchWith(Function.EditAffStatus) && i.IsHost);
            }

            return true;
        }

        public ResultInfo ValidateCommGroup(CommGroupSettingParam commGroup)
        {
            var validateGroupType = ValidateGroupType(commGroup.GroupTypeId);

            var result = ValidateAllFees(commGroup);

            var validateTierSettingList = ValidateTierSettingList(commGroup.TierSettingList);

            if (!validateGroupType.IsSuccessful || !validateTierSettingList.IsSuccessful)
            {
                result.Code = ErrCode.GeneralErrorCode;
                result.Message = string.Join(validateGroupType.Message, result.Message, validateTierSettingList.Message);
            }

            return result;
        }

        private ResultInfo ValidateAllFees(CommGroupSettingParam commGroup)
        {
            var result = new ResultInfo();
            var productIdOfDB = _systemService.GetProductList().Select(p => p.ProductId.ToString());

            var validateP2pDeductionList = ValidateDeductionList(commGroup.P2pDeductionList, productIdOfDB, CommGroupErrorMessage.InvalidP2pDeduction);
            if (!validateP2pDeductionList.IsSuccessful)
            {
                UpdateResult(result, validateP2pDeductionList.Message);
            }

            var validatePlatformFeeList = ValidateDeductionList(commGroup.PlatformFeeList, productIdOfDB, CommGroupErrorMessage.InvalidPlatformFee);
            if (!validatePlatformFeeList.IsSuccessful)
            {
                UpdateResult(result, validatePlatformFeeList.Message);
            }

            var validateDepositFee = ValidateFixedAmountDeductedFee(commGroup.DepositFee, CommGroupErrorMessage.InvalidDepositFee);
            if (!validateDepositFee.IsSuccessful)
            {
                UpdateResult(result, validateDepositFee.Message);
            }

            var validateWithdrawalFee = ValidateFixedAmountDeductedFee(commGroup.WithdrawalFee, CommGroupErrorMessage.InvalidWithdrawalFee);
            if (!validateWithdrawalFee.IsSuccessful)
            {
                UpdateResult(result, validateWithdrawalFee.Message);
            }

            return result;
        }

        private static ResultInfo ValidateTierSettingList(IEnumerable<TierSettingParam> tierSettingList)
        {
            var result = new ResultInfo();

            if (tierSettingList == null || !tierSettingList.Any())
            {
                UpdateResult(result, "Tier Setting must have at least 1 tier!");
                return result;
            }

            if (tierSettingList.Any(item => item.MinQm > Validation.InfinityQualifiedMember))
            {
                UpdateResult(result, "Min QP must be lesser than or equal to 1,000,000,000");
            }

            if (tierSettingList.Any(item => item.MinAmount < Validation.NegativeInfinityAmount))
            {
                UpdateResult(result, "Min Net Win must be greater than or equal to -100,000,000,000");
                return result;
            }

            if (tierSettingList.Any(item => item.MinAmount > Validation.InfinityAmount))
            {
                UpdateResult(result, "Min Amount must be lesser than or equal to 100,000,000,000");
                return result;
            }

            if (tierSettingList.First().MinAmount > 0)
            {
                UpdateResult(result, "The first Tier of the Min Net Win column must be <= 0");
                return result;
            }

            if (!IsIncreasingMinNetWin(tierSettingList))
            {
                UpdateResult(result, "The values in the Min Net Win column must increase sequentially.");
                return result;
            }

            if (isMinNetWinNotStartFromOneAfterNegative(tierSettingList))
            {
                UpdateResult(result, "The next Tier after a negative Tier must start from 1 in the Min Net Win column.");
                return result;
            }

            return result;
        }

        private static bool IsIncreasingMinNetWin(IEnumerable<TierSettingParam> tierSettingList)
        {
            TierSettingParam previous = null;

            foreach (var tierSetting in tierSettingList)
            {
                if (previous != null && tierSetting.MinAmount <= previous.MinAmount)
                {
                    return false;
                }
                previous = tierSetting;
            }

            return true;
        }

        private static bool isMinNetWinNotStartFromOneAfterNegative(IEnumerable<TierSettingParam> tierSettingList)
        {
            TierSettingParam previous = null;

            foreach (var tierSetting in tierSettingList)
            {
                if (previous != null && previous.MinAmount < 0 && tierSetting.MinAmount > 1)
                {
                    return true;
                }
                previous = tierSetting;
            }

            return false;
        }

        private ResultInfo ValidateDeductionList(IEnumerable<DeductionSettingParam> deductionList, IEnumerable<string> productIdOfDB, string message)
        {
            var result = new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = message
            };

            if (deductionList == null || !deductionList.Any())
            {
                return result;
            }

            if (deductionList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                return deductionList.Count() == Validation.OnlyOneRow ? new ResultInfo() : result;
            }

            var productIdOfDeduction = string.Join(",", deductionList.Select(p => p.ProductIds.Replace(" ", "")))
                                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var distinctProductIdOfPlatform = productIdOfDeduction.Distinct();

            if (distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                return result;
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateFixedAmountDeductedFee(TierDeductionSettingParam deductedFee, string message)
        {
            var result = new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = message
            };

            if (!deductedFee.IsDeductedPercent)
            {
                try
                {
                    var deductedAmount = Convert.ToDecimal(deductedFee.DeductedAmount);

                    if (deductedAmount < 0 || deductedAmount > Money.AllowedMaxValue)
                    {
                        return result;
                    }
                }
                catch
                {
                    return result;
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ValidateGroupType(int groupTypeId)
        {
            var commGroupTypeList = _commGroupCommonService.GetCommGroupTypes();

            var groupType = commGroupTypeList.FirstOrDefault(_ => _.GroupTypeId == groupTypeId);

            var result = new ResultInfo();

            if (groupType is null)
            {
                UpdateResult(result, CommGroupErrorMessage.InvalidGroupType);
            }

            return result;
        }

        private static void UpdateResult(ResultInfo initialResult, string message)
        {
            initialResult.Code = ErrCode.GeneralErrorCode;
            initialResult.Message = string.Join(initialResult.Message + brElementHtml, message);
        }
    }
}